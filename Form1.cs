using System;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Calculator : Form
    {
        double result = 0;  // Stores the result of the operation
        double currentOperand = 0;  // Stores the current operand
        double lastOperand = 0;  // Stores the last operand for repeated '=' presses
        double memory = 0;  // Stores the memory value
        string operation = "";  // The current operation (+, -, *, ÷)
        string lastOperation = "";  // Stores the last operation for repeated '=' presses
        bool equalsPressed = false;  // Flag to determine if '=' was pressed
        bool operationPressed = false; // Flag to track operator presses

        public Calculator()
        {
            InitializeComponent();
        }

        // Handles number and decimal point button clicks
        private void btn_clicked(object sender, EventArgs e)
        {
            if (txtResult.Text == "0" || equalsPressed || operationPressed)
            {
                txtResult.Clear();  // Clear the display for new input
                operationPressed = false; // Reset operator press flag
            }

            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!txtResult.Text.Contains("."))
                {
                    txtResult.Text += button.Text;  // Add the decimal point
                }
            }
            else
            {
                txtResult.Text += button.Text;  // Add the clicked number to the result
            }

            equalsPressed = false;  // Reset the equals flag
        }

        // Handles operator button clicks (+, -, *, ÷)
        private void operation_clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string newOperation = button.Text;

            if (operationPressed)
            {
                operation = newOperation; // Update the operation without calculating
                lblCurrentOp.Text = $"{result} {operation}";
                return;
            }

            if (!equalsPressed)
            {
                if (!string.IsNullOrEmpty(operation)) // If there's an ongoing operation
                {
                    currentOperand = double.Parse(txtResult.Text); // Store the current operand
                    PerformCalculation(); // Perform the operation immediately
                }
                else
                {
                    result = double.Parse(txtResult.Text); // Store the current input as the result
                }
            }

            operation = newOperation; // Set the new operation
            lblCurrentOp.Text = $"{result} {operation}"; // Update the label
            txtResult.Text = result.ToString(); // Display the result so far
            operationPressed = true; // Mark that an operator was pressed
            equalsPressed = false; // Reset equals flag
        }

        // Handles the equals button click
        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (!equalsPressed)
            {
                currentOperand = double.Parse(txtResult.Text); // Store the current operand
                lastOperand = currentOperand; // Store the last operand for repeated '=' presses
                lastOperation = operation; // Store the last operation for repeated '=' presses
                lblCurrentOp.Text = $"{result} {operation} {currentOperand} ="; // Update label for full equation
            }
            else
            {
                currentOperand = lastOperand; // Use the last operand for repeated '=' presses
                operation = lastOperation; // Use the last operation for repeated '=' presses
                lblCurrentOp.Text = $"{result} {operation} {currentOperand} ="; // Update label for full equation
            }

            PerformCalculation(); // Perform the operation
            txtResult.Text = result.ToString(); // Display the result

            equalsPressed = true; // Mark that '=' was pressed
        }

        // Centralized calculation logic
        private void PerformCalculation()
        {
            switch (operation)
            {
                case "+":
                    result += currentOperand;
                    break;
                case "-":
                    result -= currentOperand;
                    break;
                case "*":
                    result *= currentOperand;
                    break;
                case "÷":
                    if (currentOperand == 0)
                    {
                        txtResult.Text = "Error";
                        lblCurrentOp.Text = "";
                        ResetCalculator();
                        return;
                    }
                    result /= currentOperand;
                    break;
            }

            currentOperand = 0; // Reset the current operand for subsequent calculations
        }

        // Clear the entry (CE)
        private void btnCE_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0"; // Clear only the result
        }

        // Clear everything (C)
        private void btnC_Click(object sender, EventArgs e)
        {
            ResetCalculator(); // Reset everything
        }

        // Reset the calculator to the initial state
        private void ResetCalculator()
        {
            txtResult.Text = "0";
            result = 0;
            currentOperand = 0;
            lastOperand = 0;
            operation = "";
            lastOperation = "";
            lblCurrentOp.Text = "";
            equalsPressed = false;
            operationPressed = false;
        }

        // Handle square root operation (√)
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            double value = double.Parse(txtResult.Text);
            if (value < 0)
            {
                txtResult.Text = "Error"; // Square root of negative number
                lblCurrentOp.Text = "";
            }
            else
            {
                result = Math.Sqrt(value);
                txtResult.Text = result.ToString();
                lblCurrentOp.Text = $"√({value})";
            }
        }

        // Handle memory operations (M+ / M- / MR / MC)
        private void btnMPlus_Click(object sender, EventArgs e)
        {
            memory += double.Parse(txtResult.Text);
            btnMR.Enabled = true;
            btnMC.Enabled = true;
            result = 0;
            txtResult.Text = result.ToString();
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            memory -= double.Parse(txtResult.Text);
            btnMR.Enabled = true;
            btnMC.Enabled = true;
            result = 0;
            txtResult.Text = result.ToString();
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            txtResult.Text = memory.ToString();
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memory = 0;
            btnMR.Enabled = false;
            btnMC.Enabled = false;
            result = 0;
            txtResult.Text = result.ToString();
        }
    }
}
