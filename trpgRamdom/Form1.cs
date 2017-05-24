using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trpgRamdom {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        Resources.Function functionList = new Resources.Function();

        private void button1_Click(object sender, EventArgs e) {
            listBox1.Items.Clear();

            int selectedDice = 0;

            if (radioButton1.Checked) {
                selectedDice = Convert.ToInt16(comboBox1.SelectedItem);
            }
            else {
                selectedDice = Convert.ToInt16(numericUpDown1.Value);
            }

            int totalNumber = 0;
            for (int i = 0; i <= comboBox2.SelectedIndex ; i++) {
                int randomNumber = functionList.RandomNumber( selectedDice);

                totalNumber += randomNumber;
                listBox1.Items.Add(randomNumber.ToString());
            }
            label2.Text = totalNumber.ToString();
            
        }

        private void Form1_Load(object sender, EventArgs e) {
            radioButton1.Checked = true;
            radioButton2.Checked = false;

            label2.Text = "";
            comboBox1.SelectedIndex = 0;

            for (int i=0; i <= 24; i++) {
                comboBox2.Items.Add(i+1);
            }
            comboBox2.SelectedIndex = 0;

        }

        private void button2_Click(object sender, EventArgs e) {
            listBox1.Items.Clear();
            label2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e) {
            Form2 createChar = new Form2();
            createChar.Show();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e) {
            button2_Click(sender, e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            if (radioButton1.Checked) {
                comboBox1.Enabled = true;
                numericUpDown1.Enabled = false;
            }
            else {
                comboBox1.Enabled = false;
                numericUpDown1.Enabled = true;
            }

        }
    }
}
