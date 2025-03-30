using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment5_calculator
{
    public partial class Form1: Form
    {
        private double _firstOperand = 0;
        private string _currentInput = "";
        private char _currentOperator = '\0';

        public Form1()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            _currentInput += btn.Text;
            UpdateDisplay();
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                _firstOperand = double.Parse(_currentInput);
                _currentOperator = ((Button)sender).Text[0];
                _currentInput = "";
                UpdateDisplay();
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (_firstOperand == 0 || string.IsNullOrEmpty(_currentInput)) return;

            var secondOperand = double.Parse(_currentInput);
            double result = 0;

            switch (_currentOperator)
            {
                case '+':
                    result = _firstOperand + secondOperand;
                    break;
                case '-':
                    result = _firstOperand - secondOperand;
                    break;
                case '*':
                    result = _firstOperand * secondOperand;
                    break;
                case '/':
                    if (secondOperand != 0)
                        result = _firstOperand / secondOperand;
                    else
                        MessageBox.Show("Cannot divide by zero!");
                    break;
            }

            txtDisplay.Text = $"{_firstOperand}{_currentOperator}{secondOperand}={result}";
            _currentInput = result.ToString();
            _firstOperand = 0;
            _currentOperator = '\0';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _currentInput = "";
            _firstOperand = 0;
            _currentOperator = '\0';
            txtDisplay.Text = "";
        }

        private void UpdateDisplay()
        {
            txtDisplay.Text = _currentOperator == '\0'
                ? _currentInput
                : $"{_firstOperand}{_currentOperator}{_currentInput}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
