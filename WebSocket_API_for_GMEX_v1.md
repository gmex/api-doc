# GMEX WebSocket API (v1) beta

## 说明

目前 GMEX (https://www.gmex.io) 对于外提供 WebSocket API 开发接口， 供开发者获取行情数据和进行交易操作。
请注意，行情和交易两个服务是分开的，行情接口无需认证可以自由访问，交易部分则需要用户开通API-KEY后通过自己的KEY认证授权后方可使用。


GMEX官方的生产环境(暂未开放)：
```
官方网址： https://www.gmex.io
行情服务： wss://api-market.gmex.io/v1/market
交易服务： wss://api-trade.gmex.io/v1/trade
```

为方便大家测试，官方提供模拟环境:
```
模拟网址： https://simgo.gmex.io
模拟行情： wss://market01.gmex.io/v1/market
模拟交易： wss://trade01.gmex.io/v1/trade
```


## 行情API

1. 获取交易对/合约列表： GetAssetD
```JavaScript
// 发送请求消息
{"req":"GetAssetD","rid":"0","expires":1537706670830}

// 收到返回消息
{
    "rid":"0",
    "code":0,
    "data":[
        {
            "Sym":"ETH1812",                    // 交易对名称
            "Beg":1,
            "Expire":1545984000000,
            "PrzMaxChg":1000,
            "PrzMinInc":0.05,
            "PrzMax":1000000,
            "OrderMaxQty":10000000,
            "LotSz":1,
            "PrzM":244.8799999999999954525264911353588104248046875,
            "MIR":0.07,
            "MMR":0.05,
            "OrderMinVal":0,
            "PrzLatest":244.95,
            "OpenInterest":2181200,
            "PrzIndex":244.8863,
            "PosLmtStart":10000000,
            "PosOpenRatio":0.05,
            "FeeMkrR":-0.0003,
            "FeeTkrR":0.0007,
            "Mult":1,
            "FromC":"ETH",
            "ToC":"USD",
            "TrdCls":2,
            "MkSt":1,
            "Flag":1,
            "SettleCoin":"ETH",
            "QuoteCoin":"ETH",
            "SettleR":0.0005,
            "DenyOpenAfter":1545980400000
        },
        {"Sym":"BTC1812","Beg":1,"Expire":1545984000000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6731.3100000000004001776687800884246826171875,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6731.0,"OpenInterest":3431840,"PrzIndex":6737.3525,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1545980400000},
        {"Sym":"ETH1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.05,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":244.19999999999998863131622783839702606201171875,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":244.20,"OpenInterest":4500733,"PrzIndex":244.8863,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"ETH","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"ETH","QuoteCoin":"ETH","SettleR":0.0005,"DenyOpenAfter":1538118000000},
        {"Sym":"BTC1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6727.5500000000001818989403545856475830078125,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6728.0,"OpenInterest":1451134,"PrzIndex":6737.3525,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1538118000000}
    ]
}
```
用户发送和接收到的所有消息统一采用JSON格式，发送请求的消息参数说明：

|参数 |	描述|
| :-----   | :-----   |
|req|用户的请求操作动作，如： GetAssetD，GetCompositeIndex，GetHistKLine等|
|rid|用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答。|
|expires|消息超时，毫秒，建议每次发送请求时填写当前时间加1秒。一般宜在初始化时先用Time消息获取服务端时间,可以相对时差与服务端保持同步。|



交易对/合约的结构定义如下：
```golang
type AssetD struct {
    Sym                 string  // 合约符合/交易对符号
    Beg                 int64   // 开始时间,毫秒
    DenyOpenAfter       int64   // 到期前禁止开仓时间,毫秒
    Expire              int64   // 到期时间,毫秒
    PrzMaxChg           int32   // 市价委托的撮合的最多次数。比如5
    PrzMinInc           float64 // 最小的价格变化
    PrzMax              float64 // 最大委托价格
    OrderMaxQty         float64 // 最大委托数量
    LotSz               float64 // 最小合约数量,当前只支持为1;
    PrzM                float64 // 标记价格
    MIR                 float64 // 起始保证金率
    MMR                 float64 // 维持保证金率
    OrderMinVal         float64 // 委托的最小价值
    PrzLatest           float64 // 最新成交价格
    OpenInterest        int64   // 持仓量
    PrzIndex            float64 // 指数价格
    PosLmtStart         int64   // 个人持仓比例激活条件
    PosOpenRatio        float64 // 个人持仓最大比例
    FeeMkrR             float64 // 提供流动性的费率
    FeeTkrR             float64 // 消耗流动性的费率
    Mult                int64   // 乘数
    FromC               string  // 从什么货币
    ToC                 string  // 兑换为什么货币
    TrdCls              int32   // 交易类型, 1-现货交易, 2-期货交易, 3-永续
    SettleCoin          string  // 结算货币
    QuoteCoin           string  // 报价货币
    SettleR             float64 // 结算费率
}
```


2. 获取指数列表： GetCompositeIndex
```js
// 发送请求消息
{
    "req":"GetCompositeIndex",
    "rid":"1",
    "expires":1537706670831,
    "args":{}
}

// 收到返回消息
{
    "rid":"1",
    "code":0,
    "data":["GMEX_CI_ETH","GMEX_CI_BTC"]
}
```

3. 获取历史K线数据： GetHistKLine
```js
// 发送请求消息
{
    "req":"GetHistKLine",
    "rid":"2",
    "expires":1537708009100,
    "args":{
        "Sym":"ETH1812",
        "Typ":"1m",
        "beginSec":1537077600,
        "Offset":0,
        "Count":10
    }
}

// 收到返回消息
{
    "rid":"2",
    "code":0,
    "data":
        {
            "Sym":"ETH1812",
            "Typ":"1m",
            "Count":10,
            "Sec":[1537077600,1537077660,1537077720,1537077780,1537077840,1537077900,1537077960,1537078020,1537078080,1537078140],
            "PrzOpen":[216.45,216,215.8,215.6,215.05,215.25,215.3,215.45,215.5,215.35],
            "PrzClose":[216,215.75,215.6,215,215.35,215.45,215.45,215.7,215.4,215.6],
            "PrzHigh":[216.5,216,215.9,215.6,215.4,215.5,215.45,215.7,215.55,215.6],
            "PrzLow":[215.9,215.65,215.6,214.9,215.05,215.2,215.25,215.4,215.35,215.25],
            "Volume":[1354,717,473,1751,238,269,94,123,123,275],
            "Turnover":[6.26501568815296,3.321813453440903,2.192467668789991,8.137750477124483,1.1054031648919223,1.2493419094774725,0.4364373010900492,0.5702923655266671,0.5709143037474378,1.276362876440699]
    }
}
```

当前系统支持的K线类型有:
```js
/**
 * 
 * 类型: 1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 8h, 12h, 1d, 3d, 1w, 2w, 1M
 * m: 代表分钟(minutes)
 * h: 代表小时(hours)
 * d: 代表天(days)
 * w: 代表周(weeks)
 * M: 代表月(months)
 *
 **/
```

4. 订阅/取消订阅: Sub / UnSub
```js
// 发送订阅请求消息
{
    "req":"Sub",
    "rid":"20",
    "expires":1537708219903,
    "args":["tick_BTC1812"]
}

// 收到订阅返回消息
{
    "rid":"20",
    "code":0,
    "data":"OK"
}

// 收到推送消息
{
    "subj":"tick",
    "data":{
        "Sym":"BTC1812",
        "At":1537708218726,
        "PrzBid1":6723.5,
        "SzBid1":429,
        "SzBid":876837,
        "PrzAsk1":6725.5,
        "SzAsk1":494,
        "SzAsk":1022136,
        "LastPrz":6725,
        "SettPrz":6723.98,
        "Prz24":6678.5,
        "High24":6774,
        "Low24":6632,
        "Volume24":5659148,
        "Turnover24":843.7992005395208,
        "Volume":40901214,
        "Turnover":0,
        "OpenInterest":3436394
    }
}



// 发送取消订阅请求消息
{
    "req":"UnSub",
    "rid":"21",
    "expires":1537708267910,
    "args":["tick_BTC1812"]
}

// 收到的取消订阅返回的消息
{
    "rid":"21",
    "code":0,
    "data":"OK"
}
```

##### 用户可以订阅的内容有如下：
| 订阅内容 | 描述 |
| :-----   | :-----  |
|TICK|比如： tick_BTC1812|
|成交|比如： trade_BTC1812|
|20档深度|比如： order20_BTC1812|
|全档深度|比如： orderl2_BTC1812|
|K线|比如： kline_1m_BTC1812，kline_1h_BTC1812|
|指数|比如： index_GMEX_CI_BTC，index_GMEX_CI_ETH|

**NOTE**: UnSub 时可以用 * 一次清空, Sub 时必须提供合法的名字.


5. 获取服务器时间
```js
// 发送请求消息， 由于本消息开销很小，可用于和服务器端保持网络连接用，比如每隔55秒发送一次；
{
    "req":"Time",
    "rid":"6",
    "expires":1537706745839,
    "args":1537706744839
}
// 收到返回消息
{
    "rid":"6",
    "code":0,
    "data":{
        "time":1537706745295,
        "data":"1537706744839"
    }
}
```


## 交易API

1. 用户登录
用户首先要在网站上的个人中心开启API-KEY功能并生成需要的公私秘钥才能使用交易API功能。

```js
// 发送用户的登录消息

/**
 *  参数说明
 * {
 *  "req":"Login",              // 请求的动作类型
 *  "rid":"1",
 *  "expires":1538222696758,    // 消息超时时间
 *  "args":{                    // 服务端所需的参数
 *      "UserName":"example@gaea.com",              // 账号
 *      "UserCred":"mVAAADjNHzhvehaEvU$BMJoU7BZk"   // APIKey
 *  },
 *  "signature": "74c33368e9a1f8d6d13cdf0bf5aa02a8" // 签名,可参考生产签名的方法
 * }
 * 
 * 生成签名的方法:
 * 公式: md5(Req+rid+Args+Expires+API.SecretKey)
 * 例:
 *  UserName: "example@gaea.com",
 *  UserCred: "mVAAADjNHzhvehaEvU$BMJoU7BZk"
 *  SecretKey: "uLgAAHMw62di3hUPypuETMWGzHx852swxM7V0b2HObba5gYNNrLkuvQ4I"
 *  Req: "Login"
 *  rid: 1
 * 根据上面的信息可以生成如下签名:
 * signature = md5("Login"+"1"+ JSON.stringify({UserName:"example@gaea.com",UserCred:"mVAAADjNHzhvehaEvU$BMJoU7BZk"}) +"1538222696758" + "uLgAAHMw62di3hUPypuETMWGzHx852swxM7V0b2HObba5gYNNrLkuvQ4I")
 * signature结果为:"74c33368e9a1f8d6d13cdf0bf5aa02a8"
 * */

// 需要注意的是: Args 参数一般为JSON对象(除Time)，在签名时需要序列化为字符串，序列化没有字段顺序要求,但是需要保持签名时序列化的顺序与最终发出消息时序列化的顺序一致。
// 补充: Time消息不要签名

// 收到返回消息
// 注意：这里收到的 UserId 是用户的系统内部唯一编号，简称 UID， 非常重要，系统用此ID后面增加两位数字表示用户的子账户ID,比如UID=1234567，则合约子账号的AId即为123456701；

{
    "rid":"0",
    "code":0,
    "data":{
        "UserName":"gmex-test@gmail.com",
        "UserId":"1234567"
    }
}
```

用户成功登录交易系统后，所有用户相关信息会自动推送给用户，如报单变更，仓位变化，成交通知，钱包日志等等。
交易的消息定义和行情类似，但多了签名字段：

|参数|	描述|
| :-----   | :-----   |
|req|用户的请求操作动作，如： GetAssetD, GetWallets, GetTrades, GetOrders, GetPositions, OrderNew, OrderDel等等。|
|rid|用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答。|
|expires|消息超时，毫秒，建议每次发送请求时填写当前时间加1秒。|
|args|用户的参数，可选，具体根据req来设置。|
|signature|消息签名: MD5(Req+ReqId+Args+Expires+API.SecretKey)，小写。|

# 重要说明

+ UID 和 AID
	一个用户对应一个系统内唯一的UID(字符串);
	一个用户下面可以有多个AID,AID是UID后面加两位构成;
	每个用户注册出来时会自动创建一个默认的AID,就是UID后面加01;
	用户可以自己创建多个子账户,因此,一个用户会有多个AID;在系统内部,所有的AID全局唯一;
	用户可以在自己的子账户之间相互转移自己的所有的数字货币;
	用户下单时指定自己的子账户,该单的风险可以控制在这个子账户范围内,从而可以控制风险.
    
2. 查询当前系统的合约列表(必须参数 AId)： GetAssetD

```js
// 发送请求消息
{
    "req":"GetAssetD",
    "rid":"2",
    "expires":1537710766358,
    "args":{
        "AId":"1525354501"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}

// 收到返回消息
{
    "rid":"2",
    "code":0,
    "data":[
        {
            "Sym":"ETH1812",
            "Beg":1,
            "Expire":1545984000000,
            "PrzMaxChg":1000,
            "PrzMinInc":0.05,
            "PrzMax":1000000,
            "OrderMaxQty":10000000,
            "LotSz":1,
            "PrzM":245.330000000000012505552149377763271331787109375,
            "MIR":0.07,
            "MMR":0.05,
            "OrderMinVal":0,
            "PrzLatest":245.35,
            "OpenInterest":2181137,
            "PrzIndex":244.9823,
            "PosLmtStart":10000000,
            "PosOpenRatio":0.05,
            "FeeMkrR":-0.0003,
            "FeeTkrR":0.0007,
            "Mult":1,
            "FromC":"ETH",
            "ToC":"USD",
            "TrdCls":2,
            "MkSt":1,
            "Flag":1,
            "SettleCoin":"ETH",
            "QuoteCoin":"ETH",
            "SettleR":0.0005,
            "DenyOpenAfter":1545980400000
        },
        {"Sym":"BTC1812","Beg":1,"Expire":1545984000000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6725.75,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6724.5,"OpenInterest":3431245,"PrzIndex":6729.2552,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1545980400000},
        {"Sym":"ETH1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.05,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":244.650000000000005684341886080801486968994140625,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":244.65,"OpenInterest":4501232,"PrzIndex":244.9823,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"ETH","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"ETH","QuoteCoin":"ETH","SettleR":0.0005,"DenyOpenAfter":1538118000000},
        {"Sym":"BTC1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6726.8000000000001818989403545856475830078125,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6724.0,"OpenInterest":1449455,"PrzIndex":6729.2552,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1538118000000}
    ]
}
```

返回的结果和行情中获取到的数据是一样的。


3. 查询用户子账号的钱包列表信息(必须参数 AId)： GetWallets

```js
// 发送请求消息
{
    "req":"GetWallets",
    "rid":"3",
    "expires":1537710967223,
    "args":{
        "AId":"1525354501"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}
// 收到返回消息
{
    "rid":"3",
    "code":0,
    "data":[
        {
            "UId":"1234567",
            "AId":"123456701",
            "Coin":"ETH",
            "Depo":1.00000000,
            "WDrw":0,
            "PNL":-0.452374713185744188799864398523351731769754619710,
            "Frz":0,
            "MI":0.2906262618301145,
            "RD":0.530702779487773,
            "Status":2
        },
        {"UId":"1234567","AId":"123456701","Coin":"BTC","Depo":0.16518449,"WDrw":0,"PNL":-0.00426155098522019276893748290683797431047794081854,"Frz":0,"MI":0.04244959641866207,"RD":0.26378834912257804,"Status":2},
        {"UId":"1234567","AId":"123456701","Coin":"GAEA","Depo":5,"WDrw":0,"PNL":0,"Frz":0,"Status":2}
    ]
}
```

4. 查询用户子账号的最近的成交记录(必须参数 AId)： GetTrades
```js
// 发送请求消息
{
    "req":"GetTrades",
    "rid":"4",
    "expires":1537711037271,
    "args":{
        "AId":"123456701"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}

// 收到返回消息, 默认最多返回200条记录，通过在args中增加设置参数("Start"=0,"Stop"=500)可以最多返回500条记录.
{
    "rid":"4",
    "code":0,
    "data":[
        {
            "UId":"1234567",
            "AId":"123456701",
            "Sym":"BTC1812",
            "MatchId":"01CQES0XMV1WWWXP63PMPKE9RF",
            "OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3",
            "Sz":259,
            "Prz":6.75E+3,
            "Fee":-0.00001151111111111111,
            "FeeCoin":"BTC",
            "At":1537703229547,
            "Via":7
        },
        {"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMVKDQCD192BG4STJF6","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":519,"Prz":6.75E+3,"Fee":-0.00002306666666666667,"FeeCoin":"BTC","At":1537703229346,"Via":7},
        {"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMVXD7ZPV22YF49AW93","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":504,"Prz":6.75E+3,"Fee":-0.00002240000000000000,"FeeCoin":"BTC","At":1537703229146,"Via":7},
        {"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMV9FDYBTRX29VZ1WAP","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":585,"Prz":6.75E+3,"Fee":-0.00002600000000000000,"FeeCoin":"BTC","At":1537703228944,"Via":7}
    ]
}
```

5. 查询用户最长的当前有效的报单列表(必须参数 AId)： GetOrders
```js
// 发送请求消息
{
    "req":"GetOrders",
    "rid":"5",
    "expires":1537711298635,
    "args":{
        "AId":"123456701"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}
// 收到返回消息
{
    "rid":"5",
    "code":0,
    "data":[
        {
            "UId":"1234567",
            "AId":"123456701",
            "Sym":"ETH1812",
            "OrdId":"01CQES0XMVA1CWXJ823B8TV9FA",
            "COrdId":"1537703785880",
            "Dir":-1,
            "OType":1,
            "Prz":265,
            "Qty":1000,
            "QtyDsp":0,
            "PrzStop":0,
            "At":1537703785905,
            "Upd":1537703785905,
            "Until":9223372036854775807,
            "Frz":0,
            "Status":2,
            "QtyF":0,
            "PrzF":0,
            "Val":0,
            "StopPrz":0
        },
        {"UId":"1234567","AId":"123456701","Sym":"BTC1812","OrdId":"01CQES0XMV4FKJMJFFC8SC4EE3","COrdId":"1537703873308","Dir":-1,"OType":1,"Prz":7.10E+3,"Qty":4000,"QtyDsp":0,"PrzStop":0,"At":1537703873327,"Upd":1537703873328,"Until":9223372036854775807,"Frz":0,"Status":2,"QtyF":0,"PrzF":0,"Val":0,"StopPrz":0}
    ]
}
```

6. 查询用户子账号的当前持仓列表(必须参数 AId)： GetPositions
```js
// 发送请求消息
{
    "req":"GetPositions",
    "rid":"6",
    "expires":1537711980986,
    "args":{
        "AId":"123456701"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}

// 收到返回消息
{
    "rid":"6",
    "code":0,
    "data":[
        {
            "UId":"1234567",
            "PId":"01CQES0XMVZ2T6GBWVBX62TMMK",
            "AId":"123456701",
            "Sym":"ETH1812",
            "Sz":100,
            "PrzIni":246.75,
            "RPNL":0.000121580547112462,
            "Val":0.4061408496466575,
            "MMnF":0.020307042482332876,
            "MI":0.26066975804143216,
            "UPNL":-0.0008723592717840498,
            "PrzLiq":154.1425703534361,
            "PrzBr":146.7046448590807,
            "FeeEst":0.0002842985947526602,
            "ROE":-0.04236534514766641,
            "ADLIdx":-0.07944234516057963
        },
        {"UId":"1234567","PId":"01CQES0XMVJGCXCS0MF1P2ZK5V","AId":"123456701","Sym":"BTC1809","Sz":-120,"PrzIni":6737.5,"RPNL":0.000005343228200371059,"Val":0.017813378143875687,"MMnF":0.0008906689071937843,"UPNL":0.0000026174759721611044,"PrzLiq":63949689.43002453,"PrzBr":67365100.00002584,"FeeEst":0.000012469364700712979,"ROE":0.0028982007003982577,"ADLIdx":0.0007826905463650653,"ADLLight":3},
        {"UId":"1234567","PId":"01CQES0XMVS6XK7P4W46ZTY17H","AId":"123456701","Sym":"ETH1809","Sz":50,"PrzIni":245.55,"RPNL":0.00006108735491753208,"Val":0.20377389248889433,"MMnF":0.010188694624444716,"UPNL":-0.00014937609712074688,"PrzLiq":111.97759051807083,"PrzBr":106.57427478640034,"FeeEst":0.000142641724742226,"ROE":-0.014458545542610519,"ADLIdx":-0.027112272106186153}
    ]
}
```

7. 查询用户子账号的最近已完成的报单列表(必须参数 AId)： GetHistOrders
```js
// 发送请求消息
{
    "req":"GetTrades",
    "rid":"7",
    "expires":1537712072667,
    "args":{
        "AId":"123456701"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}

// 收到返回消息
{
    "rid":"7",
    "code":0,
    "data":[
        {
            "UId":"1234567",
            "AId":"123456701",
            "Sym":"BTC1809",
            "MatchId":"01CQES0XMV4VFBEETKJ2522BKH",
            "OrdId":"01CQES0XMVW9BFVS1QNZXXE3AC",
            "Sz":-120,
            "Prz":6737.5,
            "Fee":-0.000005343228200371059,
            "FeeCoin":"BTC",
            "At":1537711867713,
            "Via":7
        },
        {"UId":"1234567","AId":"123456701","Sym":"ETH1809","MatchId":"01CQES0XMVJKBYW747NX6HKYMV","OrdId":"01CQES0XMVM6BR3JQC4Y3TJ0GP","Sz":50,"Prz":245.55,"Fee":-0.00006108735491753208,"FeeCoin":"ETH","At":1537711603240,"Via":7},
        {"UId":"1234567","AId":"123456701","Sym":"ETH1812","MatchId":"01CQES0XMVPEVSRHC5N6F44NVQ","OrdId":"01CQES0XMVMQ06C0MDCJ9FN4SC","Sz":100,"Prz":246.75,"Fee":-0.0001215805471124620,"FeeCoin":"ETH","At":1537711597374,"Via":7},
        {"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMV1WWWXP63PMPKE9RF","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":259,"Prz":6.75E+3,"Fee":-0.00001151111111111111,"FeeCoin":"BTC","At":1537703229547,"Via":7},
        {"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMVKDQCD192BG4STJF6","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":519,"Prz":6.75E+3,"Fee":-0.00002306666666666667,"FeeCoin":"BTC","At":1537703229346,"Via":7},
    ]
}
```

8. 查询用户子账号的最近钱包日志列表(必须参数 AId)： GetWalletsLog
```js

// 发送请求消息
{
    "req":"GetWalletsLog",
    "rid":"9",
    "expires":1537712200855,
    "args":{
        "AId":"123456701"
    },
    "signature": "1234567890abcdef1234567890abcdef"
}

// 收到返回消息
{
    "rid":"9",
    "code":0,
    "data":[
        {
            "UId":"1234567",
            "AId":"123456701",
            "Seq":"01CQES0XMVR78FS73QV5WBT3F8",
            "Coin":"BTC",
            "Qty":0.000005343228200371059,
            "Fee":0,
            "WalBal":0.1609282822429802,
            "At":1537711867713,
            "Op":3,
            "Via":8,
            "Info":"Fee :01CQES0XMV4VFBEETKJ2522BKH",
            "Stat":4,
            "DirtyFlag":11
        },
        {"UId":"1234567","AId":"123456701","Seq":"01CQES0XMVTMQHGPGJZMP9WJKT","Coin":"ETH","Qty":0.00006108735491753208,"Fee":0,"WalBal":0.5478079547162858,"At":1537711603240,"Op":3,"Via":8,"Info":"Fee :01CQES0XMVJKBYW747NX6HKYMV","Stat":4,"DirtyFlag":11},
        {"UId":"1234567","AId":"123456701","Seq":"01CQES0XMVYS0W0E1Q69Y9GC3X","Coin":"ETH","Qty":0.0001215805471124620,"Fee":0,"WalBal":0.5477468673613683,"At":1537711597374,"Op":3,"Via":8,"Info":"Fee :01CQES0XMVPEVSRHC5N6F44NVQ","Stat":4,"DirtyFlag":11},
    ]
}
```

9. 获取服务器时间： Time
```js
// 发送请求消息， 由于本消息开销很小，可用于和服务器端保持网络连接用，比如每隔55秒发送一次；
{
    "req":"Time",
    "rid":"13",
    "expires":1537713841078,
    "args":1537713840076
}

// 收到返回消息
{
    "rid":"13",
    "code":0,
    "data":{
        "time":1537713840095,
        "data":"1537713840076"
    }
}
```

10. 下单： OrderNew

```js
// 发送下单请求
// COrdId 是 Client Order ID 的意思，不能为空，由用户生成管理并维护其唯一性，当报单成功后，会对应一个OrdId，为系统能够识别的报单编号;
// 注意，用户发起下单后，要通过 onOrder 消息来监控管理报单的状态变化;
{
    "req":"OrderNew",
    "rid":"10",
    "expires":1537712923999,
    "args":{
        "AId":"123456701",
        "COrdId":"0",
        "Sym":"BTC1809",
        "Dir":1,
        "OType":1,
        "Prz":6500,
        "Qty":2,
        "QtyDsp":0,
        "Tif":0,
        "OrdFlag":0,
        "PrzChg":0
    },
    "signature": "1234567890abcdef1234567890abcdef"
}

# 收到返回消息
{
    "rid":"10",
    "code":0,
    "data":{
        "UId":"1234567",
        "AId":"123456701",
        "Sym":"BTC1809",
        "OrdId":"01CQES0XMVV3SMWJ7N683FWJR8",
        "COrdId":"0",
        "Dir":1,
        "OType":1,
        "Prz":6500,
        "Qty":2,
        "QtyDsp":0,
        "PrzStop":0,
        "At":1537712923017,
        "Until":9223372036854775807,
        "Frz":0,
        "Status":1,
        "QtyF":0,
        "PrzF":0,
        "Val":0,
        "StopPrz":0
    }
}


// 后继 onOrder 推送消息会汇报报单的变更情况
{
    "subj":"onOrder",
    "data":{
        "WId":"123456701BTC",
        "Prz":6500,
        "Qty":2,
        "Upd":1537712923017,
        "Frz":0,
        "PrzF":0,
        "AId":"112562301",
        "COrdId":"0",
        "QtyDsp":0,
        "PrzStop":0,
        "Val":0,
        "Dir":1,
        "Until":9223372036854776000,
        "Status":2,
        "QtyF":0,
        "StopPrz":0,
        "UId":"1125623",
        "Sym":"BTC1809",
        "OrdId":"01CQES0XMVV3SMWJ7N683FWJR8",
        "OType":1,
        "At":1537712923017
    }
}
```

报单的参数说明：
```js
args: {
"AId": "账户Id",
"COrdId": "filled by client,客户端自己填写",
"Sym": "BTC1809",  // 交易符号，比如XBTUSD
"Dir": 1,   // 委单方向 买/卖, 1:BID/BUY, -1:ASK/SELL
"OType": 1,  // 报价类型, 1:Limit(限价委单 ), 2: Market(市价委单,匹配后转限价), 3: StopMarket (市价止损);
"Prz": 8000,  // 价格
"Qty": 10000, // 数量(如果>0则为做多,如果<0则为做空)
"QtyDsp": 0,  // 显示数量, 0表示不隐藏, 用于支持冰山委托
"Tif": 0, // 生效时间设定, 0:GoodTillCancel, 1:ImmediateOrCancel/FillAndKill, 2:FillOrKill
"OrdFlag": 0, // 标志位, 0: OF_INVALID, 1: POSTONLY, 2: REDUCEONLY, 4: CLOSEONTRIGGER;
"PrzChg" 0, // 市价成交档位
}
```

11. 撤单： OrderDel
```js
// 发送撤单请求
{
    "req":"OrderDel",
    "rid":"11",
    "expires":1537713298949,
    "args":{
        "AId":"123456701",
        "OrdId":"01CQES0XMVV3SMWJ7N683FWJR8",
        "Sym":"BTC1809"
    },
    "signature": "1234567890abcdef1234567890abcdef"
    }
}

// 收到返回消息
{
    "rid":"11",
    "code":0,
    "data":{
        "UId":"1234567",
        "AId":"123456701",
        "Sym":"BTC1809",
        "OrdId":"01CQES0XMVV3SMWJ7N683FWJR8",
        "Prz":0,
        "Qty":0,
        "QtyDsp":0,
        "PrzStop":0,
        "At":1537713297967,
        "Until":9223372036854775807,
        "Frz":0,
        "Status":1,
        "QtyF":0,
        "PrzF":0,
        "Val":0,
        "StopPrz":0
    }
}

// 后继 onOrder 推送消息会汇报报单的变更情况
{
    "subj":"onOrder",
    "data":{
        "WId":"123456701BTC",
        "At":1537713297967,
        "Until":9223372036854776000,
        "Status":1,
        "OrdId":"01CQES0XMVV3SMWJ7N683FWJR8",
        "Prz":0,
        "PrzF":0,
        "Val":0,
        "Frz":0,
        "QtyF":0,
        "UId":"1234567",
        "Sym":"BTC1809",
        "Qty":0,
        "PrzStop":0,
        "AId":"123456701",
        "QtyDsp":0,
        "Upd":1537713297967,
        "StopPrz":0
    }
}
```


12. 设置超时撤单(必须参数 AId)： CancelAllAfter

|参数|	描述|
| :-----   | :-----   |
|AId|用户的子账号ID|
|Sec|设置N秒后自动撤销AId下的所有报单|

调用此接口成功后，用户该AId下的所有报单将在n秒后被全部自动撤单。通过设置0秒可以禁用此功能,常见的使用模式是设 timeout 为 60000，并每隔 15 秒调用一次,建议每次使用完API将Sec设置为0,禁用此功能。



13. 用户收到的推送消息

用户登录后会收到的推送消息的subj有：
报单通知 onOrder
持仓通知 onPosition
钱包通知 onWallet
钱包日志 onWltLog
成交通知 onTrade

```js
// 比如：钱包变化
{
    "subj":"onWallet",
    "data":{
        "PNL":-0.004261550985220193,
        "MI":0.04240898874506015,
        "Status":2,
        "Coin":"BTC",
        "WId":"112562301BTC",
        "Depo":0.16518449,
        "Frz":0,
        "RD":0.2635360067663513,
        "UId":"1234567",
        "AId":"123456701",
        "WDrw":0
    }
}
```


对应的数据结构定义如下:
```golang
type Ord struct {    // **报单结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    Sym     string   // 交易符号，比如BTC1809
    OrdId   string   // 服务器端为其分配的ID
    COrdId  string   // 客户端为其分配的ID
    Dir     int32    // 委单方向 买/卖, 1:BID/BUY, -1:ASK/SELL
    OType   int32    // 报价类型, 1:Limit(限价委单 ), 2: Market(市价委单,匹配后转限价), 3: StopMarket (市价止损);
    Prz     float64  // 报价
    Qty     float64  // 数量(如果>0则为做多,如果<0则为做空)
    QtyDsp  float64  // 显示数量, 0表示不隐藏, 用于支持冰山委托
    Tif     int32    // 生效时间设定, 0:GoodTillCancel, 1:ImmediateOrCancel/FillAndKill, 2:FillOrKill
    OrdFlag int32    // 标志位, 0: OF_INVALID, 1: POSTONLY, 2: REDUCEONLY, 4: CLOSEONTRIGGER;
    Via     int32    // 订单来源
    At      int64    // 报单时间戳，毫秒
    Upd     int64    // 报单更新时间戳，毫秒
    Until   int64    // 有效期，毫秒 。绝对时间
    PrzChg  int32    // 最大价格变动次数， 市价成交档位
    Frz     float64  // 冻结的金额
    ErrCode int32    // 错误编码
    ErrTxt  string   // 错误文本
    Status  int32    // 0-无效, 1-正在排队, 2-有效(撮合中), 3-提交失败, 4-已执行, 5-取消, 6-部分执行, 7-执行失败
    QtyF    float64  // 已成交
    PrzF    float64  // 已成交的平均价格
    Val     float64  // 合约价值:
    StopBy  int32    // 判断依据, 0-PriceMark, 1-PriceLatest, 2-PriceIndex
    StopPrz float64  // 止损价格,止盈价格
    // //////////////////////////////////////////////////////////////////////////////////////////////////
    MM      float64  // 委托保证金 Mgn Initial + 佣金
    FeeEst  float64  // 预估的手续费：按照手续费计算
    UPNLEst float64  // 预估的UPNL	.. Predicatee
}

type Position struct {    // **持仓结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    PId     string   // 持仓Id
    Sym     string   // 交易符号，比如BTC1809
    Sz      float64  //仓位(正数为多仓，负数为空仓)
    PrzIni  float64  // 开仓平均价格
    RPNL    float64  // 已实现盈亏
    Val     float64  // 计算值：价值,仓位现时的名义价值，受到标记价格价格的影响
    MMnF    float64  // 保证金，被仓位使用并锁定的保证金
    MI      float64  //
    UPNL    float64  // 计算值：未实现盈亏 PNL==  Profit And Loss
    PrzLiq  float64  // 计算值: 强平价格 亏光当前保证金的 (如果是多仓，并且标记价格低于PrzLiq,则会被强制平仓。/如果是空仓,并缺标记价格高于PrzLiq，则会被强制平仓
    PrzBr   float64  // 计算值: 破产价格 BandRuptcy
    FeeEst  float64  // 预估的平仓费
    // //////////////////////////////////////////////////////////////////////////
    ROE     float64
    ADLIdx  float64  // ADLIdx, 这个是用来排序ADL的
    ADLLight int32   // ADL红绿灯
}

type Wlt struct {    // **钱包结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    Coin    string   // 货币符号 BTC/ETH/GAEA
    Depo    float64  // 入金金额
    WDrw    float64  // 出金金额
    PNL     float64  // 已实现盈亏
    Frz     float64  // 冻结金额
    UPNL    float64  // 未实现盈亏：根据持仓情况、标记价格 刷新， 统计值
    MI      float64  // 委托保证金 = 计算自已有委单 + 平仓佣金 + 开仓佣金 Mgn Initial
    MM      float64  // 仓位保证金 + 平仓佣金 Mgn Maintaince
    RD      float64  // 风险度 // Risk Degree.
    Status  int32    // 账户状态，0-INVALID，1-NOT_ACTIVED，2-NORMAL，3-LIQUIDATION，4-TAKEN_OVER
}

type WltLog struct {    // **资金历史结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    Seq     string   // 顺序号
    Coin    string   // 货币类型
    Qty     float64  // 货币数量
    Fee     float64  // 手续费
    Peer    string   // 货币地址(假设是出金，则是地址)
    WalBal  float64  //
    At      int64    // 时间
    Op      int32    // 钱包操作: 0-非法, 1-存钱, 2-取钱, 3-已实现盈亏, 4-现货交易, 5-查询
    Via     int32    // OrderVia
    ErrCode int32    // 错误代码
    ErrTxt  string   // 错误文本
    Stat    int32    // OrderStatus
}

type TrdRec struct {        // **成交结构体字段定义说明**
    UId             string  // 用户Id
    AId             string  // 账户Id
    Sym             string  // 交易对符号
    MatchId         string  // 撮合ID
    OrdId           string  // 报单ID
    Sz              float64 // 数量
    Prz             float64 // 价格
    Fee             float64 // 手续费
    FeeCoin         string  // 手续费货币类型
    At              int64   // 成交时间(ms)
    Via             int32   // 报单来源， 0-无效, 1-WEB, 2-APP, 3-API, 4-平仓Liquidate, 5-ADLEngine, 6-Settlement, 7-Trade, 8-Fee, 9-Depo, 10-Wdrw

}

```
 14. 查询用户的风险限额GetRiskLimit(内测中,不建议使用)
```JavaScript
    /**
    * 功能: 查询某个交易对用户的风险限额
    * 参数说明:
    * expires: 消息的有效时间
    * rid: 10   //用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答。
    * req: 'GetRiskLimit'   // 请求的动作名称
    * signature: ""        // 签名,参考签名的生成规则
    * args: {
    *  "AId": "", // 账号的AId,
    *  "Sym": "", // 交易对名称
    * }
    */
    // 请求发送参数
    {"req":"GetRiskLimit","rid":"15","expires":1537712072667,"args":{"AId":"123456701",Sym:"BTC1812"},"signature": "1234567890abcdef1234567890abcdef"}
    // 接收到的返回消息
    {"rid":"15","code":0,"data":{....}}
    
```
```golang
    // RiskLimit的数据结构
    type RiskLimitDef struct {
	 Sym         string      // Symbol 交易对
	 Base        float64     // Base Risk Limit 当 Pos Val < Base 的时候，
	 BaseMMR     float64     // Base Maintenance Margin Val < Base 的时候 MMR
	 BaseMIR     float64     // Initial Margin  Val < Base 的时候 MIR
	 Step        float64     // Step  StepS = math.Ceil((Val - Base)/Step) 表示递增次数
	 StepMR      float64     // StepM  每次递增的时候，MMR MIR 的增量
         PosSzMax    float64     // 最大持仓
         StepIR      float64     // 每次递增的时候，MIR 的增量
    }
```


## 相关术语
| 名称 | 描述 |
| :------: | :------ |
|UId | 是用户的系统内部唯一编号, 例: UId:123456|
|AId | 子账户ID, AId是在UId的后面添加两位数字生成, 01为合约ID, 02为现货ID, 例: UId:123456加01则为AId:12345601|
|rid | 用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答,值必须为字符串类型|



## 委托状态码


| ErrCode| ErrTxt | 描述 |
|:------:|:------|:------|
| 0       |  NORERROR| 没有错误  |
| 1       |  GENERAL | 数据错误 |
| 2       |  DATA    | 数据错误 |
| 3       |  NOT_IMPLEMENTED     | 服务器未实现 |
| 4       |  NO_MARGIN     | 保证金不足 |
| 5       |  FATAL     | 致命错误 |
| 6       |  NOT_FOUND     | 未找到 |
| 7       |  UNKNOWN_DIR     | 未知的委托方向 |
| 8       |  INVALID_CODE     | 操作码错误 |
| 9       |  EXISTS     | 已存在 |
| 10      |  NOT_FOUND_ORD     | 没有找到订单号 |
| 11      |  PRZ_INVALID     | 价格错误 |
| 12      |  EXPIRED     | 已过期 |
| 13      |  NOT_SUFFICIENT     | 资金不足 |
| 14      |  WILLFILL     | 对于PostOnly，本委托会成交 |
| 15      |  EXECUTE_FAIL     | 对FillOrKill委托，这表示执行撮合失败 |
| 16      |  EXCEED_LIMIT_MINVAL     | 超过限制|
| 17      |  VAL_TOO_SMALL     | 委托价值太小 |
| 18      |  EXCEED_LIMIT_PRZ_QTY     | 价格或者数量超出限制 |
| 19      |  DENYOPEN_BY_POS     | 仓位超出限制 |
| 20      |  DENYOPEN_BY_RD     | 禁止开仓 |
| 21      |  TRADE_STOPED     |  交易暂停 |
| 22      |  EXCEED_PRZ_LIQ     | 超过强平价格 |
| 23      |  TOO_MANY_ORDER     | 太多的委托 |
| 24      |  DENYOPEN_BY_TIME     | 超出开仓时间限制 |
| 25      |  MD5_INVALID     | MD5签名验证错误 |
| 26      |  RATELIMIT     | 限速,每秒50次API调用 |


