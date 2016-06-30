﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ServiceReference1.ServiceCalculatorClient client = new ServiceReference1.ServiceCalculatorClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                textBox3.Text = client.Div(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)).ToString();
            }
            catch (FaultException<ServiceReference1.DevideByZeroExeption> ex)
            {
                MessageBox.Show(ex.Detail.Error + "\n" + ex.Detail.Details);
            }
        }
    }
}
