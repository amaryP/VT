using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using statisticsmatch;
using WindowsFormsApp1;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Data = Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Windows.Forms.VisualStyles;
using System.Security.Policy;
using System.Runtime.InteropServices;
using System.Web.Compilation;
using System.ComponentModel;
using System.Globalization;
using System.Configuration;

namespace ServiceCritere
{
    class Criteres
    {
        public string get { get; set; }
        public Critere titre = new Critere();// { get; set; }
        public Critere commentaire = new Critere();//{ get; set; }
        public Critere valeurlay = new Critere();//{ get; set; }
        public Critere round = new Critere();//{ get; set; }
        public Critere idmatch = new Critere();//{ get; set; }
        public Critere nom_championnat = new Critere();//{ get; set; }
        public Critere idteamhome = new Critere();//{ get; set; }
        public Critere nameteamhome = new Critere();//{ get; set; }
        public Critere idteamaway = new Critere();//{ get; set; }
        public Critere nameteamaway = new Critere();//{ get; set; }
        public Critere json = new Critere();//{ get; set; }
        public Critere typelay = new Critere();//{ get; set; }  //"Lay 0-0",
        public Critere statzerozerosaisonencours = new Critere();// { get; set; } //"statistique saison en cours (>1.5)",
        public Critere statnulssaisonencours = new Critere();//{ get; set; } //"statistique saison en cours <3)",
        public Critere score = new Critere();// { get; set; }     //  "Score de 0-0 ",
        public Critere score_domicile = new Critere();// { get; set; }
        public Critere score_exterieure = new Critere();//{ get; set; }
        public Critere nb_cartons_rouges = new Critere();//{ get; set; } //    "nombre de cartons rouges",
        public Critere nbdomicilezerozero10derniersmatchs = new Critere();// { get; set; }//  "Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
        public Critere nbexterieurezerozero10derniersmatchs = new Critere();//{ get; set; }   //     "Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
        public Critere nbdomicilezerozero5derniersmatchs = new Critere();//{ get; set; } //       "Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
        public Critere nbexterieurezerozero5derniersmatchs = new Critere();//{ get; set; } //        "Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
        public Critere nbdomicilenuls10derniersmatchs = new Critere();//{ get; set; }//  "Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
        public Critere nbexterieurenuls10derniersmatchs = new Critere();//{ get; set; }   //     "Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
        public Critere nbdomicilenuls5derniersmatchs = new Critere();//{ get; set; } //       "Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
        public Critere nbexterieurenuls5derniersmatchs = new Critere();//{ get; set; } //        "Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",

        public Critere domicileratiobutmis = new Critere();// { get; set; } //"Equipe domicile ratio but marqués (=>1)",
        public Critere domicileratiobutpris = new Critere();//{ get; set; } //       "Equipe domicile ratio but encaissés (=>1)",
        public Critere exterieureratiobutmis = new Critere();// { get; set; } //"Equipe exterieure ratio but marqués (=>1)",
        public Critere exterieureratiobutpris = new Critere();//{ get; set; } //   "Equipe exterieure ratio but encaissés (=>1)",
        public Critere nbmatchzerozerojourneechampionnatencours = new Critere();//{ get; set; } //  "Resultat journée de championnat en cours nombre de matchs à 0-0",
        public Critere nbmatchnulsjourneechampionnatencours = new Critere();//{ get; set; } //  "Resultat journée de championnat en cours nombre de matchs à 0-0",
        public Critere nbdomicilezerozerodepuisdebutsaison = new Critere();// { get; set; } // "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
        public Critere nbmatchzerozero3dernieresjournees = new Critere();//nombre de match zerozero 3 dernieres journées de championnats
        public Critere nbexterieurezerozerodepuisdebutsaison = new Critere();//{ get; set; }  //        "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",
        public Critere nbdomicilenulsdepuisdebutsaison = new Critere();//{ get; set; } // "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
        public Critere nbexterieurenulsdepuisdebutsaison = new Critere();//{ get; set; }  //        "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",

        public Critere nbdomiciletirs = new Critere();//{ get; set; } //"Equipe domicile nombre de tirs /   
        public Critere nbexterieuretirs = new Critere();// { get; set; }   ///Equipe exterieure nombre de tirs ",

        public Critere equipedevant = new Critere();//{ get; set; }
        public Critere equipederriere = new Critere();//{ get; set; }
        public Critere equipedevantratiobutmis = new Critere();//{ get; set; } //"Equipe qui mene /   
        public Critere equipedevantratiobutpris = new Critere();//{ get; set; } //"Equipe qui mene /   
        public Critere equipederriereratiobutmis = new Critere();// { get; set; } //"Equipe qui mene /   
        public Critere equipederriereratiobutpris = new Critere();//{ get; set; } //"Equipe qui mene /   
        public Critere ratiotirmatch = new Critere();//{ get; set; }   ///calcul du ratio

        public Critere gonogo = new Critere(); //rendu global de l'evaluation GO or NOGO

        //"Valeur du lay (<4)", 
        public Form1 FormZ = new Form1();
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public Criteres()
        {
            //
        }
        public Criteres(string idmatch_in, string json_in, string championnat_in)
        {
            
            json_in = json_in.Replace("null", "0");  //valeur non define = zero dans le contexte, sinon ca explose.
            json_in = json_in.Replace("Passes %", "Passes");  //probleme sur Passes %, car % est un caractere reservé en c#
            var obj = JsonConvert.DeserializeObject<RootobjectStatistics>(json_in.Replace(" ", "")); //ppff c'est ca... il pouvait pas matcher a cause des espaces
                                                                                                     //  oblist = new List<object>()
                                                                                                     //analyse en premier le score
            string Match_pariable = "NON";
            
            //controle preliminaire (est ce que le match est dans une configuration lay admises
            if ((obj.api.fixtures[0].goalsHomeTeam == 0) && (obj.api.fixtures[0].goalsAwayTeam == 0)) //cas 0-0
            {
                Match_pariable = "OUI";
                this.typelay.value = "0-0";
            }
            if ((obj.api.fixtures[0].goalsHomeTeam == 1) && (obj.api.fixtures[0].goalsAwayTeam == 0)) //cas 1-0
            {
                Match_pariable = "OUI";
                this.typelay.value = "1-0";
            }
            if ((obj.api.fixtures[0].goalsHomeTeam == 0) && (obj.api.fixtures[0].goalsAwayTeam == 1)) //cas 0-1
            {
                Match_pariable = "OUI";
                this.typelay.value = "0-1";
            }
/*
#if DEBUG
            Match_pariable = "OUI";
            this.typelay.value = "1-0";


# endif
*/

            //pour le moment on va signaler tous les matchs candidats, sans tenir compte des cartons rouges, que l'on va par contre signaler dans l'evenement
            if (Match_pariable == "OUI")
            {
                //string idmatch, string feuillet, string teamhome, int scorehome,int scoreaway, string teamaway,int cartonrouge)
                this.idmatch.value= idmatch_in;
                this.nom_championnat.value = championnat_in;
                this.round.value = obj.api.fixtures[0].round;
                this.idteamhome.value = obj.api.fixtures[0].homeTeam.team_id.ToString();
                this.nameteamhome.value = obj.api.fixtures[0].homeTeam.team_name;
                this.idteamaway.value = obj.api.fixtures[0].awayTeam.team_id.ToString();
                this.nameteamaway.value = obj.api.fixtures[0].awayTeam.team_name;
                if (this.typelay.value == "0-0") { this.statzerozerosaisonencours.value = GetvaluefromStat_saison_encoursFromAppSettingorCell(this.nom_championnat.value, "RATIO_ZEROZERO"); }
                if ((this.typelay.value == "0-1") || (this.typelay.value == "1-0")) { this.statnulssaisonencours.value = GetvaluefromStat_saison_encoursFromAppSettingorCell(this.nom_championnat.value, "RATIO_NULS"); }
                  
                this.score.value = obj.api.fixtures[0].score.halftime;
                this.score_domicile.value = obj.api.fixtures[0].goalsHomeTeam.ToString();
                this.score_exterieure.value = obj.api.fixtures[0].goalsAwayTeam.ToString();
                this.nb_cartons_rouges.value = ((int.Parse(obj.api.fixtures[0].statistics.RedCards.home.ToString()) + int.Parse(obj.api.fixtures[0].statistics.RedCards.away.ToString())).ToString());

                if (this.typelay.value == "0-0")
                {
                    this.nbdomicilezerozero10derniersmatchs.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "NB_ZEROZERO_10DMATCHS");
                    this.nbexterieurezerozero10derniersmatchs.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "NB_ZEROZERO_10DMATCHS");
                    this.nbdomicilezerozero5derniersmatchs.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "NB_ZEROZERO_5DMATCHS");
                    this.nbexterieurezerozero5derniersmatchs.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "NB_ZEROZERO_5DMATCHS");
                    this.nbmatchzerozerojourneechampionnatencours.value = "non renseigné";//fonction a creer (on a pas vraiment l'info pour le moment"NB_ZEROZERO
                    this.nbmatchzerozero3dernieresjournees.value = Getvaluefromchampionnatsleagues("adefinir", this.nom_championnat.value, "M");
                    this.nbdomicilezerozerodepuisdebutsaison.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "NB_ZEROZERO");
                    this.nbexterieurezerozerodepuisdebutsaison.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "NB_ZEROZERO");

                }
                this.domicileratiobutmis.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "MOYENNE_BUTS_MARQUES");
                this.domicileratiobutpris.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "MOYENNE_BUTS_ENCAISSES");
                this.exterieureratiobutmis.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "MOYENNE_BUTS_MARQUES");
                this.exterieureratiobutpris.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "MOYENNE_BUTS_ENCAISSES");
                
                this.nbdomiciletirs.value = (obj.api.fixtures[0].statistics.TotalShots.home.ToString());
                this.nbexterieuretirs.value = (obj.api.fixtures[0].statistics.TotalShots.away.ToString());

                if ((this.typelay.value == "0-1") || (this.typelay.value == "1-0"))
                {
                    if (int.Parse(this.score_domicile.value)>int.Parse(this.score_exterieure.value))
                    {
                        this.equipedevant.value = this.idteamhome.value;
                        this.equipederriere.value = this.idteamaway.value;
                        this.equipedevantratiobutmis.value = this.domicileratiobutmis.value;
                        this.equipedevantratiobutpris.value = this.domicileratiobutpris.value;
                        this.equipederriereratiobutmis.value = this.exterieureratiobutmis.value;
                        this.equipederriereratiobutpris.value = this.exterieureratiobutpris.value;
                    }
                    else
                    { 
                        this.equipedevant.value = this.idteamaway.value; 
                        this.equipederriere.value = this.idteamhome.value;
                        this.equipedevantratiobutmis.value = this.exterieureratiobutmis.value;
                        this.equipedevantratiobutpris.value = this.exterieureratiobutpris.value;
                        this.equipederriereratiobutmis.value = this.domicileratiobutmis.value;
                        this.equipederriereratiobutpris.value = this.domicileratiobutpris.value;
                    }

                    this.nbdomicilenuls10derniersmatchs.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "NB_NUL_10DMATCHS");
                    this.nbexterieurenuls10derniersmatchs.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "NB_NUL_10DMATCHS");
                    this.nbdomicilenuls5derniersmatchs.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "NB_NUL_5DMATCHS");
                    this.nbexterieurenuls5derniersmatchs.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "NB_NUL_5DMATCHS");
                    this.nbmatchnulsjourneechampionnatencours.value = "non renseigné";//fonction a creer (on a pas vraiment l'info pour le moment"       
                    this.nbdomicilenulsdepuisdebutsaison.value = Getvaluefromleague_teams(this.idteamhome.value, this.nom_championnat.value, "NB_NULS");
                    this.nbexterieurenulsdepuisdebutsaison.value = Getvaluefromleague_teams(this.idteamaway.value, this.nom_championnat.value, "NB_NULS");

                }
                Console.WriteLine("Nouvelle instance CelluleSheet créée.");
                //string SheetServiceId,
                //on va commencer par aller recuperer l'ensemble des infos...donc pour le moment on est pas en full dynamique ? il faut table de reference quelquepart...autant que ca soit ici...
                //meme pas besoin d'aller chercher l'info dans une feuille pour le moment, il faut forcement l'avoir quelquepart...donc bon.suffisant ici (on va essayer d'utiliser le meme modele pour
                //l'ensemble des championnats, pas de raisons que cela soit different.
                //idclasseur = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
                //nom_championnat = feuillet_championnat;
                //feuillet = feuillet_championnat;
                //a mettre en place la recherche en dynamique. on placera les libelles a rechercher dans un ensemble ["",""] à mixer avec la fonction de recherche deja developpé.

            } //fin du if match pariable

            //besoin d'une fonction qui retourne la ligne de l'equipe (en recupant l'id ligne) ==> en fait c'est cette fonction..
            //Form1 FormZ = new Form1();
            // FormZ.GetRefCelluleId(FormZ.service,"","");
            // critere.GetColonneID("teams", "Ligue1", "NB_ZEROZERO_5DMATCHS")
            //a partir de la, je peux avoir une fonction pour chaque valeur...

            //    

            //besoin d'une fonction qui retourne la colonne en se basant sur le nom de la colonne..si on se base direct sur les coordonnées trop dangereux...

            //FormZ.create_fiche_match(this.idmatch.value, this, this.nom_championnat.value);
            ////PopulateCritere(this);
            //Console.WriteLine(this.nbexterieurenuls5derniersmatchs.title);

        }
        public void EvaluationCritere(Criteres criteres_in)
        {
            //cartons rouges
            int valeurreferenceint = 0;
            int valeurencoursint = int.Parse(this.nb_cartons_rouges.value.Replace(",", "."));
            if (valeurencoursint== valeurreferenceint) { this.nb_cartons_rouges.evaluation = "OUI"; }
            else { this.nb_cartons_rouges.evaluation = "NON"; }

            this.gonogo.value = "NON";
            //type lay 0-0
            if (this.typelay.value == "0-0")
                {
                if (this.score.value == "0-0") { this.typelay.evaluation = "OUI"; this.gonogo.value = "OUI"; }
                else { this.typelay.evaluation = "NON"; }
            }
            if ((this.typelay.value == "0-1") || (this.typelay.value == "1-0"))
            {
                if ((this.score.value == "0-1") || (this.score.value == "1-0")) { this.typelay.evaluation = "OUI"; this.gonogo.value = "OUI"; }
                else { this.typelay.evaluation = "NON"; }
            }

            if (this.typelay.value == "0-0")
            {
                //calcul en dur...mefiance
                string valeursaisonencours = this.statzerozerosaisonencours.value.Replace("%", "");
                valeursaisonencours = valeursaisonencours.Replace(",", ".");
                double valeurreference = 1.5;
                double valeursaisonencoursfloat = Double.Parse(valeursaisonencours, CultureInfo.InvariantCulture.NumberFormat);
                if (valeursaisonencoursfloat> valeurreference) { this.statzerozerosaisonencours.evaluation = "OUI"; }
                else { this.statzerozerosaisonencours.evaluation = "NON"; this.gonogo.value = "NON"; }
                ////Equipe domicile ratio but marqués(=>1)         
                valeurreference = 1;
                double valeurencours = Double.Parse(this.domicileratiobutmis.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat); //.Replace(",", "."));
                if (valeurencours >= valeurreference) { this.domicileratiobutmis.evaluation = "OUI"; }
                else { this.domicileratiobutmis.evaluation = "NON"; this.gonogo.value = "NON"; }
                //Equipe domicile ratio but encaissés(=>1)              
                valeurreference = 1;
                valeurencours = Double.Parse(this.domicileratiobutpris.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.domicileratiobutpris.evaluation = "OUI"; }
                else { this.domicileratiobutpris.evaluation = "NON"; this.gonogo.value = "NON"; }
                //Equipe exterieure ratio but marqués(=>1)              
                valeurreference = 1;
                valeurencours = Double.Parse(this.exterieureratiobutmis.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.exterieureratiobutmis.evaluation = "OUI"; }
                else { this.exterieureratiobutmis.evaluation = "NON"; this.gonogo.value = "NON"; }
                //Equipe exterieure ratio but encaissés(=>1)             
                valeurreference = 1;
                valeurencours = Double.Parse(this.exterieureratiobutpris.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.exterieureratiobutpris.evaluation = "OUI"; }
                else { this.exterieureratiobutpris.evaluation = "NON"; this.gonogo.value = "NON"; }
                
            }

            if ((this.typelay.value == "0-1") || (this.typelay.value == "1-0"))
            {
                //calcul en dur...mefiance
                string valeursaisonencours = this.statnulssaisonencours.value.Replace("%", "");
                valeursaisonencours = valeursaisonencours.Replace(",", ".");
                double valeurreference = 3;
                double valeursaisonencoursfloat = Double.Parse(valeursaisonencours.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeursaisonencoursfloat < valeurreference) { this.statnulssaisonencours.evaluation = "OUI"; }
                else { this.statnulssaisonencours.evaluation = "NON"; this.gonogo.value = "NON"; }
                //equipe qui mene but mis
                valeurreference = 1.5;//2
                double valeurencours = Double.Parse(this.equipedevantratiobutmis.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.equipedevantratiobutmis.evaluation = "OUI"; }
                else { this.equipedevantratiobutmis.evaluation = "NON"; this.gonogo.value = "NON"; }
                //Equipe qui mene but encaissés  =>1
                valeurreference = 1;
                valeurencours = Double.Parse(this.equipedevantratiobutpris.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.equipedevantratiobutpris.evaluation = "OUI"; }
                else { this.equipedevantratiobutpris.evaluation = "NON"; this.gonogo.value = "NON"; }
                //Equipe menée but marqués(=>1)              
                valeurreference = 1;
                valeurencours = Double.Parse(this.equipederriereratiobutmis.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.equipederriereratiobutmis.evaluation = "OUI"; }
                else { this.equipederriereratiobutmis.evaluation = "NON"; this.gonogo.value = "NON"; }
                //Equipe menée but encaissés(=>1.5)             //2
                valeurreference = 1.5;
                valeurencours = Double.Parse(this.equipederriereratiobutpris.value.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                if (valeurencours >= valeurreference) { this.equipederriereratiobutpris.evaluation = "OUI"; }
                else { this.equipederriereratiobutpris.evaluation = "NON"; this.gonogo.value = "NON"; }
            }
            this.gonogo.evaluation = this.gonogo.value;
        }


            public void PopulateCritere(Criteres criteres_in)
        {
           // titre et  valaleur order d'affichage
            int zorder = 0;
            criteres_in.titre.title = "Critères";
            criteres_in.titre.orderby = zorder++;
            criteres_in.statzerozerosaisonencours.title = "statistique saison en cours (>1.5)";
            criteres_in.statzerozerosaisonencours.reference = ">1.5";
            criteres_in.statzerozerosaisonencours.orderby = zorder++;
            criteres_in.statnulssaisonencours.title = "statistique saison en cours inferieure (<3)";
            criteres_in.statnulssaisonencours.reference = "<3";
            criteres_in.statnulssaisonencours.orderby = zorder++;

            criteres_in.score.title = "Score à la mi-temps";
            criteres_in.score.orderby = zorder++;
            criteres_in.nb_cartons_rouges.title = "Nombre de carton(s) rouge(s) à la mi-temps";
            criteres_in.nb_cartons_rouges.orderby = zorder++;
            criteres_in.nb_cartons_rouges.reference = "=0";
              if (criteres_in.typelay.value == "0-0")
            {
                criteres_in.nbdomicilezerozero10derniersmatchs.title = "Equipe domicile matchs à 0-0 sur les 10 derniers matchs";
                criteres_in.nbdomicilezerozero10derniersmatchs.orderby = zorder++;
                criteres_in.nbexterieurezerozero10derniersmatchs.title = "Equipe exterieure matchs à 0-0 sur les 10 derniers matchs";
                criteres_in.nbexterieurezerozero10derniersmatchs.orderby = zorder++;
                criteres_in.nbdomicilezerozero5derniersmatchs.title = "Equipe domicile matchs à 0-0 sur les 5 derniers matchs";
                criteres_in.nbdomicilezerozero5derniersmatchs.orderby = zorder++;
                criteres_in.nbexterieurezerozero5derniersmatchs.title = "Equipe exterieure matchs à 0-0 sur les 5 derniers matchs";
                criteres_in.nbexterieurezerozero5derniersmatchs.orderby = zorder++;
                criteres_in.domicileratiobutmis.title = "Equipe domicile ratio but marqués (=>1)";
                criteres_in.domicileratiobutmis.reference="=>1";
                criteres_in.domicileratiobutmis.orderby = zorder++;
                criteres_in.domicileratiobutpris.title = "Equipe domicile ratio but encaissés (=>1)";
                criteres_in.domicileratiobutpris.reference = "=>1";
                criteres_in.domicileratiobutpris.orderby = zorder++;
                criteres_in.exterieureratiobutmis.title = "Equipe exterieure ratio but marqués (=>1)";
                criteres_in.exterieureratiobutmis.reference = "=>1";
                criteres_in.exterieureratiobutmis.orderby = zorder++;
                criteres_in.exterieureratiobutpris.title = "Equipe exterieure ratio but encaissés (=>1)";
                criteres_in.exterieureratiobutpris.reference = "=>1";
                criteres_in.valeurlay.title = "Valeur du lay (<4)";
                criteres_in.valeurlay.reference = "<4";
            }
            if ((criteres_in.typelay.value == "0-1") || (criteres_in.typelay.value == "1-0"))
            {
                criteres_in.nbdomicilenuls10derniersmatchs.title = "Equipe domicile matchs nuls sur les 10 derniers matchs";
                criteres_in.nbdomicilenuls10derniersmatchs.orderby = zorder++;
                criteres_in.nbexterieurenuls10derniersmatchs.title = "Equipe exterieure matchs nuls sur les 10 derniers matchs";
                criteres_in.nbexterieurenuls10derniersmatchs.orderby = zorder++;
                criteres_in.nbdomicilenuls5derniersmatchs.title = "Equipe domicile matchs nuls sur les 5 derniers matchs";
                criteres_in.nbdomicilenuls5derniersmatchs.orderby = zorder++;
                criteres_in.nbexterieurenuls5derniersmatchs.title = "Equipe exterieure matchs nuls sur les 5 derniers matchs";
                criteres_in.nbexterieurenuls5derniersmatchs.orderby = zorder++;

                criteres_in.equipedevantratiobutmis.title = "Pour l'équipe qui mene 1-0 à minima >1.5/2 but marqués";
                criteres_in.equipedevantratiobutmis.reference = "=>1.5";
                criteres_in.equipedevantratiobutmis.orderby = zorder++;
                criteres_in.equipedevantratiobutpris.title = "Pour l'équipe qui mene 1-0 à minima =>1 but encaissés";
                criteres_in.equipedevantratiobutpris.reference = "=>1";
                criteres_in.equipedevantratiobutpris.orderby = zorder++;
                criteres_in.equipederriereratiobutmis.title = "Pour l'équipe qui est menée 1-0 à minima =>1  but marqués";
                criteres_in.equipederriereratiobutmis.reference = "=>1";
                criteres_in.equipederriereratiobutmis.orderby = zorder++;
                criteres_in.equipederriereratiobutpris.title = "Pour l'équipe qui est menée 1-0 à minima >1.5/2 but encaissés";
                criteres_in.equipederriereratiobutpris.reference = ">=1.5";
                criteres_in.equipederriereratiobutpris.orderby = zorder++;
                criteres_in.valeurlay.title = "Valeur du lay (<4.3)";//4.2 . ou 4.3 ?
                 criteres_in.valeurlay.reference = "<4.3"; //4.2 . ou 4.3 ?
            }

            criteres_in.domicileratiobutmis.title = "Equipe domicile ratio but marqués (=>1)";

            /*
            criteres_in.domicileratiobutmis.orderby = zorder++;
            criteres_in.domicileratiobutpris.title = "Equipe domicile ratio but encaissés (=>1)";
            criteres_in.domicileratiobutpris.orderby = zorder++;
            criteres_in.exterieureratiobutmis.title = "Equipe exterieure ratio but marqués (=>1)";
            criteres_in.exterieureratiobutmis.orderby = zorder++;
            criteres_in.exterieureratiobutpris.title = "Equipe exterieure ratio but encaissés (=>1)";
            criteres_in.exterieureratiobutpris.orderby = zorder++;
            */
            criteres_in.nbmatchzerozerojourneechampionnatencours.title = "Resultat journée de championnat en cours nombre de matchs à 0-0";
            criteres_in.nbmatchzerozerojourneechampionnatencours.orderby = zorder++;

            criteres_in.nbmatchzerozero3dernieresjournees.title = "Nombre de 0-0 au cours des 3 dernieres journées";
            criteres_in.nbmatchzerozero3dernieresjournees.orderby = zorder++;

            criteres_in.nbdomicilezerozerodepuisdebutsaison.title = "Equipe domicile nombre de matchs à 0-0 depuis debut de saison";
            criteres_in.nbdomicilezerozerodepuisdebutsaison.orderby= zorder++;
            criteres_in.nbexterieurezerozerodepuisdebutsaison.title = "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison";
            criteres_in.nbexterieurezerozerodepuisdebutsaison.orderby = zorder++;

            criteres_in.nbmatchnulsjourneechampionnatencours.title = "Resultat journée de championnat en cours nombre de matchs nuls";
            criteres_in.nbmatchnulsjourneechampionnatencours.orderby = zorder++;
            criteres_in.nbdomicilenulsdepuisdebutsaison.title = "Equipe domicile nombre de matchs nuls depuis debut de saison";
            criteres_in.nbdomicilenulsdepuisdebutsaison.orderby = zorder++;
            criteres_in.nbexterieurenulsdepuisdebutsaison.title = "Equipe exterieure nombre de matchs nuls depuis debut de saison";
            criteres_in.nbexterieurenulsdepuisdebutsaison.orderby = zorder++;

            criteres_in.ratiotirmatch.title = "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ";
            //pas vraiment de critere precis en terme d'equilibe ou déséquilibre...
            criteres_in.ratiotirmatch.orderby = zorder++;
            criteres_in.typelay.title = "Type Lay";
            //criteres_in.valeurlay.title = "Valeur du lay";
            
            criteres_in.valeurlay.orderby = zorder++;
            if (criteres_in.typelay.value == "0-0")
            {
                criteres_in.commentaire.title = " il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 ";
                criteres_in.commentaire.orderby= zorder++;
            }
            if ((criteres_in.typelay.value == "0-1") || (criteres_in.typelay.value == "1-0"))
            {
                criteres_in.commentaire.title = "Le critere matchs nul sur la journée en cours est ici moins important que dans la strategie des lay 0-0";
                criteres_in.commentaire.orderby = zorder++;
            }
            //reference
            criteres_in.gonogo.title = "Evaluation GO / NOGO";
        }

        public string Getvaluefromleague_teams(string idteamhome,string championnat,string colonne_in)
        {
            string line = "";
            string colonne = "";
            string range = "";
            string retour = "";
            //"NB_ZEROZERO_10DMATCHS"
            line =FormZ.GetRefCelluleId(FormZ.service,championnat + "_teams", idteamhome);
            colonne=GetColonneID("teams", championnat, colonne_in);
            range=colonne+line;
            championnat = championnat + "_teams";
            //Console.WriteLine(GetValuecell(championnat, range));
            retour = GetValuecell(championnat, range);
            //return int.Parse(GetValuecell(championnat, range));
            return retour;
        }

        public string Getvaluefromchampionnatsleagues(string idleague, string championnat, string colonne_in)
        {
            //if idleague="a definir"
            if (championnat== "Ligue1") { idleague = "2664";}
            if (championnat == "Ligue2") { idleague = "2652"; }
            if (championnat == "Bundesliga") { idleague = "2755"; }
            if (championnat == "Bundesliga2") { idleague = "2743"; }
            if (championnat == "Premier_league") { idleague = "2790"; }
            if (championnat == "Championship") { idleague = "2794"; }
            if (championnat == "SerieA") { idleague = "2857"; }
            if (championnat == "SerieB") { idleague = "2946"; }
            if (championnat == "LaLiga") { idleague = "2833"; }
            if (championnat == "LaLiga2") { idleague = "2847"; }

            //a modifier car appel de la colonne en dur...
            string line = "";
            string colonne = colonne_in;
            string range = "";
            string retour = "";
            //"NB_ZEROZERO_10DMATCHS"
            line = FormZ.GetRefCelluleId(FormZ.service, "championnatsleagues", idleague);
            //colonne = GetColonneID("championnatsleagues", championnat, colonne_in);
            range = colonne + line;
            //championnat = championnat + "_teams";
            //Console.WriteLine(GetValuecell(championnat, range));
            retour = GetValuecell("championnatsleagues", range);
            //return int.Parse(GetValuecell(championnat, range));
            return retour;
        }

        public string GetvaluefromStat_saison_encours(string championnat, string colonne_in)
        {
            //string line = "";
            //string colonne = "";
            string range = "";
            string retour = "";
            //coder en dur... feuillet figé. mefiance
            //on va verifier si on a pas deja lu et stocker en local dans le fichier config ?
            //bon peut etre que ca peux tourner sans souci ces requetes sur google ?

            if (championnat=="Ligue1")
                {

                if (colonne_in=="RATIO_NULS") { range = "D18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "D14"; }
            }

            if (championnat == "Ligue2")
            {
                if (colonne_in == "RATIO_NULS") { range = "E18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "E14"; }
            }

            if (championnat == "Bundesliga")
            {
                if (colonne_in == "RATIO_NULS") { range = "F18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "F14"; }
            }

            if (championnat == "Bundesliga2")
            {
                if (colonne_in == "RATIO_NULS") { range = "G18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "G14"; }
            }

            if (championnat == "Premier_league")
            {
                if (colonne_in == "RATIO_NULS") { range = "B18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "B14"; }
            }

            if (championnat == "Championship")
            {
                if (colonne_in == "RATIO_NULS") { range = "C18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "C14"; }
            }

            if (championnat == "SerieA")
            {
                if (colonne_in == "RATIO_NULS") { range = "H18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "H14"; }
            }

            if (championnat == "SerieB")
            {
                if (colonne_in == "RATIO_NULS") { range = "I18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "I14"; }
            }

            if (championnat == "LaLiga")
            {
                if (colonne_in == "RATIO_NULS") { range = "J18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "J14"; }
            }

            if (championnat == "LaLiga2")
            {
                if (colonne_in == "RATIO_NULS") { range = "K18"; }
                if (colonne_in == "RATIO_ZEROZERO") { range = "K14"; }
            }

            //championnat = championnat + "_teams";
            //Console.WriteLine(GetValuecell(championnat, range));
            retour = GetValuecell("Stats saison en cours", range);
            //return int.Parse(GetValuecell(championnat, range));
            return retour;
        }

        public string GetvaluefromStat_saison_encoursFromAppSettingorCell(string championnat, string colonne_in)
        {
            string retour = "KO";
            string ratio_courant = "";
           // "RATIO_NULS") 
           // "RATIO_ZEROZERO") 
           ///raz sur les stats de maj championnats that' all... encore plus simple non ?
            //si ca commence par analyse les chaines _ alors on recupere le reste de la chaine, si on a une serie de 6 chiffes alors on l'extrait et on remove les chaines.
            for (int i = 0; i < ConfigurationManager.AppSettings.Keys.Count;)
            {
                if (ConfigurationManager.AppSettings.Keys[i].Contains(colonne_in+"_" +championnat) == true)
                {
                   retour = ConfigurationManager.AppSettings[colonne_in + "_" + championnat]; // est ce que la valeur existe et est un numerique
                }
                i++;
            }
            if (retour=="KO") // dans le cas contraire on  recupere la valeur et on la stocke dans l'app avant de la retourner.
            {
                ratio_courant = GetvaluefromStat_saison_encours(championnat, colonne_in);
                config.AppSettings.Settings.Add(colonne_in + "_" + championnat, ratio_courant); 
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                retour = ratio_courant;
            }
            return retour;
        }


        public string GetColonneID(string typefeuillet, string championnat, string nom_colonne)
        {
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet Ligue1
                                                                                  //String range = "Class Data!A2";  // single cell D5
                                                                                  //range par feuillet./championnat ou equipe
                                                                                  //range par feuillet./championnat ou equipe
            String range = "";

            if (typefeuillet == "championnat")
            {
                if (championnat == "Ligue1")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                     //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                     //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "Ligue2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                     //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                     //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "Bundesliga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A307";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                     //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                     //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "Bundesliga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A307";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                     //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                     //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "Premier_league")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                     //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                     //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "Championship")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A553";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                  //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                  //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "SerieA")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                  //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                  //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "SerieB")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                  //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                  //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "LaLiga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                  //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                  //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "LaLiga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble// peut etre pas le meme nombre d'equipe.
                {
                    range = championnat + "!A1:A463";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                  //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                  //String myNewCellValue = "Alexandrie";
                }


            }


      //  }
            if (typefeuillet == "teams")
            {
                if (championnat == "Ligue1")
                {
                    range = championnat + "_teams"+"!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                    //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                    //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "Ligue2")
                {
                    range = championnat+"_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                    //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                    //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "Bundesliga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "Bundesliga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "Premier_league")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "Championship")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "SerieA")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }
                if (championnat == "SerieB")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "LaLiga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }

                if (championnat == "LaLiga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
                {
                    range = championnat + "_teams" + "!A1:Q1";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                              //String myNewCellValue = "Alexandrie";
                }


            }

            if (typefeuillet == "championnatsleagues")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = typefeuillet + "!A1:A11";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (typefeuillet == "Stat saison en cours")  // 
            {
               //a gerer en fonction des championnats
                range = typefeuillet + "!A1:A11";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                                 //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                                 //String myNewCellValue = "Alexandrie";
            }


            ValueRange valueRange = new ValueRange();
            string resultat = "NON TROUVE";
            // SpreadsheetsResource.CreateRequest.
            valueRange.MajorDimension = "ROWS";//"ROWS";//COLUMNS
            Form1 FormZ = new Form1();
            SpreadsheetsResource.ValuesResource.GetRequest quest = FormZ.service.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange recupQuest = quest.Execute();

            IList<IList<Object>> values = recupQuest.Values;

            //int i;
           // i = 0;
            int z = 1;
            string s1;
            string s2 = nom_colonne;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    //
                    for (int i = 0; i < row.Count; i++)
                    {

                        // Console.WriteLine(row[i].ToString());
                        s1 = row[i].ToString();

                        if (FormZ.IsFind(s1, s2, z) == true)
                        {
                            resultat = String.Concat("", i);// Convert.ToString(i); 
                            z = i+1;
                        }
                    }

                   // z = z + 1;
                }

            }
            //Console.WriteLine(recupQuest.Values[0].ToString());
            resultat= Getalphabeticnotation(z.ToString());

      return resultat;
        }
        public string Getalphabeticnotation(string index_in)
        {
            string resultat = "";
            //faut de mieux c'est à l'arrache mais bon...
            if (index_in=="1") { resultat = "A"; }
            if (index_in == "2") { resultat = "B"; }
            if (index_in == "3") { resultat = "C"; }
            if (index_in == "4") { resultat = "D"; }
            if (index_in == "5") { resultat = "E"; }
            if (index_in == "6") { resultat = "F"; }
            if (index_in == "7") { resultat = "G"; }
            if (index_in == "8") { resultat = "H"; }
            if (index_in == "9") { resultat = "I"; }
            if (index_in == "10") { resultat = "J"; }
            if (index_in == "11") { resultat = "K"; }
            if (index_in == "12") { resultat = "L"; }
            if (index_in == "13") { resultat = "M"; }
            if (index_in == "14") { resultat = "N"; }
            if (index_in == "15") { resultat = "O"; }
            if (index_in == "16") { resultat = "P"; }
            if (index_in == "17") { resultat = "Q"; }
            if (index_in == "18") { resultat = "R"; }
            if (index_in == "19") { resultat = "S"; }
            if (index_in == "20") { resultat = "T"; }
            if (index_in == "21") { resultat = "U"; }
            if (index_in == "22") { resultat = "V"; }
            if (index_in == "23") { resultat = "W"; }
            if (index_in == "24") { resultat = "X"; }
            if (index_in == "25") { resultat = "Y"; }
            if (index_in == "26") { resultat = "Z"; }
            if (index_in == "27") { resultat = "AA"; }
            if (index_in == "28") { resultat = "AB"; }
            if (index_in == "29") { resultat = "AC"; }

            return resultat;

        }

     

        public string GetValuecell(string feuillet, string range_in )
        {
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet Ligue1
                                                                                  //String range = "Class Data!A2";  // single cell D5
                                                                                  //range par feuillet./championnat ou equipe
                                                                                  //range par feuillet./championnat ou equipe
            String range = "";
            range = feuillet + "!"+range_in+":"+range_in;
            ValueRange valueRange = new ValueRange();
            string resultat = "NON TROUVE";
            // SpreadsheetsResource.CreateRequest.
            valueRange.MajorDimension = "ROWS";//"ROWS";//COLUMNS
            Form1 FormZ = new Form1();
            SpreadsheetsResource.ValuesResource.GetRequest quest = FormZ.service.Spreadsheets.Values.Get(spreadsheetId, range);
            ValueRange recupQuest = quest.Execute();
            IList<IList<Object>> values = recupQuest.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    resultat = row[0].ToString();
                    //Console.WriteLine(row[0].ToString());
                }
            }
            return resultat;
        }
    }

    public class Critere
    {
        public string title { get; set; }
        public string name { get; set; }
        //public string typelay { get; set ; }

        public string id { get; set; }
        public string value { get; set; }
        public string reference { get; set; }
        public string evaluation { get; set; }
        public string evaluationIA { get; set; }
        public string colorcell { get; set; }
        public int orderby { get; set; }
    }


};



	
