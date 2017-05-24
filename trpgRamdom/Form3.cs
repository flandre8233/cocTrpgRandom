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
using System.Resources;

namespace trpgRamdom {
    

    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
        }
        public Form3(Resources.Player pastplayerdata) {
            playerData = pastplayerdata;
            InitializeComponent();
        }
        Resources.Player playerData = null;

        public class combovaluePPval {
            public int PPval { get; set; }
        }
        public class combovalueIPval {
            public int IPval { get; set; }
        }
        public class combovalueGPval {
            public int GPval { get; set; }
        }
        int test;
        int CthulhuSkillinObjIndex = 0;

        private void Form3_Load(object sender, EventArgs e) {

            if (playerData != null) {  //當角色資料成功讀入時
                labelSTR.Text = playerData.STRNUM.ToString();
                labelCON.Text = playerData.CONNUM.ToString();
                labelSIZE.Text = playerData.SIZENUMTotal.ToString();
                labelDEX.Text = playerData.DEXNUM.ToString();
                labelAPP.Text = playerData.APPNUM.ToString();
                labelPOW.Text = playerData.POWNUM.ToString();
                labelINT.Text = playerData.INTNUMTotal.ToString();
                labelSAN.Text = playerData.SANNUM.ToString();
                labelEDU.Text = playerData.EDUNUMTotal.ToString();
                labelIDEA.Text = playerData.IDEANUM.ToString();
                labelLUCK.Text = playerData.LUCKNUM.ToString();
                labelKNOW.Text = playerData.KNOWNUM.ToString();
                labelHP.Text = " / " + playerData.HPNUM.ToString();
                labelMP.Text = " / " + playerData.MPNUM.ToString();
                labelDB.Text = playerData.DB;
                currentlySAN.Text = playerData.currentlySAN.ToString();
                labelprofessionpointcount.Text = (playerData.PPNUM - playerData.selectedPP) + " / " + playerData.PPNUM.ToString();
                labelinterestpointcount.Text = (playerData.IPNUM - playerData.selectedIP) + " / " + playerData.IPNUM.ToString();


                textBoxPCName.Text = playerData.PCName;
                textBoxPLName.Text = playerData.PLName;
                textBoxPClivePlace.Text = playerData.PClivePlace;
                textBoxEducational.Text = playerData.Educational;
                textBoxAge.Text = playerData.PCAge;
                textBoxGender.Text = playerData.PCGender;
                textBoxBirthplace.Text = playerData.PCBirthplace;
                textBoxProfession.Text = playerData.PCProfession;
                textBoxDecade.Text = playerData.PCDecade;

                Resources.Function myFunction = new Resources.Function();
                label25.Text = myFunction.featureText(playerData);

                UInt16 comboboxStartNum = 10;

                for (int i = playerData.HPNUM; i >= 0 - comboboxStartNum; i--) {  //HP設定
                    comboBoxHP.Items.Add(i);
                }
                for (int i = playerData.MPNUM; i >= 0; i--) {
                    comboBoxMP.Items.Add(i);
                }
                comboBoxHP.Text = playerData.currentlyHP.ToString();
                comboBoxMP.Text = playerData.currentlyMP.ToString();

                for (int i = 0; i >= -playerData.SANNUM; i--) {  //san值設定
                    comboBoxSANin_decrease.Items.Add(i);
                }

                foreach (Resources.Player.objectSkill obj in playerData.playerobjectSkill) {
                    obj.Name.Replace("\r\n","");
                }

                for(int i=0; i<=playerData.playerobjectSkill.Length-1; i++) {
                    //label1.Text = playerData.playerobjectSkill[i].ToString();
                }

                comboBoxSANin_decrease.Text = playerData.SANin_decrease.ToString();
                startDataGridView();

                if (playerData.PCItem != null) {
                    foreach (string Str in playerData.PCItem) {
                        richTextBox1.Text += Str;
                        richTextBox1.Text += '\n';
                    }
                }

                

                
            }
            
        }

        void startDataGridView() {
            dataGridView1.Rows.Add();
            dataGridView1.Rows.AddCopies(0, playerData.playerobjectSkill.Length - 1); //初始化dataGridView1 rows
            for (int i = 0; i <= playerData.playerobjectSkill.Length - 1 ; i++) {
                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell InitialCell = new DataGridViewTextBoxCell();
                DataGridViewComboBoxCell PPcombobox = new DataGridViewComboBoxCell();
                DataGridViewComboBoxCell IPcombobox = new DataGridViewComboBoxCell();
                DataGridViewComboBoxCell GPcombobox = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell totalCell = new DataGridViewTextBoxCell();



                nameCell.Value = playerData.playerobjectSkill[i].Name;
                InitialCell.Value = playerData.playerobjectSkill[i].InitialValue;

                nameCell.Tag = playerData.playerobjectSkill[i].Name;
                InitialCell.Tag = playerData.playerobjectSkill[i].Name;

                dataGridView1.Rows[i].Cells[0] = nameCell;  //不變數值
                dataGridView1.Rows[i].Cells[1] = InitialCell; ////不變數值
                dataGridView1.Rows[i].Cells[2] = PPcombobox;
                dataGridView1.Rows[i].Cells[3] = IPcombobox;
                dataGridView1.Rows[i].Cells[4] = GPcombobox;
                dataGridView1.Rows[i].Cells[5] = totalCell;

                List<combovaluePPval> listPPVal = new List<combovaluePPval>();
                List<combovalueIPval> listIPVal = new List<combovalueIPval>();
                List<combovalueGPval> listGPVal = new List<combovalueGPval>();
                

                for (int j = 0; j <= 100
                    ; j++) {
                    listPPVal.Add(new combovaluePPval { PPval = j });
                    listIPVal.Add(new combovalueIPval { IPval = j });
                    listGPVal.Add(new combovalueGPval { GPval = j });
                }

                PPcombobox.DataSource = listPPVal;
                PPcombobox.ValueMember = "PPval";
                PPcombobox.DisplayMember = "PPval";
                PPcombobox.Tag = playerData.playerobjectSkill[i].Name;
                IPcombobox.DataSource = listIPVal;
                IPcombobox.ValueMember = "IPval";
                IPcombobox.DisplayMember = "IPval";
                IPcombobox.Tag = playerData.playerobjectSkill[i].Name;
                GPcombobox.DataSource = listGPVal;
                GPcombobox.ValueMember = "GPval";
                GPcombobox.DisplayMember = "GPval";
                GPcombobox.Tag = playerData.playerobjectSkill[i].Name;


                updateRowsValue(i, PPcombobox, IPcombobox, GPcombobox,totalCell,0);
                setupOtherValue(ref CthulhuSkillinObjIndex);
                updateOtherValue(CthulhuSkillinObjIndex);
            }
        }

        void updateRowsValue(int index , DataGridViewComboBoxCell PPcombobox, DataGridViewComboBoxCell IPcombobox, DataGridViewComboBoxCell GPcombobox, DataGridViewTextBoxCell totalCell, int type) {

            switch (type) {
                case 0:
                    IPcombobox.Value = playerData.playerobjectSkill[index].interestValue; //設定combobox起始值
                    PPcombobox.Value = playerData.playerobjectSkill[index].professionValue; //設定combobox起始值
                    GPcombobox.Value = playerData.playerobjectSkill[index].growValue;  //設定combobox起始值
                    break;
                case 2:
                    IPcombobox.Value = playerData.playerobjectSkill[index].interestValue; //設定combobox起始值
                    GPcombobox.Value = playerData.playerobjectSkill[index].growValue;  //設定combobox起始值
                    break;
                case 3:
                    PPcombobox.Value = playerData.playerobjectSkill[index].professionValue; //設定combobox起始值
                    GPcombobox.Value = playerData.playerobjectSkill[index].growValue;  //設定combobox起始值
                    break;
                case 4:
                    IPcombobox.Value = playerData.playerobjectSkill[index].interestValue; //設定combobox起始值
                    GPcombobox.Value = playerData.playerobjectSkill[index].growValue;  //設定combobox起始值
                    break;
            }

            countTotalValue(index);
            totalCell.Value = playerData.playerobjectSkill[index].totalValue + "%";
            /*
            for (int j = 0; j <= 100 -
                playerData.playerobjectSkill[index].InitialValue -
                playerData.playerobjectSkill[index].interestValue -
                playerData.playerobjectSkill[index].growValue
                ; j++) {
                listPPVal.Add(new combovaluePPval { PPval = j });
            }
            PPcombobox.DataSource = listPPVal;
            PPcombobox.ValueMember = "PPval";
            PPcombobox.DisplayMember = "PPval";
            PPcombobox.Value = playerData.playerobjectSkill[index].professionValue; //設定combobox起始值
            PPcombobox.Tag = playerData.playerobjectSkill[index].Name;

            for (int j = 0; j <= 100 -
                playerData.playerobjectSkill[index].InitialValue -
                playerData.playerobjectSkill[index].professionValue -
                playerData.playerobjectSkill[index].growValue
                ; j++) {
                listIPVal.Add(new combovalueIPval { IPval = j });
            }
            IPcombobox.DataSource = listIPVal;
            IPcombobox.ValueMember = "IPval";
            IPcombobox.DisplayMember = "IPval";
            IPcombobox.Value = playerData.playerobjectSkill[index].interestValue; //設定combobox起始值
            IPcombobox.Tag = playerData.playerobjectSkill[index].Name;

            for (int j = 0; j <= 100 -
                playerData.playerobjectSkill[index].InitialValue -
                playerData.playerobjectSkill[index].professionValue -
                playerData.playerobjectSkill[index].interestValue
                ; j++) {
                listGPVal.Add(new combovalueGPval { GPval = j });
            }
            GPcombobox.DataSource = listGPVal;
            GPcombobox.ValueMember = "GPval";
            GPcombobox.DisplayMember = "GPval";
            GPcombobox.Value = playerData.playerobjectSkill[index].growValue;  //設定combobox起始值
            GPcombobox.Tag = playerData.playerobjectSkill[index].Name;
            */
        }

        void countTotalValue(int Index) {
            playerData.playerobjectSkill[Index].totalValue =
                playerData.playerobjectSkill[Index].InitialValue + 
                playerData.playerobjectSkill[Index].professionValue +
                playerData.playerobjectSkill[Index].interestValue +
                playerData.playerobjectSkill[Index].growValue;
        }

        void setupOtherValue(ref int CthulhuSkillinObjIndex) {  //起動初始化其他數據
            //foreach (Resources.Player.objectSkill eachObj in playerData.playerobjectSkill) 
            for (int i = 0; i <= playerData.playerobjectSkill.Length-1; i++) {
                if (playerData.playerobjectSkill[i].Name == "克蘇魯神話") {  //將克蘇魯神話點數同步
                    labelCthulhuPoint.Text = playerData.playerobjectSkill[i].totalValue.ToString();
                    CthulhuSkillinObjIndex = i;
                    break;
                }
            }
        }

        void updateOtherValue(int CthulhuSkillinObjIndex) { 
            int costPP = 0;  //重置pp ip 花費點數
            int costIP = 0;
            for (int i = 0; i <= playerData.playerobjectSkill.Length - 1; i++) {  //加總
                costPP += playerData.playerobjectSkill[i].professionValue;
                costIP += playerData.playerobjectSkill[i].interestValue;
            }
            playerData.selectedPP = costPP;
            playerData.selectedIP = costIP;

            labelprofessionpointcount.Text = (playerData.PPNUM - playerData.selectedPP) + " / " + playerData.PPNUM.ToString();
            labelinterestpointcount.Text = (playerData.IPNUM - playerData.selectedIP) + " / " + playerData.IPNUM.ToString();

            labelCthulhuPoint.Text = playerData.playerobjectSkill[CthulhuSkillinObjIndex].totalValue.ToString(); //將克蘇魯神話點數同
        }

        private void button1_Click(object sender, EventArgs e) {  //存檔


            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    StreamWriter mystreamwriter = new StreamWriter(saveFileDialog1.OpenFile());
                // Code to write the stream goes here.  SAVE data
                FileInfo fi = new FileInfo(saveFileDialog1.FileName);  //取存檔名字
                this.Text = fi.Name.Remove(fi.Name.Length-4) + "    角色卡";

                mystreamwriter.Write("thisISSaveFile");
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.STRNUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.CONNUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.SIZENUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.DEXNUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.APPNUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.POWNUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.INTNUM);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.EDUNUM);
                mystreamwriter.Write('_');

                mystreamwriter.Write(textBoxPCName.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxPLName.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxPClivePlace.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxEducational.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxAge.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxGender.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxBirthplace.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxProfession.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(textBoxDecade.Text);
                mystreamwriter.Write('_');

                mystreamwriter.Write(comboBoxHP.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(comboBoxMP.Text);
                mystreamwriter.Write('_');
                mystreamwriter.Write(comboBoxSANin_decrease.Text);
                mystreamwriter.Write('_');

                mystreamwriter.Write(playerData.feature[0]);
                mystreamwriter.Write('_');
                mystreamwriter.Write(playerData.feature[1]);
                mystreamwriter.Write('_');

                mystreamwriter.Write(playerData.DB);
                mystreamwriter.Write('_');

                mystreamwriter.Write(playerData.playerobjectSkill.Length);
                mystreamwriter.Write('_');

                foreach (var objeach in playerData.playerobjectSkill) {
                    mystreamwriter.Write(objeach.Name);
                    mystreamwriter.Write('_');
                    mystreamwriter.Write(objeach.InitialValue);
                    mystreamwriter.Write('_');
                    mystreamwriter.Write(objeach.professionValue);
                    mystreamwriter.Write('_');
                    mystreamwriter.Write(objeach.interestValue);
                    mystreamwriter.Write('_');
                    mystreamwriter.Write(objeach.growValue);
                    mystreamwriter.Write('_');
                }

                mystreamwriter.Write(richTextBox1.Text.Replace('\n','\\' )  );
                mystreamwriter.Write('_');

                mystreamwriter.Dispose();
                mystreamwriter.Close();
            }

        }

        private void comboBoxSANin_decrease_SelectedIndexChanged(object sender, EventArgs e) {
            playerData.SANin_decrease = Convert.ToInt16(comboBoxSANin_decrease.Text);
            playerData.updateData();
            currentlySAN.Text = playerData.currentlySAN.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            //label30.Text = test++.ToString();

            ComboBox cb = e.Control as ComboBox;
            if (cb != null) {
                // first remove event handler to keep from attaching multiple:
                cb.SelectionChangeCommitted -= new
                EventHandler(cb_SelectionChangeCommitted);

                // now attach the event handler
                cb.SelectionChangeCommitted += new
                EventHandler(cb_SelectionChangeCommitted);
            }
        }

        void cb_SelectionChangeCommitted(object sender, EventArgs e) {

            ComboBox cb = (ComboBox)sender;
            int rowIndex = dataGridView1.CurrentRow.Index;

            if (cb.SelectedValue != null) {
                /// Your Code goes here
                /// 

                //dataGridView1.CurrentRow.Cells[dataGridView1.CurrentCell.ColumnIndex].Value = cb.Text;

                switch (dataGridView1.CurrentCell.ColumnIndex) {
                    case 2:
                        playerData.playerobjectSkill[rowIndex].professionValue = Convert.ToInt16(cb.Text);
                        break;
                        
                    case 3:
                        playerData.playerobjectSkill[rowIndex].interestValue = Convert.ToInt16(cb.Text);
                        break;
                        
                    case 4:
                        playerData.playerobjectSkill[rowIndex].growValue = Convert.ToInt16(cb.Text);
                        break;
                }

                DataGridViewComboBoxCell PPcombobox = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[2];
                DataGridViewComboBoxCell IPcombobox = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[3];
                DataGridViewComboBoxCell GPcombobox = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[4];
                DataGridViewTextBoxCell totalCell = (DataGridViewTextBoxCell)dataGridView1.CurrentRow.Cells[5];

                //不能改當前combobox數據
                updateRowsValue(rowIndex, PPcombobox, IPcombobox, GPcombobox,totalCell,0);
                updateOtherValue(CthulhuSkillinObjIndex);  //每次更新COMBOBOX數值時更新數值
            }

        }
    }
}
