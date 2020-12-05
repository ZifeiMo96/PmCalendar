using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PmCalendar
{
    public partial class AnalysisPage : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private bool ifTime;
        private bool ifTemp;
        private bool ifCbwd;
        private bool ifPres;
        private bool ifLws;
        private bool ifDewp;
        private PmService.Service service;

        public AnalysisPage(PmService.Service service)
        {
            InitializeComponent();
            SetComboBox();
            ifTime = false;
            ifTemp = false;
            ifCbwd = false;
            ifPres = false;
            ifLws = false;
            ifDewp = false;
            this.service = service;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResult();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResult();
        }

        private void TimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ifTime = TimeCheckBox.Checked;
            TimeComboBox.Visible = ifTime;
            TimeComboBox.SelectedIndex = 0;
            GetResult();
        }

        private void TempCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ifTemp = TempCheckBox.Checked;
            TempComboBox.Visible = ifTemp;
            TempComboBox.SelectedIndex = 30;
            GetResult();
        }

        private void CbwdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ifCbwd = CbwdCheckBox.Checked;
            CbwdComboBox.Visible = ifCbwd;
            CbwdComboBox.SelectedIndex = 0;
            GetResult();
        }

        private void PresCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ifPres = PresCheckBox.Checked;
            PresComboBox.Visible = ifPres;
            PresComboBox.SelectedIndex = 25;
            GetResult();
        }

        private void LwsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ifLws = LwsCheckBox.Checked;
            LwsComboBox.Visible = ifLws;
            LwsComboBox.SelectedIndex = 25;
            GetResult();
        }

        private void DewpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ifDewp = DewpCheckBox.Checked;
            DewpComboBox.Visible = ifDewp;
            DewpComboBox.SelectedIndex = 30;
            GetResult();
        }

        private void SetComboBox()
        {
            for(int i = 0; i <= 300; i++)
            {
                LwsComboBox.Items.Add(i);
            }
            for(int i = 1000; i < 1050; i++)
            {
                PresComboBox.Items.Add(i);
            }
            for(int i = -30; i <= 45; i++)
            {
                TempComboBox.Items.Add(i);
                DewpComboBox.Items.Add(i);
            }
        }

        private void GetResult()
        {
            if (!(ifCbwd || ifDewp || ifLws || ifPres || ifTime || ifTemp))
            {
                resultLabel.Text = "无";
                resultLabel.ForeColor = Color.Black;
                return;
            }
            else
            {
                PmService.PmData data = new PmService.PmData();
                if (ifCbwd)
                {
                    data.Cbwd = CbwdComboBox.SelectedIndex+1;
                }
                else
                {
                    data.Cbwd = init;
                }
                if (ifDewp)
                {
                    data.Dewp = CbwdComboBox.SelectedIndex - 30;
                }
                else
                {
                    data.Dewp = init;
                }
                if (ifLws)
                {
                    data.Lws = LwsComboBox.SelectedIndex;
                }
                else
                {
                    data.Lws = init;
                }
                if (ifPres)
                {
                    data.Pres = PresComboBox.SelectedIndex + 1000;
                }
                else
                {
                    data.Pres = init;
                }
                if (ifTime)
                {
                    data.Hour = TimeComboBox.SelectedIndex;
                }
                else
                {
                    data.Hour = init;
                }
                if (ifTemp)
                {
                    data.Temp = TempComboBox.SelectedIndex - 30;
                }
                else
                {
                    data.Temp = init;
                }
                switch (service.GetPmLevel(data))
                {
                    case 0:
                        resultLabel.Text = "优秀";
                        resultLabel.ForeColor = Color.Green;
                        break;
                    case 1:
                        resultLabel.Text = "良好";
                        resultLabel.ForeColor = Color.LightGreen;
                        break;
                    case 2:
                        resultLabel.Text = "轻度污染";
                        resultLabel.ForeColor = Color.Black;
                        break;
                    case 3:
                        resultLabel.Text = "中度污染";
                        resultLabel.ForeColor = Color.MediumVioletRed;
                        break;
                    case 4:
                        resultLabel.Text = "重度污染";
                        resultLabel.ForeColor = Color.Red;
                        break;
                    case 5:
                        resultLabel.Text = "严重污染";
                        resultLabel.ForeColor = Color.DarkRed;
                        break;
                };
            }
        }

        private void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResult();
        }

        private void TempComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResult();
        }

        private void CbwdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResult();
        }

        private void DewpComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResult();
        }

        const int init = -114514;
    }
}
