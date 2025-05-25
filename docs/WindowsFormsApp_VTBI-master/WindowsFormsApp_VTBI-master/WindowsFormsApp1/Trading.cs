using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static WindowsFormsApp1.Model1Container;
//using WindowsFormsApp1;
namespace WindowsFormsApp1
{
    class trading
    {
        //futur fonction de passage d'ordre relle
        //là justement enregistrement dans la table TradeSet de la valeur, prix, date, quantité
        public Trade InitializeTrade(Evenement MyEvent,string codeMethodeTriggerTrade, string codeMethodeSuivi , string  codeMethodeSortie) 
        {
            Trade MyTrade = new Trade();
            MyTrade.DateHeureDebut = MyEvent.DateHeure;
            MyTrade.Symbol = MyEvent.Symbol;
            MyTrade.ValeurAchat = MyEvent.valeur;
            MyTrade.QuantiteAchat = CalculQuantiteAchat(MyTrade.ValeurAchat.Value); //il faudra l'initialiser en fonction du plan de trade, a priori c'est 1000 dollars la, il faudra affiner en fonctione de la volatilité ???)
            MyTrade.ValeurCourante = MyEvent.valeur;
            MyTrade.Gain = 0;
            MyTrade.Perte = 0;
            MyTrade.Statut = "OPEN";
            MyTrade.CodeMethodeTriggerTrade= codeMethodeTriggerTrade;
            MyTrade.CodeMethodeSuivi = codeMethodeSuivi;
            MyTrade.CodeMethodeSortie = codeMethodeSortie;
            MyTrade.TypeTrade = "TEST";

            //on initialise pour le moment le R0 (stop loss) a l'aide de la formule (on affinera) 1% du montant du trade.sans les frais pour le moment)
            if (MyTrade.CodeMethodeSuivi == "STOPLOSSPLUS")
            { MyTrade.R0 = CalculR0Plus(MyTrade.ValeurAchat.Value, MyTrade.QuantiteAchat.Value); }
            else
            {
                MyTrade.R0 = CalculR0(MyTrade.ValeurAchat.Value, MyTrade.QuantiteAchat.Value);
            }

            MyTrade.R1= CalculR1(MyTrade.ValeurAchat.Value, MyTrade.QuantiteAchat.Value,MyTrade.FraisAchat);
            MyTrade.R2 = CalculR2(MyTrade.R1.Value);
            MyTrade.R2Trailling = CalculTrailling(MyTrade.R2.Value);
            MyTrade.STOP_COURANT = MyTrade.R0;
            return MyTrade;
        }

        public void  CloseTrade(Trade MyTrade, decimal prix)
        {

            MyTrade.ValeurVente = prix;
            MyTrade.Statut = "CLOSE";
            MyTrade.DateHeureFin = DateTime.Now;
            //pour le moment la quantié de vente est = quantité d'achat
            MyTrade.QuantiteVente = MyTrade.QuantiteAchat.Value;
            //MYtradeSet.ValeurVente = prix;
            decimal MontantAchat = MyTrade.QuantiteAchat.Value * MyTrade.ValeurAchat.Value;
            decimal MontantVente = MyTrade.QuantiteVente.Value * MyTrade.ValeurVente.Value;

            decimal Resultat = MontantVente - MontantAchat;
            if (Resultat>0)
            {
                MyTrade.Gain = Resultat;
            }
            else
            {
                MyTrade.Perte = Math.Abs(Resultat);
            }
           // return MyTrade;
        }

        public decimal CalculR0(decimal ValeurAchat,decimal Quantite)
        {
            decimal Montant = Quantite * ValeurAchat;
            decimal UnPourCentMontant = Montant / 100;
            decimal MontantMoinsUnPourCentMontant = Montant - UnPourCentMontant;
            decimal ValeurVenteR0 = MontantMoinsUnPourCentMontant / Quantite;
            return ValeurVenteR0;
        }

        public decimal CalculR0Plus(decimal ValeurAchat, decimal Quantite)
        {
            //on positionne a 0.5 % de moins en valeur de perte max.
            decimal Montant = Quantite * ValeurAchat;
            decimal UnPourCentMontant = Montant / 100;
            decimal UnDemiPourCent = UnPourCentMontant / 2;
            decimal MontantMoinsUnDemiPourCentMontant = Montant - UnDemiPourCent;
            decimal ValeurVenteR0 = MontantMoinsUnDemiPourCentMontant / Quantite;
            return ValeurVenteR0;
        }

        public decimal CalculR2(decimal ValeurRUn) // pour le momentR2 = 1% de gain sur le trade. donc c'est 1% sur le R1 tout simplement non (+ frais de vente)?
        {
            //pour le moment on inclus pas les frais de vente.
            decimal UnPourCent = ValeurRUn / 100;
            decimal ValeurRDeux = ValeurRUn + UnPourCent;
            return ValeurRDeux;
        }

        public decimal CalculTrailling(decimal ValeurRUn) // pour le momentR2 = 1% de gain sur le trade. donc c'est 1% sur le R1 tout simplement non (+ frais de vente)?
        {
            //pour le moment on inclus pas les frais de vente.
            //on positionne a 0.5 % de moins que ValeurRUn
            decimal UnPourCent = ValeurRUn / 100;
            decimal UnDemiPourCent = UnPourCent / 2;
            decimal Valeurtrailling = ValeurRUn - UnDemiPourCent;
            return Valeurtrailling;
        }

        //il va falloir savoir quand utilisé R1... R0 c'est ok, mais R1... et meme R2... il faut ajouter un % un effet cliqué.
        // pour R2 pour le moment, on va placer l'ordre de vente ?  le trailling doit etre emuler, peut etre des indicateurs pour vendre ?
        public decimal CalculR1(decimal ValeurAchat, decimal QuantiteAchat ,decimal FraisAchat)
        {
            decimal MontantSeuil = (QuantiteAchat * ValeurAchat) + FraisAchat;
            decimal ValeurVenteR1 = MontantSeuil/ QuantiteAchat ;
            return ValeurVenteR1;
        }

        public decimal CalculQuantiteAchat(decimal ValeurAchatInitial)
        {
            //on cree des lignes de 1000 dollars. (virtuel pour le moment)
            decimal QuantiteAchat = 0;
            if (ValeurAchatInitial > 0)
                { 
                 QuantiteAchat = 1000 / ValeurAchatInitial;
                }
            return decimal.Round(QuantiteAchat);
        }

        public SuiviValeur TraceValeur(Evenement MyEvent)
        {
            SuiviValeur MySuiviValeur = new SuiviValeur();
            MySuiviValeur.DateHeure = MyEvent.DateHeure;
            MySuiviValeur.Symbol = MyEvent.Symbol;
            MySuiviValeur.valeur = MyEvent.valeur;
            return MySuiviValeur;
            /*
            MyEvent.Symbol = Symbol;
            MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
            MyEvent.RSI14 = RSI14;
            MyEvent.RSI5 = RSI5;
            MyEvent.valeur = prix;
            MyEvent.typeintervaltime = Interval;
        */
        }

        //creation ou non des trades ? suivant les methodes et suivi
        public void CreateAllPotentialTrades (Model1Container MyDb ,Evenement MyEvent, string codeMethodeTriggertrade )
        {
            //Model1Container db = new Model1Container();
            //on va boucler sur les suiviTrade, voir si il ya 5 max, si c'est le cas on cree
            //MethodeSuivi MyMethodeSuivi = new MethodeSuivi();
            List<MethodeSuivi> MylistSuivi = MyDb.MethodeSuiviSet.ToList();
            
            foreach (var MyMethodeSuivi in MylistSuivi)

               // MyMethodeSuivi.codemethodesuivi)//
            {
                
                string Mycodemethodesuivi = MyMethodeSuivi.codemethodesuivi;
                List<MethodeSortie> MylistSortie = MyDb.MethodeSortieSet.ToList();
                foreach (var MyMethodeSortie in MylistSortie)
                {
                    string Mycodemethodesortie = MyMethodeSortie.codemethodesortie;
                    int tradeENCOURSALLSYMBOL = MyDb.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN"
                    && TradeSet.CodeMethodeTriggerTrade == codeMethodeTriggertrade &&
                    TradeSet.CodeMethodeSuivi == Mycodemethodesuivi
                    && TradeSet.CodeMethodeSortie == Mycodemethodesortie
                    ).Count();// il faudra ajouter les futurs statuts également
                    if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                    {
                        int tradedejaencours = MyDb.TradeSet.Where(TradeSet => TradeSet.Symbol == MyEvent.Symbol && TradeSet.Statut == "OPEN" 
                        && TradeSet.CodeMethodeTriggerTrade == codeMethodeTriggertrade
                        && TradeSet.CodeMethodeSuivi == Mycodemethodesuivi
                         && TradeSet.CodeMethodeSortie == Mycodemethodesortie
                        ).Count();
                        if (tradedejaencours == 0)
                        {

                            MyDb.TradeSet.Add(InitializeTrade(MyEvent, codeMethodeTriggertrade, Mycodemethodesuivi,Mycodemethodesortie));
                        }
                    }
                }

            }

            /*
            int tradeENCOURSALLSYMBOL = MyDb.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == codeMethodeTriggertrade).Count();// il faudra ajouter les futurs statuts également
            if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
            {
                int tradedejaencours = MyDb.TradeSet.Where(TradeSet => TradeSet.Symbol == MyEvent.Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == codeMethodeTriggertrade).Count();
                if (tradedejaencours == 0)
                {

                    MyDb.TradeSet.Add(InitializeTrade(MyEvent, codeMethodeTriggertrade));
                }
            }
            */

        }


    }
}
