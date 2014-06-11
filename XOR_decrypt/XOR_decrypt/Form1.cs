using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR_decrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XORdecryptor decryptor= new XORdecryptor();

            decryptor.DecryptFile("cipher1.txt");
            textBoxOutput.Text = decryptor.bestAnswer;
            textBoxScore.Text = decryptor.bestScore.ToString();
            textBoxPassword.Text = decryptor.bestPassword;
            textBoxTime.Text = decryptor.bestTime.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
