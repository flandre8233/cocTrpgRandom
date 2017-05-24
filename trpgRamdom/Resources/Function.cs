using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpgRamdom.Resources {
    class Function {

        public int RandomNumber(int selectedDice) {
            Random myRand = new Random(Guid.NewGuid().GetHashCode());
            int randomNumber = (myRand.Next(1, selectedDice + 1));
            return randomNumber;
        }

        public int TimesDiceSided(int Times,int sided_dice) {
            int totalRandomNumber = 0;
            for (int i = 1; i <= Times; i++) {
                totalRandomNumber += RandomNumber(sided_dice);
            }
            return totalRandomNumber;
        }

        public string featureText(Player playerdata) {
            string result="";
            for (int i = 0; i <= playerdata.feature.Length - 1; i++) {

                string resource_data = Properties.Resources.featureData;
                

                result += resource_data.Split('\n')[playerdata.feature[i]];

                result += Environment.NewLine;

            }
            return result;
        }

    }
}
