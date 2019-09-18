# coding=utf-8

import logging
import time
import os.path
import consts

from gmex_api_lib import GmexTradeWebsocket, GmexMarketWebsocket
from helper import my_json_marshal, my_json_unmarshal

from gmex_types import GT_Object, GT_Order, GT_Postion


def setup_logger():
    # Prints logger info to terminal
    logger = logging.getLogger()
    logger.setLevel(logging.DEBUG)  # Change this to DEBUG/INFO if you want a lot more info
    ch = logging.StreamHandler()
    # create formatter
    formatter = logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s")
    # add formatter to ch
    ch.setFormatter(formatter)
    logger.addHandler(ch)
    return logger


def callback_on_wallet(_, data):
    wlt = GT_Object(data)
    logger.debug("on_wallet: %s" % wlt.json_marshal())


def callback_on_order(_, data):
    order = GT_Order(data)
    logger.debug("on_order: %s" % order.json_marshal())


def callback_on_trade(_, data):
    trd = GT_Object(data)
    logger.debug("on_trade: %s" % my_json_marshal(trd))


def callback_on_postion(_, data):
    pos = GT_Postion(data)
    logger.debug("on_position: %s" % pos.json_marshal())


def callback_on_wltlog(_, data):
    wltlog = GT_Object(data)
    logger.debug("on_wltlog: %s" % my_json_marshal(wltlog))


def callback_on_mkt_trade(_, data):
    mkt_trd = GT_Object(data)
    logger.debug("on_mkt_trade: %s" % my_json_marshal(mkt_trd))


def callback_on_mkt_orderl2(_, data):
    mkt_ordl2 = GT_Object(data)
    logger.debug("on_mkt_orderl2: %s" % my_json_marshal(mkt_ordl2))


def callback_on_mkt_order20(_, data):
    mkt_ord20 = GT_Object(data)
    logger.debug("on_mkt_order20: %s" % my_json_marshal(mkt_ord20))


def callback_on_mkt_tick(_, data):
    mkt_tick = GT_Object(data)
    logger.debug("on_mkt_tick: %s" % my_json_marshal(mkt_tick))


def callback_on_mkt_index(_, data):
    mkt_idx = GT_Object(data)
    logger.debug("on_mkt_index: %s" % my_json_marshal(mkt_idx))


def callback_on_mkt_kline(_, data):
    mkt_kline = GT_Object(data)
    logger.debug("on_mkt_kline: %s" % my_json_marshal(mkt_kline))


def callback_on_mkt_notification(_, data):
    mkt_notify = GT_Object(data)
    logger.debug("on_mkt_notification: %s" % my_json_marshal(mkt_notify))


def callback_on_mkt_request_response(_, code, data):
    logger.debug("on_mkt_resp: %d %s" % (code, my_json_marshal(data)))


def sample_main():
    cfg = {
        "prod" : {
            "trd_ws_url": consts.GMEX_PROD_TRADE_WEBSOCKET_URL,
            "mkt_ws_url": consts.GMEX_PROD_MARKET_WEBSOCKET_URL,
            "user_name": "<your-user-name>",
            "api_key": "<your-api-key>",
            "api_secret": "<your-api-secret>",
            },
        "simgo": {
            "trd_ws_url": consts.GMEX_SIMGO_TRADE_WEBSOCKET_URL,
            "mkt_ws_url": consts.GMEX_SIMGO_MARKET_WEBSOCKET_URL,
            "user_name": "<your-user-name>",
            "api_key": "<your-api-key>",
            "api_secret": "<your-api-secret>",
            }
        }

    user_cfg_fname = 'user_cfg.json'
    if os.path.isfile(user_cfg_fname):
        json_data=open('user_cfg.json').read()
        cfg = my_json_unmarshal(json_data)

    env = 'prod'
    trd_ws = GmexTradeWebsocket(cfg[env]['trd_ws_url'],
                                cfg[env]['user_name'],
                                cfg[env]['api_key'],
                                cfg[env]['api_secret'],
                                on_wallet=callback_on_wallet,
                                on_order=callback_on_order,
                                on_trade=callback_on_trade,
                                on_position=callback_on_postion,
                                on_wltlog=callback_on_wltlog
                                )
    logger.info("connect to: %s" % trd_ws.url)
    trd_ws.do_login()

    mkt_ws = GmexMarketWebsocket(cfg[env]['mkt_ws_url'],
                                 on_mkt_trade=callback_on_mkt_trade,
                                 on_mkt_orderl2=callback_on_mkt_orderl2,
                                 on_mkt_order20=callback_on_mkt_order20,
                                 on_mkt_tick=callback_on_mkt_tick,
                                 on_mkt_index=callback_on_mkt_index,
                                 on_mkt_kline=callback_on_mkt_kline,
                                 on_mkt_notification=callback_on_mkt_notification
                                 )

    # 查询可以订阅的指数行情
    mkt_ws.REQ_GetCompositeIndex(callback_on_mkt_request_response)

    # 获取到服务端返回的交易对后的回调处理函数
    def callback_on_mkt_GetAssetD(_, code, data):
        # logger.debug("on_mkt_resp: %d %s" % (code, my_json_marshal(data)))
        for it in data:
            logger.debug("[交易对]: 符号=%s 类型=%d" % (it['Sym'], it['TrdCls'])) # 具体字段定请参考API文档里的说明
            sym = it['Sym']
            if 'BTC.USDT' == sym:
                # mkt_ws.REQ_Sub_tick(sym)
                mkt_ws.REQ_Sub_order20(sym)
                # mkt_ws.REQ_Sub_orderl2(sym)
                mkt_ws.REQ_Sub_trade(sym)

    # 查询可以访问的交易对
    mkt_ws.REQ_GetAssetD(callback_on_mkt_GetAssetD)

    # mkt_ws.REQ_GetAssetEx(callback_on_mkt_request_response)
    # mkt_ws.REQ_Sub_index('GMEX_CI_BTC')
    # mkt_ws.REQ_Sub_tick('BTC.USDT')

    while trd_ws.is_connected() and mkt_ws.is_connected():
        time.sleep(1)


if __name__ == "__main__":
    logger = setup_logger()
    sample_main()
