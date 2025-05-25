using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Newtonsoft.Json;
using statisticsmatch;
using WindowsFormsApp1;
using ServiceCritere;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Data = Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using ConsoleApp1;


namespace AnalyzeStats
{
    class Analyzestat
    {
        static public void Analyse(string idmatch, string json_in, string championnat)
        {
            Console.WriteLine("analyse stats...");

            string Match_pariable = "NON";
            int carton_rouge = 0;

            string debug = "NON";
            //var oblist = new List<object>();
            //if (MYGlobalVars.APIFOOTBALL == "ALPHA")

            //ok, on va modifier les alertes mitemps en fonction d'un niveau d'alerte 1,2,3,4 etc.
            //deja on va plus alerter si il y a eu un carton rouge. ca fera direct moins d'alertes
            json_in = json_in.Replace("null", "0");  //valeur non define = zero dans le contexte, sinon ca explose.
            json_in = json_in.Replace("Passes %", "Passes");  //probleme sur Passes %, car % est un caractere reservé en c#
            var obj = JsonConvert.DeserializeObject<RootobjectStatistics>(json_in.Replace(" ", "")); //ppff c'est ca... il pouvait pas matcher a cause des espaces
                                                                                                     //  oblist = new List<object>()
            Criteres criteresencours = new Criteres();                                                                                        //analyse en premier le score
            if ((obj.api.fixtures[0].goalsHomeTeam == 0) && (obj.api.fixtures[0].goalsAwayTeam == 0)) //cas 0-0
            {
                Match_pariable = "OUI";
            }
            if ((obj.api.fixtures[0].goalsHomeTeam == 1) && (obj.api.fixtures[0].goalsAwayTeam == 0)) //cas 1-0
            {
                Match_pariable = "OUI";
            }
            if ((obj.api.fixtures[0].goalsHomeTeam == 0) && (obj.api.fixtures[0].goalsAwayTeam == 1)) //cas 0-1
            {
                Match_pariable = "OUI";
            }
            //on analyse si pas de cartons rouge 
            carton_rouge = int.Parse(obj.api.fixtures[0].statistics.RedCards.home.ToString()) + int.Parse(obj.api.fixtures[0].statistics.RedCards.away.ToString());
            {
                if (carton_rouge > 0) { Match_pariable = "NON"; }
            }
/*#if DEBUG
            Match_pariable = "OUI";
# endif
*/
            //pour le moment on va signaler tous les matchs candidats, sans tenir compte des cartons rouges, que l'on va par contre signaler dans l'evenement
            if (Match_pariable == "OUI")
            {
                //   string idmatch, string json_in, string championnat
                //alerte
                Form1 FormZ = new Form1();
                /*
                #if DEBUG
                                debug = "OUI";
                # endif
                */
                 CelluleSheet servicecellulesheet1 = new CelluleSheet();

                if ((FormZ.IsAlerteMatchAlreadyScheduled(idmatch) == false) && (servicecellulesheet1.Isfichematchalreadyexist(FormZ.service, "IDMATCH_" + idmatch)==false)) // || (debug=="OUI")) // on ajout un controle si la fiche match est déjà crée.
                {
                    //on cree le feuillet avant l'alerte, pour avoir le lien hypertexte
                    string destinationSpreadsheetId = "";
                    destinationSpreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";  // TODO: Update placeholder value. 
                    
                    //il faudra un classeur par championnat
                    // TODO: Assign values to desired properties of `requestBody`:

                    var addSheetRequest = new AddSheetRequest();
                    addSheetRequest.Properties = new SheetProperties();
                    addSheetRequest.Properties.Title = "IDMATCH_" + idmatch;
                    BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
                    batchUpdateSpreadsheetRequest.Requests = new List<Request>();
                    batchUpdateSpreadsheetRequest.Requests.Add(new Request { AddSheet = addSheetRequest });
                    var batchUpdateRequest = FormZ.service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, destinationSpreadsheetId);
                    batchUpdateRequest.Execute();

                    //on recupere le sheetid pour calculer le lien hypertexte
                   
                    //get sheet id by sheet name
                    Spreadsheet spr = FormZ.service.Spreadsheets.Get(destinationSpreadsheetId).Execute();
                    Sheet sh = spr.Sheets.Where(s => s.Properties.Title == "IDMATCH_" + idmatch).FirstOrDefault();
                    int sheetId = (int)sh.Properties.SheetId;
                    string url = "https://docs.google.com/spreadsheets/d/1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs/edit#gid=" + sheetId;// 1396179187"

                   // FormZ.AddAlertemitempsmatchoncalendar(idmatch, championnat, obj.api.fixtures[0].homeTeam.team_name, obj.api.fixtures[0].goalsHomeTeam, obj.api.fixtures[0].awayTeam.team_name, obj.api.fixtures[0].goalsAwayTeam, carton_rouge, url);
                    Criteres criteres = new Criteres(idmatch, json_in, championnat);
                    //Console.WriteLine(criteres.nbdomicilezerozero5derniersmatchs.value);
                    //Console.WriteLine(criteres.nbdomicilezerozero5derniersmatchs.title);
                    criteres.PopulateCritere(criteres);
                    //Console.WriteLine(criteres.nbdomicilezerozero5derniersmatchs.title);
                    criteres.EvaluationCritere(criteres);
                    //create_fiche_match(idmatch, criteres, championnat);
                   
                    // The ID of the spreadsheet to copy the sheet to.

                    //Form1 FormZ = new Form1();
                    //creation du feuillet
                    //if (championnat == "Ligue1")   // pour le moment, on utilise le meme classeur pour tous les championnats
                    //batchUpdateRequest.SpreadsheetId
                    //FormZ.service.Spreadsheets.Values.Get.shee
                    //
                    //on devra également preparer l'ensemble des valeurs pour les enregistrer ailleurs.. (passes t on par un objet ???)
                    //clairement, on a des elements a recuperer dans tous les sens...
                    //ecriture des criteres
                    String range = "IDMATCH_" + idmatch + "!A1";  // single cell D5
                    ValueRange valueRange = new ValueRange();
                    valueRange.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                                                          //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                                                          // on fait une pause d'une seconde  
                    System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                        //type lay 0-0
                                                        // if (criteres.typelay.value == "0-0")
                                                        // {

                    var oblistLAY00 = new List<object>()
                 {
                criteres.nameteamhome.value+"-"+criteres.nameteamaway.value+" "+championnat+" "+criteres.round.value,
                criteres.titre.title, //"Critères",
                criteres.typelay.title,  //"Lay 0-0",
                criteres.statzerozerosaisonencours.title,// "statistique saison en cours (>1.5)",
               // criteres.score.title,//"Score de 0-0 ",
                criteres.nb_cartons_rouges.title,//"nombre de cartons rouges",
                criteres.nbdomicilezerozero10derniersmatchs.title,//"Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbexterieurezerozero10derniersmatchs.title,//"Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbdomicilezerozero5derniersmatchs.title,//"Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                criteres.nbexterieurezerozero5derniersmatchs.title,//"Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                criteres.domicileratiobutmis.title,//"Equipe domicile ratio but marqués (=>1)",
                criteres.domicileratiobutpris.title,// "Equipe domicile ratio but encaissés (=>1)",
                criteres.exterieureratiobutmis.title,// "Equipe exterieure ratio but marqués (=>1)",
                criteres.exterieureratiobutpris.title,// "Equipe exterieure ratio but encaissés (=>1)",
                criteres.nbmatchzerozerojourneechampionnatencours.title,//    "Resultat journée de championnat en cours nombre de matchs à 0-0",
                criteres.nbmatchzerozero3dernieresjournees.title,//
                criteres.nbdomicilezerozerodepuisdebutsaison.title,//  "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
                criteres.nbexterieurezerozerodepuisdebutsaison.title,// "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",
                criteres.ratiotirmatch.title,//      "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                criteres.valeurlay.title,// "Valeur du lay (<4)",
                criteres.gonogo.title,
                "",
                "",
                "",
                criteres.commentaire.title // "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "
                // DateTime.Now.ToString()
                };

                    var oblistLAY01 = new List<object>()
                 {
                criteres.nameteamhome.value+"-"+criteres.nameteamaway.value+" "+criteres.round.value,
                criteres.titre.title, //"Critères",
                criteres.typelay.title,  //"Lay 0-1" "0-1",
                criteres.statnulssaisonencours.title,// 
               // criteres.score.title,//"Score de 0-0 ",
                criteres.nb_cartons_rouges.title,//"nombre de cartons rouges",
                criteres.nbdomicilenuls10derniersmatchs.title,//"Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbexterieurenuls10derniersmatchs.title,//"Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbdomicilenuls5derniersmatchs.title,//"Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                criteres.nbexterieurenuls5derniersmatchs.title,//"Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                criteres.equipedevantratiobutmis.title,//
                criteres.equipedevantratiobutpris.title,// 
                criteres.equipederriereratiobutmis.title,// 
                criteres.equipederriereratiobutpris.title,// 
                criteres.nbmatchnulsjourneechampionnatencours.title,//
                criteres.nbdomicilenulsdepuisdebutsaison.title,//  
                criteres.nbexterieurenulsdepuisdebutsaison.title,// 
                criteres.ratiotirmatch.title,//      "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                criteres.valeurlay.title,// "Valeur du lay (<4)",
                criteres.gonogo.title,
                "",
                "",
                "",
                criteres.commentaire.title // "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "
                // DateTime.Now.ToString()
                };
                    // }
                    if (criteres.typelay.value == "0-0")
                    {
                        valueRange.Values = new List<IList<object>> { oblistLAY00 };
                    }
                    if ((criteres.typelay.value == "0-1") || (criteres.typelay.value == "1-0"))
                    {
                        valueRange.Values = new List<IList<object>> { oblistLAY01 };
                    }

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = FormZ.service.Spreadsheets.Values.Update(valueRange, destinationSpreadsheetId, range);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse result = update.Execute();

                    // on fait une pause d'une seconde dans le code ?
                    System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                        //ecriture des criteres
                                                        //  String range = "IDMATCH_" + idmatch + "!A1";  // single cell D5
                    ValueRange valueRangeValeurs = new ValueRange();
                    valueRangeValeurs.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                    range = "IDMATCH_" + idmatch + "!E1";  // single cell D5
                                                           //ValueRange valueRange = new ValueRange();
                    valueRangeValeurs.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                                                                 //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                                                                 // on fait une pause d'une seconde  
                    System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                    var oblistValeursLAY00 = new List<object>()
                {
                 "",
                "Valeurs",//criteres.titre.title, //"Critères",
                criteres.typelay.value,  //"Lay 0-0",
                criteres.statzerozerosaisonencours.value,// "statistique saison en cours (>1.5)",
                //criteres.score.value,//"Score de 0-0 ",
                criteres.nb_cartons_rouges.value,//"nombre de cartons rouges",
                criteres.nbdomicilezerozero10derniersmatchs.value,//"Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbexterieurezerozero10derniersmatchs.value,//"Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbdomicilezerozero5derniersmatchs.value,//"Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                criteres.nbexterieurezerozero5derniersmatchs.value,//"Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                criteres.domicileratiobutmis.value,//"Equipe domicile ratio but marqués (=>1)",
                criteres.domicileratiobutpris.value,// "Equipe domicile ratio but encaissés (=>1)",
                criteres.exterieureratiobutmis.value,// "Equipe exterieure ratio but marqués (=>1)",
                criteres.exterieureratiobutpris.value,// "Equipe exterieure ratio but encaissés (=>1)",
                criteres.nbmatchzerozerojourneechampionnatencours.value,//    "Resultat journée de championnat en cours nombre de matchs à 0-0",
                criteres.nbmatchzerozero3dernieresjournees.value,
                criteres.nbdomicilezerozerodepuisdebutsaison.value,//  "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
                criteres.nbexterieurezerozerodepuisdebutsaison.value,// "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",
                criteres.nbdomiciletirs.value+"/"+criteres.nbexterieuretirs.value,//        criteres.ratiotirmatch.value,//      "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                criteres.valeurlay.value,// "Valeur du lay (<4)",
                "",
                "",
                "",
                "",
                "" //criteres.commentaire.title // "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "

                };//

                    var oblistValeursLAY01 = new List<object>()
                {
                 "",
                "Valeurs",//criteres.titre.title, //"Critères",
                criteres.typelay.value,  //"Lay 0-0",
                criteres.statnulssaisonencours.value,// "statistique saison en cours (>1.5)",
                //criteres.score.value,//"Score de 0-0 ",
                criteres.nb_cartons_rouges.value,//"nombre de cartons rouges",
                criteres.nbdomicilenuls10derniersmatchs.value,//"Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbexterieurenuls10derniersmatchs.value,//"Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbdomicilenuls5derniersmatchs.value,//"Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                criteres.nbexterieurenuls5derniersmatchs.value,//"Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                criteres.equipedevantratiobutmis.value,//
                criteres.equipedevantratiobutpris.value,// 
                criteres.equipederriereratiobutmis.value,// 
                criteres.equipederriereratiobutpris.value,// 
                criteres.nbmatchnulsjourneechampionnatencours.value,//
                criteres.nbdomicilenulsdepuisdebutsaison.value,//  
                criteres.nbexterieurenulsdepuisdebutsaison.value,// 
                criteres.nbdomiciletirs.value+"/"+criteres.nbexterieuretirs.value,//        criteres.ratiotirmatch.value,//      "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                criteres.valeurlay.value,// "Valeur du lay (<4)",
                "",
                "",
                "",
                "",
                "" //criteres.commentaire.title // "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "

                };//


                    if (criteres.typelay.value == "0-0")
                    {
                        valueRangeValeurs.Values = new List<IList<object>> { oblistValeursLAY00 };
                    }
                    if ((criteres.typelay.value == "0-1") || (criteres.typelay.value == "1-0"))
                    {
                        valueRangeValeurs.Values = new List<IList<object>> { oblistValeursLAY01 };
                    }
                    SpreadsheetsResource.ValuesResource.UpdateRequest updateValeurs = FormZ.service.Spreadsheets.Values.Update(valueRangeValeurs, destinationSpreadsheetId, range);
                    updateValeurs.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse resultValeurs = updateValeurs.Execute();

                    // on fait une pause d'une seconde dans le code ?
                    System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                        // Console.WriteLine("fait");                                                
                    //evaluation
                    ValueRange valueRangeEvaluations = new ValueRange();
                    valueRangeEvaluations.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                    range = "IDMATCH_" + idmatch + "!F1";  // single cell D5
                                                           //ValueRange valueRange = new ValueRange();
                    valueRangeValeurs.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                                                                 //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                                                                 // on fait une pause d'une seconde  
                    System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                    var oblistEvaluationsLAY00 = new List<object>()
                {
                criteres.gonogo.evaluation,//on veut le rouge ou vert dès le debut
                "Evaluations",//criteres.titre.title, //"Critères",
                criteres.typelay.evaluation,  //"Lay 0-0",
                criteres.statzerozerosaisonencours.evaluation,// "statistique saison en cours (>1.5)",
                //criteres.score.evaluation,//"Score de 0-0 ",
                criteres.nb_cartons_rouges.evaluation,//"nombre de cartons rouges",
                criteres.nbdomicilezerozero10derniersmatchs.evaluation,//"Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbexterieurezerozero10derniersmatchs.evaluation,//"Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbdomicilezerozero5derniersmatchs.evaluation,//"Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                criteres.nbexterieurezerozero5derniersmatchs.evaluation,//"Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                criteres.domicileratiobutmis.evaluation,//"Equipe domicile ratio but marqués (=>1)",
                criteres.domicileratiobutpris.evaluation,// "Equipe domicile ratio but encaissés (=>1)",
                criteres.exterieureratiobutmis.evaluation,// "Equipe exterieure ratio but marqués (=>1)",
                criteres.exterieureratiobutpris.evaluation,// "Equipe exterieure ratio but encaissés (=>1)",
                criteres.nbmatchzerozerojourneechampionnatencours.evaluation,//    "Resultat journée de championnat en cours nombre de matchs à 0-0",
                criteres.nbmatchzerozero3dernieresjournees.evaluation,
                criteres.nbdomicilezerozerodepuisdebutsaison.evaluation,//  "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
                criteres.nbexterieurezerozerodepuisdebutsaison.evaluation,// "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",
                criteres.ratiotirmatch.evaluation,//      "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                criteres.valeurlay.evaluation,// "Valeur du lay (<4)",
                criteres.gonogo.evaluation,
                "",
                "",
                "",
                "" //criteres.commentaire.title // "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "

                };//

                    var oblistEvaluationsLAY01 = new List<object>()
                {
                criteres.gonogo.evaluation,//on veut le rouge ou vert dès le debut
                "Evaluations",//criteres.titre.title, //"Critères",
                criteres.typelay.evaluation,  //"Lay 0-0",
                criteres.statnulssaisonencours.evaluation,// "statistique saison en cours (>1.5)",
                //criteres.score.evaluation,//"Score de 0-0 ",
                criteres.nb_cartons_rouges.evaluation,//"nombre de cartons rouges",
                criteres.nbdomicilenuls10derniersmatchs.evaluation,//"Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbexterieurenuls10derniersmatchs.evaluation,//"Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                criteres.nbdomicilenuls5derniersmatchs.evaluation,//"Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                criteres.nbexterieurenuls5derniersmatchs.evaluation,//"Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                criteres.equipedevantratiobutmis.evaluation,//
                criteres.equipedevantratiobutpris.evaluation,// 
                criteres.equipederriereratiobutmis.evaluation,// 
                criteres.equipederriereratiobutpris.evaluation,// 
                criteres.nbmatchzerozerojourneechampionnatencours.evaluation,//    "Resultat journée de championnat en cours nombre de matchs à 0-0",
                criteres.nbdomicilezerozerodepuisdebutsaison.evaluation,//  "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
                criteres.nbexterieurezerozerodepuisdebutsaison.evaluation,// "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",
                criteres.ratiotirmatch.evaluation,//      "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                criteres.valeurlay.evaluation,// "Valeur du lay (<4)",
                criteres.gonogo.evaluation,
                "",
                "",
                "",
                "" //criteres.commentaire.title // "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "

                };//

                    if (criteres.typelay.value == "0-0")
                    {
                        valueRangeEvaluations.Values = new List<IList<object>> { oblistEvaluationsLAY00 };
                    }
                    if ((criteres.typelay.value == "0-1") || (criteres.typelay.value == "1-0"))
                    {
                        valueRangeEvaluations.Values = new List<IList<object>> { oblistEvaluationsLAY01 };
                    }

                    SpreadsheetsResource.ValuesResource.UpdateRequest updateEvaluations = FormZ.service.Spreadsheets.Values.Update(valueRangeEvaluations, destinationSpreadsheetId, range);
                    updateEvaluations.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse resultEvaluations = updateEvaluations.Execute();

                    // on fait une pause d'une seconde dans le code ?
                    System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                        // Console.WriteLine("fait");                                                
                    //colorisation des evaluations.
                    CelluleSheet servicecellulesheet = new CelluleSheet();
                    //futur emplacement du copiage en ligne des elements de calcul mi temps valuer, evalution.moyenne de but à l'instant T etc ?

                    //Console.WriteLine("debut de colorisation");
                    //on desactive la colorisation, ca semble saturer l'api google.
                   // servicecellulesheet.FormatEvaluation(FormZ.service,"IDMATCH_" + idmatch);
                    //Console.WriteLine("fin de colorisation");
                    //on ajout l'alerte du match uniquement si le critere go/nogo est a OUI.
                    if (criteres.gonogo.evaluation == "OUI")
                    {
                        FormZ.AddAlertemitempsmatchoncalendar(idmatch, championnat, obj.api.fixtures[0].homeTeam.team_name, obj.api.fixtures[0].goalsHomeTeam, obj.api.fixtures[0].awayTeam.team_name, obj.api.fixtures[0].goalsAwayTeam, carton_rouge, url);
                    }
                    //sinon on a juste crée la fiche, mais on ne place pas l'alerte
                    //}//fin type 0-0


                }
                //return criteresencours;
            }//fin test deja programmé

        }



        public void create_fiche_match(string idmatch, Criteres critere_in, string championnat)
        {
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            //String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet ?
            // String spreadsheetId = "10xBxzW0ZdPb9I4pn2mug6ECnXF91dqcGDc5KavsF3nU";  //classeur des matchs max 200 feuillets.. bon on verra bien dans 200 matchs
            //String range = "Class Data!A2";  // single cell D5
            //String range = feuillet + "!" + refcellule;  // single cell D5
            //myss.
            //ValueRange valueRange = new ValueRange();
            //string resultat = "NON TROUVE";
            // SpreadsheetsResource.CreateRequest.
            //valueRange.MajorDimension = "COLUMNS";//"ROWS";//COLUMNS
            string destinationSpreadsheetId = "";
            //int sheetId = 0;  // TODO: Update placeholder value.
            // The ID of the spreadsheet to copy the sheet to.
            Form1 FormZ = new Form1();
            //creation du feuillet
            if (championnat == "Ligue1")
            {
                destinationSpreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";  // TODO: Update placeholder value. 
            }
            if (championnat == "Ligue2")
            {
                destinationSpreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";  // TODO: Update placeholder value. 
            }
            //il faudra un classeur par championnat
            // TODO: Assign values to desired properties of `requestBody`:

            var addSheetRequest = new AddSheetRequest();
            addSheetRequest.Properties = new SheetProperties();
            addSheetRequest.Properties.Title = "IDMATCH_" + idmatch;
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { AddSheet = addSheetRequest });
            var batchUpdateRequest = FormZ.service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, destinationSpreadsheetId);
            batchUpdateRequest.Execute();

            //
            //on devra également preparer l'ensemble des valeurs pour les enregistrer ailleurs.. (passes t on par un objet ???)
            //clairement, on a des elements a recuperer dans tous les sens...
            //ecriture des criteres
            String range = "IDMATCH_" + idmatch + "!A1";  // single cell D5
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                                                  //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                                                  // on fait une pause d'une seconde  
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google

            var oblist = new List<object>()
                {
                "Critères",
                //"Lay 0-0",
                "statistique saison en cours (>1.5)",
                "Score de 0-0 ",
                "nombre de cartons rouges",
                "Equipe domicile matchs a 0-0 sur les 10 derniers matchs",
                "Equipe exterieure matchs a 0-0 sur les 10 derniers matchs",
                "Equipe domicile matchs a 0-0 sur les 5 derniers matchs",
                "Equipe exterieure matchs a 0-0 sur les 5 derniers matchs",
                "Equipe domicile ratio but marqués (=>1)",
                "Equipe domicile ratio but encaissés (=>1)",
                "Equipe exterieure ratio but marqués (=>1)",
                "Equipe exterieure ratio but encaissés (=>1)",
                "Resultat journée de championnat en cours nombre de matchs à 0-0",
                "Equipe domicile nombre de matchs à 0-0 depuis debut de saison",
                "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison",
                "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs ",
                "Valeur du lay (<4)",
                "",
                "",
                "",
                "",
                "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "
                // DateTime.Now.ToString()
                };//
            valueRange.Values = new List<IList<object>> { oblist };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = FormZ.service.Spreadsheets.Values.Update(valueRange, destinationSpreadsheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result = update.Execute();

            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                //ecriture des criteres
                                                //  String range = "IDMATCH_" + idmatch + "!A1";  // single cell D5
            ValueRange valueRangeValeurs = new ValueRange();
            valueRangeValeurs.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
            range = "IDMATCH_" + idmatch + "!E1";  // single cell D5
                                                   //ValueRange valueRange = new ValueRange();
            valueRangeValeurs.MajorDimension = "COLUMNS";// "ROWS";//COLUMNS
                                                         //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                                                         // on fait une pause d'une seconde  
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
            var oblistValeurs = new List<object>()
                {
                "Valeurs",
                 "pas encore calculé",//"statistique saison en cours > 1.5",
                "Score de 0-0 :",
                "nombre de cartons rouges:",
                "Equipe domicile matchs a 0-0 sur les 10 derniers matchs:",
                "Equipe exterieure matchs a 0-0 sur les 10 derniers matchs:",
                "Equipe domicile matchs a 0-0 sur les 5 derniers matchs:",
                "Equipe exterieure matchs a 0-0 sur les 5 derniers matchs:",
                "Equipe domicile moyenne but marqués par match:",
                "Equipe domicile moyenne but encaissés par match:",
                "Equipe exterieure moyenne but marqués par match:",
                "Equipe exterieure moyenne but encaissés par match:",
                "Resultat journée de championnat en cours nombre de matchs à zero zero:",
                "Equipe domicile nombre de matchs à 0-0 depuis debut de saison:",
                "Equipe exterieure nombre de matchs à 0-0 depuis debut de saison:",
                "Equipe domicile nombre de tirs / Equipe exterieure nombre de tirs :",
                "Valeur du lay < 4:",
                "",
                "",
                "",
                "",
                "il est possible d'adopter une strategie agressive si pas de critere 1 mais deja eu un ou plusieurs 0-0 "
                // DateTime.Now.ToString()
                };//
            valueRangeValeurs.Values = new List<IList<object>> { oblistValeurs };
            SpreadsheetsResource.ValuesResource.UpdateRequest updateValeurs = FormZ.service.Spreadsheets.Values.Update(valueRangeValeurs, destinationSpreadsheetId, range);
            updateValeurs.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse resultValeurs = updateValeurs.Execute();

            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                // Console.WriteLine("fait");                                                
            CelluleSheet servicecellulesheet = new CelluleSheet();
            //SheetsService service;
            //  public Form1 FormZ = new Form1();
            //FormZ.service;
            //CelluleSheet servicecellulesheet = new CelluleSheet();
          //  servicecellulesheet.FormatEvaluation("IDMATCH_" + idmatch);// "IDMATCH_568088");
            
          // servicecellulesheet.FormatEvaluation(service(), "IDMATCH_" + idmatch);
        }

    }


}
