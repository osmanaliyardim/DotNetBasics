using HelloWorldStandard;

namespace HelloWorldWF
{
    public partial class Form1 : Form
    {
        SalutingStandardClass salute = new SalutingStandardClass();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == string.Empty) 
            {
                label1.Text = "Name cannot be null!";
            }
            else lblNameOutput.Text = salute.SaluteMe(textBox1.Text);
        }
    }
}