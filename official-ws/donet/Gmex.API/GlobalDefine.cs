using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Gmex.API
/// 
/// PM> Install-Package Newtonsoft.Json
/// 
/// </summary>
namespace Gmex.API
{
    public static class GlobalDefine
    {
        // PROD websockets server URL
        public const string K_PROD_GmexTradeServerWsUrl = @"wss://api-trade.gmex.io/v1/trade";
        public const string K_PROD_GmexMarketServerWsUrl = @"wss://api-market.gmex.io/v1/market";
        // PROD HTTPS/REST server URL
        public const string K_PROD_GmexTradeServerRestUrl = @"https://api-market.gmex.io/v1/rest";
        public const string K_PROD_GmexMarketServerRestUrl = @"https://api-market.gmex.io/v1/rest";


        // SIMGO websockets server URL
        public const string K_SIMGO_GmexTradeServerWsUrl = @"wss://trade01.gmex.io/v1/trade";
        public const string K_SIMGO_GmexMarketServerWsUrl = @"wss://market01.gmex.io/v1/market";
        // SIMGO HTTPS/REST server URL
        public const string K_SIMGO_GmexTradeServerRestUrl = @"https://trade02.gmex.io/v1/rest";
        public const string K_SIMGO_GmexMarketServerRestUrl = @"https://market02.gmex.io/v1/rest";

        // 请求消息的超时过期值，建议大于1秒但不要超过10秒, 单位毫秒.
        public readonly static long MSG_REQ_EXPIRES_DEFAULT_TIMEOUT = 5000;

        // 消息大小定义
        public const int K_DefaultWebSocketMaxMessageSize = 4096 * 8;
        public const int K_MAX_MSG_SIZE = K_DefaultWebSocketMaxMessageSize * 16;
    }
}
