using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnFetch_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("请输入有效的URL");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    string html = await client.GetStringAsync(url);

                    // 提取手机号码
                    var phones = ExtractPhones(html);
                    txtPhones.Text = string.Join(Environment.NewLine, phones);

                    // 提取邮箱
                    var emails = ExtractEmails(html);
                    txtEmails.Text = string.Join(Environment.NewLine, emails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"错误：{ex.Message}");
            }
        }

        private List<string> ExtractPhones(string input)
        {
            List<string> result = new List<string>();
            // 匹配11位手机号码（中国大陆）
            string pattern = @"(?:^|\D)(1[3-9]\d{9})(?:$|\D)";

            foreach (Match match in Regex.Matches(input, pattern))
            {
                if (match.Groups[1].Success)
                {
                    string phone = match.Groups[1].Value;
                    if (!result.Contains(phone))
                        result.Add(phone);
                }
            }
            return result;
        }

        private List<string> ExtractEmails(string input)
        {
            List<string> result = new List<string>();
            // 邮箱匹配正则
            string pattern = @"[\w\.%-]+@[\w\.-]+\.[A-Za-z]{2,4}";

            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                string email = match.Value;
                if (!result.Contains(email))
                    result.Add(email);
            }
            return result;
        }
    }
}
