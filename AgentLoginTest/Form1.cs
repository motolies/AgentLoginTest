using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AgentLoginTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("empNo", this.txtEmpNo.Text.Trim());
                param.Add("timeZoneOffset", "540");
                WebApiUtil.HttpPost(this.txtURL.Text.Trim(), param, out result);
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                this.txtResult.AppendText(result);
                this.txtResult.AppendText(Environment.NewLine);
            }

        }
    }
}
