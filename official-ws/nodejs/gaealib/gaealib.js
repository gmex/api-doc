const WebSocket = require('ws');
const CryptoJS = require('crypto-js');

module.exports = gaea;

function gaea() {
    this.rid = 0;
    this.msgMap = {};  // rid - msg
    this.listeners = {};
    return this;
}

gaea.prototype.init = function (options, cb) {
    if(typeof options !=='object') return;
    var self = this;

    self.SecretKey = options.SecretKey||"";

    self.expiretm = options.expiretm||1000;
    self.timeout = options.timeout||20000;
    self.notimeout = options.notimeout||10000; //
    self.activetm = 0;
    self.fixTime = 0;   // Time autosync
    self.close();
    self.tmrClean = setInterval(function(){
        var tm = Date.now();
        for(var rid in self.msgMap) {
            var m = self.msgMap[rid];
            if(m.tm+self.timeout<tm) {
                if(typeof(m.cb)=='function') {
                    m.cb({code:-9999,data:'timeout',req:m.req, args:m.args})
                }
                delete self.msgMap[rid];
            }
        }
    },1000);

    if(typeof cb == 'function') self.listeners['open'] = cb;

    var onPush = function (route,msg) {
        if(typeof(self.listeners[route])=='function') self.listeners[route](msg);
    }

    var ws = new WebSocket(options.ws_url);
    ws.on('open', () => {
        self.connected = true;
        self.activetm = Date.now();
        self.synctime(onPush);
        self.tmr = setInterval(function(){
            if(self.activetm+self.notimeout>Date.now()) return;
            if(self.activetm+self.timeout<Date.now()) {
                self.ws.close();
                return;
            }
            if(self.connected){
                self.synctime();
            }
        },self.notimeout/2);
    });
    ws.on('message', (data) => {
        self.activetm = Date.now();
        try{
            var msg = JSON.parse(data);
        } catch (e) {
            //console.log(data,e)
            return;
        }
        if(msg.subj==='kick') {
            onPush('kick',msg.data)
            return
        }

        var rid = msg.rid;
        var m = self.msgMap[rid];
        delete msg.rid;
        if(rid && m && typeof(m.cb)=='function') {
            m.cb(msg);
        } else if(msg.subj && typeof(self.listeners[msg.subj])=='function') {
            self.listeners[msg.subj](msg.data);
        }
        delete self.msgMap[rid];
    });
    ws.on('close', () => {
        self.connected = false;
        clearInterval(self.tmr);
        onPush('close')
    });
    ws.on('error', err => {
        onPush('error',err)
    });

    self.ws = ws;
}

gaea.prototype.synctime = function (onPush) {
    var self = this;
    var tm = Date.now()
    self.request('Time',tm,(ret)=>{
        self.activetm = Date.now();
        if(ret.code==0) {
            self.fixTime = ret.data.time-tm;
            if(onPush)
                onPush('open')
        }
    })
}

gaea.prototype.on = function (route,cb) {
    this.listeners[route] = cb;
}
gaea.prototype.remove = function (route) {
    delete this.listeners[route];
}
gaea.prototype.close = function () {
    var self = this;
    if(this.ws) {
        this.ws.on('close',()=>{})
        this.ws.close();
        delete this.ws;
    }
    this.connected = false;
    if(self.tmrClean) {
        clearInterval(self.tmrClean);
        delete self.tmrClean;
    }
    if(self.tmr) {
        clearInterval(self.tmr);
        delete self.tmr;
    }
    self.msgMap = {};
}

gaea.prototype.request = function (route,pmsg,cb) {
    if(arguments.length !== 3 || typeof route !='string' || typeof(cb)!='function' || !this.ws) {
        console.log('Request params error:',arguments.length !== 3 , typeof route !='string' , typeof(cb)!='function' )
        return;
    }
    var msg = {}
    msg.req = route;
    msg.rid = String(this.rid++);
    msg.args = pmsg;
    msg.expires = Date.now()+this.fixTime+this.expiretm;
    this.msgMap[msg.rid] = {tm:Date.now(),cb:cb,req:route,args:pmsg};

    if(typeof(msg.args)=='object') {
        msg.Signature = CryptoJS.MD5(msg.req+String(msg.rid)+JSON.stringify(msg.args)+String(msg.expires)+this.SecretKey).toString().toLowerCase();
    }

    try{
        if(this.connected)
            this.ws.send(JSON.stringify(msg));
    } catch (e) {}
}


