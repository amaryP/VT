using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MethodeTrading
    {

            public string code_Methode { get; set; }
            public string Methode_entree { get; set; }
            public string Methode_suivi { get; set; }
            public string Methode_sortie { get; set; }
        //    public string CodeMethodeTriggerTrade { get; set; }

          //  public virtual Methode Methode { get; set; }
       
        public MethodeTrading InitializeMethodeTrading(string code_Methode)
        {
            MethodeTrading MyMethodeTrading= new MethodeTrading();
            MyMethodeTrading.code_Methode = code_Methode;
            string[] subs=code_Methode.Split('_');
            MyMethodeTrading.Methode_entree = subs[0];
            MyMethodeTrading.Methode_suivi = subs[1];
            MyMethodeTrading.Methode_sortie = subs[3];
            return MyMethodeTrading;
        }

    }
}
