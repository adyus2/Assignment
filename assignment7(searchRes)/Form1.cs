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

namespace assignment7_searchRes_
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入搜索关键词");
                return;
            }

            txtBaiduResult.Clear();
            txtBingResult.Clear();

            try
            {
                Task baiduTask = SearchAndExtract("baidu", keyword, txtBaiduResult);
                Task bingTask = SearchAndExtract("bing", keyword, txtBingResult);

                await Task.WhenAll(baiduTask, bingTask);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}");
            }
        }

        private async Task SearchAndExtract(string engine, string keyword, TextBox resultBox)
        {
            try
            {
                string url;
                switch (engine.ToLower())
                {
                    case "baidu":
                        url = "https://www.baidu.com/s?wd=" + Uri.EscapeDataString(keyword);
                        break;
                    case "bing":
                        url = "https://www.bing.com/search?q=" + Uri.EscapeDataString(keyword);
                        break;
                    default:
                        throw new ArgumentException("不支持的搜索引擎");
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                    string html = await httpClient.GetStringAsync(url);
                    string textContent = ExtractTextContent(html);

                    this.Invoke(new Action(() =>
                    {
                        resultBox.Text = textContent.Length > 200
                            ? textContent.Substring(0, 200) + "..."
                            : textContent;
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    resultBox.Text = $"获取{engine}结果失败: {ex.Message}";
                }));
            }
        }

        private string ExtractTextContent(string html)
        {
            // 简单去除HTML标签
            string text = Regex.Replace(html, "<[^>]+>", " ");
            // 合并多个空白字符
            text = Regex.Replace(text, @"\s+", " ");
            return text.Trim();
        }


    }
}
