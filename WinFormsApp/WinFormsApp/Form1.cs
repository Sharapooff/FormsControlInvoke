namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(string text);
        private Button button1;
        private TextBox textBox1;
        private Thread thread2 = null;

        public Form1()
        {
            InitializeComponent();
            button1 = new Button
            {
                Location = new Point(15, 55),
                Size = new Size(240, 20),
                Text = "Set text safely"
            };
            button1.Click += new EventHandler(button1_Click);
            textBox1 = new TextBox
            {
                Location = new Point(15, 15),
                Size = new Size(240, 20)
            };
            Controls.Add(button1);
            Controls.Add(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread2 = new Thread(new ThreadStart(SetText));
            thread2.Start();
            Thread.Sleep(1000);
        }

        private void WriteTextSafe(string text)
        {
            if (textBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                textBox1.Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.Text = text;
            }
        }
        private void SetText()
        {
            WriteTextSafe("This text was set safely.");
        }

        
        
    }
}