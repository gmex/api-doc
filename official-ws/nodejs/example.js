const GAEA = require('./gaealib/gaealib')

const test_market_url = 'wss://market01.gmex.io/v1/market';
const test_trade_url  = 'wss://trade01.gmex.io/v1/trade';

var UserName   = "";  //your account name
var api_key    = "";  // your api key
var secret_key = "";  // your api SecretKey


var market = new GAEA()
var trade = new GAEA()

market.on("close",() => {
    console.log("market closed!");
})
market.on("error",(err) => {
    console.log("market error!",err);
})

trade.on("close",() => {
    console.log("trade closed!");
})
trade.on("error",(err) => {
    console.log("trade error!",err);
})

// listen on some market push events
market.on("tick",function (ret){
    console.log(`[on.market.tick]<< ${JSON.stringify(ret)}`)
});
market.on("trade",function (ret){
    console.log(`[on.market.trade]<< ${JSON.stringify(ret)}`)
});
market.on("orderl2",function (ret){
    console.log(`[on.market.orderl2]<< ${JSON.stringify(ret)}`)
});
market.on("order10",function (ret){
    console.log(`[on.market.order20]<< ${JSON.stringify(ret)}`)
});
market.on("kline",function (ret){
    console.log(`[on.market.kline]<< ${JSON.stringify(ret)}`)
});

market.init({ws_url:test_market_url}, ()=>{
    console.log("market connected!")
    market.request('GetInstruments',{},(ret)=>{
        console.log(`[market.GetInstruments]<< ${JSON.stringify(ret)}`)
        if(ret.code==0) {
            var sym = ret.data[0]; // example: BTC1903
            market.request('Sub',["trade_"+sym,"tick_"+sym,"kline_1m_"+sym,"order20_"+sym,"orderl2_"+sym],(ret)=>{})
        }
    })
    market.request('GetAssetD',{}, (ret)=>{
        console.log(`[market.GetAssetD]<< ${JSON.stringify(ret)}`)
    });
});


// listen on some trade push events
trade.on("onOrder",function (ret){
    console.log(`[on.trade.onOrder]<< ${JSON.stringify(ret)}`)
});
trade.on("onPosition",function (ret){
    console.log(`[on.trade.onPosition]<< ${JSON.stringify(ret)}`)
});
trade.on("onWallet",function (ret){
    console.log(`[on.trade.onWallet]<< ${JSON.stringify(ret)}`)
});
trade.on("onTrade",function (ret){
    console.log(`[on.trade.onTrade]<< ${JSON.stringify(ret)}`)
});


if(!UserName||!secret_key||!api_key) console.warn("If you want to test trade, please set your account info at first!");
var userdata = {};
trade.init({ws_url:test_trade_url,SecretKey: secret_key}, ()=>{
    console.log("trade connected!")
    var msg = {
      UserName: UserName,
      UserCred: api_key
    };
    trade.request('Login', msg, (ret)=>{
        console.log(`[trade.Login]<< ${JSON.stringify(ret)}`)
        if(ret.code==0) {
            userdata.uid = ret.data.UserId;
            userdata.aid01 = userdata.uid+"01";  // Future account
            userdata.aid02 = userdata.uid+"02";  // Token trading account

						// do some init queries
            trade.request('GetWallets',{AId: userdata.aid01}, (ret)=>{
                console.log(`[trade.GetWallets]<< ${JSON.stringify(ret)}`)
            });
            trade.request('GetTrades',{AId: userdata.aid01}, (ret)=>{
                console.log(`[trade.GetTrades]<< ${JSON.stringify(ret)}`)
            });
            trade.request('GetOrders',{AId: userdata.aid01}, (ret)=>{
                console.log(`[trade.GetOrders]<< ${JSON.stringify(ret)}`)
            });
            trade.request('GetPositions',{AId: userdata.aid01}, (ret)=>{
                console.log(`[trade.GetPositions]<< ${JSON.stringify(ret)}`)
            });

           // do some trade etc, if you need
           //trade.request('OrderNew', ....
           //trade.request('OrderDel', ....
        }
    })
});

