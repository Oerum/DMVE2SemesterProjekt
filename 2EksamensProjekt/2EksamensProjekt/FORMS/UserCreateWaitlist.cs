﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace _2EksamensProjekt.FORMS.secretary
{
    public partial class UserCreateWaitlist : Form
    {        
        API api = API.Getinstance();

        private static UserCreateWaitlist singleton = new UserCreateWaitlist(); 
        private UserCreateWaitlist()
        {
            InitializeComponent();
            comboBox1.Text = "normal";
            Task t1 = new Task(() => worker());
            t1.Start();
        }

        public static UserCreateWaitlist GetInstance()
        {
            return singleton;
        }

        private void worker()
        {
            do
            {
                api.ComboBoxReader(comboBox2, "CreateAccountUsername");
                api.ComboBoxReader(comboBox3, "Password");
                api.ComboBoxReader(comboBox1, "WaitlistType");
            }
            while (true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            api.CreateUser_Waitlist();
            this.Hide();
        }
    }
}
