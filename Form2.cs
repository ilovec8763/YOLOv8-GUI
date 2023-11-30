using System;
using System.Windows.Forms;

namespace YOLO_GUI
{
    public partial class Form2 : Form
    {

        // 定義一個委託
        public delegate void MessageUpdateHandler(string message);

        // 定義一個事件
        public event MessageUpdateHandler MessageUpdated;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageUpdated?.Invoke(textBox1.Text);
            //確定是否為null，不是的話，用Invoke進行委派，然後把textBox1.Text作為參數傳到MessageUpdated物件
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "請選擇檔案";
            dialog.Filter = "所有檔案|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filepath = dialog.FileName; // 取得選中的檔案路徑

                //string filename = Path.GetFileName(filepath); // 使用 Path.GetFileName 獲取包含副檔名的檔案名稱


                // 更新 TextBox 顯示檔案路徑
                textBox1.Text = filepath; 

            }
            MessageUpdated?.Invoke(textBox1.Text); // 執行事件，將檔案路徑傳遞出去

        }

    }
}
