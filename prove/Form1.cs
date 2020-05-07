using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using System.Windows.Forms;
using lbControlWebPages;

namespace prove
{
    public partial class Form1 : Form
    {
        static Registry Scheduler = new Registry();
        public Form1()
        {
            InitializeComponent();
            _ =  InteractiveDB.AddUpdateSiteAsync(@"https://acpol2.army.mil/vacancy/vacancy_list.asp", 12);

        }

    }
}
