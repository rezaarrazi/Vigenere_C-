using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chipper
{
    public partial class Form1 : Form
    {
        int mode = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);

                textBox1.Text = openFileDialog1.FileName;
                string read = sr.ReadToEnd();
                richTextBox1.Text = read;
                //MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
                MessageBox.Show("Fill the key!");
            else if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                MessageBox.Show("No string to encode!");
            else
            {
                string key = textBox2.Text.ToUpper();
                string in_key = "";
                string out_string = "";
                int j=0;

                string input = "";
                string read = richTextBox1.Text.ToUpper();
                for (int i = 0; i < read.Length; i++)
                {
                    if (read[i] > 64 && read[i] < 91)
                    {
                        input += read[i];
                    }
                }
                
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i] < 65 || key[i] > 90)
                    {
                        MessageBox.Show("Key must contains alphabetic only");
                        return;
                    }
                }
                
                if(mode == 0){
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (j > key.Length - 1) j = 0;
                        in_key += key[j++];
                        int karakter = ((((int)input[i] - 65) + ((int)in_key[i] - 65)) % 26) + 65;
                        out_string += (char)karakter;
                    }
                }
                else
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (j > key.Length - 1) j = 0;
                        in_key += key[j++];
                        int karakter = (((int)input[i]-65) - ((int)in_key[i]-65));
                        if(karakter>=0)
                            out_string += (char) ((karakter % 26) + 65);
                        else
                            out_string += (char) (26 + (karakter % (-26)) + 65);
                    }
                }
                richTextBox2.Text = out_string;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mode == 0)
            {
                mode = 1;
                button4.Text = "Chiper To Plain Text";
                label1.Text = "Chiper text";
                label3.Text = "Plain text";
                button3.Text = "Decode";
            }
            else
            {
                mode = 0;
                button4.Text = "Plain Text To Chiper";
                label1.Text = "Plain text";
                label3.Text = "Chiper text";
                button3.Text = "Encode";
            }
            richTextBox1.Text = "";
            richTextBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                MessageBox.Show("No string to save!");
                return;
            }
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, richTextBox2.Text);
                }
            }
        }
    }
}
