using Gmex.API;
using Gmex.API.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo1
{
    public partial class FormRestApiMkt : Form
    {
        public FormRestApiMkt()
        {
            InitializeComponent();

            comboBoxMktServer.SelectedIndex = 0;
        }

        private void FormRestApiMkt_Load(object sender, EventArgs e)
        {

        }

        private void FormRestApiMkt_FormClosed(object sender, FormClosedEventArgs e)
        {

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

        private async void buttonGetTime_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var delta = await client.GetTimeAsync();
                LOG($"[Time]<< time delta(ms): {delta}\r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetServerInfo_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetServerInfoAsync();
                LOG($"[ServerInfo] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetAssetD_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;

            try
            {
                comboBoxMktSym.Items.Clear();

                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetAssetDAsync();

                LOG($"[GetAssetD] <<");
                foreach (var item in ret)
                {
                    comboBoxMktSym.Items.Add(item.Sym);
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");

                if (comboBoxMktSym.Items.Count > 0)
                {
                    comboBoxMktSym.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }


        private async void buttonGetCompositeIndex_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;

            try
            {
                comboBoxMktIndex.Items.Clear();

                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetCompositeIndexAsync();

                LOG($"[GetCompositeIndex] <<");
                foreach (var item in ret)
                {
                    comboBoxMktIndex.Items.Add(item.Sym);
                    LOG($"{Helper.MyJsonMarshal(item)}\r\n");
                }
                LOG($"========\r\n");

                if (comboBoxMktIndex.Items.Count > 0)
                {
                    comboBoxMktIndex.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetTick_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            if (comboBoxMktSym.Text.Length < 1) return;

            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetTickAsync(comboBoxMktSym.Text);
                LOG($"[GetTick] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonIndexTick_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            if (comboBoxMktIndex.Text.Length < 1) return;

            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetIndexTickAsync(comboBoxMktIndex.Text);
                LOG($"[GetIndexTick] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetTrdRec_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            if (comboBoxMktSym.Text.Length < 1) return;

            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetTradesAsync(comboBoxMktSym.Text);
                LOG($"[GetTrades] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetOrd20_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            if (comboBoxMktSym.Text.Length < 1) return;

            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetOrd20Async(comboBoxMktSym.Text);
                LOG($"[GetOrd20] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetKLineSym_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            if (comboBoxMktSym.Text.Length < 1) return;

            Gmex.API.Models.MktKLineType typ;
            int beginSec, offset, count;
            try
            {
                beginSec = Convert.ToInt32(textBoxBeginSecSym.Text.Trim());
                offset = Convert.ToInt32(textBoxOffsetSym.Text.Trim());
                count = Convert.ToInt32(textBoxCountSym.Text.Trim());
                typ = (Gmex.API.Models.MktKLineType)(comboBoxMktKLineTypSym.SelectedIndex + 1);
            }
            catch (Exception)
            {
                return;
            }
            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetHistKLineAsync(comboBoxMktSym.Text, typ, beginSec, offset, count);
                LOG($"[GetHistKLine] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetKLineIdx_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;
            if (comboBoxMktIndex.Text.Length < 1) return;

            Gmex.API.Models.MktKLineType typ;
            int beginSec, offset, count;
            try
            {
                beginSec = Convert.ToInt32(textBoxBeginSecIdx.Text.Trim());
                offset = Convert.ToInt32(textBoxOffsetIdx.Text.Trim());
                count = Convert.ToInt32(textBoxCountIdx.Text.Trim());
                typ = (Gmex.API.Models.MktKLineType)(comboBoxMktKLineTypIdx.SelectedIndex + 1);
            }
            catch (Exception)
            {
                return;
            }

            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetHistKLineAsync(comboBoxMktIndex.Text, typ, beginSec, offset, count);
                LOG($"[GetHistKLine] << {Helper.MyJsonMarshal(ret)} \r\n");
            }
            catch (Exception ex)
            {
                LOG("[ERROR] Exception: " + ex.Message + "\r\n");
            }
        }

        private async void buttonGetAssetEx_ClickAsync(object sender, EventArgs e)
        {
            if (comboBoxMktServer.Text.Length < 4) return;

            try
            {
                var client = new RESTClient4Market(comboBoxMktServer.Text);
                var ret = await client.GetAssetExAsync();

                LOG($"[GetAssetEx] <<");
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
    }
}
