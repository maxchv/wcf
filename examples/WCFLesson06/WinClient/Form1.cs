using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinClient.ServiceReference1;

namespace WinClient
{
    //[CallbackBehavior(UseSynchronizationContext = false)]
    public partial class Form1 : Form, ICallbackServiceCallback
    {
        CallbackServiceClient client;
        public Form1()
        {
            InitializeComponent();
            client = new CallbackServiceClient(new InstanceContext(this));
        }

        public void Notify(int percent)
        {
            progressBar1.Value = percent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.Start();
        }
    }
}
