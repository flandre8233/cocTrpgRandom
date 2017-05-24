using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;


namespace trpgRamdom {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }
        Resources.Function functionList = new Resources.Function();
        Resources.Player myPlayer = new Resources.Player();

    //Resources.Function playerData = new Form1().myPlayer()
    //Resources.Player playerData = new Resources.Player();

        ListBox mouseListbox = null;

        bool incheckState = false;
        
        private void Form2_Load(object sender, EventArgs e) {
            Form1 form1 = new Form1();

            //Resources.Player myPlayer = form1.test();
          //label35.Text = playerData.testnumber.ToString();
            labelSTR.Text = "3D6  ";
            labelCON.Text = "3D6  ";
            labelSIZE.Text = "2D6+6  ";
            labelDEX.Text = "3D6  ";
            labelAPP.Text = "3D6  ";
            labelPOW.Text = "3D6  ";
            labelINT.Text = "2D6+6  ";
            labelSAN.Text = "POW x 5  ";
            labelEDU.Text = "3D6+3  ";
            labelIDEA.Text = "INT x 5  ";
            labelLUCK.Text = "POW x 5  ";
            labelKNOW.Text = "EDU x 5  ";
            labelHP.Text = "(CON+SIZ)/2,小數無條件進位  ";
            labelMP.Text = "POW  ";
            labelDB.Text = "DB";
            labelPP.Text = "EDUx20  ";
            labelIP.Text = "INTx10  ";

            label36.Text = "";
            label36.AutoSize = true;


            foreach (Control c in this.Controls) {
                if (c.GetType() == typeof(ListBox)) {
                    c.Enabled = false;
                }
            }
            comboBox1.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;

            string resource_data = Properties.Resources.featureData;
            string[] resource_dateArray = resource_data.Split('\n');

            if (functionList.RandomNumber(3) > 1) {  //決定角色特徵數量
                myPlayer.feature[0] = functionList.RandomNumber(resource_dateArray.Length-1);
                myPlayer.feature[1] = 0;
            }
            else {
                do {
                    myPlayer.feature[0] = functionList.RandomNumber(resource_dateArray.Length-1 );
                    myPlayer.feature[1] = functionList.RandomNumber(resource_dateArray.Length-1 );
                }
                while (myPlayer.feature[0] == myPlayer.feature[1]);
            }

            for (int i = 0; i <= myPlayer.feature.Length - 1; i++) {

                label36.Text += resource_dateArray[myPlayer.feature[ i ] ];

                label36.Text += Environment.NewLine;
            }
            label36.Hide();

        }

        //電腦配點
        private void button1_Click(object sender, EventArgs e) {  
            label36.Show();
            foreach (String c in comboBox1.Items) {
                switch (c) { //分配好
                    case "STR":
                        myPlayer.STRNUM = functionList.TimesDiceSided(3,6);
                        break;
                    case "DEX":
                        myPlayer.DEXNUM = functionList.TimesDiceSided(3,6);
                        break;
                    case "INT":
                        myPlayer.INTNUM = functionList.TimesDiceSided(2,6);
                        break;
                    case "CON":
                        myPlayer.CONNUM = functionList.TimesDiceSided(3,6);
                        break;
                    case "APP":
                        myPlayer.APPNUM = functionList.TimesDiceSided(3,6);
                        break;
                    case "SIZE":
                        myPlayer.SIZENUM = functionList.TimesDiceSided(2,6);
                        break;
                    case "POW":
                        myPlayer.POWNUM = functionList.TimesDiceSided(3,6);
                        break;
                    case "EDU":
                        myPlayer.EDUNUM = functionList.TimesDiceSided(3,6);
                        break;
                }
            }
            updateData();
            myPlayer.resetPlayerHPMP();

            enterForm3();

            /*
            button1.Enabled = false;
            button2.Hide();
            button3.Hide();
            button4.Hide();

            foreach (Control c in this.Controls) {
                if (c.GetType() == typeof(ListBox)) {
                    c.Hide();

                }
            }
            comboBox1.Enabled = false;

            button4.Enabled = false;

            button2.Hide();
            */
        }

        //自選角色數值
        private void button2_Click(object sender, EventArgs e) {
            //myPlayer = new Resources.Player();
            label36.Show();
            for (int i = 1; i <= 24; i++) {  //24D6
                TOALROLL.Items.Add(functionList.RandomNumber(6));
            }

            foreach (Control c in this.Controls) {
                if (c.GetType() == typeof(ListBox)) {
                    c.Enabled = true;
                }
            }
            comboBox1.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;

            updateData();

            button1.Enabled = false;
            button2.Enabled = false;

        }   

        private void updateData() {

            myPlayer.updateData();

            labelSTR.Text = myPlayer.STRNUM + "  3D6";
            labelCON.Text = myPlayer.CONNUM + "  3D6";
            labelSIZE.Text = myPlayer.SIZENUMTotal + "  2D6+6";
            labelDEX.Text = myPlayer.DEXNUM + "  3D6";
            labelAPP.Text = myPlayer.APPNUM + "  3D6";
            labelPOW.Text = myPlayer.POWNUM + "  3D6";
            labelINT.Text = myPlayer.INTNUMTotal + "  2D6+6";
            labelSAN.Text = myPlayer.SANNUM + "  POW x 5";
            labelEDU.Text = myPlayer.EDUNUMTotal + "  3D6+3";
            labelIDEA.Text = myPlayer.IDEANUM + "  INT x 5";
            labelLUCK.Text = myPlayer.LUCKNUM + "  POW x 5";
            labelKNOW.Text = myPlayer.KNOWNUM + "  EDU x 5";
            labelHP.Text = myPlayer.HPNUM + "  (CON+SIZ)/2,小數無條件進位";
            labelMP.Text = myPlayer.MPNUM + "  POW";
            labelPP.Text = myPlayer.PPNUM + "  EDUx20";
            labelIP.Text = myPlayer.IPNUM + "  INTx10";
        }

        private void listBox_MouseClick(object sender, MouseEventArgs e) {
            mouseListbox = sender as ListBox;
            foreach (object obj in comboBox1.Items) {
                if (obj.ToString() == mouseListbox.Tag.ToString()) {
                    comboBox1.SelectedItem = obj;
                }
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e) {
            //ListBox listbox = sender as ListBox; //
            ListBox box = null;

            foreach (Control c in this.Controls) {  //除掉其他已選內容
                if (c.GetType() == typeof(ListBox)) {
                    box = c as ListBox;
                    if (box.Name != mouseListbox.Name) {
                        box.SelectedItem = null;
                    }
                }
            }

            /*
            ListBox thisLB = sender as ListBox; //目標
            label35.Text = thisLB.Tag.ToString();
            */
            
            if (mouseListbox != null && mouseListbox.Tag != null && comboBox1.SelectedItem != null) {
                incheckState = false;
                button3.Text = "檢查配點" ;

                if (mouseListbox.Tag.ToString() == "TOALROLL") {

                    foreach (Control c in this.Controls) {  //ROLL出來的數值傳去右邊  左傳去右
                        if (c.GetType() == typeof(ListBox)) {
                            if (c.Tag == comboBox1.SelectedItem && TOALROLL.SelectedItem != null) {  //已鎖死只檢查目標

                                box = c as ListBox;
                                if (box.Items.Count < 3 && (box.Tag.ToString() != "INT" && box.Tag.ToString() != "SIZE")) {
                                    box.Items.Add(TOALROLL.SelectedItem);
                                    TOALROLL.Items.RemoveAt(TOALROLL.SelectedIndex);
                                }
                                if (box.Items.Count < 2 && (box.Tag.ToString() == "INT" || box.Tag.ToString() == "SIZE")) {
                                    box.Items.Add(TOALROLL.SelectedItem);
                                    TOALROLL.Items.RemoveAt(TOALROLL.SelectedIndex);
                                }

                                if (box.Items.Count == 3 && (box.Tag.ToString() != "INT" && box.Tag.ToString() != "SIZE")) {
                                    box.BackColor = Color.White;  //還原
                                }
                                if (box.Items.Count == 2 && (box.Tag.ToString() == "INT" || box.Tag.ToString() == "SIZE")) {
                                    box.BackColor = Color.White;  //還原
                                }

                            }
                        }
                    }
                }
                else {  //右邊數據傳回左面  mouseListbox是選擇了對象
                    if (mouseListbox.SelectedItem != null) {
                        box = mouseListbox;
                        TOALROLL.Items.Add(box.SelectedItem);
                        box.Items.RemoveAt(box.SelectedIndex);
                    }
                }

                int totalNum = 0;
                foreach (int eachitemV in box.Items) { //將box結果相加
                    totalNum += eachitemV;       
                }
                switch (comboBox1.SelectedItem.ToString()) { //分配好數字到文本中
                    case "STR":
                        myPlayer.STRNUM = totalNum;
                        break;
                    case "DEX":
                        myPlayer.DEXNUM = totalNum;
                        break;
                    case "INT":
                        myPlayer.INTNUM = totalNum;
                        break;
                    case "CON":
                        myPlayer.CONNUM = totalNum;
                        break;
                    case "APP":
                        myPlayer.APPNUM = totalNum;
                        break;
                    case "SIZE":
                        myPlayer.SIZENUM = totalNum;
                        break;
                    case "POW":
                        myPlayer.POWNUM = totalNum;
                        break;
                    case "EDU":
                        myPlayer.EDUNUM = totalNum;
                        break;
                }
                updateData();
            }

        }

        //reset 自選內容
        private void button4_Click(object sender, EventArgs e) {  
            foreach (Control c in this.Controls) {
                if (c.GetType() == typeof(ListBox)) {
                    ListBox box = c as ListBox;
                    if (box.Name != TOALROLL.Name) {

                        box.BackColor = Color.White;//重置顏色
                        foreach (int boxitems in box.Items) {//還原listbox中數值
                            TOALROLL.Items.Add(boxitems);
                        }
                        box.Items.Clear();
                    }
                }
            }

            //myPlayer = new Resources.Player();

            updateData();

                }

        //確定數據
        private void button3_Click(object sender, EventArgs e) { 

            bool checkIsDone = true;

            foreach (Control c in this.Controls) {  //check有冇錯
                if (c.GetType() == typeof(ListBox)) {
                    ListBox LB = c as ListBox;
                    if (LB.Tag.ToString() != "TOALROLL") {
                        if ( (LB.Tag.ToString() != "INT" && LB.Tag.ToString() != "SIZE") && LB.Items.Count != 3) {
                            checkIsDone = false;
                            LB.BackColor = Color.Red;
                        }
                       if ( (LB.Tag.ToString() == "INT" || LB.Tag.ToString() == "SIZE") &&  LB.Items.Count != 2) {
                            checkIsDone = false;
                            LB.BackColor = Color.Red;
                        }

                    }
                }
            }
            if (checkIsDone) {  //檢查完  沒有錯
                /*
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;

                foreach (Control c in this.Controls) {
                    if (c.GetType() == typeof(ListBox)) {
                        ListBox LB = c as ListBox;
                        comboBox1.Hide();
                        LB.Hide();
                    }
                }
                */


                
                myPlayer.resetPlayerHPMP(); //重置角色hp mp

                button3.Text = "確定配點";

                if (!incheckState) {
                    incheckState = true;
                    MessageBox.Show("沒有未分配數值");

                }
                else {
                    enterForm3();
                }
                 
                
            }
            else {
                MessageBox.Show("你有數值未加上去");
            }

        }

        void enterForm3() {
            myPlayer.updateData();
            myPlayer.changeSpecialSkill();
            myPlayer.updataSkillData();

            Form3 form3 = new Form3(myPlayer);
            if(form3Filename != null) {
                form3.Text = form3Filename.Remove(form3Filename.Length-4) + "    角色卡";  //-4為了消除後4個文字
            }
            form3.Show();
            this.Close();
        }

        string form3Filename = null;

        private void button6_Click(object sender, EventArgs e) {
            string[] readFileString=null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader myReader = new StreamReader(openFileDialog1.OpenFile());
                readFileString = myReader.ReadToEnd().Split('_');

                form3Filename = openFileDialog1.SafeFileName;
                //角色數值傳入
                myPlayer.STRNUM = Convert.ToInt16(readFileString[1]);
                myPlayer.CONNUM = Convert.ToInt16(readFileString[2]);
                myPlayer.SIZENUM = Convert.ToInt16(readFileString[3]);
                myPlayer.DEXNUM = Convert.ToInt16(readFileString[4]);
                myPlayer.APPNUM = Convert.ToInt16(readFileString[5]);
                myPlayer.POWNUM = Convert.ToInt16(readFileString[6]);
                myPlayer.INTNUM = Convert.ToInt16(readFileString[7]);
                myPlayer.EDUNUM = Convert.ToInt16(readFileString[8]);

                myPlayer.PCName = readFileString[9];
                myPlayer.PLName = readFileString[10];
                myPlayer.PClivePlace = readFileString[11];
                myPlayer.Educational = readFileString[12];
                myPlayer.PCAge = readFileString[13];
                myPlayer.PCGender = readFileString[14];
                myPlayer.PCBirthplace = readFileString[15];
                myPlayer.PCProfession = readFileString[16];
                myPlayer.PCDecade = readFileString[17];

                myPlayer.currentlyHP = Convert.ToInt16(readFileString[18]);
                myPlayer.currentlyMP = Convert.ToInt16(readFileString[19]);
                myPlayer.SANin_decrease = Convert.ToInt16(readFileString[20]);

                myPlayer.feature[0] = Convert.ToInt16(readFileString[21]);
                myPlayer.feature[1] = Convert.ToInt16(readFileString[22]);

                myPlayer.DB = readFileString[23];

                myPlayer.playerobjectSkill = new Resources.Player.objectSkill[Convert.ToInt16(readFileString[24]) ]; //初始化playerobjectSkill需要多少空間

                int j = 0;
                for (int i = 25 ; i <= ( (myPlayer.playerobjectSkill.Length-1) *5) + 25 ; i+=5) {
                    myPlayer.playerobjectSkill[j] = new Resources.Player.objectSkill();
                    myPlayer.playerobjectSkill[j].Name = readFileString[i];
                    myPlayer.playerobjectSkill[j].InitialValue = Convert.ToInt16(readFileString[i + 1]);
                    myPlayer.playerobjectSkill[j].professionValue = Convert.ToInt16(readFileString[i + 2]);
                    myPlayer.playerobjectSkill[j].interestValue = Convert.ToInt16(readFileString[i + 3]);
                    myPlayer.playerobjectSkill[j].growValue = Convert.ToInt16(readFileString[i + 4]);
                    j++;
                }
                myPlayer.PCItem = readFileString[ (j * 5)  +25].Split('\\');

                myReader.Close();



                enterForm3();
            }

            
        }
    }
}
