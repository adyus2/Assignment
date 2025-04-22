using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.IO;


namespace assignment8
{
    public partial class Form1 : Form
    {
        private SQLiteConnection conn;
        private SQLiteDataReader reader;
        private string currentEnglish;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase(); // 自动创建数据库
            LoadWords();
            ShowNextWord();
        }

        // 自动初始化数据库和表
        private void InitializeDatabase()
        {
            string dbPath = Application.StartupPath + "\\words.db";
            bool isNewDatabase = !File.Exists(dbPath);

            // 连接数据库（如果不存在会自动创建）
            conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            // 如果是新数据库或表不存在，则创建表
            if (isNewDatabase || !TableExists("Words"))
            {
                string sql = @"
            CREATE TABLE Words (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EnglishWord TEXT NOT NULL,
                ChineseMeaning TEXT NOT NULL
            );
            INSERT INTO Words (EnglishWord, ChineseMeaning) VALUES 
                ('apple', '苹果'),
                ('banana', '香蕉'),
                ('computer', '电脑'),
                ('hello', '你好');
        ";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // 检查表是否存在
        private bool TableExists(string tableName)
        {
            string sql = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            return result != null && result.ToString() == tableName;
        }

        private void LoadWords()
        {
            string dbPath = Application.StartupPath + "\\words.db";
            conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Words", conn);
            reader = cmd.ExecuteReader();
        }

        private void ShowNextWord()
        {
            if (reader.Read())
            {
                lblChinese.Text = "中文词义：" + reader["ChineseMeaning"].ToString();
                currentEnglish = reader["EnglishWord"].ToString();
                txtEnglish.Clear(); // 清空输入框，但保留结果显示
            }
            else
            {
                MessageBox.Show("所有单词已学完！");
                conn.Close();
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            // 检查用户输入
            string userInput = txtEnglish.Text.Trim().ToLower();
            if (userInput == currentEnglish.ToLower())
            {
                lblResult.Text = "正确！";
                lblResult.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblResult.Text = $"错误！正确单词是：{currentEnglish}";
                lblResult.ForeColor = System.Drawing.Color.Red;
            }

            timerNextWord.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerNextWord_Tick(object sender, EventArgs e)
        {
            timerNextWord.Stop(); 
            ShowNextWord();      
            lblResult.Text = "";
        }

    }
}
