using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 窗体加载时初始化
        }

        private void InitializeControls()
        {
            cmbOperator.Items.Clear();
            cmbOperator.Items.AddRange(new object[] { "+", "-", "×", "÷" });
            cmbOperator.SelectedIndex = 0;
            lblResult.Text = "等待计算...";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            double num1 = double.Parse(txtNum1.Text);
            double num2 = double.Parse(txtNum2.Text);
            string op = cmbOperator.SelectedItem.ToString();

            try
            {
                double result = Calculate(num1, num2, op);
                lblResult.Text = $"{num1} {op} {num2} = {result:F2}";
            }
            catch (Exception ex)
            {
                lblResult.Text = "计算错误";
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            if (!double.TryParse(txtNum1.Text, out _))
            {
                MessageBox.Show("第一个数字无效", "输入错误");
                isValid = false;
            }
            if (!double.TryParse(txtNum2.Text, out _))
            {
                MessageBox.Show("第二个数字无效", "输入错误");
                isValid = false;
            }
            return isValid;
        }

        private double Calculate(double num1, double num2, string op)
        {
            switch (op)
            {
                case "+": return num1 + num2;
                case "-": return num1 - num2;
                case "×": return num1 * num2;
                case "÷":
                    if (Math.Abs(num2) < 1e-10)
                        throw new DivideByZeroException("除数不能为零");
                    return num1 / num2;
                default: throw new ArgumentException("未知运算符");
            }
        }
    }
}
