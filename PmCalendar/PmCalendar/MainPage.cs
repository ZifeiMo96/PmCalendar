using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PmService;


namespace PmCalendar
{
    public partial class MainPage : Form
    {
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
            InitializeComponent();
            isDayButtonVisit = new bool[42];
            dayButton = new Button[42];
            tyear = DateTime.Now.Year.ToString();
            tmonth = DateTime.Now.Month.ToString();
            tday = DateTime.Now.Day.ToString();
            year = DateTime.Now.Year.ToString();
            month = DateTime.Now.Month.ToString();
            day = DateTime.Now.Day.ToString();
            LoadButton();
            dayButton[1].Text = "12";
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

    }
}
