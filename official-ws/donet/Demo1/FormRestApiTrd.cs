using Gmex.API;
using Gmex.API.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo1
{
    public partial class FormRestApiTrd : Form
    {
        public FormRestApiTrd()
        {
            InitializeComponent();
        }

        private void FormRestApiTrd_Load(object sender, EventArgs e)
        {
            comboBoxTrdServer.SelectedIndex = 0;
            comboBoxTrdAccType.SelectedIndex = 0;
        }

        public void LOG(string txt)
        {
            if (textBoxOUT.InvokeRequired)
            {
                textBoxOUT.Invoke(new Action(() => { textBoxOUT.AppendText(txt); }));
            }
            else
            {
                textBoxOUT.AppendText(txt);
            }
        }

        private RESTClient4Trade cli4trd = null;
        private async Task<RESTClient4Trade> GetClientAsync()
        {
            string uid = cli4trd?.GetCurrentUId();
            if (uid == null || uid.Length < 1)
            {
                if (comboBoxTrdServer.Text.Length < 1 || textBoxUname.Text.Length < 1 || textBoxApiKey.Text.Length < 1 || textBoxApiSecret.Text.Length < 1)
                {
                    LOG("[WARN] 请先正确填写参数。。。\r\n");
                    return null;
                }
                try
                {
                    var client = new RESTClient4Trade(comboBoxTrdServer.Text, textBoxUname.Text, textBoxApiKey.Text, textBoxApiSecret.Text);
                    var info = await client.GetUserInfoAsync();
                    LOG($"[DEBUG] connect trade-server, get user info: {info}\r\n");
                    cli4trd = client;
                }
                catch (Exception ex)
                {
                    LOG("[ERROR] connect trade server Exception: " + ex.Message + "\r\n");
                    return null;
                }
            }
            return cli4trd;
        }

        private async void buttonInit_ClickAsync(object sender, EventArgs e)
        {
            if (!comboBoxTrdServer.Enabled)
            {
                comboBoxTrdServer.Enabled = true;
                textBoxUname.Enabled = true;
                textBoxApiKey.Enabled = true;
                textBoxApiSecret.Enabled = true;
                cli4trd = null;
                LOG($"[DEBUG] 重置结束，可以继续重新测试服务器连接了。\r\n");
                return;
            }

            try
            {
                var client = new RESTClient4Trade(comboBoxTrdServer.Text, textBoxUname.Text, textBoxApiKey.Text, textBoxApiSecret.Text);
                var info = await client.GetUserInfoAsync();
                LOG($"[DEBUG] connect trade-server, GetUserInfo: {info}\r\n");

                cli4trd = client;
                comboBoxTrdServer.Enabled = false;
                textBoxUname.Enabled = false;
                textBoxApiKey.Enabled = false;
                textBoxApiSecret.Enabled = false;
            }
            catch (Exception ex)
            {
                LOG("[ERROR] connect trade server Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetTime_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var delta = await cli4trd.GetTimeAsync();
                LOG($"[Time]<< time delta(ms): {delta}\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetServerInfo_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetServerInfoAsync();
                LOG($"[ServerInfo] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetAssetD_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetAssetDAsync(comboBoxTrdAccType.SelectedIndex + 1);
                comboBoxTrdSym.Items.Clear();
                LOG($"[GetAssetD] <<");
                foreach (var item in ret)
                {
                    comboBoxTrdSym.Items.Add(item.Sym);
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");

                if (comboBoxTrdSym.Items.Count > 0)
                {
                    comboBoxTrdSym.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetWlts_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetWalletsAsync(comboBoxTrdAccType.SelectedIndex + 1);
                LOG($"[GetWallets] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetCcsWallets_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;

            try
            {
                var ret = await cli4trd.GetCcsWalletsAsync();
                LOG($"[GetCcsWallets] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetTrades_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetTradesAsync(comboBoxTrdAccType.SelectedIndex + 1, comboBoxTrdSym.Text);
                LOG($"[GetTrades] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetOrders_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetOrdersAsync(comboBoxTrdAccType.SelectedIndex + 1, comboBoxTrdSym.Text);
                LOG($"[GetOrders] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetPos_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetPositionsAsync(comboBoxTrdAccType.SelectedIndex + 1, comboBoxTrdSym.Text);
                LOG($"[GetPositions] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetHistOrd_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetHistOrdersAsync(comboBoxTrdAccType.SelectedIndex + 1, comboBoxTrdSym.Text);
                LOG($"[GetHistOrders] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetWltLog_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetWalletsLogAsync(comboBoxTrdAccType.SelectedIndex + 1, comboBoxTrdSym.Text);
                LOG($"[GetWalletsLog] <<\r\n");
                foreach (var item in ret)
                {
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetRiskLimit_ClickAsync(object sender, EventArgs e)
        {
            if (cli4trd == null) return;
            try
            {
                var ret = await cli4trd.GetRiskLimitAsync(comboBoxTrdAccType.SelectedIndex + 1, comboBoxTrdSym.Text);
                LOG($"[GetRiskLimit] <<\r\n");
                LOG($"{Helper.MyJsonMarshal(ret)}\r\n");
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonOrderNew_ClickAsync(object sender, EventArgs e)
        {
            /******
             * 用户下单注意事项：
             * 1. 用户通常在本地维护 【所有委托】表； 主键使用 COrdId;
             * 2. 所有委托通常会被分为三种：当前委托，条件委托，历史委托;
             * 2. 尽管 Server 端是使用 OrdId 来作为 Ord 的主键的，但是由于websockets的异步通讯机制，用户在下单（新建委托）时只能
             *    自己维护 COrdId 而要等待服务器返回对应的 OrdId；因此建议本地使用GUID来自己维护自己Ord的唯一性；故，本地保存的
             *    所有委托的主键使用 COrdId 比较方便;
             * 3. 特别注意的是，在WebSocket模式下， OrderNew 的返回结果并不能保证一定比 onOrder 消息到来的快，因此，在下单前，应该
             *    先本地保存 COrdId 入表以便 onOrder 能正确识别;
             * 4. 委托的有效性，首先看 ErrorCode 是否为 0，然后再看 Status;
             * 5. 对于条件委托，一开始OType是条件类型，触发后会自动变成 Limit 或 Market 类型；如在历史单中想判断是否条件单，可借
             *    助 StopPrz 来进一步判断。
             * 6. 性能考虑，建议单独建立一个历史委托的表单独存放已经不活动的委托，因为这些委托数据是不会再变化了。
             */
            if (cli4trd == null) return;

            var sym = textBox_ORD_sym.Text.Trim();
            if (sym.Length < 1) return;

            var acctype = comboBox_ORD_AccType.SelectedIndex + 1;

            Gmex.API.Models.Ord ord = new Gmex.API.Models.Ord();
            try
            {
                ord.COrdId = Guid.NewGuid().ToString();
                ord.AId = cli4trd.GetCurrentUId() + acctype.ToString("D2");
                ord.Sym = sym;
                ord.Prz = Convert.ToDecimal(textBox_ORD_Prz.Text.Trim());
                ord.Qty = Convert.ToDecimal(textBox_ORD_Qty.Text.Trim());
                ord.Dir = comboBox_ORD_DIR.SelectedIndex == 1 ? Gmex.API.Models.Dir.SELL : Gmex.API.Models.Dir.BUY;
                ord.Tif = (Gmex.API.Models.TimeInForce)comboBox_ORD_TIF.SelectedIndex;
                ord.OType = (Gmex.API.Models.OfferType)comboBox_ORD_OffetType.SelectedIndex;

                if (checkBoxPOSTONLY.Checked)
                    ord.OrdFlag__Set(Gmex.API.Models.OrdFlag.POSTONLY);
                if (checkBoxREDUCEONLY.Checked)
                    ord.OrdFlag__Set(Gmex.API.Models.OrdFlag.REDUCEONLY);

                var ret = await cli4trd.OrderNewAsync(ord);
                LOG($"[OrderNew] <<\r\n");
                LOG($"{Helper.MyJsonMarshal(ret)}\r\n");
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message);
                return;
            }
        }

        private async void buttonOrderDel_ClickAsync(object sender, EventArgs e)
        {
            var ordid = textBox_ORD_OrdId.Text.Trim();
            if (ordid.Length < 1) return;

            var sym = textBox_ORD_sym.Text.Trim();
            if (sym.Length < 1) return;

            var acctype = comboBox_ORD_AccType.SelectedIndex + 1;

            Gmex.API.Models.Ord ord = new Gmex.API.Models.Ord
            {
                AId = cli4trd.GetCurrentUId() + acctype.ToString("D2"),
                Sym = sym,
                OrdId = ordid
            };

            try
            {
                var ret = await cli4trd.OrderDelAsync(ord);
                LOG($"[OrderDel] <<\r\n");
                LOG($"{Helper.MyJsonMarshal(ret)}\r\n");
                LOG($"========\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message);
                return;
            }
        }

        private void comboBoxTrdServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var url = comboBoxTrdServer.Text.Trim();
            try
            {
                string apikeyfile = @"user-api-key.json";
                if (File.Exists(apikeyfile))
                {
                    var config = Helper.MyJsonUnmarshal<Dictionary<string, Gmex.API.Models.UserApiKeyData>>(File.ReadAllText(apikeyfile));
                    if (config.ContainsKey(url))
                    {
                        textBoxUname.Text = config[url].UserName;
                        textBoxApiKey.Text = config[url].ApiKey;
                        textBoxApiSecret.Text = config[url].ApiSecret;
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
