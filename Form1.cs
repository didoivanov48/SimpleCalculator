using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Calculator : Form
    {
        double result = 0;
        String operation = "";
        bool performedOperation = false;
        public Calculator()
        {
            InitializeComponent();
        }

        private void btn_clicked(object sender, EventArgs e)
        {
            if((txtResult.Text=="0") || (performedOperation))
            {
                txtResult.Clear();
            }
            performedOperation = false;
            Button button = (Button)sender;
            if (button.Text==".")
            {
                if(!txtResult.Text.Contains("."))
                    txtResult.Text = txtResult.Text + button.Text;
            }else
            txtResult.Text = txtResult.Text + button.Text;            
        }

        private void operation_clicked(object sender, EventArgs e)
        {
            performedOperation = true;
            Button button = (Button)sender;
            string newOp = button.Text;
            lblCurrentOp.Text = lblCurrentOp.Text + " " + txtResult.Text + " " + newOp;
            switch (operation)
            {
                case "+":
                    txtResult.Text = (result + double.Parse(txtResult.Text)).ToString();
                    break;
                case "-":
                    txtResult.Text = (result - double.Parse(txtResult.Text)).ToString();
                    break;
                case "*":
                    txtResult.Text = (result * double.Parse(txtResult.Text)).ToString();
                    break;
                case "÷":
                    if (txtResult.Text == "0")
                    {
                        txtResult.Text = "Error";
                    }else
                    txtResult.Text = (result / double.Parse(txtResult.Text)).ToString();
                    break;
                default:
                    break;
            }
            operation = newOp;
            result = double.Parse(txtResult.Text);

        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            if(lblCurrentOp.ToString().Contains("="))
            {
                lblCurrentOp.Text = "";
                txtResult.Text = "0";
            }else
            txtResult.Text = "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            result = 0;
            lblCurrentOp.Text = "";
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string newOp = button.Text;
            lblCurrentOp.Text = lblCurrentOp.Text + " " + txtResult.Text + " " + newOp;
            switch (operation)
            {
                case "+":
                    txtResult.Text = (result + double.Parse(txtResult.Text)).ToString();
                    break;
                case "-":
                    txtResult.Text = (result - double.Parse(txtResult.Text)).ToString();
                    break;
                case "*":
                    txtResult.Text = (result * double.Parse(txtResult.Text)).ToString();
                    break;
                case "÷":
                    if (txtResult.Text == "0")
                    {
                        txtResult.Text = "Error";
                    }
                    else
                        txtResult.Text = (result / double.Parse(txtResult.Text)).ToString();
                    break;
                default:
                    break;
            }
        }
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            operation = "√";
            result = double.Parse(txtResult.Text);
            txtResult.Text = (Math.Sqrt(result)).ToString();
            performedOperation = true;
        }

        private void btnMPlus_Click(object sender, EventArgs e)
        {
            result += double.Parse(txtResult.Text);
            btnMR.Enabled = true;
            btnMC.Enabled = true;
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            result -= double.Parse(txtResult.Text);
            btnMR.Enabled = true;
            btnMC.Enabled = true;
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            txtResult.Text = result.ToString();
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            result = 0;
            btnMR.Enabled = false;
            btnMC.Enabled = false;
        }
    }
}
