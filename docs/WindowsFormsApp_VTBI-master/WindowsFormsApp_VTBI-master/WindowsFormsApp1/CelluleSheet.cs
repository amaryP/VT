using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using Fixture;
using Newtonsoft.Json;
//using SheetsQuickstart;
using WindowsFormsApp1;
using statisticsmatch;
using Data = Google.Apis.Sheets.v4.Data;

using System.Collections;
using Google.Apis.Util;

namespace ConsoleApp1
{

    public class CelluleSheet
    {
        public SheetsService myss;
        private SpreadsheetsResource.BatchUpdateRequest bur;

        //public string refcellule;
        public CelluleSheet()
        {
            Console.WriteLine("Nouvelle instance CelluleSheet créée.");
        }

        public CelluleSheet(SheetsService myss, string feuillet, string refcellule, string oFixtureJsoncourant, int compteur)
        {
            Console.WriteLine("Nouvelle instance CelluleSheet créée.");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet ?
                                                                                  //String range = "Class Data!A2";  // single cell D5
            String range = feuillet + "!" + refcellule;  // single cell D5
                                                         //String myNewCellValue = "Alexandrie";
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";//COLUMNS
            //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
            // on fait une pause d'une seconde  
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
            var oblist = new List<object>();
            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
            {
                var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(oFixtureJsoncourant);
                Form1 FormZ = new Form1();
                //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                oblist = new List<object>()
                {
                       obj.api.fixtures[compteur].event_date,    //    item.fixture.date //DATE_HEURE ,   // problmee sur le format de la date
                       obj.api.fixtures[compteur].round,//  obj.response[compteur].league.round,
                       GetDay_Round_Int(obj.api.fixtures[compteur].round),  //DAY_ROUND                       +//DAY_ROUND_INT  /E
                       obj.api.fixtures[compteur].status,  // "status long:"
                       obj.api.fixtures[compteur].statusShort, //   obj.response[compteur].fixture.status.Short,  // "status cours:" +
                       obj.api.fixtures[compteur].homeTeam.team_id,//    obj.response[compteur].teams.home.id,//id team home
                       obj.api.fixtures[compteur].homeTeam.team_name,//     obj.response[compteur].teams.home.name, //name team home
                       obj.api.fixtures[compteur].goalsHomeTeam,//        obj.response[compteur].goals.home,  //but domicile //c'est quoi la difference ...
                       //obj.api.fixtures[compteur].score.fulltime, //    goalsHomeTeam
                       FormZ.scorehome(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.halftime),//"score mi temps domicile",//obj.response[compteur].score.halftime.home,//score mi temps domicile
                       FormZ.scorehome(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.fulltime),//"score finale domicile ",//obj.response[compteur].score.fulltime.home, //score finale domicile
                       obj.api.fixtures[compteur].awayTeam.team_id,// ,//obj.response[compteur].teams.away.id, //id team away
                       obj.api.fixtures[compteur].awayTeam.team_name,//obj.response[compteur].teams.away.name, //name team away
                       obj.api.fixtures[compteur].goalsAwayTeam,// ,  //but exterieur
                       //obj.api.fixtures[compteur].score.fu
                       FormZ.scoreaway(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.halftime),//   "score mi temps exterieur",// obj.response[compteur].score.halftime.away,//score mi temps exterieur
                       FormZ.scoreaway(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.fulltime),//                   "score final exterieur",// obj.response[compteur].score.fulltime.away, //score finale exterieur
                       "timezone",//obj.response[compteur].fixture.timezone, //timezone (ca c'est important pour recalculer l'horaire des matchs pour la france)
                      FormZ.VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(),obj.api.fixtures[compteur].goalsAwayTeam.ToString()),//)obj.response[compteur].teams.home.winner, //victoire domicile
                      FormZ.VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(),obj.api.fixtures[compteur].goalsAwayTeam.ToString()),//)obj.response[compteur].teams.home.winner, //victoire domicile obj.response[compteur].teams.away.winner,//victoire exterieur  /T
                      IsMatchNul(obj.api.fixtures[compteur].statusShort,FormZ.scorehome(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.fulltime),FormZ.scoreaway(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.fulltime)),
                      IsMatchZeroZero(obj.api.fixtures[compteur].statusShort,FormZ.scorehome(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.fulltime),FormZ.scoreaway(obj.api.fixtures[compteur].statusShort,obj.api.fixtures[compteur].score.fulltime)),
                      DateTime.Now.ToString()
            };
            }
            else  //non alpha
            {
                var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
                oblist = new List<object>()
                {
                    obj.response[compteur].fixture.date.ToString(),   // problmee sur le format de la date
                    obj.response[compteur].league.round,
                    GetDay_Round_Int(obj.response[compteur].league.round),  //DAY_ROUND                       +//DAY_ROUND_INT  /E
                    obj.response[compteur].fixture.status.Long,  // "status long:"
                    obj.response[compteur].fixture.status.Short,  // "status cours:" +
                    obj.response[compteur].teams.home.id,//id team home
                    obj.response[compteur].teams.home.name, //name team home
                    obj.response[compteur].goals.home,  //but domicile //c'est quoi la difference ...
                    obj.response[compteur].score.halftime.home,//score mi temps domicile
                    obj.response[compteur].score.fulltime.home, //score finale domicile
                    obj.response[compteur].teams.away.id, //id team away
                    obj.response[compteur].teams.away.name, //name team away
                    obj.response[compteur].goals.away,  //but exterieur
                    obj.response[compteur].score.halftime.away,//score mi temps exterieur
                    obj.response[compteur].score.fulltime.away, //score finale exterieur
                    obj.response[compteur].fixture.timezone, //timezone (ca c'est important pour recalculer l'horaire des matchs pour la france)
                    obj.response[compteur].teams.home.winner, //victoire domicile
                    obj.response[compteur].teams.away.winner,//victoire exterieur  /T
                    IsMatchNul(obj.response[compteur].fixture.status.Short, obj.response[compteur].score.fulltime.home.ToString(), obj.response[compteur].score.fulltime.away.ToString())
                       ,
                    IsMatchZeroZero(obj.response[compteur].fixture.status.Short, obj.response[compteur].score.fulltime.home.ToString(), obj.response[compteur].score.fulltime.away.ToString()),
                    DateTime.Now.ToString()
                };
            }
            //
            valueRange.Values = new List<IList<object>> { oblist };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result = update.Execute();
            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                // Console.WriteLine("fait");
        }
        public CelluleSheet(SheetsService myss, string feuillet, string refcellule, string oFixtureJsoncourant, int compteur, Statsteams sttcourant)
        {
            Console.WriteLine("Nouvelle instance CelluleSheet créée.");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet ?
                                                                                  //String range = "Class Data!A2";  // single cell D5
            String range = feuillet + "!" + refcellule;  // single cell D5
                                                         //String myNewCellValue = "Alexandrie";
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";//COLUMNS
            //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
            // on fait une pause d'une seconde  
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
            var oblist = new List<object>()
                {
                sttcourant.nbmatchjoues,
                sttcourant.nbvictoire,
                sttcourant.nbdefaite,
                sttcourant.nbnul,
                sttcourant.nbzerozero,
                sttcourant.ratiobutmis,
                sttcourant.ratiobutpris,
                sttcourant.nbnul10derniermatch,
                sttcourant.nbnul5derniermatch,
                sttcourant.nbzerozero10derniermatch,
                sttcourant.nbzerozero5derniermatch,
                 DateTime.Now.ToString()
                };//
            valueRange.Values = new List<IList<object>> { oblist };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result = update.Execute();
            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                // Console.WriteLine("fait");
        }

        public CelluleSheet(SheetsService myss, string feuillet, string refcellule, string oFixtureJsoncourant, int compteur, StatsLeagues stlcourant)
        {
            Console.WriteLine("Nouvelle instance CelluleSheet créée.");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet ?
                                                                                  //String range = "Class Data!A2";  // single cell D5
            String range = feuillet + "!" + refcellule;  // single cell D5
                                                         //String myNewCellValue = "Alexandrie";
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";//COLUMNS
            //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
            // on fait une pause d'une seconde  
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google

            var oblist = new List<object>()
                {
                stlcourant.nbmatchfinished,//journée joués totalement
                stlcourant.currentround,//journee en cours
                stlcourant.lastround,
                stlcourant.nextround,
                stlcourant.nbnulcurrentround,
                stlcourant.nbzerozerocurrentround,
                stlcourant.nbnul3lastrounds,
                stlcourant.nbzerozer3lastrounds,
                 DateTime.Now.ToString()
                };//
            valueRange.Values = new List<IList<object>> { oblist };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result = update.Execute();
            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                // Console.WriteLine("fait");
        }
    
     
        public CelluleSheet(SheetsService myss, string feuillet, string refcellule, string oFixtureJsoncourant, int compteur,string oneshot)
        {
            Console.WriteLine("Nouvelle instance CelluleSheet créée.");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet ?
                                                                                  //String range = "Class Data!A2";  // single cell D5
            String range = feuillet + "!" + refcellule;  // single cell D5
                                                         //String myNewCellValue = "Alexandrie";
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";//COLUMNS
            //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
            // on fait une pause d'une seconde  
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
            var oblist = new List<object>();
            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
            {
                oFixtureJsoncourant = oFixtureJsoncourant.Replace("null", "0");  //valeur non define = zero dans le contexte, sinon ca explose.
                oFixtureJsoncourant = oFixtureJsoncourant.Replace("Passes %", "Passes");  //probleme sur Passes %, car % est un caractere reservé en c#
                var obj = JsonConvert.DeserializeObject<RootobjectStatistics>(oFixtureJsoncourant.Replace(" ", "")); //ppff c'est ca... il pouvait pas matcher a cause des espaces
                oblist = new List<object>()
                {
                      obj.api.fixtures[0].statusShort,
                      obj.api.fixtures[0].goalsHomeTeam,
                      obj.api.fixtures[0].goalsAwayTeam,
obj.api.fixtures[0].statistics.ShotsonGoal.home,
obj.api.fixtures[0].statistics.ShotsonGoal.away,
obj.api.fixtures[0].statistics.ShotsoffGoal.home,
obj.api.fixtures[0].statistics.ShotsoffGoal.away,
obj.api.fixtures[0].statistics.TotalShots.home,
obj.api.fixtures[0].statistics.TotalShots.away,
obj.api.fixtures[0].statistics.BlockedShots.home,
obj.api.fixtures[0].statistics.BlockedShots.away,
obj.api.fixtures[0].statistics.Shotsinsidebox.home,
obj.api.fixtures[0].statistics.Shotsinsidebox.away,
obj.api.fixtures[0].statistics.Shotsoutsidebox.home,
obj.api.fixtures[0].statistics.Shotsoutsidebox.away,
obj.api.fixtures[0].statistics.Fouls.home,
obj.api.fixtures[0].statistics.Fouls.away,
obj.api.fixtures[0].statistics.CornerKicks.home,
obj.api.fixtures[0].statistics.CornerKicks.away,
obj.api.fixtures[0].statistics.Offsides.home,
obj.api.fixtures[0].statistics.Offsides.away,
obj.api.fixtures[0].statistics.BallPossession.home,
obj.api.fixtures[0].statistics.BallPossession.away,
obj.api.fixtures[0].statistics.YellowCards.home,
obj.api.fixtures[0].statistics.YellowCards.away,
obj.api.fixtures[0].statistics.RedCards.home,
obj.api.fixtures[0].statistics.RedCards.away,
obj.api.fixtures[0].statistics.GoalkeeperSaves.home,
obj.api.fixtures[0].statistics.GoalkeeperSaves.away,
obj.api.fixtures[0].statistics.Totalpasses.home,
obj.api.fixtures[0].statistics.Totalpasses.away,
obj.api.fixtures[0].statistics.Passesaccurate.home,
obj.api.fixtures[0].statistics.Passesaccurate.away,
obj.api.fixtures[0].statistics.Passes.home,
obj.api.fixtures[0].statistics.Passes.away,
DateTime.Now.ToString()
                    
                };
           
            }
            //
            valueRange.Values = new List<IList<object>> { oblist };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result = update.Execute();
            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                // Console.WriteLine("fait");
        }

        /*
        bool  GetNulOrZero(string statut_match, string score_home, string score_away)
                {
            bool resultat = false;
            //ajout des calculs match nul et 0-0
            if (statut_match == "FT")// (si on peux c'est a dire si le match est a statut fini.)
            {
                resultat = true;
                IsMatchNul(statut_match,score_home, score_away);
                resultat = resultat + IsMatchZeroZero(statut_match,score_home, score_away);
            }
            return resultat;
        }
        */
        private string  IsMatchNul(string statut_match, string score_home, string score_away)
        {
            string resultat = "";
            if (statut_match == "FT")
            {
                if (score_home == score_away)
                {
                    resultat = "TRUE";
                }
                else 
                {
                    resultat = "FALSE";
                };

            }
         
            return resultat;
        }
        private string IsMatchZeroZero(string statut_match, string score_home, string score_away)
        {
            //est ce un zero a zero   
            string resultat = "";
            if (statut_match == "FT")
            {
                if (score_home == "0" & score_away == "0")
                {
                    resultat = "TRUE";
                }
                else resultat = "FALSE";
            }

            return resultat;
          
        }

        private string GetDay_Round_Int(string Id_round_string)
        {
            string resultat = "";
            Id_round_string = Id_round_string.Trim();
            int finstring = Id_round_string.Length;
            string boutstring;
            //  resultat.Substring(1);
            for (int i = 1; i < finstring; i++)
            {
                boutstring = Id_round_string.Substring(i, 1);
                if (boutstring == "-")
                {
                    resultat = Id_round_string.Substring(i + 1, finstring - (i + 1));
                }
            }

            return resultat.Trim();
        }

        public void FormatEvaluation(SheetsService myss_in,string feuillet)
        {
            //on boucle sur la fonction CelluleColor
            //on est clairement sur du dur, mais bon, l'essentiel est fait
            for (int i = 1; i < 20; i++)
            {
                //
            //    CelluleSheet servicecellulesheet = new CelluleSheet();
                CelluleColor(myss_in,feuillet, i);
            }
        }

        public void CelluleColor(SheetsService myss_in,string feuillet, int line_in)//, string feuillet, string refcellule, string oFixtureJsoncourant, int compteur, Statsteams sttcourant)
        {
            //SheetsService myss = new SheetsService();
            //Console.WriteLine("Nouvelle instance CelluleSheet pour modifier les couleurs créée.");
            //eventuellement on precisera le feuillet, si on subdivise par championnat.
            String spreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";//classeur fiche match
            string range = "!F" + line_in + ":F" + line_in;// "Couleur!A2";
            int row_inDeb = line_in - 1;
            int row_inFin =line_in ;
            //get sheet id by sheet name
            Spreadsheet spr = myss_in.Spreadsheets.Get(spreadsheetId).Execute();
            Sheet sh = spr.Sheets.Where(s => s.Properties.Title == feuillet).FirstOrDefault();
            int sheetId = (int)sh.Properties.SheetId;
            //recuperation de la valeur
             SpreadsheetsResource.ValuesResource.GetRequest quest = myss_in.Spreadsheets.Values.Get(spreadsheetId, feuillet+range);
             ValueRange recupQuest = quest.Execute();
            IList<IList<Object>> values = recupQuest.Values;
            // Console.WriteLine(values[0].ToString());
            string s1;
            int i = 0;
            Color BackgroundColorVert = new Color()
            {
                Green = 1
            };
            Color BackgroundColorRouge = new Color()
            {
                Red = 1,
            };

            Color BackgroundColorblanc = new Color()
            {
                Alpha = 1
            };

            Color Backgroundcolorchoice = new Color();

            Backgroundcolorchoice = BackgroundColorblanc;

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Console.WriteLine(row[i].ToString());

                    s1 = row[i].ToString();

                    if (s1=="OUI")
                    {
                        Backgroundcolorchoice = BackgroundColorVert;
                    }
                    if (s1 == "NON")
                    {
                        Backgroundcolorchoice = BackgroundColorRouge;
                    }
                    i++;
                }

            }
            //define cell color
            if (Backgroundcolorchoice != BackgroundColorblanc)
            {
                var userEnteredFormat = new CellFormat()
                {
                    BackgroundColor = Backgroundcolorchoice,// new Color()
                    /*
                    TextFormat = new TextFormat()
                    {
                        Bold = true,
                        FontFamily = "your font family",
                        FontSize = 12
                    }
                    */
                };
                BatchUpdateSpreadsheetRequest bussr = new BatchUpdateSpreadsheetRequest();

                //create the update request for cells from the first row
                var updateCellsRequest = new Request()
                {
                    RepeatCell = new RepeatCellRequest()
                    {
                       // Range = range;
                      
                Range = new GridRange()
                        {
                            SheetId = sheetId,
                            StartColumnIndex = 5,
                            StartRowIndex = row_inDeb,
                            EndColumnIndex = 6,
                            EndRowIndex = row_inFin
                        },
                     
                        Cell = new CellData()
                        {
                            UserEnteredFormat = userEnteredFormat
                        },
                        Fields = "UserEnteredFormat(BackgroundColor,TextFormat)"
                    }
                };

                bussr.Requests = new List<Request>();
                bussr.Requests.Add(updateCellsRequest);
                bur = myss_in.Spreadsheets.BatchUpdate(bussr, spreadsheetId);
                bur = myss_in.Spreadsheets.BatchUpdate(bussr, spreadsheetId);
                bur.Execute();
                //ajout d'une pause de 1/2 seconde pour eviter la saturation de l'api...googlesheet (contrat)
                System.Threading.Thread.Sleep(500);// pour une pause de 1/2 seconde.
            }
           // SpreadsheetsResource.ValuesResource.GetRequest quest = myss.Spreadsheets.Values.Get(spreadsheetId, range);

          //  quest.Service.Features.Add.

          //  ValueRange recupQuest = quest.Execute();

            //IList<IList<Object>> values = recupQuest.Values;

            //recupQuest.Values.
          //  /var rangaverif = spreadsheetId.getRange();

           // valueRange.MajorDimension = "ROWS";//COLUMNS
            //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJsoncourant);
            // on fait une pause d'une seconde  
          //  System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
            /* var oblist = new List<object>()
                 {
                 sttcourant.nbmatchjoues,
                 sttcourant.nbvictoire,
                 sttcourant.nbdefaite,
                 sttcourant.nbnul,
                 sttcourant.nbzerozero,
                 sttcourant.ratiobutmis,
                 sttcourant.ratiobutpris,
                 sttcourant.nbnul10derniermatch,
                 sttcourant.nbnul5derniermatch,
                 sttcourant.nbzerozero10derniermatch,
                 sttcourant.nbzerozero5derniermatch,
                  DateTime.Now.ToString()
                 };//
            */
            //    newCellFormat
            //    valueRange.
            //    valueRange.Values = setBackground("red");
            // new List<IList<object>> { oblist };
            //myss.Spreadsheets.Values.
            //  SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);

            //    SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.b
            //update.v
            //  update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            // UpdateValuesResponse result = update.Execute();
            // on fait une pause d'une seconde dans le code ?
            // System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
            // Console.WriteLine("fait");
        }


        /*
		 * public CelluleSheet(SheetsService myss, string feuillet, string refcellule, string valeurcellule)
		{
			Console.WriteLine("Nouvelle instance CelluleSheet créée.");
			//String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
			String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet Ligue1

			//String range = "Class Data!A2";  // single cell D5

			String range = feuillet+"!"+refcellule;  // single cell D5
											 //String myNewCellValue = "Alexandrie";
			ValueRange valueRange = new ValueRange();
			valueRange.MajorDimension ="ROWS";//COLUMNS
			//var oblist = new List<object>() { "Alexandrie10" };
			var oblist = new List<object>() { valeurcellule};//			{ "TestA", "TestB" };//	{ "TestA", "TestB" };//	
			valueRange.Values = new List<IList<object>> { oblist };

			SpreadsheetsResource.ValuesResource.UpdateRequest update = myss.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
			update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
			UpdateValuesResponse result = update.Execute();
			Console.WriteLine("fait");
		}
		
		 * */
        public void Duplicatanddeletesheetfichesmatchs(SheetsService myss_in)
        {
            //SheetsService myss_in = ServiceAccesClasseur

            string sourcespreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";
            string ciblespreadsheetId = "1gcdceaccP57q63-gZvUXdwFKASkP0A3nCekzsZVflv4";//classeur archive fiche match
            //on va boucler duplication et suppression 
            Spreadsheet sprsource = myss_in.Spreadsheets.Get(sourcespreadsheetId).Execute();
            for (int i = 1; i < sprsource.Sheets.Count; i++) //on ne supprime pas le premier 'modele'
            {
                string sheetid = sprsource.Sheets[i].Properties.SheetId.ToString();
                DuplicateSheetfichesmatchs(myss_in, sheetid, sourcespreadsheetId, ciblespreadsheetId);  //on duplicate
                DeleteSheet(myss_in, sheetid, sourcespreadsheetId);//on supprime du classeur courant
            }
        }
        public void Purge_archivefiche_matchs(SheetsService myss_in)
        {
            //SheetsService myss_in = ServiceAccesClasseur
            //ref du classeur archive, attention a en faire une copie avant
            string sourcespreadsheetId = "1gcdceaccP57q63-gZvUXdwFKASkP0A3nCekzsZVflv4";
            //on va boucler et suppression 
            Spreadsheet sprsource = myss_in.Spreadsheets.Get(sourcespreadsheetId).Execute();
            for (int i = 1; i < sprsource.Sheets.Count; i++) //on ne supprime pas le premier 'modele'
            {
                string sheetid = sprsource.Sheets[i].Properties.SheetId.ToString();
                DeleteSheet(myss_in, sheetid, sourcespreadsheetId);//on supprime du classeur courant
            }
        }
        public void DeleteSheet(SheetsService myss_in, string sheetid_in, string sourcespreadsheetId)
        {
            //sourcespreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";
            //Spreadsheet sprsource = myss_in.Spreadsheets.Get(sourcespreadsheetId).Execute();
            //Sheet sh = sprsource.Sheets.Where(s => s.Properties.Title == feuillet).FirstOrDefault();
            //int sheetId = (int)sh.Properties.SheetId;
            var Deletequest = new Request()
            {
                DeleteSheet = new DeleteSheetRequest()
                {
                    SheetId = int.Parse(sheetid_in)
                }
            };
            List <Request> requests = new List<Request>();
            requests.Add(Deletequest);
            BatchUpdateSpreadsheetRequest DeleteRequest = new BatchUpdateSpreadsheetRequest();
            DeleteRequest.Requests = requests;
            SpreadsheetsResource.BatchUpdateRequest request = myss_in.Spreadsheets.BatchUpdate(DeleteRequest, sourcespreadsheetId);
            request.Execute();
            System.Threading.Thread.Sleep(1000);
        }
            public void DuplicateSheetfichesmatchs(SheetsService myss_in, string sheetid_in, string sourcespreadsheetId, string ciblespreadsheetId)
        {
            //cela vient donc d'une fonction qui va boucler sur les sheet ID du classeur source
            //et qui va copier dans le sheet id dans le classeur cible
            //
            //sourcespreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";
            //Console.WriteLine("Nouvelle instance CelluleSheet créée.");
            //ciblespreadsheetId = "1gcdceaccP57q63-gZvUXdwFKASkP0A3nCekzsZVflv4";//classeur archive fiche match
            Spreadsheet sprsource = myss_in.Spreadsheets.Get(sourcespreadsheetId).Execute();
            //Sheet sh = sprsource.Sheets.Where(s => s.Properties.Title == feuillet).FirstOrDefault();
            //int sheetId = (int)sh.Properties.SheetId;
            Data.CopySheetToAnotherSpreadsheetRequest requestBody = new Data.CopySheetToAnotherSpreadsheetRequest();
            requestBody.DestinationSpreadsheetId = ciblespreadsheetId;
            SpreadsheetsResource.SheetsResource.CopyToRequest request = myss_in.Spreadsheets.Sheets.CopyTo(requestBody, sourcespreadsheetId, int.Parse(sheetid_in));
            Data.SheetProperties response = request.Execute();
            System.Threading.Thread.Sleep(1000);
            //Console.WriteLine("fait");
        }

       public Boolean Isfichematchalreadyexist(SheetsService myss_in, string namefichematch)
        {
            Boolean retour = true;
            String spreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";//classeur fiche match
            //get sheet id by sheet name
            Spreadsheet spr = myss_in.Spreadsheets.Get(spreadsheetId).Execute();
            Sheet sh = spr.Sheets.Where(s => s.Properties.Title == namefichematch).FirstOrDefault();
            /*
            string sheetId = sh.Properties.SheetId.ToString();
            //int sheetId = (int)sh.Properties.SheetId;
            if (sheetId!=null) { retour = true; }
            */
            /* string sheetId;
             try
             {
                 //Console.WriteLine("String length is {0}", s.Length);
                  sheetId = sh.Properties.SheetId.ToString();
             }
             catch (NullReferenceException e)
             {
                 retour = false;
             }
             //if (String.IsNullOrEmpty(sh.Properties.SheetId.ToString())) { retour = true; }
            */
            retour = false;
            return retour;
        }
        //
        /*

function simpleduplicatesheet(){
var ss = SpreadsheetApp.getActiveSpreadsheet();

var sheet = ss.getActiveSheet();
if (sheet.getName()==='MODELE'){
for (var i=1 ;i<=31;i++){
sheet2 = sheet.copyTo(ss).setName(i);
}
}
ss.getSheetByName('Modèle').hideSheet();
}

         function supprimersheet(){
var classeur = SpreadsheetApp.getActiveSpreadsheet();
var ss = SpreadsheetApp.getActive();
var nb =  classeur.getNumSheets();
var  feuilles = classeur.getSheets();
classeur.getSheetByName('Modèle').showSheet()
for (var i=1 ;i<nb;i++){
var sheet= feuilles[i].activate();
if(feuilles[i].getName()=== ss.getSheetByName(i));
{ss.deleteSheet(sheet)};
}
}
  BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new 
BatchUpdateSpreadsheetRequest();

List<Request> requests = new ArrayList<>();

DuplicateSheetRequest requestBody = new DuplicateSheetRequest();
requestBody.setNewSheetName("test");
requestBody.setSourceSheetId(sheetId);

requests.add(new Request().setDuplicateSheet(requestBody));

Sheets sheetsService = createSheetsService();

batchUpdateSpreadsheetRequest.setRequests(requests);
Sheets.Spreadsheets.BatchUpdate request =
        sheetsService.spreadsheets().batchUpdate(spreadsheetId, batchUpdateSpreadsheetRequest);

BatchUpdateSpreadsheetResponse response = request.execute();

         */

    }

}
