using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment4_FileManager_
{
    public partial class Form1 : Form
    {
        private string filePath1 = "";
        private string filePath2 = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectFile1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath1 = openFileDialog.FileName;
                lblFile1.Text = filePath1;
            }
        }

        private void btnSelectFile2_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath2 = openFileDialog.FileName;
                lblFile2.Text = openFileDialog.FileName;
            }
        }

        private void btnMerge_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath1) || string.IsNullOrEmpty(filePath2))
            {
                MessageBox.Show("请先选择两个文件！");
                return;
            }

            try
            {
                // 读取文件内容
                string content1 = File.ReadAllText(filePath1);
                string content2 = File.ReadAllText(filePath2);

                // 创建目标目录
                string dataDir = Path.Combine(Application.StartupPath, "Data");
                Directory.CreateDirectory(dataDir);

                // 生成新文件名
                string newFilePath = Path.Combine(dataDir, $"merged_{DateTime.Now:yyyyMMddHHmmss}.txt");

                // 合并写入文件
                File.WriteAllText(newFilePath, content1 + Environment.NewLine + content2);

                MessageBox.Show($"文件合并成功！\n保存路径：{newFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败：{ex.Message}");
            }
        }
    }
}
