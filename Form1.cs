using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;




namespace YOLO_GUI
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        string CurrentImagePath = "";

        public Form1()
        {
            InitializeComponent();
            textBox2.Text = @"C:\Users\asus\anaconda3\python.exe";
            textBox3.Text = @"C:\Users\asus\source\repos\YOLO_GUI\YOLO_runtime.py";
            textBox4.Text = "C:\\Users\\asus\\source\\repos\\YOLO_GUI\\results.jpg";

            form2 = new Form2();
            form2.MessageUpdated += UpdateTextBox;
            //+=符號是一個運算子多載，意思就是 把委託的實例UpdateTextBox註冊為MessageUpdated事件的處理程序
            //就是把UpdateTextBox添加為form2.MessageUpdated事件的一部份，添加的當下就等同於你也在觸發MessageUpdated，
            //觸發時，UpdateTextBox就會讓form2.MessageUpdated把收到的message assign給 textBox1.Text
        }

        private void UpdateTextBox(string message)
        {
            
            textBox1.ReadOnly = true;
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Text = message;

        }

        private Bitmap MyImage;
        public void ShowMyImage(String fileToDisplay)
        {
            // Sets up an image object to be displayed.
            if (MyImage != null)
            {
                MyImage.Dispose();
            }

            try
            {
                // Stretches the image to fit the pictureBox.
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                MyImage = new Bitmap(fileToDisplay);
                Bitmap Resize_MyImage = new Bitmap(MyImage, new Size(500, (int)Math.Round(500 * (MyImage.Height / (double)MyImage.Width))));
                pictureBox1.ClientSize = new Size(Resize_MyImage.Width, Resize_MyImage.Height);
                pictureBox1.Image = (System.Drawing.Image)Resize_MyImage;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                //throw;
                MessageBox.Show("Error: " + e.Message, "Need to choose one image !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void button1_Click(object sender, EventArgs e) //Choose file path
        {
            CurrentImagePath = textBox1.Text;
            form2.ShowDialog();
            CurrentImagePath = textBox1.Text;
            label1.Text = "No Result.";
            this.AutoSize = true;
            ShowMyImage(CurrentImagePath);

        }

        public void call_python(string args) //static void call_python(string args)
        {
                // 指定 Python 解釋器的路徑
                string pythonPath = textBox2.Text; //@"C:\Users\asus\anaconda3\python.exe";

                // 指定 Python 腳本的路徑
                
                string scriptPath = textBox3.Text;//@"C:\Users\asus\source\repos\YOLO_GUI\YOLO_runtime.py";


                // 構建 ProcessStartInfo 對象
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\" {args}",  // 傳遞參數給 Python 腳本
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                // 建立 Process 物件
                using (Process process = new Process { StartInfo = psi })
                {
                    // 開始執行 Python 腳本
                    process.Start();
                    // 讀取 py的std輸出
                    string output = process.StandardOutput.ReadToEnd();
                    // 等待 Python 腳本執行完成
                     process.WaitForExit();

                    //印出python print出來的結果
                    Console.WriteLine("Python Script Output: " + output);
                }
        }


    
        private void button2_Click(object sender, EventArgs e) // Predict buttom
        {
            //呼叫.py程式碼，執行物件偵測
            label1.Width = 120;
            label1.Text = "Now Processing ...";
                

            if (textBox1.Text!="")
            {
                call_python(textBox1.Text);
                label1.Text = "Finished.";
            }
            else
            {
                label1.Text = "Processing Failed";
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Finished.")
            {
                ShowMyImage(textBox4.Text); //存放結果的位置
            }
            else
            {
                MessageBox.Show("No Prediction.", "Need to choose one image !");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowMyImage(CurrentImagePath);
        }
    }
}
