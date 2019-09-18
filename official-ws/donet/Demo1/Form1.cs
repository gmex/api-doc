using Gmex.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo1
{
    public partial class Form1 : Form
    {
        private Gmex.API.WS.Client4Market cli4mkt;
        private CancellationTokenSource cts4mkt = new CancellationTokenSource();

        private Gmex.API.WS.Client4Trade cli4trd;
        private CancellationTokenSource cts4trd = new CancellationTokenSource();

        private bool IsClient4MarketOK()
        {
            if (cli4mkt == null) return false;
            return cli4mkt.IsConnected();
        }
        private bool IsClient4TradeOK()
        {
            if (cli4trd == null) return false;
            return cli4trd.IsConnected();
        }

        public void UI_ACTION(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public void LOG(string txt)
        {
            bool uiMarshal = textBoxOUT1.InvokeRequired;
            if (uiMarshal)
            {
                textBoxOUT1.Invoke(new Action(() => { textBoxOUT1.AppendText(txt); }));
            }
            else
            {
                textBoxOUT1.AppendText(txt);
            }
        }
        public void LOG2(string txt)
        {
            bool uiMarshal = textBoxOUT2.InvokeRequired;
            if (uiMarshal)
            {
                textBoxOUT2.Invoke(new Action(() => { textBoxOUT2.AppendText(txt); }));
            }
            else
            {
                textBoxOUT2.AppendText(txt);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxMktServer.SelectedIndex = 0;
            comboBoxMktKLineTyp.SelectedIndex = 0;
            comboBoxTrdServer.SelectedIndex = 0;
            comboBoxTrdAccType.SelectedIndex = 0;

            comboBox_ORD_DIR.SelectedIndex = 0;
        }

        private void OnMarketPushMessage(object obj)
        {
            if (obj is Gmex.API.Models.MktOrderItem)
            {
                var msg = obj as Gmex.API.Models.MktOrderItem;
                if (msg.At == 0)
                {
                    LOG2($"<<[K_SUBJ_ORDERL2] {msg.Sym} orderl2 begin...");
                }
                else if (msg.At == 1)
                {
                    LOG2($"<<[K_SUBJ_ORDERL2] {msg.Sym} orderl2 end");
                }
                else
                {
                    LOG2("<< [K_SUBJ_ORDERL2] " + Gmex.API.Helper.MyJsonMarshal(msg) + "\r\n");
                }
            }
            else if (obj is Gmex.API.Models.MktOrder20Result)
            {
                LOG2("<< [K_SUBJ_ORDER20] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.MktTradeItem)
            {
                LOG2("<< [K_SUBJ_TRADE] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.MktInstrumentTick)
            {
                LOG2("<< [K_SUBJ_TICK] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.MktCompositeIndexTick)
            {
                LOG2("<< [K_SUBJ_INDEX] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.MktKLineItem)
            {
                LOG2("<< [K_SUBJ_KLINE] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else
            {
                LOG2("[???]<< [" + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
        }

        private void OnTradePushMessage(object obj)
        {
            if (obj is Gmex.API.Models.Wlt)
            {
                var msg = obj as Gmex.API.Models.Wlt;
                if (checkBox_onWallet.Checked) LOG2("<< [TRD.onWallet] " + Gmex.API.Helper.MyJsonMarshal(msg) + "\r\n");
            }
            else if (obj is Gmex.API.Models.TrdRec)
            {
                if (checkBox_onTrade.Checked) LOG2("<< [TRD.onTrade] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.Ord)
            {
                /**
                 * 重要说明：
                 * 
                 * 1. 用户通常在本地维护 【所有委托】表； 主键使用 COrdId;
                 * 2. 这里收到委托的推送消息后，应该去找到本地的委托，进行内容更新替换;
                 * 3. 如有专门的历史委托表，则当委托已经无效或结束后，从【所有委托】表中移除到历史表。
                 * 
                 **/
                if (checkBox_onOrder.Checked) LOG2("<< [TRD.onOrder] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.Position)
            {
                if (checkBox_onPosition.Checked) LOG2("<< [TRD.onPosition] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else if (obj is Gmex.API.Models.WltLog)
            {
                if (checkBox_onWltLog.Checked) LOG2("<< [TRD.onWltLog] " + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
            else
            {
                LOG2("[???]<< [" + Gmex.API.Helper.MyJsonMarshal(obj) + "\r\n");
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (IsClient4MarketOK())
            {
                LOG("[WANR] 行情服务已经连接，如需重连，断开先！\r\n");
                return;
            }
            //var url = Gmex.API.GmexClient.K_SIMGO_GmexMarketServerWsUrl;
            //var url = @"ws://192.168.2.49:20080/v1/market";
            var url = comboBoxMktServer.Text.Trim();
            if (!url.StartsWith("ws"))
            {
                LOG("[WANR] 行情服务器地址必须 ws 开头.\r\n");
                return;
            }

            try
            {
                LOG(">> " + url + " ...\r\n");

                cli4mkt = new Gmex.API.WS.Client4Market();
                await cli4mkt.ReceiveLoop(url, true,
                    (obj) => { UI_ACTION(() => { OnMarketPushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnMarketPushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnMarketPushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnMarketPushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnMarketPushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnMarketPushMessage(obj); }); },
                    () => { UI_ACTION(() => { LOG2("[NOTE] market-server disconnected"); }); },
                    () => { UI_ACTION(() => { LOG2("[NOTE] market-server reconnected"); }); },
                    cts4mkt.Token);

                LOG("[+] connected.\r\n");

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                LOG("[Exception] " + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LOG("[+] close and reset.\r\n");
            cts4mkt.Cancel();
            cli4mkt?.SafeClose();

            comboBoxMktIndex.Items.Clear();
            comboBoxMktIndex.Text = "";
            comboBoxMktSym.Items.Clear();
            comboBoxMktSym.Text = "";

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;

            cts4mkt.Dispose();
            cts4mkt = new CancellationTokenSource();
        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4MarketOK())
            {
                LOG("[WARN] market client is not connected!\r\n");
                return;
            }

            await cli4mkt.REQ_TimeAsync((code, delta) =>
            {
                LOG("<< time delta(ms): " + delta + "\r\n");
            }, cts4mkt.Token);

        }

        private async void button4_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4MarketOK())
            {
                LOG("[WARN] market client is not connected!\r\n");
                return;
            }

            await cli4mkt.REQ_GetCompositeIndexAsync((code, indices) =>
            {
                indices.Sort();
                LOG("<< indices: " + String.Join(", ", indices.ToArray()) + "\r\n");
                UI_ACTION(() =>
                {
                    comboBoxMktIndex.Items.Clear();
                    comboBoxMktIndex.Items.AddRange(indices.ToArray());
                    comboBoxMktIndex.SelectedIndex = 0;
                });
            }, cts4mkt.Token);

            await cli4mkt.REQ_GetAssetDAsync((code, instruments) =>
            {

                UI_ACTION(() =>
                {
                    comboBoxMktSym.Items.Clear();

                    List<string> lines = new List<string>();

                    foreach (var inst in instruments)
                    {
                        LOG("<< AssetD: " + inst.Sym + "\r\n");
                        lines.Add(inst.Sym);
                    }

                    lines.Sort();
                    comboBoxMktSym.Items.AddRange(lines.ToArray());
                    comboBoxMktSym.SelectedIndex = 0;
                });


            }, cts4mkt.Token);
        }


        private async void checkBox1_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktIndex.Text.Trim();
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_index_xxx(sym, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_index({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox1.Checked);
        }

        private async void checkBox2_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text.Trim();
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_tick_xxx(sym, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_tick({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox2.Checked);
        }

        private async void checkBox3_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text.Trim();
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_trade_xxx(sym, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_trade({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox3.Checked);
        }

        private async void checkBox4_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text.Trim();
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_order20_xxx(sym, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_order20({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox4.Checked);
        }

        private async void checkBox5_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text;
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_orderl2_xxx(sym, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_orderl2({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox5.Checked);
        }

        private async void checkBox6_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text;
            if (sym.Length < 1) return;
            await cli4mkt.REQ_Sub_kline_xxx(sym, Gmex.API.Models.MktKLineType.TYP_1m, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_kline_1m({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox6.Checked);
        }

        private async void checkBox7_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text;
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_kline_xxx(sym, Gmex.API.Models.MktKLineType.TYP_5m, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_kline_5m({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox7.Checked);
        }

        private async void checkBox8_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text;
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_kline_xxx(sym, Gmex.API.Models.MktKLineType.TYP_1h, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_kline_1h({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox8.Checked);
        }

        private async void checkBox9_CheckedChangedAsync(object sender, EventArgs e)
        {
            var sym = comboBoxMktSym.Text;
            if (sym.Length < 1) return;
            await cli4mkt?.REQ_Sub_kline_xxx(sym, Gmex.API.Models.MktKLineType.TYP_1d, (code, msg) =>
            {

                UI_ACTION(() =>
                {
                    LOG($"<< Sub_kline_1d({sym}) result: {code} - {msg}\r\n");
                });

            }, cts4mkt.Token, !checkBox9.Checked);
        }

        private async void button5_ClickAsync(object sender, EventArgs e)
        {
            var sym = textBox5.Text.Trim();
            if (sym.Length < 1) return;

            Gmex.API.Models.MktKLineType typ;
            int beginSec, offset, count;
            try
            {
                beginSec = Convert.ToInt32(textBox2.Text.Trim());
                offset = Convert.ToInt32(textBox3.Text.Trim());
                count = Convert.ToInt32(textBox4.Text.Trim());
                typ = (Gmex.API.Models.MktKLineType)(comboBoxMktKLineTyp.SelectedIndex + 1);
            }
            catch (Exception)
            {
                return;
            }

            await cli4mkt?.REQ_GetHistKLine(sym, typ, beginSec, offset, count, (code, msg) =>
            {
                UI_ACTION(() =>
                {
                    LOG($"<< GetHistKLine({sym},{typ}) ret code={code}: \r\n {Gmex.API.Helper.MyJsonMarshal(msg)}\r\n");
                });
            },
            cts4mkt.Token);

        }

        private async void button7_ClickAsync(object sender, EventArgs e)
        {
            if (IsClient4TradeOK())
            {
                LOG("[WANR] 交易服务已经连接，如需重连，断开先！\r\n");
                return;
            }
            var url = comboBoxTrdServer.Text.Trim();
            if (!url.StartsWith("ws"))
            {
                LOG("[WANR] 交易服务器地址必须 ws 开头.\r\n");
                return;
            }

            try
            {
                LOG(">> " + url + " ...\r\n");
                cli4trd = new Gmex.API.WS.Client4Trade(textBoxUname.Text.Trim(), textBoxApiKey.Text.Trim(), textBoxApiSecret.Text.Trim());

                await cli4trd.ReceiveLoop(url, true,
                    (obj) => { UI_ACTION(() => { OnTradePushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnTradePushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnTradePushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnTradePushMessage(obj); }); },
                    (obj) => { UI_ACTION(() => { OnTradePushMessage(obj); }); },
                    () => { UI_ACTION(() => { LOG2("[NOTE] trade-server disconnected"); }); },
                    () => { UI_ACTION(() => { LOG2("[NOTE] trade-server reconnected"); }); },
                    cts4trd.Token);

                LOG("[+] connected.\r\n");

                await cli4trd.REQ_LoginAsync((code, data) =>
                {
                    LOG($"[DEBUG] login-ret {code}: {data}\r\n");
                }, CancellationToken.None);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                LOG("[Exception] " + ex);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LOG("[+] close and reset.\r\n");
            cts4trd.Cancel();
            cli4trd?.SafeClose();
            comboBoxTrdSym.Items.Clear();
            comboBoxTrdSym.Text = "";

            cts4trd.Dispose();
            cts4trd = new CancellationTokenSource();
        }

        private async void button6_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_TimeAsync((code, delta) =>
            {
                LOG("<< time delta(ms): " + delta + "\r\n");
            }, cts4trd.Token);
        }

        private async void button10_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetWalletsAsync(comboBoxTrdAccType.SelectedIndex + 1, (code, wlts) =>
            {
                LOG($"[DEBUG] GetWallets ret {code}\r\n");
                foreach (var w in wlts)
                {
                    LOG("<< Wlt: " + Helper.MyJsonMarshal(w) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button9_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetAssetDAsync((code, instruments) =>
            {
                LOG($"[DEBUG] GetAssetD ret {code}\r\n");
                UI_ACTION(() =>
                {
                    comboBoxTrdSym.Items.Clear();

                    List<string> lines = new List<string>();

                    foreach (var inst in instruments)
                    {
                        LOG("<< AssetD: " + Helper.MyJsonMarshal(inst) + "\r\n");
                        lines.Add(inst.Sym);
                    }

                    lines.Sort();
                    comboBoxTrdSym.Items.AddRange(lines.ToArray());
                    if (lines.Count > 0) comboBoxTrdSym.SelectedIndex = 0;
                });
            }, cts4trd.Token);
        }

        private async void button20_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetCcsWalletsAsync((code, wlts) =>
            {
                LOG($"[DEBUG] GetCcsWallets ret {code}\r\n");
                foreach (var w in wlts)
                {
                    LOG("<< Wlt: " + Helper.MyJsonMarshal(w) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button11_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetTradesAsync(comboBoxTrdAccType.SelectedIndex + 1, (code, msgs) =>
            {
                LOG($"[DEBUG] GetTrades ret {code}\r\n");
                foreach (var m in msgs)
                {
                    LOG("<< TrdRec: " + Helper.MyJsonMarshal(m) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button13_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetOrdersAsync(comboBoxTrdAccType.SelectedIndex + 1, (code, msgs) =>
            {
                LOG($"[DEBUG] GetOrders ret {code}\r\n");
                foreach (var m in msgs)
                {
                    LOG("<< Orders: " + Helper.MyJsonMarshal(m) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button14_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetPositionsAsync(comboBoxTrdAccType.SelectedIndex + 1, (code, msgs) =>
            {
                LOG($"[DEBUG] GetPositions ret {code}\r\n");
                foreach (var m in msgs)
                {
                    LOG("<< Positions: " + Helper.MyJsonMarshal(m) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button12_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetHistOrdersAsync(comboBoxTrdAccType.SelectedIndex + 1, (code, msgs) =>
            {
                LOG($"[DEBUG] GetHistOrders ret {code}\r\n");
                foreach (var m in msgs)
                {
                    LOG("<< Orders: " + Helper.MyJsonMarshal(m) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button15_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            await cli4trd.REQ_GetWalletsLogAsync(comboBoxTrdAccType.SelectedIndex + 1, (code, wlts) =>
            {
                LOG($"[DEBUG] GetWalletsLog ret {code}\r\n");
                foreach (var w in wlts)
                {
                    LOG("<< WltLog: " + Helper.MyJsonMarshal(w) + "\r\n");
                }
            }, cts4trd.Token);
        }

        private async void button16_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            var sym = comboBoxTrdSym.Text.Trim();
            if (sym.Length < 1) return;

            await cli4trd.REQ_GetRiskLimitAsync(comboBoxTrdAccType.SelectedIndex + 1, sym, (code, obj) =>
            {
                LOG($"[DEBUG] GetRiskLimit ret {code}\r\n");
                LOG("<< RiskLimit: " + Helper.MyJsonMarshal(obj) + "\r\n");
            }, cts4trd.Token);
        }

        private async void button19_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

            var sym = comboBoxTrdSym.Text.Trim();
            if (sym.Length < 1) return;
            int sec = -1;
            try
            {
                sec = Convert.ToInt32(textBoxTrdCancelTimeout.Text);
            }
            catch (Exception)
            { }
            if (sec < 0)
                return;


            await cli4trd.REQ_CancelAllAfterAsync(comboBoxTrdAccType.SelectedIndex + 1, sec, (code, obj) =>
            {
                LOG($"[DEBUG] CancelAllAfter ret {code}\r\n");
                LOG("<< CancelAllAfter: " + Helper.MyJsonMarshal(obj) + "\r\n");
            }, cts4trd.Token);
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
             * 3. 特别注意的是，OrderNew 的返回结果并不能保证一定比 onOrder 消息到来的快，因此，在下单前，应该先本地保存 COrdId 入表
             *    以便 onOrder 能正确识别;
             * 4. 委托的有效性，首先看 ErrorCode 是否为 0，然后再看 Status;
             * 5. 对于条件委托，一开始OType是条件类型，触发后会自动变成 Limit 或 Market 类型；如在历史单中想判断是否条件单，可借
             *    助 StopPrz 来进一步判断。
             * 6. 性能考虑，建议单独建立一个历史委托的表单独存放已经不活动的委托，因为这些委托数据是不会再变化了。
             */
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }

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
            }
            catch (Exception ex)
            {
                LOG("[ERROR]: " + ex.Message);
                return;
            }

            await cli4trd.REQ_OrderNewAsync(ord, (code, obj) =>
            {
                LOG($"[DEBUG] OrderNew ret {code}\r\n");
                LOG("<< OrderNew: " + Helper.MyJsonMarshal(obj) + "\r\n");
            }, cts4trd.Token);

        }

        private async void buttonOrderDel_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4TradeOK())
            {
                LOG("[WARN] trade client is not connected!\r\n");
                return;
            }
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

            await cli4trd.REQ_OrderDelAsync(ord, (code, obj) =>
            {
                LOG($"[DEBUG] OrderDel ret {code}\r\n");
                LOG("<< OrderDel: " + Helper.MyJsonMarshal(obj) + "\r\n");
            }, cts4trd.Token);
        }


        private void button17_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //var frm = new Form2();
            //frm.FormClosed += new FormClosedEventHandler((sender2, e2) =>
            //{
            //    this.Show();
            //});
            //frm.Show();
            var frm = new FormRestApiMkt();
            frm.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var frm = new FormRestApiTrd();
            frm.Show();
        }

        private void comboBoxTrdServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var url = comboBoxTrdServer.Text.Trim();
            try
            {
                string apikeyfile = @"user-api-key.json";
                if (File.Exists(apikeyfile))
                {
                    var config = Helper.MyJsonUnmarshal<Dictionary<string,Gmex.API.Models.UserApiKeyData>>(File.ReadAllText(apikeyfile));
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

        private async void buttonGetAssetEx_ClickAsync(object sender, EventArgs e)
        {
            if (!IsClient4MarketOK())
            {
                LOG("[WARN] market client is not connected!\r\n");
                return;
            }
            await cli4mkt.REQ_GetAssetExAsync((code, instruments) =>
            {

                UI_ACTION(() =>
                {
                    foreach (var inst in instruments)
                    {
                        LOG("<< AssetEx: " + inst.Sym + "\r\n");
                    }
                });


            }, cts4mkt.Token);
        }
    }
}
