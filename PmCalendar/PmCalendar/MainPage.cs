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
using PmService;


namespace PmCalendar
{
    public partial class MainPage : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private AnalysisPage sonPage;

        private Service service;

        public bool[] isDayButtonVisit;

        public Button[] dayButton;

        public string tyear;

        public string tmonth;

        public string tday;

        public string year;

        public string month;

        public string day;

        public MainPage()
        {
            String path = "train.csv";
            InitializeComponent();
            isDayButtonVisit = new bool[42];
            dayButton = new Button[42];
            tyear = DateTime.Now.Year.ToString();
            tmonth = DateTime.Now.Month.ToString();
            tday = DateTime.Now.Day.ToString();
            year = DateTime.Now.Year.ToString();
            month = DateTime.Now.Month.ToString();
            day = DateTime.Now.Day.ToString();
            service = new Service(path);
            LoadButton();
            initYearComboBox();
            initMonthComboBox();
            GenerateCalendar();
            AddButtonEvent();
            tagToday();
            SetLevelMessage();
            

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); 
        }

        private void LoadButton()
        {

            for (int i = 0; i < 42; i++)
            {
                string name = "dayButton" + (i + 1);
                Button btn = (Button)this.tableLayoutPanel2.Controls.Find(name, false)[0];
                dayButton[i] = btn;
            }
        }

        private void initYearComboBox()
        {
            yearComboBox.Text = year;
            int additem = Int32.Parse(year) - 100;
            for (int i = 0; i < 201; i++)
            {
                yearComboBox.Items.Add(additem);
                additem++;
            }
        }

        private void initMonthComboBox()
        {
            monthComboBox.Text = month;
            for (int i = 0; i < 12; i++)
            {
                monthComboBox.Items.Add(i + 1);
            }
        }

        private Button GetButton(int n)
        {
            return dayButton[n-1];
        }

        private void setYear(int year)
        {
            this.year = year.ToString();
            yearComboBox.Text = this.year;
        }

        private void setMonth(int month)
        {
            this.month = month.ToString();
            monthComboBox.Text = this.month;
        }

        private int daysInMonth()
        {
            int days = 0;
            int year = Int32.Parse(this.year);
            int month = Int32.Parse(this.month);
            switch (month)
            {
                case 1: case 3: case 5: case 7: case 8: case 10: case 12: days = 31; break;
                case 4: case 6: case 9: case 11: days = 30; break;
                case 2:
                    if ((year % 100 != 0 && year % 4 == 0) || (year % 400 == 0)) days = 29;
                    else days = 28;
                    break;
                default:
                    days = 0; break;
            }
            return days;
        }

        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            month = monthComboBox.Text;
            GenerateCalendar();
        }

        private void yearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = yearComboBox.Text;
            GenerateCalendar();
        }

        private void GenerateCalendar()
        {
            Button btn;
            int year = Int32.Parse(this.year);
            int month = Int32.Parse(this.month);
            int dayNumber = daysInMonth();
            int dayOrder = ParseDateToWeek(year, month, 1);
            for (int i = 1; i < dayOrder; i++)
            {
                btn = GetButton(i);
                btn.Text = "";
                btn.Visible = false;
            }
            for (int i = 1; i <= dayNumber; i++)
            {
                btn = GetButton(dayOrder + i - 1);
                btn.Text = i.ToString();
                btn.Visible = true;
            }
            for (int i = dayOrder + dayNumber; i <= 42; i++)
            {
                btn = GetButton(i);
                btn.Text = "";
                btn.Visible = false;
            }
        }

        public int ParseDateToWeek(int y, int m, int d)
        {
            int day;
            day = (d + 1 + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
            if (day == 0) day = 7;
            return day;
        }

        private void AddButtonEvent()
        {
            for (int i = 0; i < 42; i++)
            {
                GetButton(i + 1).Click += new EventHandler(SetDate);
            }
        }


        private void SetDate(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            year = yearComboBox.Text;
            month = monthComboBox.Text;
            day = btn.Text;
            tagChoice(btn);
            SetLevelMessage();
        }


        private void tagToday()
        {
            Button btn;
            for (int i = 1; i <= 42; i++)
            {
                btn = GetButton(i);
                btn.BackColor = Color.FromArgb(255, 244, 244);
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.BorderColor = Color.White;
            }
            if (this.year == tyear && this.month == tmonth)
            {
                int year = Int32.Parse(this.year);
                int month = Int32.Parse(this.month);
                int dayOrder = ParseDateToWeek(year, month, 1) + Int32.Parse(tday) - 1;
                btn = GetButton(dayOrder);
                btn.BackColor = Color.LightBlue;
            }
        }

        private void tagChoice(Button btn)
        {
            for (int i = 1; i <= 42; i++)
            {
                Button b = GetButton(i);
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.BorderColor = Color.White;
            }
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.DeepSkyBlue;
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LastMonthbutton_Click(object sender, EventArgs e)
        {
            int month = Int32.Parse(this.month);
            int year = Int32.Parse(this.year);
            if (month - 1 <= 0)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }
            setYear(year);
            setMonth(month);
            GenerateCalendar();
            tagToday();
        }

        private void SetLevelMessage()
        {
            DateTime date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            PmData data = new PmData();
            data.Pdate = date;
            switch(service.GetPmLevelByDate(data)){
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

        private void NextMonthbutton_Click(object sender, EventArgs e)
        {
            int month = Int32.Parse(this.month);
            int year = Int32.Parse(this.year);
            if (month + 1 > 12)
            {
                month = 1;
                year++;
            }
            else
            {
                month++;
            }
            setYear(year);
            setMonth(month);
            GenerateCalendar();
            tagToday();
        }

        private void MinButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void yearComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            year = yearComboBox.Text;
            GenerateCalendar();
        }

        private void monthComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            month = monthComboBox.Text;
            GenerateCalendar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sonPage = new AnalysisPage(service);
            sonPage.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "请选择训练源文件";
                //记录选中的目录  
                String defaultPath = openFileDialog.FileName;
                service.LoadPmData(defaultPath);
                service.CalculateLevelFrequency();
                service.CalculateItemFrequency();
            }
        }
    }
}
