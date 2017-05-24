using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpgRamdom.Resources {
    public class Player {
        public class objectSkill {
            public string Name { set; get; }
            public int InitialValue { set; get; }
            public int professionValue { set; get; }
            public int interestValue { set; get; }
            public int growValue { set; get; }
            public int totalValue { set; get; }
        }


        public int testnumber { get; set; }

        public int STRNUM { get; set; }  //角色數值
        public int DEXNUM { get; set; }
        public int INTNUM { get; set; }
        public int CONNUM { get; set; }
        public int APPNUM { get; set; }
        public int SIZENUM { get; set; }
        public int POWNUM { get; set; }
        public int EDUNUM { get; set; }

        public int IDEANUM { get; set; }
        public int SANNUM { get; set; }
        public int LUCKNUM { get; set; }
        public int KNOWNUM { get; set; }
        public int HPNUM { get; set; }
        public int MPNUM { get; set; }
        public int PPNUM { get; set; }
        public int IPNUM { get; set; }

        public string DB { get; set; }

        public int INTNUMTotal;
        public int EDUNUMTotal;
        public int SIZENUMTotal;

        public int SANin_decrease { get; set; }
        public int currentlyHP { get; set; }
        public int currentlyMP { get; set; }
        public int currentlySAN { get; set; }

        public int selectedPP { get; set; }
        public int selectedIP { get; set; }


        public string PCName { get; set; }
        public string PLName { get; set; }
        public string PClivePlace { get; set; }
        public string Educational { get; set; }
        public string PCAge { get; set; }
        public string PCGender { get; set; }
        public string PCBirthplace { get; set; }
        public string PCProfession { get; set; }
        public string PCDecade { get; set; }
        public string[] PCItem { get; set; }

        public objectSkill[] playerobjectSkill;
        

        public int[] feature = new int[2]; //角色擁有的特色數量

        /*
        public Player() {
            
        }
        */


        public Player() {
            resetPlayerSkill();  //起動playerSkill
            updataSkillData();
        }
        
        public void resetPlayerHPMP() {
            currentlyHP = HPNUM;
            currentlyMP = MPNUM;
        }

        public void resetPlayerSkill() {
            List<string> skillNameList = new List<string>();
            List<int> skillValueList = new List<int>();


            string[]  arrayValue = Properties.Resources.skillName.Replace("\r\n", "|").Split('|'); //將文本轉成array

            foreachValue(arrayValue, skillNameList);
            arrayValue = Properties.Resources.skillInitialValue.Split('\n');
            foreachValue(arrayValue,skillValueList);

            playerobjectSkill = new objectSkill[skillNameList.Count ]; //初始化playerobjectSkill需要多少空間
            for (int i=0; i<= skillNameList.Count - 1; i++) {
                playerobjectSkill[i] = new objectSkill();  //為每個playerobjectSkill[]空間初始化
                playerobjectSkill[i].Name = skillNameList[i];  
                playerobjectSkill[i].InitialValue = skillValueList[i];

                
                
            }

            

            
        }

        void foreachValue(string[] arrayvalue, List<string> list) {
            foreach (string item in arrayvalue) {

                if (item == arrayvalue[0]) {
                }
                else {
                    list.Add(item);
                }
            }
        }  //將array轉成list 並消掉所有
        void foreachValue(string[] arrayvalue, List<int> list) {
                foreach (string item in arrayvalue) {

                    if (item == arrayvalue[0]) {
                    }
                    else {
                        list.Add(int.Parse(item));
                    }
                }
            }

        public void updataSkillData() {
            foreach(objectSkill obj in playerobjectSkill) {
                obj.totalValue = obj.InitialValue + obj.professionValue + obj.interestValue;
            }

        }

        public void updateData() {
            INTNUMTotal = 6 + INTNUM;
            EDUNUMTotal = 3 + EDUNUM;
            SIZENUMTotal = 6 + SIZENUM;

            double DcharHP = (CONNUM + SIZENUMTotal) / 2.0f;
            int charHP = Convert.ToInt16(Math.Ceiling(DcharHP));
            SANNUM = POWNUM * 5;
            LUCKNUM = (POWNUM * 5);
            IDEANUM = (INTNUMTotal * 5);
            KNOWNUM = EDUNUMTotal * 5;
            HPNUM = charHP;
            MPNUM = POWNUM;
            PPNUM = (EDUNUMTotal * 20);
            IPNUM = (INTNUMTotal * 10);

            currentlySAN = SANNUM + SANin_decrease;

            int STRSIZ = STRNUM + SIZENUMTotal;
            if (STRSIZ >= 2 && STRSIZ <= 12) {
                DB = "-1D6";
            }
            else if (STRSIZ >= 13 && STRSIZ <= 16) {
                DB = "-1D4";
            }
            else if (STRSIZ >= 17 && STRSIZ <= 24) {
                DB = "no Damage bouns";
            }
            else if (STRSIZ >= 25 && STRSIZ <= 32) {
                DB = "+1D4";
            }
            else if (STRSIZ >= 33 && STRSIZ <= 40) {
                DB = "+1D6";
            }

        }

        public void changeSpecialSkill() {
            foreach (objectSkill obj in playerobjectSkill) {
                if (obj.Name == "閃躲" || obj.Name == "閃躲\r" || obj.Name == "閃躲\r\r") {
                    obj.InitialValue = DEXNUM * 2;
                }
                
            }
            
        }
        
    }
}
