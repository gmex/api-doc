# coding=utf-8

import decimal

from helper import my_json_marshal

ZERO_DECIMAL = decimal.Decimal(0)

class GT_Object(object):
    def __init__(self, kv):
        if not isinstance(kv, dict):
            raise Exception('GT_Object.__init__(kv): need dict kv!')
        self.kv = kv

    def __is_ignored_val(self, val):
        if val is not None:
            if isinstance(val, int):
                return val == 0
            if isinstance(val, float):
                return val == 0.0
            if isinstance(val, str) or isinstance(val, dict) or isinstance(val, list) or isinstance(val, set):
                return len(val) == 0
            if isinstance(val, decimal.Decimal):
                return val.is_zero()
        return False

    def __get_or_set(self, key, val_new, val_default):
        if val_new is None:  # try to get if val IS None
            return self.kv.get(key, val_default)
        else: # try to set if val ISNOT None
            if self.__is_ignored_val(val_new):  # remove this key if need
                return self.kv.pop(key, None)
            else:  # set key-value
                self.kv[key] = val_new

    def json_marshal(self):
        return my_json_marshal(self.kv)


class GT_Order(GT_Object):
    '''
    /// <summary>
    /// Ord 委托/报单
    ///
    /// 重要说明：
    ///
    /// 1. 用户通常在本地维护 【所有委托】表； 主键使用 COrdId;
    /// 2. 性能考虑，建议单独建立一个【历史委托】的表专门存放已经不活动的委托，这些委托数据是不会再变化了;
    /// 3. 收到委托的推送消息后，应该去【所有委托】找到委托，进行内容更新替换，如需要则移到【历史委托】里;
    /// 4. 所有委托通常会被分为三种：当前委托，条件委托，历史委托;
    /// 5. 尽管 Server 端是使用 OrdId 来作为 Ord 的主键的，但是由于websockets的异步通讯机制，用户在下单（新建委托）时只能
    ///    自己维护 COrdId 而要等待服务器返回对应的 OrdId；因此建议本地使用GUID来自己维护自己Ord的唯一性；故，本地保存的
    ///    所有委托的主键使用 COrdId 比较方便;
    /// 6. 特别注意的是，OrderNew 的返回结果并不能保证一定比 onOrder 消息到来的快，因此，在下单前，应该先本地保存 COrdId 入
    ///    表以便 onOrder 能正确识别;
    /// 7. 委托的有效性，首先看 ErrorCode 是否为 0，然后再看 Status;
    /// 8. 对于条件委托，一开始OType是条件类型，触发后会自动变成 Limit 或 Market 类型；如在历史单中想判断是否条件单，可借助
    ///    StopPrz 来进一步判断。
    /// </summary>
    '''
    # 用户Id
    def X_UId(self, val=None): return self.__get_or_set('UId', val, '')

    # 账户Id
    def X_AId(self, val=None): return self.__get_or_set('AId', val, '')

    # 合约符合/交易对符号
    def X_Sym(self, val=None): return self.__get_or_set('Sym', val, '')

    # 钱包ID
    def X_WId(self, val=None): return self.__get_or_set('WId', val, '')

    # 服务器端为其分配的报单ID
    def X_OrdId(self, val=None): return self.__get_or_set('OrdId', val, '')

    # 客户端为其分配的报单ID
    def X_COrdId(self, val=None): return self.__get_or_set('COrdId', val, '')

    # 委单方向: 1=买, -1=卖
    def X_Dir(self, val=None): return self.__get_or_set('Dir', val, 0)

    # 报价类型: 1:Limit(限价委单 ), 2: Market(市价委单,匹配后转限价), 3: StopMarket (市价止损);
    def X_OType(self, val=None): return self.__get_or_set('OType', val, ZERO_DECIMAL)

    # 价格
    def X_Prz(self, val=None): return self.__get_or_set('Prz', val, ZERO_DECIMAL)

    # 数量
    def X_Qty(self, val=None): return self.__get_or_set('Qty', val, ZERO_DECIMAL)

    # 显示数量。如果为0,则显示全部Qty
    def X_QtyDsp(self, val=None): return self.__get_or_set('QtyDsp', val, ZERO_DECIMAL)

    # TimeInForce/生效时间设定:一直有效=0,剩余撤销=1,全部成交或者全部取消=2;GoodTillCancel=0,ImmediateOrCancel=1,FillOrKill=2;
    def X_Tif(self, val=None): return self.__get_or_set('Tif', val, 0)

    # 委托标志/标志位, OrdFlag 按位设置有效位 UInt32
    def X_OrdFlag(self, val=None): return self.__get_or_set('OrdFlag', val, 0)

    # 来源
    def X_Via(self, val=None): return self.__get_or_set('Via', val, 0)

    # 下单时间戳,单位:毫秒 UInt64
    def X_At(self, val=None): return self.__get_or_set('At', val, 0)

    # 更新时间戳,单位:毫秒 Int64
    def X_Upd(self, val=None): return self.__get_or_set('Upd', val, 0)

    # 有效期,单位:毫秒,绝对时间 UInt64
    def X_Until(self, val=None): return self.__get_or_set('Until', val, 0)

    # 最大价格变动次数.（价格档位) Int32
    def X_PrzChg(self, val=None): return self.__get_or_set('PrzChg', val, 0)

    # 报单的状态
    def X_Status(self, val=None): return self.__get_or_set('Status', val, 0)

    # 判断依据 Int32
    def X_StopBy(self, val=None): return self.__get_or_set('StopBy', val, 0)

    # 止损价格,止盈价格
    def X_StopPrz(self, val=None): return self.__get_or_set('StopPrz', val, ZERO_DECIMAL)

    # 追踪委托中，回调的比率. Reverse Ratio. 小数。
    def X_TraceRR(self, val=None):  return self.__get_or_set('TraceRR', val, 0.0)

    # 追踪的Min, float
    def X_TraceMin(self, val=None): return self.__get_or_set('TraceMin', val, 0.0)

    # 追踪的Max, float
    def X_TraceMax(self, val=None): return self.__get_or_set('TraceMax', val, 0.0)

    # 冻结金额
    def X_Frz(self, val=None): return self.__get_or_set('Frz', val, ZERO_DECIMAL)

    # 委托保证金 Mgn Initial + 佣金
    def X_MM(self, val=None): return self.__get_or_set('MM', val, ZERO_DECIMAL)

    # 预估的手续费：按照手续费计算
    def X_FeeEst(self, val=None): return self.__get_or_set('FeeEst', val, ZERO_DECIMAL)

    # 预估的UPNL, decimal
    def X_UPNLEst(self, val=None): return self.__get_or_set('UPNLEst', val, ZERO_DECIMAL)

    # 已成交的数量, decimal
    def X_QtyF(self, val=None): return self.__get_or_set('QtyF', val, ZERO_DECIMAL)

    # 已成交的平均价格 Prz Filled, decimal
    def X_PrzF(self, val=None): return self.__get_or_set('PrzF', val, ZERO_DECIMAL)

    # 错误代码 int
    def X_ErrCode(self, val=None): return self.__get_or_set('ErrCode', val, 0)

    # 错误文本 str
    def X_ErrTxt(self, val=None): return self.__get_or_set('ErrTxt', val, '')


class GT_Postion(GT_Object):
    # 用户Id
    def X_UId(self, val=None): return self.__get_or_set('UId', val, '')

    # 账户Id
    def X_AId(self, val=None): return self.__get_or_set('AId', val, '')

    # 合约符合/交易对符号
    def X_Sym(self, val=None): return self.__get_or_set('Sym', val, '')

    # 钱包ID
    def X_WId(self, val=None): return self.__get_or_set('WId', val, '')

    # 持仓ID
    def X_PId(self, val=None): return self.__get_or_set('PId', val, '')

    # 仓位(正数为多仓，负数为空仓)
    def X_Sz(self, val=None): return self.__get_or_set('Sz', val, ZERO_DECIMAL)

    # 开仓平均价格
    def X_PrzIni(self, val=None): return self.__get_or_set('PrzIni', val, ZERO_DECIMAL)

    # 已实现盈亏
    def X_RPNL(self, val=None): return self.__get_or_set('RPNL', val, 0.0)

    # 杠杆
    def X_Lever(self, val=None): return self.__get_or_set('Lever', val, 0.0)

    # 动态数据, 最大杠杆
    def X_LeverMax(self, val=None): return self.__get_or_set('LeverMax', val, 0.0)

    # 动态数据, 有效MMR
    def X_MMR(self, val=None): return self.__get_or_set('MMR', val, 0.0)

    # 动态数据, 有效MIR
    def X_MIR(self, val=None): return self.__get_or_set('MIR', val, 0.0)

    # 仓位保证金
    def X_MgnISO(self, val=None): return self.__get_or_set('MgnISO', val, ZERO_DECIMAL)

    # 逐仓下的已实现盈亏
    def X_PNLISO(self, val=None): return self.__get_or_set('PNLISO', val, ZERO_DECIMAL)

    # 计算值：价值,仓位现时的名义价值，受到标记价格价格的影响
    def X_Val(self, val=None): return self.__get_or_set('Val', val, ZERO_DECIMAL)

    # 维持保证金,被仓位使用并锁定的保证金。
    def X_MMnF(self, val=None): return self.__get_or_set('MMnF', val, ZERO_DECIMAL)

    # 开仓保证金
    def X_MI(self, val=None): return self.__get_or_set('MI', val, ZERO_DECIMAL)

    # 计算值：未实现盈亏 PNL==  Profit And Loss
    def X_UPNL(self, val=None): return self.__get_or_set('UPNL', val, ZERO_DECIMAL)

    # 计算值: 强平价格 亏光当前保证金的 (如果是多仓，并且标记价格低于PrzLiq,则会被强制平仓。/如果是空仓,并缺标记价格高于PrzLiq，则会被强制平仓
    def X_PrzLiq(self, val=None): return self.__get_or_set('PrzLiq', val, ZERO_DECIMAL)

    # 计算值: 破产价格 BandRuptcy
    def X_PrzBr(self, val=None): return self.__get_or_set('PrzBr', val, ZERO_DECIMAL)

    # 预估的平仓费
    def X_FeeEst(self, val=None): return self.__get_or_set('FeeEst', val, ZERO_DECIMAL)

    # ROE
    def X_ROE(self, val=None): return self.__get_or_set('ROE', val, 0.0)

     # ADLLight
    def X_ADLLight(self, val=None): return self.__get_or_set('ADLLight', val, 0)

    # ADLIdx, 是用来排序ADL的
    def X_ADLIdx(self, val=None): return self.__get_or_set('ADLIdx', val, 0.0)
