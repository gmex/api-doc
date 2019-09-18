# coding=utf-8

import websocket
import threading
import traceback
import time
import logging
import hashlib
import inspect
import sys
import uuid

from helper import my_json_marshal, my_json_unmarshal


class GmexTradeWebsocket:
    """
    Higher level of APIs are provided.
    The interface is like JavaScript WebSocket object.
    """

    def __init__(self, url, user_name, api_key, api_secret,
                 on_open=None, on_close=None, on_error=None,
                 on_wallet=None, on_trade=None, on_order=None, on_position=None, on_wltlog=None):
        """
        url: GMEX 交易服务器的 WS 服务地址.
        user_name: 用户名
        api_key: 用户 API-KEY
        api_secret: 用户 API-SECRET
        on_open: callable object which is called at opening websocket.
          this function has one argument. The argument is this class object.
        on_close: callable object which is called when closed the connection.
         this function has one argument. The argument is this class object.
        on_error: callable object which is called when we get error.
         on_error has 2 arguments.
         The 1st argument is this class object.
         The 2nd argument is exception object.
        on_wallet: callable object which is called when received WALLET push-message from trade-server.
         on_message has 2 arguments.
         The 1st argument is this class object.
         The 2nd argument is object Wlt which we get from the server.
        on_trade: same as above, object TrdRec
        on_order: same as above, object Ord
        on_position: same as above, object Position
        on_wltlog:  same as above, object WltLog
        """

        '''Connect to the websocket and initialize data stores.'''
        self.logger = logging.getLogger(__name__)
        self.logger.debug("Initializing WebSocket.")

        if api_key is None or api_secret is None:
            raise ValueError('api_key and api_secret must be provided')

        self.time_delta = 0  # 初始化默认时间偏差为0
        self.uid = ""        # empty string when init means we have no uid yet

        self.url = url
        self.user_name = user_name
        self.api_key = api_key
        self.api_secret = api_secret

        self.on_open = on_open
        self.on_close = on_close
        self.on_error = on_error

        self.on_wallet = on_wallet
        self.on_trade = on_trade
        self.on_order = on_order
        self.on_position = on_position
        self.on_wltlog = on_wltlog

        self.exited = False
        self._cmd_callback_dict = {}
        self._cmd_callback_lock = threading.Lock()

        self.logger.info("Connecting to %s" % self.url)
        self.__connect(self.url)
        self.logger.info('Connected to WS.')

        # # Connected. Wait for login
        # self.logger.info('Try to login')
        # self.do_login()


    def exit(self):
        '''Call this to exit - will close websocket.'''
        self.exited = True
        self.ws.close()

    def is_connected(self):
        if self.ws:
            if self.ws.sock:
                return self.ws.sock.connected
        return False

    def is_login_ok(self):
        return len(self.uid) > 1

    def do_login(self):
        # check time-delta first, then send Login ...
        local_tm = int(round(time.time() * 1000))

        def on_cmd_login_resp(_, code, data):
            if code != 0:
                self.logger.info("ERROR: cmd=Login, code=%d, data=%s", code, my_json_marshal(data))
                self.exit()
                return
            self.uid = data.pop("UserId", "")
            # self.UserName = data.pop("UserName", "")
            self.logger.debug("DEBUG: login ok, uid=%s data=%s", self.uid, my_json_marshal(data))

        def on_cmd_time_resp(_, code, data):
            if code != 0:
                self.logger.error("ERROR: cmd=Time, code=%d, data=%s", code, my_json_marshal(data))
                self.exit()
                return
            server_tm = data.pop("time", local_tm)
            self.time_delta = server_tm - local_tm
            self.logger.debug("DEBUG: local_tm:%d, server_tm:%d, delta %d", local_tm, server_tm, self.time_delta)
            # now send login
            self.__send_command('Login',
                               {'UserName': self.user_name, 'UserCred': self.api_key, 'DeviceInfo': 'gmex-api-py sample'
                                }, on_cmd_login_resp)

        self.__send_command('Time', local_tm, on_cmd_time_resp, need_sig=False)


    def REQ_GetAssetD(self, cb=None):
        # NOTE: 建议查询交易对列表信息使用行情API，这里只是适配旧接口定义，不建议使用.
        self.__send_command('GetAssetD', None, cb)

    def REQ_GetAssetEx(self, aid, sym, cb=None):
        # NOTE: 这里只能取一个sym的，要一次查全部的请使用行情API.
        self.__send_command('GetAssetEx', {'AId': aid, 'Sym': sym}, cb)

    def REQ_GetWallets(self, aid, cb=None):
        '''查询钱包信息'''
        self.__send_command('GetWallets', {'AId': aid}, cb)

    def REQ_GetTrades(self, aid, cb=None):
        '''查询成交记录'''
        # NOTE: 这里只提供最近100条记录，更多信息请去网站下载。
        self.__send_command('GetTrades', {'AId': aid}, cb)

    def REQ_GetOrders(self, aid, cb=None):
        '''查询当前有效的报单'''
        self.__send_command('GetOrders', {'AId': aid}, cb)

    def REQ_GetPositions(self, aid, cb=None):
        '''查询持仓'''
        self.__send_command('GetPositions', {'AId': aid}, cb)

    def REQ_GetWalletsLog(self, aid, cb=None):
        '''查询钱包日志记录'''
        # NOTE: 这里只提供最近100条记录，更多信息请去网站下载。
        self.__send_command('GetWalletsLog', {'AId': aid}, cb)

    def REQ_GetCcsWallets(self, cb=None):
        '''查询资金中心钱包信息'''
        self.__send_command('GetCcsWallets', None, cb)

    def REQ_GetHistOrders(self, aid, cb=None):
        '''查询最近的历史报单'''
        # NOTE: 这里只提供最近100条记录，更多信息请去网站下载。
        self.__send_command('GetHistOrders', {'AId': aid}, cb)

    def REQ_GetRiskLimitAsync(self, aid, sym, cb=None):
        '''获取风险限额'''
        self.__send_command('GetRiskLimit', {'AId': aid, 'Sym': sym}, cb)

    def REQ_CancelAllAfter(self, aid, sec, cb=None):
        '''设置超时取消报单'''
        self.__send_command('CancelAllAfter', {'AId': aid, 'Sec': sec}, cb)

    def REQ_PosLeverage(self, aid, sym, postionId, param, cb=None):
        '''调整杠杆'''
        self.__send_command('PosLeverage',
                            {
                                'AId': aid,
                                'Sym': sym,
                                'PId': postionId,    # 仓位ID
                                'Param': param       # 调整杠杆 float
                            }, cb)

    def REQ_PosTransMgn(self, aid, sym, postionId, param, cb=None):
        '''调整保证金'''
        self.__send_command('PosTransMgn',
                            {
                                'AId': aid,
                                'Sym': sym,
                                'PId': postionId,  # 仓位ID
                                'Param': param  # 调整杠杆 float
                            }, cb)

    def REQ_OrderNew(self, order, cb=None):
        '''下单'''
        self.__send_command('OrderNew', order, cb)

    def REQ_OrderDel(self, order, cb=None):
        '''撤单'''
        self.__send_command('OrderDel', order, cb)

    #
    # End Public Methods
    #

    def __callback(self, callback, *args):
        if callback:
            try:
                if inspect.ismethod(callback):
                    callback(*args)
                else:
                    callback(self, *args)

            except Exception as e:
                self.logger.error("error from callback {}: {}".format(callback, e))
                if self.logger.isEnabledFor(logging.DEBUG):
                    _, _, tb = sys.exc_info()
                    traceback.print_tb(tb)

    def __connect(self, wsURL):
        '''Connect to the websocket in a thread.'''
        self.logger.debug("Starting thread")

        self.ws = websocket.WebSocketApp(wsURL,
                                         on_message=self.__on_message,
                                         on_close=self.__on_close,
                                         on_open=self.__on_open,
                                         on_error=self.__on_error)

        self.wst = threading.Thread(target=lambda: self.ws.run_forever())
        self.wst.daemon = True
        self.wst.start()
        self.logger.debug("Started thread")

        # Wait for connect before continuing
        conn_timeout = 50
        while not self.ws.sock or not self.ws.sock.connected and conn_timeout:
            time.sleep(0.1)
            conn_timeout -= 1
        if not conn_timeout:
            self.logger.error("Couldn't connect to WS! Exiting.")
            self.exit()
            raise websocket.WebSocketTimeoutException('Couldn\'t connect to WS! Exiting.')

    def __send_command(self, command, args=None, on_resp_callback=None, need_sig=True):
        '''Send a raw command.'''
        rid = str(uuid.uuid1())
        expires = int(round(time.time() * 1000)) + 2500 + self.time_delta
        msg = {
            'req': command,
            'rid': rid,
            'expires': expires
        }
        if args is not None:
            msg['args'] = args

        if need_sig and self.api_secret is not None:
            # signature = MD5(Req+ReqId+Args+Expires+API.SecretKey)
            body = str(command) + str(rid) + \
                   (my_json_marshal(args) if args else "") + \
                   str(expires) + self.api_secret
            # sig = hmac.new(self.api_secret.encode('utf-8'), body.encode('utf-8'), digestmod=hashlib.md5).hexdigest()
            msg['signature'] = hashlib.md5(body.encode('utf-8')).hexdigest()

        self._cmd_callback_lock.acquire()
        self._cmd_callback_dict[rid] = on_resp_callback
        self._cmd_callback_lock.release()

        buf = my_json_marshal(msg)
        # self.logger.debug("MSG-buf: %s", buf)
        self.ws.send(buf)

    def __on_error(self, error):
        '''Called on fatal websocket errors. We exit on these.'''
        if not self.exited:
            self.logger.error("__on_error : %s" % error)
            if self.on_error:
                self.__callback(self.on_error, error)
            else:
                raise websocket.WebSocketException(error)

    def __on_open(self):
        '''Called when the WS opens.'''
        self.logger.debug("Websocket Opened.")
        self.__callback(self.on_open)

    def __on_close(self):
        '''Called on websocket close.'''
        self.logger.info('Websocket Closed')
        self.__callback(self.on_close)

    def __on_message(self, message):
        '''Handler for parsing WS messages.'''
        message = my_json_unmarshal(message)
        # self.logger.debug(my_json_marshal(message))
        if 'subj' in message:
            subj = message['subj']
            data = message['data'] if 'data' in message else None
            if subj == 'onWallet':
                self.__callback(self.on_wallet, data)
            elif subj == 'onTrade':
                self.__callback(self.on_trade, data)
            elif subj == 'onOrder':
                self.__callback(self.on_order, data)
            elif subj == 'onPosition':
                self.__callback(self.on_position, data)
            elif subj == 'onWltLog':
                self.__callback(self.on_wltlog, data)
            else:
                raise Exception("Unknown subj: %s" % subj)
        elif 'rid' in message:
            rid = message['rid']
            self._cmd_callback_lock.acquire()
            cb = self._cmd_callback_dict.pop(rid, None)
            self._cmd_callback_lock.release()
            if cb:
                code = message['code'] if 'code' in message else None
                data = message['data'] if 'data' in message else None
                self.__callback(cb, code, data)
        else:
            raise Exception("Unknown msg: %s" % message)


class GmexMarketWebsocket:
    """
    Higher level of APIs are provided.
    The interface is like JavaScript WebSocket object.
    """

    def __init__(self, url, on_open=None, on_close=None, on_error=None,
                 on_mkt_trade=None,
                 on_mkt_orderl2=None,
                 on_mkt_order20=None,
                 on_mkt_tick=None,
                 on_mkt_index=None,
                 on_mkt_kline=None,
                 on_mkt_notification=None):
        """
        url: GMEX 行情服务器的 WS 服务地址.
        on_open: callable object which is called at opening websocket.
          this function has one argument. The argument is this class object.
        on_close: callable object which is called when closed the connection.
         this function has one argument. The argument is this class object.
        on_error: callable object which is called when we get error.
         on_error has 2 arguments.
         The 1st argument is this class object.
         The 2nd argument is exception object.
        on_mkt_trade: callable object which is called when received subjected message from market-server.
         on_message has 2 arguments.
         The 1st argument is this class object.
         The 2nd argument is object Wlt which we get from the server.
        on_mkt_orderl2: same as above, 全深度行情数据
        on_mkt_order20: 20挡行情数据
        on_mkt_tick: TICK行情
        on_mkt_index:  指数的TICK行情
        on_mkt_kline: K线数据
        on_mkt_notification: 服务器推送的提醒消息
        """

        '''Connect to the websocket and initialize data stores.'''
        self.logger = logging.getLogger(__name__)
        self.logger.debug("Initializing WebSocket.")

        self.time_delta = 0  # 初始化默认时间偏差为0
        self.uid = ""        # empty string when init means we have no uid yet

        self.url = url
        self.on_open = on_open
        self.on_close = on_close
        self.on_error = on_error
        self.on_mkt_trade = on_mkt_trade
        self.on_mkt_orderl2 = on_mkt_orderl2
        self.on_mkt_order20 = on_mkt_order20
        self.on_mkt_tick = on_mkt_tick
        self.on_mkt_index = on_mkt_index
        self.on_mkt_kline = on_mkt_kline
        self.on_mkt_notification = on_mkt_notification

        self.exited = False
        self._cmd_callback_dict = {}
        self._cmd_callback_lock = threading.Lock()

        self.logger.info("Connecting to %s" % self.url)
        self.__connect(self.url)
        self.logger.info('Connected to WS.')
        self.REQ_Time()

    def exit(self):
        '''Call this to exit - will close websocket.'''
        self.exited = True
        self.ws.close()

    def is_connected(self):
        if self.ws:
            if self.ws.sock:
                return self.ws.sock.connected
        return False

    def REQ_Time(self, cb=None):
        local_tm = int(round(time.time() * 1000))

        def on_cmd_time_resp(_, code, data):
            if code != 0:
                self.logger.error("ERROR: cmd=Time, code=%d, data=%s", code, my_json_marshal(data))
                self.exit()
                return
            server_tm = data.pop("time", local_tm)
            self.time_delta = server_tm - local_tm
            self.logger.debug("DEBUG: local_tm:%d, server_tm:%d, delta %d", local_tm, server_tm, self.time_delta)
            self.__callback(cb, self.time_delta)

        self.__send_command('Time', local_tm, on_cmd_time_resp)

    def REQ_GetCompositeIndex(self, cb):
        self.__send_command('GetCompositeIndex', None, cb)

    def REQ_GetAssetD(self, cb):
        self.__send_command('GetAssetD', None, cb)

    def REQ_GetAssetEx(self, cb):
        self.__send_command('GetAssetEx', None, cb)

    def REQ_GetHistKLine(self, sym, typ, beginSec, offset, count, cb):
        # string sym, Models.MktKLineType typ, int beginSec, int offset, int count
        args = {
            "Sym": sym,
            "Typ": typ,   # 1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 8h, 12h, 1d, 3d, 1w, 2w, 1M
            "Sec": beginSec,
            "Offset": offset,
            "Count": count
        }
        self.__send_command('GetHistKLine', args, cb)

    def REQ_Sub(self, subjects, cb=None, unsub=False):
        if unsub:
            self.__send_command('UnSub', subjects, cb)
        else:
            self.__send_command('Sub', subjects, cb)

    def REQ_Sub_tick(self, sym, cb=None, unsub=None):
        '''订阅交易对sym的TICK行情推送'''
        self.REQ_Sub(['tick_' + sym], cb, unsub)

    def REQ_Sub_index(self, sym, cb=None, unsub=None):
        '''订阅指数sym的tick行情数据推送'''
        self.REQ_Sub(['index_' + sym], cb, unsub)

    def REQ_Sub_trade(self, sym, cb=None, unsub=None):
        '''订阅交易对sym的成交推送'''
        self.REQ_Sub(['trade_' + sym], cb, unsub)

    def REQ_Sub_order20(self, sym, cb=None, unsub=None):
        '''订阅交易对sym的20档行情数据推送'''
        self.REQ_Sub(['order20_' + sym], cb, unsub)

    def REQ_Sub_orderl2(self, sym, cb=None, unsub=None):
        '''订阅交易对sym的全档行情数据推送'''
        self.REQ_Sub(['orderl2_' + sym], cb, unsub)

    def __callback(self, callback, *args):
        if callback:
            try:
                if inspect.ismethod(callback):
                    callback(*args)
                else:
                    callback(self, *args)

            except Exception as e:
                self.logger.error("error from callback {}: {}".format(callback, e))
                if self.logger.isEnabledFor(logging.DEBUG):
                    _, _, tb = sys.exc_info()
                    traceback.print_tb(tb)

    def __connect(self, wsURL):
        '''Connect to the websocket in a thread.'''
        self.logger.debug("Starting thread")

        self.ws = websocket.WebSocketApp(wsURL,
                                         on_message=self.__on_message,
                                         on_close=self.__on_close,
                                         on_open=self.__on_open,
                                         on_error=self.__on_error)

        self.wst = threading.Thread(target=lambda: self.ws.run_forever())
        self.wst.daemon = True
        self.wst.start()
        self.logger.debug("Started thread")

        # Wait for connect before continuing
        conn_timeout = 50
        while not self.ws.sock or not self.ws.sock.connected and conn_timeout:
            time.sleep(0.1)
            conn_timeout -= 1
        if not conn_timeout:
            self.logger.error("Couldn't connect to WS! Exiting.")
            self.exit()
            raise websocket.WebSocketTimeoutException('Couldn\'t connect to WS! Exiting.')

    def __send_command(self, command, args=None, on_resp_callback=None):
        '''Send a raw command.'''
        rid = str(uuid.uuid1())
        expires = int(round(time.time() * 1000)) + 2500 + self.time_delta
        msg = {
            'req': command,
            'rid': rid,
            'expires': expires
        }
        if args is not None:
            msg['args'] = args

        self._cmd_callback_lock.acquire()
        self._cmd_callback_dict[rid] = on_resp_callback
        self._cmd_callback_lock.release()

        buf = my_json_marshal(msg)
        # self.logger.debug("MSG-buf: %s", buf)
        self.ws.send(buf)

    def __on_error(self, error):
        '''Called on fatal websocket errors. We exit on these.'''
        if not self.exited:
            self.logger.error("__on_error : %s" % error)
            if self.on_error:
                self.__callback(self.on_error, error)
            else:
                raise websocket.WebSocketException(error)

    def __on_open(self):
        '''Called when the WS opens.'''
        self.logger.debug("Websocket Opened.")
        self.__callback(self.on_open)

    def __on_close(self):
        '''Called on websocket close.'''
        self.logger.info('Websocket Closed')
        self.__callback(self.on_close)

    def __on_message(self, message):
        '''Handler for parsing WS messages.'''
        message = my_json_unmarshal(message)
        # self.logger.debug(my_json_marshal(message))
        if 'subj' in message:
            subj = message['subj']
            data = message['data'] if 'data' in message else None
            if subj == 'trade':
                self.__callback(self.on_mkt_trade, data)
            elif subj == 'orderl2':
                self.__callback(self.on_mkt_orderl2, data)
            elif subj == 'order20':
                self.__callback(self.on_mkt_order20, data)
            elif subj == 'tick':
                self.__callback(self.on_mkt_tick, data)
            elif subj == 'index':
                self.__callback(self.on_mkt_index, data)
            elif subj == 'kline':
                self.__callback(self.on_mkt_kline, data)
            elif subj == 'notification':
                self.__callback(self.on_mkt_notification, data)
            else:
                raise Exception("Unknown subj: %s" % subj)
        elif 'rid' in message:
            rid = message['rid']
            self._cmd_callback_lock.acquire()
            cb = self._cmd_callback_dict.pop(rid, None)
            self._cmd_callback_lock.release()
            if cb:
                code = message['code'] if 'code' in message else None
                data = message['data'] if 'data' in message else None
                self.__callback(cb, code, data)
        else:
            raise Exception("Unknown msg: %s" % message)

