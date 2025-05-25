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
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;
using Newtonsoft.Json;
using Fixture;
using System.Drawing.Text;
using ConsoleApp1;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Data = Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
//using System.Web.Script.Serialization;
using System.Threading;
using System.Web.UI.WebControls.WebParts;
using System.Web.ModelBinding;
using System.Reflection;
using System.Linq.Expressions;
using ServiceAccesClasseur;
//using Google.GData.Client;
//using Google.GData.Spreadsheets;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Xml.XPath;
using statisticsmatch;
using AnalyzeStats;
using ServiceCritere;
using classTeam2;
using System.Globalization;
using Binance.Net;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using Newtonsoft.Json.Linq;
using ServiceSymbolsTools;
using System.Collections.ObjectModel;
//using Trading;
//using Evenement;

namespace WindowsFormsApp1

{
    // public string APIFOOTBALL = "ALPHA";
    public partial class Form1 : Form
    {
        string chaine;
        string typeGetApiFootball = "";
        string league;
        string season;
        string nameleague = "";
        string CurrentRound = "PAS INITIALISE";
        string param1 = "";
        string param2 = "";
        string param3 = "ALL";
        string Interval = "";

       // Model1Container db = new Model1Container();
//        int nbevent = db.EvenementSet.Count();

        /// liste des leagues concernés.
        //public string APIFOOTBALL = "ALPHA";//"ALPHA";//beta sinon... soit programme payant, soit gratuit

        ColonnesClasseurChampionnat ClasseurChampionnatFranceLigue1;
        public SheetsService service;//= GetServiceOnGooglesheet();
        CalendarService servicecalendar;

        //public Criteres criteres_in = new Criteres();
        string oFixtureJson;//        var obj = JsonConvert.DeserializeObject<RootobjectFixture>(json);
        static string[] Scopes = { SheetsService.Scope.Spreadsheets, CalendarService.Scope.Calendar };// , CalendarService.Scope.Calendar };//                Calendar };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        // static string[] ScopesCalendar = { CalendarService.Scope.Calendar  };//                Calendar};
        static string ApplicationNameCalendar = "Google Calendar API .NET Quickstart";
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        trading TradingServices = new trading();

        public Form1()
        {
            InitializeComponent();
            lb_typeGet.SetSelected(0, true);
            service = GetServiceOnGooglesheet();
            servicecalendar = GetServiceOnGooglecalendar();
            //  ClasseurChampionnatFranceLigue1 = new ColonnesClasseurChampionnat("Ligue1","2020");

            // Console.WriteLine();
            //  Invoke this sample with an arbitrary set of command line arguments.
            string[] arguments = Environment.GetCommandLineArgs();
            int nbrparam = arguments.Count();
            //on prends jusqu'a 3 parametres...
            if (nbrparam > 1)
            {
                param1 = arguments[1];
                param1 = param1.Trim();
                textBox1.AppendText(arguments[1]);
            }
            if (nbrparam > 2)
            {
                param2 = arguments[2];
                param2 = param2.Trim();
                textBox1.AppendText(arguments[2]);
            }
            if (nbrparam > 3)
            {
                param3 = arguments[3];
                param3 = param3.Trim();
                textBox1.AppendText(arguments[3]);
            }

            /*
            //controle & verification des matchs planifiés
            if (CountmatchAgendaAppConfig()==0)  //si aucun matchs planifié dans l'appconfig alors on relance le calcul 
            {
                GetMatchNext24h(service, "Ligue1", "");
                GetMatchNext24h(service, "Ligue2", "");
                GetMatchNext24h(service, "Bundesliga", "");
                GetMatchNext24h(service, "Bundesliga2", "");
                GetMatchNext24h(service, "Premier_league", "");
                GetMatchNext24h(service, "Championship", "");
                GetMatchNext24h(service, "SerieA", "");
                GetMatchNext24h(service, "SerieB", "");
                GetMatchNext24h(service, "LaLiga", "");
                GetMatchNext24h(service, "LaLiga2", "");
            }
            */


            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("u9fVUfpsFXXHGRSajapz794o2jfNifB7Z1PvYQqTWGP0e13fqnOjZUVldyXuY19H", "Isasqjrtq2hHnuDqTLatJaihhLkA2GTzmodpoMQmWfCNLa4LlvUzhVdABcCx25vW"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials("u9fVUfpsFXXHGRSajapz794o2jfNifB7Z1PvYQqTWGP0e13fqnOjZUVldyXuY19H", "Isasqjrtq2hHnuDqTLatJaihhLkA2GTzmodpoMQmWfCNLa4LlvUzhVdABcCx25vW"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });


            /*using (var client = new BinanceClient())
            {
                // Spot.Market | Spot market info endpoints
                //client.Spot.Market.GetBookPrice("BTCUSDT");
                // Spot.Order | Spot order info endpoints
                // client.Spot.Order.GetAllOrders("UNIUSDT");
                // StreamWriter monStreamWriter = new StreamWriter("exportvtbi.txt");
                // StreamWriter sw = new StreamWriter("C:\\TEST\\exportvtbi.txt");
                //sw.WriteLine("test");
                // string recup;
                //Console.WriteLine("TEST");
                // client.Spot.Order.GetMyTrades("UNIUSDT").Data.ToList();
                //GetAllOrders("UNIUSDT");
                //(client.Spot.Order.GetOpenOrders());
                //sw.Write(client.Spot.Order.GetOpenOrders());
                //sw.Close();
                //GetMyTrades();
                // Spot.System | Spot system endpoints
                // client.Spot.System.GetExchangeInfo();
                // Spot.UserStream | Spot user stream endpoints. Should be used to subscribe to a user stream with the socket client
                client.Spot.UserStream.StartUserStream();
                StreamWriter sw = new StreamWriter("C:\\TEST\\exportvtbi.txt");
                /*
                int Comptage =client.Spot.Order.GetMyTrades("UNIUSDT").Data.Count();
               // sw.WriteLine("test1");
               // sw.WriteLine("test2");
               // sw.WriteLine(Comptage);
                //sw.WriteLine(
                // using (client.Spot.Order.GetMyTrades("UNIUSDT").Data.GetEnumerator()) 
                // {
                //while (MoveNext()) ;
                //recuperation des trades (sur une paire donnée)
                IEnumerator<BinanceTrade> ListeMyTrades = client.Spot.Order.GetMyTrades("UNIUSDT").Data.GetEnumerator();
                for (int i = 0; i < Comptage; i++)
                {
                    ListeMyTrades.MoveNext();
                    {
                       // sw.WriteLine(ListeMyTrades.Current.GetType());
                        sw.WriteLine(ListeMyTrades.Current.Id);
                        sw.WriteLine(ListeMyTrades.Current.Price);
                        sw.WriteLine(ListeMyTrades.Current.Quantity);
                        sw.WriteLine(ListeMyTrades.Current.QuoteQuantity);
                        sw.WriteLine(ListeMyTrades.Current.TradeTime);
                        sw.WriteLine(ListeMyTrades.Current.Commission);
                        sw.WriteLine(ListeMyTrades.Current.CommissionAsset);
                        sw.WriteLine(ListeMyTrades.Current.Symbol);
                        sw.WriteLine(ListeMyTrades.Current.OrderId);
                        sw.WriteLine(ListeMyTrades.Current.OrderListId);
                        sw.WriteLine(ListeMyTrades.Current.IsBuyer);
                    }

                }
                /*
                                //recuperation des ordres ouverts (sur une paire donnée)
                //sw.WriteLine(ListeMyTrades.Current.IsBuyer);
                String Paire = "UNIUSDT";
                IEnumerator<BinanceOrder> ListeMyOpenOrders = client.Spot.Order.GetOpenOrders(Paire).Data.GetEnumerator();
                //en l'occurence on a un seul ordre sur cette valeur.
                for (int i = 0; i < client.Spot.Order.GetOpenOrders(Paire).Data.Count(); i++)
                {
                    ListeMyOpenOrders.MoveNext();
                    {
                        //sw.WriteLine(ListeMyOpenOrders.Current.GetType());
                        //sw.WriteLine(ListeMyOpenOrders.Current.Id);
                        sw.WriteLine(ListeMyOpenOrders.Current.ClientOrderId);
                        sw.WriteLine(ListeMyOpenOrders.Current.Price);
                        sw.WriteLine(ListeMyOpenOrders.Current.Quantity);
                        sw.WriteLine(ListeMyOpenOrders.Current.QuoteQuantity);
                        sw.WriteLine(ListeMyOpenOrders.Current.CreateTime);
                        sw.WriteLine(ListeMyOpenOrders.Current.Status);
                        sw.WriteLine(ListeMyOpenOrders.Current.Type);
                        sw.WriteLine(ListeMyOpenOrders.Current.Symbol);
                        sw.WriteLine(ListeMyOpenOrders.Current.OrderId);
                        sw.WriteLine(ListeMyOpenOrders.Current.OrderListId);
                        //  sw.WriteLine(ListeMyOpenOrders.Current.IsBuyer);
                    }

                }
                //mise en oeuvre du cancelorder (a priori la paire (sympbol) et timestamp suffisent
                //var result1 = client.Spot.Order.CancelOrder(Paire, ListeMyOpenOrders.Current.OrderId);

                /*if (result1.Success)
                    sw.WriteLine("ordre annulé");
                //messageBoxService.ShowMessage("Order canceled!", "Sucess", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                else
                    sw.WriteLine("echec de l'annulation de l'ordre ");
                //messageBoxService.ShowMessage($"Order canceling failed: {result.Error.Message}", "Failed", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

                //mise en oeuvre d'un ordre d'achat
                
                //attention au facteur multiplicateur.
                // var result2 = client.Spot.Order.PlaceOrder(Paire,OrderSide.Buy, OrderType.Limit, quantity: 1, price:  10,timeInForce:TimeInForce.GoodTillCancel);
                // sw.Write(result2);
                //recuperation d'un cours sur une paire donnée
                //IBinanceClientMarket ThisbinanceClientMarket = client.Spot.Market(Paire);
                // .GetPrice(Paire);
                //BinancePrice ThisBinancePrice = new BinancePrice();

                //passage d'un ordre de vente
                //ca fonctionne, mais encore un souci de comprehension sur le x5 (peut etre que cela change a 1000 euros ?)
                //ordre a la limite
                //var result2 = client.Spot.Order.PlaceOrder(Paire, OrderSide.Sell, OrderType.Limit, quantity: 1, price: 100, timeInForce: TimeInForce.GoodTillCancel);
                // sw.Write(result2);
                //ordre stop loss  ::ca marche :)
                var result2 = client.Spot.Order.PlaceOrder(Paire, OrderSide.Sell, OrderType.StopLossLimit, quantity: 1, stopPrice: 10, price: 10, timeInForce: TimeInForce.GoodTillCancel);

                // il faudra recuperer les valeurs en ligne pour gerer la quantité.

                //client.Spot.Market.GetPrice(Paire).GetType
                ThisBinancePrice.Price = client.Spot.Market.GetPrice(Paire).Data.Price;
                ThisBinancePrice.Symbol = client.Spot.Market.GetPrice(Paire).Data.Symbol;
                ThisBinancePrice.Timestamp = client.Spot.Market.GetPrice(Paire).Data.Timestamp;
                // GetAllPrices();
                double Prixentree = 38.7;
                double Cliquet = 0.500;
                // Decimal Prix = client.Spot.Market.GetPrice(Paire).Data.Price;
                sw.WriteLine(Paire + " : " + ThisBinancePrice.Price);
                //using  client.Spot.Market;
              /*
                for (int i = 0; i < 30; i++)
                {
                    System.Threading.Thread.Sleep(10000);// apriori 10 secondes
                    //Prix = client.Spot.Market.GetPrice(Paire).Data.Price;
                    sw.WriteLine(client.Spot.Market.GetPrice(Paire).Data.Symbol + " : " + client.Spot.Market.GetPrice(Paire).Data.Price);
                    //sw.WriteLine(" : " + client.Spot.Market.GetPrice(Paire).Data.Timestamp.);

                    if (client.Spot.Market.GetPrice(Paire).Data.Price >= (decimal)(Prixentree + Cliquet))
                    {
                        sw.WriteLine("remonte le stop de vente a " + (Prixentree + Cliquet));
                    }
                    else
                    {
                        sw.WriteLine("laisse le stop de vente a " + Prixentree);
                    }
                    sw.Flush();
                }

                
                //IEnumerable<BinanceTrade>
                //client.Spot.Order.GetMyTrades("UNIUSDT").Data.Count; //Mytrade = new client.Spot.Order.GetMyTrades("UNIUSDT");

                // sw.Write(client.Spot.Order.GetMyTrades("UNIUSDT"));

                //sw.Write(client.Spot.Order.GetMyTrades("UNIUSDT").Data.ToList());
                //client.General.GetAccountInfo().ToString().All();

                // client.General.GetAccountInfo().Data;
                // Lending | Lending endpoints
                //client.Lending.GetFlexibleProductList();
                // sw.Write(client.WithdrawDeposit.GetWithdrawalHistory().ToString());
                //  sw.Write(client.General.GetAccountInfo());
                //Console.Out);
                //Console.Out
                //sw.Close();
                //RestClient
                */
            // using (var client = new BinanceClient())

            // client.Spot.UserStream.StartUserStream();
           //  StreamWriter sw = new StreamWriter("C:\\TEST\\exportvtbi.txt");

            //test de recuperation des indices...
            /*
                        //on doit passer par une api tiers payante, mais bon c'est pas cher, on va voir si c'est des bonnes valeurs
                        //et si on arrive a recuperer le truc facilement
                        string Weburl = "https://api.taapi.io/rsi?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=BTC/USDT&interval=1h&optInTimePeriod=14";
                        // APICall.RunAsync(Weburl,"GET");
                        RestClient clientRest1 = new RestClient(Weburl);
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = clientRest1.Execute(request);
                        string resultat = response.Content;
                        // textBox1.Text = resultat;
                        //var json = resultat;
                        textBox1.Text = resultat;




                        Weburl = "https://api.taapi.io/rsi?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=BTC/USDT&interval=1h&optInTimePeriod=5";
                        // APICall.RunAsync(Weburl,"GET");
                        clientRest1 = new RestClient(Weburl);
                        request = new RestRequest(Method.GET);
                        response = clientRest1.Execute(request);
                        resultat = response.Content;
                        textBox1.Text = textBox1.Text + resultat;
                        // sw.WriteLine(resultat);

                        //   sw.Close();


                        //  var socketClient = new BinanceSocketClient();


                        //  var socketClient = new BinanceSocketClient();
                        // Spot | Spot market and user subscription methods
                        // Unsubscribe
                        //   socketClient.UnsubscribeAll();


                        Console.ReadLine();

                        */
            ServiceSymbol ServiceSymbolTools = new ServiceSymbol();
           // List<string> ListACharger = ServiceSymbolTools.ListeSymbols();
            //ServiceSymbolTools.ListeSymbols().ForEach (string element,)
            //     {

            // listBox1.Items.Add(item);
            //   }
            MAJlistBox1(ServiceSymbolTools.ListeSymbols().GetEnumerator());
            //ListACharger.GetEnumerator());
            listBox1.SelectedIndex = 0;

               // ForEach(listBox1.Items.AddRange());
               //listBox1.Items.Add(ServiceSymbolTools.ListeSymbols().ToString());
               //.Add(ServiceSymbolTools.ListeSymbols());
            timer1.Start();

        }

        private void MAJlistBox1(IEnumerator<string> mylist)
        {
            listBox1.Items.Clear();
            while (mylist.MoveNext())
            {
                listBox1.Items.Add(mylist.Current+"/USDT");
            }
            
        }


        /*
        private void SubscribeToSpotUserStream()
        {
            var socketClient = new BinanceSocketClient();
            // Subscribe to a user stream
            var restClient = new BinanceClient();
            var listenKeyResult = restClient.Spot.UserStream.StartUserStream();
            if (!listenKeyResult.Success)
                throw new Exception("Failed to start user stream: " + listenKeyResult.Error);

            var successAccount = socketClient.Spot.SubscribeToUserDataUpdates(listenKeyResult.Data,
                data =>
                {
                        // Handle account info data
                        // Deprecated, will be removed in the future
                    },
                data =>
                {
                        // Handle order update info data
                    },
                null, // Handler for OCO updates
                null, // Handler for position updates
                null); // Handler for account balance updates (withdrawals/deposits)
            Console.ReadLine();
        }
            */
        //il faut deux parametres a ce stade pour lancer un programme auto. 
        //l'instruction auto
        //puis le programme a lancé..
        //System.Threading.Thread.Sleep(10000);// pour une pause de 10 secondes. est ce que cela force l'affichage ?





        //timer1.Start();

        //creer une fonction qui ouvre un service sur le classeur. ok c'est fait
        public SheetsService GetServiceOnGooglesheet()// (string[] args)
        {
            UserCredential credential;

            using (var stream =
              new FileStream("credentialsSheet.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "tokenSheet.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                  GoogleClientSecrets.Load(stream).Secrets,
                  Scopes,
                  "user",
                  CancellationToken.None,
                  new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
            /*
            var sheet = service.Sheets.newSpreadsheet();sh
            sheet.properties = Sheets.newSpreadsheetProperties();
            sheet.properties.title = "TEST_creationfichier";
            var spreadsheet = Sheets.Spreadsheets.create(sheet);
            */
            // CelluleSheet mycellulesheet = new CelluleSheet(service, "Class Data", "A2", "Tata");
            // service.
        }

        //creer une fonction qui ouvre un service sur le classeur. ok c'est fait
        public CalendarService GetServiceOnGooglecalendar()// (string[] args)
        {
            UserCredential credential;
            /*
                        using (var stream =
                          new FileStream("credentials7.json", FileMode.Open, FileAccess.Read))
                        {
                            // The file token.json stores the user's access and refresh tokens, and is created
                            // automatically when the authorization flow completes for the first time.
                            string credPath = "token.json";
                            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                              GoogleClientSecrets.Load(stream).Secrets,
                              ScopesCalendar,
                              "user",
                              CancellationToken.None,
                              new FileDataStore(credPath, true)).Result;
                            Console.WriteLine("Credential file saved to: " + credPath);
                        }
            */
            using (var stream =
                       new FileStream("credentialsSheet.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "tokenSheet.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                  GoogleClientSecrets.Load(stream).Secrets,
                  Scopes,
                  "user",
                  CancellationToken.None,
                  new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var servicecalendar = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationNameCalendar,
            });

            return servicecalendar;
            /*
            var sheet = service.Sheets.newSpreadsheet();sh
            sheet.properties = Sheets.newSpreadsheetProperties();
            sheet.properties.title = "TEST_creationfichier";
            var spreadsheet = Sheets.Spreadsheets.create(sheet);
            */
            // CelluleSheet mycellulesheet = new CelluleSheet(service, "Class Data", "A2", "Tata");
            // service.
        }

        void writeintoGooglesheet(string id, string oFixtureJsoncourant, int compteur, string championnat_in, string typefeuillet, Statsteams sttencours, StatsLeagues stlencours)// (string[] args)
        {
            //service =  GetServiceOnGooglesheet();
            string championnat = championnat_in;
            string range = "";
            string idcourant = "";
            string feuillet = "";
            //lb_league.SelectedItem.ToString();
            // string idmatch = "571484";
            if (typefeuillet == "championnat")
            {
                idcourant = GetRefCelluleId(service, championnat, id);
                range = "B" + idcourant + ":V";
                oFixtureJsoncourant = oFixtureJson;
                CelluleSheet mycellulesheet = new CelluleSheet(service, championnat, range, oFixtureJson, compteur);
            }

            if (typefeuillet == "stat_team")
            {
                feuillet = championnat + "_teams"; //feuillet
                idcourant = sttencours.refcelluleidteam; //    GetRefCelluleId_Match(service, championnat, id);
                range = "F" + idcourant + ":Q";
                oFixtureJsoncourant = oFixtureJson;
                //contexte d'appel sera different... on passe plus json...mais un objet stat_team
                CelluleSheet mycellulesheet = new CelluleSheet(service, feuillet, range, oFixtureJson, compteur, sttencours); ;
            }

            if (typefeuillet == "stat_leagues")
            {
                feuillet = "championnatsleagues";// championnat + "_teams"; //feuillet
                idcourant = stlencours.refcelluleidleague; // sttencours.refcelluleidteam; //    GetRefCelluleId_Match(service, championnat, id);
                range = "F" + idcourant + ":N";
                oFixtureJsoncourant = oFixtureJson;
                //contexte d'appel sera different... on passe plus json...mais un objet stat_team
                CelluleSheet mycellulesheet = new CelluleSheet(service, feuillet, range, oFixtureJson, compteur, stlencours); ;
            }

            if (typefeuillet == "stat_mitemps")
            {
                feuillet = championnat_in;// "championnatsleagues";// championnat + "_teams"; //feuillet
                idcourant = GetRefCelluleId(service, championnat, id); // a verifier
                range = "AA" + idcourant + ":BJ";
                // = oFixtureJson;
                //contexte d'appel sera different... on passe plus json...mais un objet stat_team
                CelluleSheet mycellulesheet = new CelluleSheet(service, feuillet, range, oFixtureJsoncourant, compteur, "oneshot"); ;
            }

            // service.
        }

        void QueryUpdateSheet()
        {
            //SpreadsheetsService service = new SpreadsheetsService("MySpreadsheetIntegration-v1");

            // TODO: Authorize the service object for a specific user (see other sections)
            // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.

        }

        //creer la fonction qui vient mettre a jour une donnée precise (a priori ca al'air de pouvoir fonctionner.

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.AppendText(lb_typeGet.SelectedItem.ToString());
                
            /*
            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
            {
                if (lb_league.SelectedIndex == 0)
                {
                    {
                        league = "2664";
                    }
                }
                if (lb_league.SelectedIndex == 1)
                {
                    {
                        league = "2652";
                    }
                }
                if (lb_league.SelectedIndex == 2)
                {
                    {
                        league = "2755";
                    }
                }
                if (lb_league.SelectedIndex == 3)
                {
                    {
                        league = "2743";
                    }
                }
                if (lb_league.SelectedIndex == 4)
                {
                    {
                        league = "2790";
                    }
                }
                if (lb_league.SelectedIndex == 5)
                {
                    {
                        league = "2794";
                    }
                }
                if (lb_league.SelectedIndex == 6)
                {
                    {
                        league = "2857";
                    }
                }
                if (lb_league.SelectedIndex == 7)
                {
                    {
                        league = "2946";
                    }
                }  //a modifier pours championnats espagnols
                if (lb_league.SelectedIndex == 8)
                {
                    {
                        league = "2833";
                    }
                }
                if (lb_league.SelectedIndex == 9)
                {
                    {
                        league = "2847";
                    }
                }

            }
            else
            {
                if (lb_league.SelectedIndex == 0)
                {
                    {
                        league = "61";
                    }
                }
                else
                {
                    league = "62";
                }

            }

            season = lb_saison.SelectedItem.ToString();

            
            textBox1.Text = "";
            listBox1.Items.Clear();


            if (lb_typeGet.SelectedItem == "Liste matchs Championnat")
            {
                typeGetApiFootball = "fixture";
            }

            if (lb_typeGet.SelectedItem == "Liste matchs championnats finis depuis la veille")
            {
                typeGetApiFootball = "fixture_FT_veille";
            }
            if (lb_typeGet.SelectedItem == "Journée en cours")
            {

                typeGetApiFootball = "roundencours";
                
                string getApifootballbefore = typeGetApiFootball;
                typeGetApiFootball = "roundencours";
                GetApifootball(typeGetApiFootball, league, season);
                typeGetApiFootball = getApifootballbefore;
                
                textBox1.Text = "";
                listBox1.Items.Clear();
            }


            if (typeGetApiFootball == "roundencours")
            {
                GetApifootballRoundencours(typeGetApiFootball, league, season);
            }
            else
            {
                GetApifootball(typeGetApiFootball, league, season);
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //example System.IO.File.WriteAllLines(@"D:\sku3.txt", lb.Items.Cast<string>().ToArray());
            string chainelocal = typeGetApiFootball + league + season;
            System.IO.File.WriteAllLines("C:\\Users\\Amaryp\\Documents\\exportlist" + chainelocal + ".csv", listBox1.Items.Cast<string>().ToArray());
            /*
            StreamWriter myOutputStream = new StreamWriter("c:exportlistboxpasp.csv");
            foreach (string item in listBox1.Items)
            {
                myOutputStream.WriteLine(item);
            }
            myOutputStream.Close();
            */
        }

        private void lb_league_SelectedIndexChanged(object sender, EventArgs e)
        {
            //a changer...
            if (lb_league.SelectedIndex == 0)
            {
                nameleague = "Ligue1";
            }
            if (lb_league.SelectedIndex == 1)
            {
                nameleague = "Ligue2";
            }
            if (lb_league.SelectedIndex == 2)
            {
                nameleague = "Bundesliga";
            }
            if (lb_league.SelectedIndex == 3)
            {
                nameleague = "Bundesliga2";
            }
            if (lb_league.SelectedIndex == 4)
            {
                nameleague = "Premier_league";
            }
            if (lb_league.SelectedIndex == 5)
            {
                nameleague = "Championship";
            }
            if (lb_league.SelectedIndex == 6)
            {
                nameleague = "SerieA";
            }
            if (lb_league.SelectedIndex == 7)
            {
                nameleague = "SerieB";
            }
            if (lb_league.SelectedIndex == 8)
            {
                nameleague = "LaLiga";
            }
            if (lb_league.SelectedIndex == 9)
            {
                nameleague = "LaLiga2";
            }

            //2833            2847
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(ClasseurChampionnatFranceLigue1.DATE_MAJ_col);
            // writeintoGooglesheet();
            //  Console.WriteLine(lb_typeGet.SelectedItem);
            //Console.WriteLine(lb_typeGet.SelectedItem.ToString());
            //.ToString());
            //  ToString);
            // ToString);
            //);

            //     SelectedItem);

            var obj = JsonConvert.DeserializeObject<RootobjectTeam>(textBox1.Text);
            ;
            // var obj = JsonConvert.DeserializeObject<RootobjectRound>(json);
            //initialisation de la ligne de titre
            //listBox1.Items.Add("ROUND_EN_COURS,DATE_MAJ");
            //ateTime now = DateTime.Now;
            foreach (var item in obj.response)
            //ResponseFixture)
            {
                //objet feature
                listBox1.Items.Add(
                  item.ToString() + ";"

                 );

            }
        }
        private string GetDay_Round_Int(string Id_round_string)
        {
            string resultat = "";
            Id_round_string = Id_round_string.Trim();
            int finstring = Id_round_string.Length;
            string boutstring;
            //  resultat.Substring(1);
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

        public string GetNulOrZero(string statut_match, string score_home, string score_away)
        {
            string resultat = ";;";
            //ajout des calculs match nul et 0-0
            if (statut_match == "FT")// (si on peux c'est a dire si le match est a statut fini.)
            {
                resultat = IsMatchNul(score_home, score_away);
                resultat = resultat + IsMatchZeroZero(score_home, score_away);
            }
            return resultat;
        }

        public string VictoireDomicile(string statut_match, string score_home, string score_away)
        {
            string resultat = "";
            //ajout des calculs match nul et 0-0
            if (statut_match == "FT")// (si on peux c'est a dire si le match est a statut fini.)
            {
                if (int.Parse(score_home) > int.Parse(score_away))
                {
                    resultat = "TRUE";
                }
                else { resultat = "FALSE"; }

            }
            return resultat;
        }

        public string VictoireExterieure(string statut_match, string score_home, string score_away)
        {
            string resultat = "";
            //ajout des calculs match nul et 0-0
            if (statut_match == "FT")// (si on peux c'est a dire si le match est a statut fini.)
            {
                if (int.Parse(score_away) > int.Parse(score_home))
                {
                    resultat = "TRUE";
                }
                else { resultat = "FALSE"; }


            }
            return resultat;
        }

        private string IsMatchNul(string score_home, string score_away)
        {
            string resultat = "FALSE;";
            //est ce un match nul
            if (score_home == score_away)
            {
                resultat = "TRUE;";
            }
            return resultat;
        }
        private string IsMatchZeroZero(string score_home, string score_away)
        {
            string resultat = "FALSE;";
            //est ce un zero a zero   
            if (score_home == "0" & score_away == "0")
            {
                resultat = "TRUE;";
            }
            return resultat;
        }

        private void lb_typeGet_SelectedIndexChanged(object sender, EventArgs e)
        {
             //
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //service =  GetServiceOnGooglesheet();

            // GetRefCelluleId_Fixture myGecf = new GetRefCelluleId_Fixture(service, "Class Data", "Tutu");
            // Console.WriteLine(GetRefCelluleId(service, "Ligue1", "571473"));
            //Console.WriteLine(scorehome("5-0"));
            //  Console.WriteLine(scoreaway("5-0"));
            Console.WriteLine(CurrentroundFromJson("erjgreoigjerge[vghh]rjhoierjhzzz"));// "{ "api":{ "results":1,"fixtures":["Regular_Season_-_2"]} })
                                                                                        //{"api":{"results":1,"fixtures":["Regular_Season_-_2"]}}//

        }

        public string GetRefCelluleId(SheetsService myss, string feuillet, string Id)
        {
            //Console.WriteLine("recherche ref cellule");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet Ligue1
                                                                                  //String range = "Class Data!A2";  // single cell D5
                                                                                  //range par feuillet./championnat ou equipe
                                                                                  //range par feuillet./championnat ou equipe
            String range = "";

            if (feuillet == "Ligue1")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Ligue2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A307";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A307";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Premier_league")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Championship")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A553";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "SerieA")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "SerieB")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "LaLiga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "LaLiga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble// peut etre pas le meme nombre d'equipe.
            {
                range = feuillet + "!A1:A463";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }


            if (feuillet == "Ligue1_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Ligue2_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga_teams")
            {
                range = feuillet + "!A1:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga2_teams")
            {
                range = feuillet + "!A1:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Premier_league_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Championship_teams")
            {
                range = feuillet + "!A1:A25";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }


            if (feuillet == "SerieA_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "SerieB_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "LaLiga_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "LaLiga2_teams")
            {
                range = feuillet + "!A1:A23";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }


            if (feuillet == "championnatsleagues")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A11";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            ValueRange valueRange = new ValueRange();
            string resultat = "NON TROUVE";
            // SpreadsheetsResource.CreateRequest.
            valueRange.MajorDimension = "COLUMNS";//"ROWS";//COLUMNS

            SpreadsheetsResource.ValuesResource.GetRequest quest = myss.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange recupQuest = quest.Execute();

            IList<IList<Object>> values = recupQuest.Values;

            int i;
            i = 0;
            int z = 1;
            string s1;
            string s2 = Id;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Console.WriteLine(row[i].ToString());
                    s1 = row[i].ToString();

                    if (IsFind(s1, s2, z) == true)
                    {
                        resultat = String.Concat("", z);// Convert.ToString(i);    
                    }

                    z = z + 1;
                }

            }
            //Console.WriteLine(recupQuest.Values[0].ToString());
            return resultat;
        }

        public bool IsFind(string s1, string s2, int index)
        {
            bool resultat = false;
            if (s1 == s2)
            {
                resultat = true;
                //Console.WriteLine("trouve :" + s1 + " " + s2 + " " + index);
            }

            return resultat;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //on va passe certains elements en parametre.  championnat, type de mise à jour et saison

            MajChampionnat(typeGetApiFootball, lb_league.SelectedItem.ToString(), "PARTIEL");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //on va devoir encapsuler l'appel pour chaque equipe. en se basant sur une liste. (ah ben la liste des equipes...que l'on charge via l'appel en mode range tout simplement.

            //StatTeam(typeGetApiFootball);
            GetList_Teams(service, "Ligue1");

            //
            // Console.WriteLine(lb_league.SelectedIndex);
            // lb_saison.SetSelected(2, true);  //2018
            //set lb_league.SelectedIndex = 1;
            // Console.WriteLine(lb_league.SelectedIndex);
            //SelectedIndex

        }

        private void GestionModeAuto(string prog, string leagues_in)
        {
            //if (ConfigurationManager.AppSettings["program_auto1"] == "program1")
            //mise a jour des stat_leagues
            //le string leagues_in pas spécifié au demarrag alors ="ALL"
            string[,] tab_leagues = new string[,] {
               {"2664","Ligue1" },
               {"2652","Ligue2" },
               {"2755", "Bundesliga"},
               {"2743", "Bundesliga2" },
               {"2790", "Premier_league"},
               {"2794", "Championship" },
               {"2857","SerieA" },
               {"2946", "SerieB"}, 
               {"2833","LaLiga"},
               {"2847","LaLiga2"} 
            };
            if (prog == "statleagues")
            {
                if (leagues_in == "ALL") //traitement de l'ensemble
                {
                    season = "2020";
                    for (int i = 0; i < 10; i++)
                    {
                        // Console.WriteLine(tab_leagues[i, 0]);
                        //  Console.WriteLine(tab_leagues[i, 1]);
                        league = tab_leagues[i, 0];
                        nameleague = tab_leagues[i, 1];
                        typeGetApiFootball = "roundencours";
                        GetApifootballRoundencours(typeGetApiFootball, league, season);
                        System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                                                            //stat_league
                        typeGetApiFootball = "fixture"; //recherche globale sur toute la saison et tous les matchs
                        GetApifootball(typeGetApiFootball, league, season);
                        System.Threading.Thread.Sleep(10000);// pour une pause de 10 secondes.
                                                             //lancement mise a jour de la requete 
                        StatLeaguesAlpha(typeGetApiFootball, league, nameleague);
                        System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                        Console.WriteLine("on sors");
                        System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                    }
                }
                else
                {//traitement unitaire
                 //A adapter avec le calcul round en cours, on 
                 //optimisera avec le stockage des round current et date maj dans le ficier de config
                 //on va calculer directement le currentround
                    if (leagues_in == "2664") { league = leagues_in; nameleague = "Ligue1"; }
                    if (leagues_in == "2652") { league = leagues_in; nameleague = "Ligue2"; }
                    if (leagues_in == "2755") { league = leagues_in; nameleague = "Bundesliga"; }
                    if (leagues_in == "2743") { league = leagues_in; nameleague = "Bundesliga2"; }
                    if (leagues_in == "2790") { league = leagues_in; nameleague = "Premier_league"; }
                    if (leagues_in == "2794") { league = leagues_in; nameleague = "Championship"; }
                    if (leagues_in == "2857") { league = leagues_in; nameleague = "SerieA"; }
                    if (leagues_in == "2946") { league = leagues_in; nameleague = "SerieB"; }
                    if (leagues_in == "2833") { league = leagues_in; nameleague = "LaLiga"; }
                    if (leagues_in == "2847") { league = leagues_in; nameleague = "LaLiga2"; }

                    season = "2020";//pour l'instant en dur...
                    typeGetApiFootball = "roundencours";
                    GetApifootballRoundencours(typeGetApiFootball, league, season);
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                                                        //stat_league
                    typeGetApiFootball = "fixture"; //recherche globale sur toute la saison et tous les matchs
                    GetApifootball(typeGetApiFootball, league, season);
                    System.Threading.Thread.Sleep(10000);// pour une pause de 10 secondes.
                                                         //lancement mise a jour de la requete 
                    StatLeaguesAlpha(typeGetApiFootball, league, nameleague);
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                    Console.WriteLine("on sors");
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                }
            }
            if (prog == "statteams")
            {
                if (leagues_in == "ALL") //traitement de l'ensemble
                {
                    season = "2020";
                    for (int i = 0; i < 10; i++)
                    {
                        
                       // Console.WriteLine(tab_leagues[i,0]);
                       // Console.WriteLine(tab_leagues[i, 1]);
                        league = tab_leagues[i, 0];
                        nameleague = tab_leagues[i, 1];
                       
                        typeGetApiFootball = "roundencours";
                        GetApifootballRoundencours(typeGetApiFootball, league, season);
                        //stat_league
                        typeGetApiFootball = "fixture"; //recherche globale sur toute la saison et tous les matchs
                        GetApifootball(typeGetApiFootball, league, season);
                        System.Threading.Thread.Sleep(10000);// pour une pause de 5 secondes.
                        GetList_Teams(service, nameleague);//lancement mise a jour de la requete 
                        System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                        Console.WriteLine("on sors");
                        System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.

                    }
                }
                else
                {//traitement unitaire
                    //on va calculer directement le currentround
                    //on va calculer directement le currentround
                    if (leagues_in == "2664") { league = leagues_in; nameleague = "Ligue1"; }
                    if (leagues_in == "2652") { league = leagues_in; nameleague = "Ligue2"; }
                    if (leagues_in == "2755") { league = leagues_in; nameleague = "Bundesliga"; }
                    if (leagues_in == "2743") { league = leagues_in; nameleague = "Bundesliga2"; }
                    if (leagues_in == "2790") { league = leagues_in; nameleague = "Premier_league"; }
                    if (leagues_in == "2794") { league = leagues_in; nameleague = "Championship"; }
                    if (leagues_in == "2857") { league = leagues_in; nameleague = "SerieA"; }
                    if (leagues_in == "2946") { league = leagues_in; nameleague = "SerieB"; }
                    if (leagues_in == "2833") { league = leagues_in; nameleague = "LaLiga"; }
                    if (leagues_in == "2847") { league = leagues_in; nameleague = "LaLiga2"; }

                    //recuperer pour Bundesliga
                    season = "2020";
                    /*
                    string getApifootballbefore = typeGetApiFootball;
                    typeGetApiFootball = "roundencours";
                    GetApifootball(typeGetApiFootball, league, season);
                    typeGetApiFootball = getApifootballbefore;
                    */
                    //comme test on va simplement charger la ligue2 2018 sans rien faire d'autre
                    //on mets a jour la ligue 1 2020
                    //lancement de la recherche sur api football 
                    //typeGetApiFootball = "fixture"; //recherche globale sur toute la saison et tous les matchs
                    //System.Threading.Thread.Sleep(10000);// pour une pause de 10 seconde.
                    typeGetApiFootball = "roundencours";
                    GetApifootballRoundencours(typeGetApiFootball, league, season);
                    //stat_league
                    typeGetApiFootball = "fixture"; //recherche globale sur toute la saison et tous les matchs
                    GetApifootball(typeGetApiFootball, league, season);
                    System.Threading.Thread.Sleep(10000);// pour une pause de 5 secondes.
                    GetList_Teams(service, nameleague);//lancement mise a jour de la requete 
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                    Console.WriteLine("on sors");
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                }
            }

            if (prog == "matchs") //maj des championnats
            {
                Console.WriteLine("on est bien dans le bon programme");
                //on va calculer directement le currentround
               //                 else
                //{ // traitement unitaire

                    if (leagues_in == "2664") { league = leagues_in; nameleague = "Ligue1"; }
                    if (leagues_in == "2652") { league = leagues_in; nameleague = "Ligue2"; }
                    if (leagues_in == "2755") { league = leagues_in; nameleague = "Bundesliga"; }
                    if (leagues_in == "2743") { league = leagues_in; nameleague = "Bundesliga2"; }
                    if (leagues_in == "2790") { league = leagues_in; nameleague = "Premier_league"; }
                    if (leagues_in == "2794") { league = leagues_in; nameleague = "Championship"; }
                    if (leagues_in == "2857") { league = leagues_in; nameleague = "SerieA"; }
                    if (leagues_in == "2946") { league = leagues_in; nameleague = "SerieB"; }
                    if (leagues_in == "2833") { league = leagues_in; nameleague = "LaLiga"; }
                    if (leagues_in == "2847") { league = leagues_in; nameleague = "LaLiga2"; }

                    //recuperer pour bundesliga
                    season = "2020";
                    Console.WriteLine("avant appel roudncurrent");
                    typeGetApiFootball = "roundencours";
                    GetApifootballRoundencours(typeGetApiFootball, league, season);
                    Console.WriteLine("apres appel roudncurrent");
                    Console.WriteLine("avant appel fixture matchs");
                    typeGetApiFootball = "fixture"; //recherche globale sur toute la saison et tous les matchs
                    GetApifootball(typeGetApiFootball, league, season);
                    Console.WriteLine("apres appel fixture matchs");
                    System.Threading.Thread.Sleep(10000);// pour une pause de 5 secondes.
                                                         //lancement de la requete de mise a jour championnat
                    Console.WriteLine("avant maj championnat");
                    MajChampionnat(typeGetApiFootball, nameleague, "PARTIEL");
                    Console.WriteLine("apres maj championnat");
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                    Console.WriteLine("on sors");
                    System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
               // }
            }
            if (prog == "agenda")
            {
                Console.WriteLine("on est bien dans le bon programme");
                //on mets a jour l'ensemble des evenements... toutes ligues confondues.
                NettoyageAgendaAppConfig();
                CelluleSheet servicecellulesheet = new CelluleSheet();
                //servicecellulesheet.DuplicateSheet(service, "IDMATCH_592917", "rien", "rien");
                servicecellulesheet.Duplicatanddeletesheetfichesmatchs(service);
                GetMatchNext24h(service, "Ligue1", "");
                GetMatchNext24h(service, "Ligue2", "");
                GetMatchNext24h(service, "Bundesliga", "");
                GetMatchNext24h(service, "Bundesliga2", "");
                GetMatchNext24h(service, "Premier_league", "");
                GetMatchNext24h(service, "Championship", "");
                GetMatchNext24h(service, "SerieA", "");
                GetMatchNext24h(service, "SerieB", "");
                GetMatchNext24h(service, "LaLiga", "");
                GetMatchNext24h(service, "LaLiga2", "");

                System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                Console.WriteLine("on sors");
                System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
            }

            if (prog == "scan_matchs")
            {
                Console.WriteLine("on est bien dans le bon programme : scan_matchs");
                scan_matchs();
                //on analyse si on est a la mi temps d'un ou plusieurs matchs programmé
                //si c'est le cas, on requete pour avoir le score et stat
                //et si les stats sont ok, alors on lance une alerte pour planifier un evenement dans le calendrier
                System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
                Console.WriteLine("on sors");
                System.Threading.Thread.Sleep(5000);// pour une pause de 1/2 seconde.
            }

            //sortie du programme.
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
            // Console.WriteLine(sender.ToString());
        }

        private void MajChampionnat(string typegetapifootball, string championnat, string mode_in)
        {

            NettoyageRatiosConfig(championnat);
            //on va mettre à jour les elements du classeur (si il y a lieu..)
            //contexte uniquement pour certains type de recherche (match de la veille par exemple (avant de passer à la date du dernier export...)
            //string[] passageenc------------------------------ours= new string[100];
            //on essaye de faire la mise a jour en entier pour voir...
            string mode_maj = "";
            if (mode_in == "TOTAL") { mode_maj = "TOTAL"; }
            else { mode_maj = "PARTIEL"; } //partiel ou total //on teste }
            string typefeuillet = "championnat";
            Statsteams sttbidon = new Statsteams();

            if ((typegetapifootball == "fixture") || (typegetapifootball == "fixture_FT_veille"))
            //if (typeGetApiFootball == "fixture_FT_veille") //donc uniquement si if (typeGetApiFootball == "fixture_FT_veille")
            {
                if (listBox1.Items.Count > 0) //uniquement si il ya des matchs a mettre a jour..
                {

                    WriteAppconfigBeforeMaj(championnat, "ras");

                    //pour le moment on chope l'id match unique dans la premiere instance
                    int compteur = 0;
                    int max = listBox1.Items.Count - 1;
                    DateTime dateheure2j = DateTime.Today.AddDays(2);

                    while (compteur < max)
                    {
                        //on passe les valeurs dans passage...
                        //c'est a cet endroit qu'on va arbitrer suivant mise a jour total ou partiel.
                        if (mode_maj == "PARTIEL")
                        {
                            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                            {
                                var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(oFixtureJson);
                                if (obj.api.fixtures[compteur].statusShort != "TBD") //bref different de TBD...["FT", "CAN", "NS"]
                                {
                                    //on va ajouter un filtre supplementaire pour tous les championnats ? sur la date d'echeance + 2 jours au dela on ne mets pas a jour
                                    DateTime recupdateheure = DateTime.Parse(obj.api.fixtures[compteur].event_date.ToString());
                                    //< 0 − If date1 is earlier than date2    //  0 − If date1 is the same as date2   // > 0 − If date1 is later than date2
                                    if (DateTime.Compare(recupdateheure.Date, dateheure2j.Date) <= 0)
                                    {
                                        writeintoGooglesheet(obj.api.fixtures[compteur].fixture_id.ToString(), oFixtureJson, compteur, championnat, typefeuillet, sttbidon, new StatsLeagues("60", "bidon", 1, 1));
                                    }
                                }
                            }
                            else //appel pas alpha
                            {
                                var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJson);
                                //ok strategieu de maj differente, en angleterre toutes les status des matchs non joué sont a NS...
                                if (obj.response[compteur].fixture.status.Short.ToString() != "TBD") //bref different de TBD...["FT", "CAN", "NS"]
                                {
                                    //on va ajouter un filtre supplementaire pour tous les championnats ? sur la date d'echeance + 2 jours au dela on ne mets pas a jour
                                    DateTime recupdateheure = DateTime.Parse(obj.response[compteur].fixture.date.ToString());
                                    //< 0 − If date1 is earlier than date2    //  0 − If date1 is the same as date2   // > 0 − If date1 is later than date2
                                    if (DateTime.Compare(recupdateheure.Date, dateheure2j.Date) <= 0) //on analyse si on est le meme jour deja (puique 24h de matchs)
                                    {
                                        writeintoGooglesheet(obj.response[compteur].fixture.id.ToString(), oFixtureJson, compteur, championnat, typefeuillet, sttbidon, new StatsLeagues("60", "bidon", 1, 1));
                                    }
                                }
                            }

                        }


                        if (mode_maj == "TOTAL") // surtout utilisé pour les initialisations

                            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                            {
                                var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(oFixtureJson);
                                // if (obj.api.fixtures[compteur].statusShort != "TBD") //bref different de TBD...["FT", "CAN", "NS"]
                                // {
                                //on va ajouter un filtre supplementaire pour tous les championnats ? sur la date d'echeance + 2 jours au dela on ne mets pas a jour
                                //   DateTime recupdateheure = DateTime.Parse(obj.api.fixtures[compteur].event_date.ToString());
                                //< 0 − If date1 is earlier than date2    //  0 − If date1 is the same as date2   // > 0 − If date1 is later than date2
                                // if (DateTime.Compare(recupdateheure.Date, dateheure2j.Date) <= 0)
                                //   {
                                writeintoGooglesheet(obj.api.fixtures[compteur].fixture_id.ToString(), oFixtureJson, compteur, championnat, typefeuillet, sttbidon, new StatsLeagues("60", "bidon", 1, 1));
                                //    }
                                // }

                               // compteur = compteur + 1;
                            }
                        compteur = compteur + 1;

                    }
                }

            }
            // }
            //si on arrive a ce stade c'est que tout est ok
            WriteAppconfigAfterMaj(championnat, "ras");

        }
        private void WriteAppconfigBeforeMaj(string championnat, string modemaj)
        {

            config.AppSettings.Settings.Remove("STATUS_MAJ_" + championnat);
            config.AppSettings.Settings.Add("STATUS_MAJ_" + championnat, "DEBUT A : " + DateTime.Now.ToString()); //=> si il y avait encours c('est que plantage')
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void WriteAppconfigAfterMaj(string championnat, string modemaj)
        {
            config.AppSettings.Settings.Remove("DATEHEURE_MAJ_" + championnat);
            config.AppSettings.Settings.Add("DATEHEURE_MAJ_" + championnat, DateTime.Now.ToString()); //=> si il y avait encours c('est que plantage') j'aurais bien aimé enregistré le debut de la mise a jour mais bon..
            config.Save(ConfigurationSaveMode.Modified);//je peux enregistrer la date de debut dans la valeur status, ainsi si on a autre chose que "OK" c'est que c'est pas bon
            config.AppSettings.Settings.Remove("STATUS_MAJ_" + championnat);
            config.AppSettings.Settings.Add("STATUS_MAJ_" + championnat, "OK"); //=> si il y avait encours c('est que plantage')
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");



        }

        private string WriteAppconfigBeforeRequete(string typeAppel, string championnat, string modemaj)
        {
            string retour = "KO";

            //par precaution
            string championnat_in = "";

            if ((championnat == "Ligue1") | (championnat == "2664")) { championnat_in = "Ligue1"; }
            if ((championnat == "Ligue2") | (championnat == "2652")) { championnat_in = "Ligue2"; }
            if ((championnat == "Bundesliga") | (championnat == "2755")) { championnat_in = "Bundesliga"; }
            if ((championnat == "Bundesliga2") | (championnat == "2743")) { championnat_in = "Bundesliga2"; }
            if ((championnat == "Premier_league") | (championnat == "2790")) { championnat_in = "Premier_league"; }
            if ((championnat == "Championship") | (championnat == "2794")) { championnat_in = "Championship"; }
            if ((championnat == "SerieA") | (championnat == "2857")) { championnat_in = "SerieA"; }
            if ((championnat == "SerieB") | (championnat == "2946")) { championnat_in = "SerieB"; }
            if ((championnat == "LaLiga") | (championnat == "2833")) { championnat_in = "LaLiga"; }
            if ((championnat == "LaLiga2") | (championnat == "2847")) { championnat_in = "LaLiga2"; }




            if (typeAppel == "championnat")
            {
                config.AppSettings.Settings.Remove("STATUS_MAJ_" + championnat_in);
                config.AppSettings.Settings.Add("STATUS_MAJ_" + championnat_in, "DEBUT A : " + DateTime.Now.ToString()); //=> si il y avait encours c('est que plantage')
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }



            if (typeAppel == "CurrentRound")
            {
                //on va ajouter les valeurs des rounds currents pour les ligues/ l'idée c'est si déjà fait ce jour, alors on recupere la valeur du jour//sinon on l'enregistre...
                if (ConfigurationManager.AppSettings["STATUS_MAJ_CURRENTROUND_" + championnat_in] == DateTime.Now.ToString("dd/MM/yyyy")) // si c'est le meme jour.
                {
                    CurrentRound = ConfigurationManager.AppSettings["CURRENTROUND_" + championnat_in]; // est ce que la valeur existe et est un numerique
                    retour = "OK";
                }

            }
            return retour;
        }


        private void WriteAppconfigAfterRequete(string typeAppel, string championnat, string modemaj)
        {

            //par precaution
            string championnat_in = "";

            if ((championnat == "Ligue1") | (championnat == "2664")) { championnat_in = "Ligue1"; }
            if ((championnat == "Ligue2") | (championnat == "2652")) { championnat_in = "Ligue2"; }
            if ((championnat == "Bundesliga") | (championnat == "2755")) { championnat_in = "Bundesliga"; }
            if ((championnat == "Bundesliga2") | (championnat == "2743")) { championnat_in = "Bundesliga2"; }
            if ((championnat == "Premier_league") | (championnat == "2790")) { championnat_in = "Premier_league"; }
            if ((championnat == "Championship") | (championnat == "2794")) { championnat_in = "Championship"; }
            if ((championnat == "SerieA") | (championnat == "2857")) { championnat_in = "SerieA"; }
            if ((championnat == "SerieB") | (championnat == "2946")) { championnat_in = "SerieB"; }
            if ((championnat == "LaLiga") | (championnat == "2833")) { championnat_in = "LaLiga"; }
            if ((championnat == "LaLiga2") | (championnat == "2847")) { championnat_in = "LaLiga2"; }




            if (typeAppel == "CurrentRound")
            {
                //on va ajouter les valeurs des rounds currents pour les ligues/ l'idée c'est si déjà fait ce jour, alors on recupere la valeur du jour//sinon on l'enregistre...
                config.AppSettings.Settings.Remove("CURRENTROUND_" + championnat_in);
                config.AppSettings.Settings.Add("CURRENTROUND_" + championnat_in, CurrentRound);
                config.AppSettings.Settings.Remove("STATUS_MAJ_CURRENTROUND_" + championnat_in);
                config.AppSettings.Settings.Add("STATUS_MAJ_CURRENTROUND_" + championnat_in, DateTime.Now.ToString("dd/MM/yyyy"));
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            // return retour;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //GestionModeAuto();
            // GestionModeAuto("statleagues", "2664");
            //GestionModeAuto("statteams", "62");
            /*
              if (param1 == "auto")
              {
                  GestionModeAuto(param2, param3);
              }
            */
            //GestionModeAuto("matchs", "2652");
            GestionModeAuto("scan_matchs", "all");
        }


        private void StatTeam(string typegetapifootball, string idteam, string nameleague)
        {
            string typefeuillet = "stat_team";
            //calcul de l'ensemble des valeurs.

            //donc une premiere boucle sur l'equipe(via la table de reference des equipes. mais on l'ajoutera ensuite, apres la mise au point

            //stat gobal, non utilisé au final mais necessaire pour controle ou calcul
            //nb de matchs joué par equipe.
            //pour le moment on ne se complique pas la vie avec des calculs victoire exterieur etc... a moins que l'on utilise ca comme controle.. et en vue d'une stat plus affinié ?
            //nb de victoire
            //nb de defaite
            //nb de match nul depuis le debut de la saison
            //nb de match zero zero depuis le debut de la saison
            //statistique sur les buts
            //moyenne de but marqué
            //moyenne de but encaissé
            //resultat des 10 derniers match de l'equipe : 
            //nb de match nul sur les 10 derniers matchs
            //nb de match zero zero sur les 10 derniers matchs
            //nb de match nul sur les 5 derniers matchs
            //nb de match zero zero sur les 5 derniers matchs
            //Reims = 93. deja deux matchs joués...
            Statsteams stt = new Statsteams();
            if ((typegetapifootball == "fixture") && (listBox1.Items.Count > 0))
            {
                //if (typeGetApiFootball == "fixture_FT_veille") //donc uniquement si if (typeGetApiFootball == "fixture_FT_veille")

                stt.idteam = idteam;// "93";//Reims
                stt.nameteam = "NON RENSEIGNE";
                stt.league = league;// "61";//ok celui on la
                stt.name_league = nameleague;
                /*switch (league)
                {
                    case "61":
                    stt.name_league = "Ligue1";
                    case "62":
                    stt.name_league = "Ligue2";
                }
                }*/
                //stt.nameteam = "Reims"; //=> celui non...hum
                //recup de la refcellule
                stt.refcelluleidteam = GetRefCelluleId(service, stt.name_league + "_teams", stt.idteam);
                var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJson);
                int compteur = 0;
                int max = listBox1.Items.Count - 1;

                // idteam = "93"; //reims
                int nbmatchjouesdomicile = 0;
                int nbmatchjouesexterieur = 0;
                int nbvictoiredomicile = 0;
                int nbdefaitedomicile = 0;
                int nbnuldomicile = 0;
                int nbzerozerodomicile = 0;
                int nbvictoireexterieur = 0;
                int nbdefaiteexterieur = 0;
                int nbnulexterieur = 0;
                int nbzerozeroexterieur = 0;
                int butmisdomicile = 0;
                int butprisdomicile = 0;
                int butmisexterieur = 0;
                int butprisexterieur = 0;
                //totalisation
                int nbmatchjoues = 0;
                int nbvictoire = 0;
                int nbdefaite = 0;
                int nbnul = 0;
                int nbzerozero = 0;
                int butmis = 0;
                int butpris = 0;

                //function non construite pour le moment
                //il faudra stocker les elements en faisant sortir de la pile les premier rentré au bout de 10 et 5 respectivement.
                //pour le  moment on stocke la totalisation sur l'ensemble de la saison...
                int nbnul10derniermatch = 0;
                int nbzerozero10derniermatch = 0;
                int nbnul5derniermatch = 0;
                int nbzerozero5derniermatch = 0;

                double ratiobutmis = 0;
                double ratiobutpris = 0;
                //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJson);
                // obj.response[compteur].fixture.
                //if (obj.response[compteur].fixture.status.Short.ToString() != "TBD") //bref different de TBD...["FT", "CAN", "NS"]
                // {
                //   writeintoGooglesheet(obj.response[compteur].fixture.id.ToString(), oFixtureJson, compteur, championnat);

                // }
                while (compteur < max)
                {
                    if (obj.response[compteur].fixture.status.Short.ToString() == "FT")//uniquement sur les matchs joués..
                                                                                       //on commence par rechercher l'idteam
                    {
                        if (obj.response[compteur].teams.home.id.ToString() == stt.idteam)
                        {
                            //au premier passage on recupere le nom de l'equipe.
                            if (stt.nameteam == "NON RENSEIGNE")
                            {
                                stt.nameteam = obj.response[compteur].teams.home.name;
                            }
                            nbmatchjouesdomicile++;
                            //on regarde si c'est une victoire/defaite/nul
                            if ((obj.response[compteur].teams.home.winner == true) && (obj.response[compteur].teams.away.winner == false))  //victoire
                            {
                                nbvictoiredomicile++;
                            }
                            if ((obj.response[compteur].teams.home.winner == false) && (obj.response[compteur].teams.away.winner == true))//defaite
                            {
                                nbdefaitedomicile++;
                            }
                            if ((obj.response[compteur].teams.home.winner == null) && (obj.response[compteur].teams.away.winner == null)) //nul
                            {
                                nbnuldomicile++;
                            }
                            //addition des buts...
                            butmisdomicile = butmisdomicile + obj.response[compteur].score.fulltime.home.Value;
                            butprisdomicile = butprisdomicile + obj.response[compteur].score.fulltime.away.Value;
                            //est ce un zero a zero
                            if ((obj.response[compteur].score.fulltime.home.Value == 0) && (obj.response[compteur].score.fulltime.away.Value == 0))
                            {
                                nbzerozerodomicile++;
                            }

                        }
                        if (obj.response[compteur].teams.away.id.ToString() == stt.idteam)
                        {
                            //au premier passage on recupere le nom de l'equipe.
                            if (stt.nameteam == "NON RENSEIGNE")
                            {
                                stt.nameteam = obj.response[compteur].teams.away.name;
                            }
                            nbmatchjouesexterieur++;
                            //on regarde si c'est une victoire/defaite/nul
                            if ((obj.response[compteur].teams.away.winner == true) && (obj.response[compteur].teams.home.winner == false))  //victoire
                            {
                                nbvictoireexterieur++;
                            }
                            if ((obj.response[compteur].teams.away.winner == false) && (obj.response[compteur].teams.home.winner == true))//defaite
                            {
                                nbdefaiteexterieur++;
                            }
                            if ((obj.response[compteur].teams.away.winner == null) && (obj.response[compteur].teams.home.winner == null)) //nul
                            {
                                nbnulexterieur++;
                            }
                            //addition des buts...
                            butmisexterieur = butmisexterieur + obj.response[compteur].score.fulltime.away.Value;
                            butprisexterieur = butprisexterieur + obj.response[compteur].score.fulltime.home.Value;
                            //est ce un zero a zero
                            if ((obj.response[compteur].score.fulltime.away.Value == 0) && (obj.response[compteur].score.fulltime.home.Value == 0))
                            {
                                nbzerozeroexterieur++;
                            }

                        }

                    }
                    compteur = compteur + 1;
                }
                //boucle inversé pour recuperer les données uniquement des 5 et 10 derniers matchs.
                int compteurinverse = listBox1.Items.Count - 2;// 0;
                int maxinverse = 0;// listBox1.Items.Count - 1;
                                   //int nbmatchjouesinverse = 0;
                int nbmatchjouesdomicileinverse = 0;
                int nbmatchjouesexterieurinverse = 0;
                int nbvictoiredomicileinverse = 0;
                int nbdefaitedomicileinverse = 0;
                int nbvictoireexterieurinverse = 0;
                int nbdefaiteexterieurinverse = 0;
                int nbzerozerodomicileinverse = 0;
                int nbzerozeroexterieurinverse = 0;
                int nbnulexterieurinverse = 0;
                int nbnuldomicileinverse = 0;
                int butmisdomicileinverse = 0;
                int butmisexterieurinverse = 0;
                int butprisdomicileinverse = 0;
                int butprisexterieurinverse = 0;

                while (compteurinverse >= maxinverse)
                {
                    //Console.WriteLine(obj.response[compteurinverse].fixture.id.ToString());
                    if (obj.response[compteurinverse].fixture.status.Short.ToString() == "FT")//uniquement sur les matchs joués..
                                                                                              //on commence par rechercher l'idteam
                    {
                        if (obj.response[compteurinverse].teams.home.id.ToString() == stt.idteam)
                        {
                            nbmatchjouesdomicileinverse++;
                            //on regarde si c'est une victoire/defaite/nul
                            if ((obj.response[compteurinverse].teams.home.winner == true) && (obj.response[compteurinverse].teams.away.winner == false))  //victoire
                            {
                                nbvictoiredomicileinverse++;
                            }
                            if ((obj.response[compteurinverse].teams.home.winner == false) && (obj.response[compteurinverse].teams.away.winner == true))//defaite
                            {
                                nbdefaitedomicileinverse++;
                            }
                            if ((obj.response[compteurinverse].teams.home.winner == null) && (obj.response[compteurinverse].teams.away.winner == null)) //nul
                            {
                                nbnuldomicileinverse++;
                            }
                            //addition des buts...
                            butmisdomicileinverse = butmisdomicileinverse + obj.response[compteurinverse].score.fulltime.home.Value;
                            butprisdomicileinverse = butprisdomicileinverse + obj.response[compteurinverse].score.fulltime.away.Value;
                            //est ce un zero a zero
                            if ((obj.response[compteurinverse].score.fulltime.home.Value == 0) && (obj.response[compteurinverse].score.fulltime.away.Value == 0))
                            {
                                nbzerozerodomicileinverse++;
                            }

                        }
                        if (obj.response[compteurinverse].teams.away.id.ToString() == stt.idteam)
                        {
                            nbmatchjouesexterieurinverse++;
                            //on regarde si c'est une victoire/defaite/nul
                            if ((obj.response[compteurinverse].teams.away.winner == true) && (obj.response[compteurinverse].teams.home.winner == false))  //victoire
                            {
                                nbvictoireexterieurinverse++;
                            }
                            if ((obj.response[compteurinverse].teams.away.winner == false) && (obj.response[compteurinverse].teams.home.winner == true))//defaite
                            {
                                nbdefaiteexterieurinverse++;
                            }
                            if ((obj.response[compteurinverse].teams.away.winner == null) && (obj.response[compteurinverse].teams.home.winner == null)) //nul
                            {
                                nbnulexterieurinverse++;
                            }
                            //addition des buts...
                            butmisexterieurinverse = butmisexterieurinverse + obj.response[compteurinverse].score.fulltime.away.Value;
                            butprisexterieurinverse = butprisexterieurinverse + obj.response[compteurinverse].score.fulltime.home.Value;
                            //est ce un zero a zero
                            if ((obj.response[compteurinverse].score.fulltime.away.Value == 0) && (obj.response[compteurinverse].score.fulltime.home.Value == 0))
                            {
                                nbzerozeroexterieurinverse++;
                            }

                        }

                    }
                    //on totalise si il y a lieu en fonction du compteur match joué (5/10)
                    if ((nbmatchjouesdomicileinverse + nbmatchjouesexterieurinverse) <= 5)
                    {
                        nbnul5derniermatch = nbnulexterieurinverse + nbnuldomicileinverse;
                        nbzerozero5derniermatch = nbzerozerodomicileinverse + nbzerozeroexterieurinverse;
                    }

                    if ((nbmatchjouesdomicileinverse + nbmatchjouesexterieurinverse) <= 10)
                    {
                        nbnul10derniermatch = nbnulexterieurinverse + nbnuldomicileinverse;
                        nbzerozero10derniermatch = nbzerozerodomicileinverse + nbzerozeroexterieurinverse;

                    }

                    compteurinverse = compteurinverse - 1;
                }

                //totalisation
                nbmatchjoues = nbmatchjouesdomicile + nbmatchjouesexterieur;
                nbvictoire = nbvictoiredomicile + nbvictoireexterieur;
                nbdefaite = nbdefaitedomicile + nbdefaiteexterieur;
                nbnul = nbnuldomicile + nbnulexterieur;
                nbzerozero = nbzerozerodomicile + nbzerozeroexterieur;
                butmis = butmisdomicile + butmisexterieur;
                butpris = butprisdomicile + butprisexterieur;

                ratiobutmis = (float)butmis / (float)nbmatchjoues;
                ratiobutpris = (float)butpris / (float)nbmatchjoues;

                ratiobutmis = Math.Round(ratiobutmis, 2);
                ratiobutpris = Math.Round(ratiobutpris, 2);

                //function non construite pour le moment
                //pour le moment, on recupere les elements ici...(on pourra se passer des variables intermediares)
                stt.nbmatchjouesdomicile = nbmatchjouesdomicile;
                stt.nbmatchjouesexterieur = nbmatchjouesexterieur;
                stt.nbvictoiredomicile = nbvictoiredomicile;
                stt.nbdefaitedomicile = nbdefaitedomicile;
                stt.nbnuldomicile = nbnuldomicile;
                stt.nbzerozerodomicile = nbzerozerodomicile;
                stt.nbvictoireexterieur = nbvictoireexterieur;
                stt.nbdefaiteexterieur = nbdefaiteexterieur;
                stt.nbnulexterieur = nbnulexterieur;
                stt.nbzerozeroexterieur = nbzerozeroexterieur;
                stt.butmisdomicile = butmisdomicile;
                stt.butprisdomicile = butprisdomicile;
                stt.butmisexterieur = butmisexterieur;
                stt.butprisexterieur = butprisexterieur;
                //totalisation
                stt.nbmatchjoues = nbmatchjoues;
                stt.nbvictoire = nbvictoire;
                stt.nbdefaite = nbdefaite;
                stt.nbnul = nbnul;
                stt.nbzerozero = nbzerozero;
                stt.butmis = butmis;
                stt.butpris = butpris;
                stt.ratiobutmis = ratiobutmis;
                stt.ratiobutpris = ratiobutpris;

                stt.nbnul10derniermatch = nbnul10derniermatch;
                stt.nbzerozero10derniermatch = nbzerozero10derniermatch;
                stt.nbnul5derniermatch = nbnul5derniermatch;
                stt.nbzerozero5derniermatch = nbzerozero5derniermatch;
                /*

                Console.WriteLine("match joués domicile : "+nbmatchjouesdomicile);
                Console.WriteLine("match joués exterieur : " + nbmatchjouesexterieur);

                Console.WriteLine("victoires domicile  : " + nbvictoiredomicile);
                Console.WriteLine("defaites domicile  : " + nbdefaitedomicile);
                Console.WriteLine("nuls domicile  : " + nbnuldomicile);
                Console.WriteLine("zero zero domicile  : " + nbzerozerodomicile);

                Console.WriteLine("buts mis domicile  : " + butmisdomicile);
                Console.WriteLine("buts pris domicile  : " + butprisdomicile);


                Console.WriteLine("victoires exterieur  : " + nbvictoireexterieur);
                Console.WriteLine("defaites exterieur  : " + nbdefaiteexterieur);
                Console.WriteLine("nuls exterieur  : " + nbnulexterieur);
                Console.WriteLine("zero zero exterieur  : " + nbzerozeroexterieur);

                Console.WriteLine("buts mis exterieur  : " + butmisexterieur);
                Console.WriteLine("buts pris exterieur  : " + butprisexterieur);

                Console.WriteLine("totalisation");
                Console.WriteLine("match joués  : " + nbmatchjoues);
                Console.WriteLine("victoires  : " + nbvictoire);
                Console.WriteLine("defaites   : " + nbdefaite);
                Console.WriteLine("nuls   : " + nbnul);
                Console.WriteLine("zero zero  : " + nbzerozero);

                Console.WriteLine("buts mis   : " + butmis);
                Console.WriteLine("buts pris   : " + butpris);

                Console.WriteLine("ratios buts mis   : " + ratiobutmis);
                Console.WriteLine("ratios buts pris   : " + ratiobutpris);

                Console.WriteLine("nbnul10derniermatch   : " + nbnul10derniermatch);
                Console.WriteLine("nbzerozero10derniermatch   : " + nbzerozero);

                Console.WriteLine("nbnul5derniermatch   : " + nbnul5derniermatch);
                Console.WriteLine("nbzerozero5derniermatch   : " + nbzerozero5derniermatch);

                //on fait apparaitre le contenu de l'objet
                Console.WriteLine("equipe : " + stt.nameteam);
                Console.WriteLine("idequipe : " + stt.idteam);
                Console.WriteLine("match joués exterieur : " + stt.nbmatchjouesexterieur);

                Console.WriteLine("match joués domicile : " + stt.nbmatchjouesdomicile);
                Console.WriteLine("match joués exterieur : " + stt.nbmatchjouesexterieur);

                Console.WriteLine("victoires domicile  : " + stt.nbvictoiredomicile);
                Console.WriteLine("defaites domicile  : " + stt.nbdefaitedomicile);
                Console.WriteLine("nuls domicile  : " + stt.nbnuldomicile);
                Console.WriteLine("zero zero domicile  : " + stt.nbzerozerodomicile);

                Console.WriteLine("buts mis domicile  : " + stt.butmisdomicile);
                Console.WriteLine("buts pris domicile  : " + stt.butprisdomicile);


                Console.WriteLine("victoires exterieur  : " + stt.nbvictoireexterieur);
                Console.WriteLine("defaites exterieur  : " + stt.nbdefaiteexterieur);
                Console.WriteLine("nuls exterieur  : " + stt.nbnulexterieur);
                Console.WriteLine("zero zero exterieur  : " + stt.nbzerozeroexterieur);

                Console.WriteLine("buts mis exterieur  : " + stt.butmisexterieur);
                Console.WriteLine("buts pris exterieur  : " + stt.butprisexterieur);

                Console.WriteLine("totalisation");
                Console.WriteLine("match joués  : " + stt.nbmatchjoues);
                Console.WriteLine("victoires  : " + stt.nbvictoire);
                Console.WriteLine("defaites   : " + stt.nbdefaite);
                Console.WriteLine("nuls   : " + stt.nbnul);
                Console.WriteLine("zero zero  : " + stt.nbzerozero);

                Console.WriteLine("buts mis   : " + stt.butmis);
                Console.WriteLine("buts pris   : " + stt.butpris);

                Console.WriteLine("ratios buts mis   : " + stt.ratiobutmis);
                Console.WriteLine("ratios buts pris   : " + stt.ratiobutpris);

                Console.WriteLine("nbnul10derniermatch   : " + stt.nbnul10derniermatch);
                Console.WriteLine("nbzerozero10derniermatch   : " + stt.nbzerozero);

                Console.WriteLine("nbnul5derniermatch   : " + stt.nbnul5derniermatch);
                Console.WriteLine("nbzerozero5derniermatch   : " + stt.nbzerozero5derniermatch);
                */
                //mise a jour de la stat de l'equipe
                writeintoGooglesheet(stt.idteam, oFixtureJson, compteur, stt.name_league, typefeuillet, stt, new StatsLeagues("60", "bidon", 1, 1));
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //on va devoir encapsuler l'appel pour chaque equipe. en se basant sur une liste. (ah ben la liste des equipes...que l'on charge via l'appel en mode range tout simplement.

            //StatTeam(typeGetApiFootball);
            GetList_Teams(service, nameleague);

            //
            // Console.WriteLine(lb_league.SelectedIndex);
            // lb_saison.SetSelected(2, true);  //2018
            //set lb_league.SelectedIndex = 1;
            // Console.WriteLine(lb_league.SelectedIndex);
            //SelectedIndex

        }
        private void Testpassage()
        {
            //var trackList = new List<Statsteams>();
            Statsteams stt = new Statsteams();
            stt.idteam = "rr";
            //stt.
        }

        public void GetList_Teams(SheetsService myss, string championnat)
        {
            //Console.WriteLine("recherche ref cellule");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet Ligue1
                                                                                  //String range = "Class Data!A2";  // single cell D5
                                                                                  //range par feuillet./championnat ou equipe
                                                                                  //range par feuillet./championnat ou equipe
            String range = "";
            string feuillet = championnat + "_teams";

            if (championnat == "Ligue1")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }
            if (championnat == "Ligue2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "Bundesliga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "Bundesliga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "Premier_league")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "Championship")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A25";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "SerieA")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "SerieB")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "LaLiga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }

            if (championnat == "LaLiga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A2:A23";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.

            }



            ValueRange valueRange = new ValueRange();
            //string resultat = "NON TROUVE";
            // SpreadsheetsResource.CreateRequest.
            valueRange.MajorDimension = "COLUMNS";//"ROWS";//COLUMNS

            SpreadsheetsResource.ValuesResource.GetRequest quest = myss.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange recupQuest = quest.Execute();

            IList<IList<Object>> values = recupQuest.Values;

            int i;
            i = 0;
            //int z = 1;
            string s1;
            // string s2 = "";// Id;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Console.WriteLine(row[i].ToString());
                    s1 = row[i].ToString();
                    //appel a la stat par equipe
                    StatTeamAlpha(typeGetApiFootball, s1, championnat);


                    //   z = z + 1;
                }

            }
            //Console.WriteLine(recupQuest.Values[0].ToString());
            // return resultat;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //string getApifootballbefore = typeGetApiFootball;
            // typeGetApiFootball = "roundencours";
            //GetApifootball(typeGetApiFootball, league, season);
            //typeGetApiFootball = getApifootballbefore;

            StatLeaguesAlpha(typeGetApiFootball, league, nameleague);


        }

        private void StatLeagues(string typegetapifootball, string idleague, string nameleague)
        {
            //on va calculer directement le currentround
            /* string getApifootballbefore = typeGetApiFootball;
            typeGetApiFootball = "roundencours";
            GetApifootball(typeGetApiFootball, idleague, season);
            typeGetApiFootball = getApifootballbefore;*/
            //  GetApifootball("roundencours", league, season);
            string typefeuillet = "stat_leagues";
            //calcul de l'ensemble des valeurs.
            //on passe en dure la..
            StatsLeagues stl = new StatsLeagues(idleague, nameleague, 38, 20);//gerer differement le nombre d'equipe et de round...
            stl.currentround = int.Parse(CurrentRound);//pas dans les regles de l'art, mais bon si pas initialisé alors ca souleve une exception.
            if ((typegetapifootball == "fixture") && (listBox1.Items.Count > 0))
            {
                //if (typeGetApiFootball == "fixture_FT_veille") //donc uniquement si if (typeGetApiFootball == "fixture_FT_veille")
                // Console.WriteLine(stl.idleague);// = idleague;// "61";//Ligue1
                // Console.WriteLine(stl.name_league);// = nameleague;
                //il faudrait initialiser églement les elements lié au nombre d'equipe etc. 
                //normalement cela devrait se faire a lcreation de l'objet...en allant recup les infos. 
                // il a deja cette info dans la classe fixture surement...
                //recup de la refcellule
                stl.refcelluleidleague = GetRefCelluleId(service, "championnatsleagues", stl.idleague);
                var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJson);
                int compteur = 0;
                int max = listBox1.Items.Count - 1;
                //matchs joué FT
                //NS pas demarré

                //bon pas de statut en cours... ca passe forcement pas l'analyse dateheure donc pour aller plus loin.
                //TDB  
                //en clair si j'ai 10 match joué (cas de la France) alors le round est complet...
                //on mets un compteur sur, on ne sors pas du probleme de la dimension. il faut gerer avec un index...38 dans le cas de la France...
                //stl.rounds[38].nbmatchsbyround =10;
                //stl.rounds[38].nbroundbyleague = 38;

                //int indexround = 0;
                //stl.rounds
                int currentround;
                while (compteur < max)
                {
                    currentround = int.Parse(GetDay_Round_Int(obj.response[compteur].league.round));
                    {
                        if (obj.response[compteur].fixture.status.Short.ToString() == "FT")
                        {
                            stl.rounds[currentround].nbmatchFT++;
                            //stl.nbmatchfinished = stl.nbmatchfinished++; // on cumul mal integré pour un objet mais bon..
                            //si c'est FT alors potentiellement un match nul
                            if ((obj.response[compteur].teams.home.winner == null) && (obj.response[compteur].teams.away.winner == null)) //nul
                            {
                                stl.rounds[currentround].nbnul++;
                                //si c'est un nul on peux analyser le score pour voir si c'est un zero a zero
                                if ((obj.response[compteur].score.fulltime.home.Value == 0) && (obj.response[compteur].score.fulltime.away.Value == 0))
                                {
                                    stl.rounds[currentround].nbzerozero++;
                                }
                            }
                        }
                        if (obj.response[compteur].fixture.status.Short.ToString() == "NS")
                        {
                            stl.rounds[currentround].nbmatchNS++;
                        }
                        if (obj.response[compteur].fixture.status.Short.ToString() == "CANC")
                        {
                            stl.rounds[currentround].nbmatchCANC++;
                        }
                        if (obj.response[compteur].fixture.status.Short.ToString() == "TDB")
                        {
                            stl.rounds[currentround].nbmatchTDB++;
                        }

                    }
                    // Console.WriteLine("round : " + obj.response[compteur].league.
                    //round.ToString());
                    compteur = compteur + 1;
                }
                /*
                for (int i = 1; i <= stl.nbrounds;i++)

                    {

                    Console.WriteLine("Numero :"+stl.rounds[i].Numero);
                    Console.WriteLine("nom : "+stl.rounds[i].Name_round);
                    Console.WriteLine("nbre matchs joués: "+stl.rounds[i].nbmatchFT);
                    Console.WriteLine("nbre matchs non commencés : " + stl.rounds[i].nbmatchNS);
                    Console.WriteLine("nbre matchs annulés: " + stl.rounds[i].nbmatchCANC);
                    Console.WriteLine("nbre matchs non planifiés: " + stl.rounds[i].nbmatchTDB);
                    Console.WriteLine("nbre matchs nuls: " + stl.rounds[i].nbnul);
                    Console.WriteLine("nbre matchs zero zero: " + stl.rounds[i].nbzerozero);
                    Console.WriteLine("journée courante: " + stl.currentround);

                    Console.WriteLine("nbre match nul round courant: " + stl.Calculnbnulcurrentround());
                    Console.WriteLine("nbre matchs nul round courant: " + stl.nbnulcurrentround);

                    Console.WriteLine("nbre match zerozero  round courant: " + stl.Calculnbzerozerocurrentround());
                    Console.WriteLine("nbre matchs zerozero round courant: " + stl.nbzerozerocurrentround);

                    Console.WriteLine("nbre match nul  3lastround : " + stl.Calculnbnul3lastrounds());
                    Console.WriteLine("nbre matchs nul 3lastround : " + stl.nbnul3lastrounds);

                    Console.WriteLine("nbre match zerozero  3lastround : " + stl.Calculnbzerozero3lastrounds());
                    Console.WriteLine("nbre matchs zerozero 3lastround : " + stl.nbzerozer3lastrounds);
                    //.Calculnbnulcurrentround.ToString());
                    //rounds[i].nbzerozero);
                    // Calculnbnulcurrentround
                }
                */
                // ok mais bon on a le detail, mais pas le cumul par journée...
                //mise a jour de la stat de l'equipe
                stl.Calculnbnulcurrentround();
                stl.Calculnbzerozerocurrentround();
                stl.Calculnbnul3lastrounds();
                stl.Calculnbzerozero3lastrounds();
                stl.Calcul();

                writeintoGooglesheet(stl.idleague, oFixtureJson, compteur, stl.name_league, typefeuillet, new Statsteams(), stl);
            }

        }

        public string scorehome(string statut_match, string scoretotal)
        {
            string resultat = "";

            // resultat = 0;
            if (statut_match == "FT")
            {
                scoretotal = scoretotal.Trim();
                int i = scoretotal.IndexOf('-');
                //i = str.IndexOf(' ', i + 1);
                resultat = scoretotal.Substring(0, i);
            }
            return resultat;
        }

        public string scoreaway(string statut_match, string scoretotal)
        {
            //on test si le resultat n'est pas null
            string resultat = "";
            // resultat = 0;
            if (statut_match == "FT")
            {
                scoretotal = scoretotal.Trim();
                int i = scoretotal.IndexOf('-');
                //i = str.IndexOf(' ', i + 1);
                resultat = (scoretotal.Substring(i + 1));
            }
            return resultat;
        }


        //function a l'arrache pour recuperer une valeur unique...dans un json, un vrai souci.
        public string CurrentroundFromJson(string jsoncourant)
        { //{"api":{"results":1,"fixtures":["Regular_Season_-_2"]}}
            int i = jsoncourant.IndexOf('[');
            int x = jsoncourant.IndexOf(']');
            int longueur = x - i;
            string retour = jsoncourant.Substring(i + 1, longueur - 1);
            //travail spécifique pour extraire le numero de round
            i = retour.IndexOf('_'); //1er occurence
            retour = retour.Substring(i + 1);
            i = retour.IndexOf('_');//2eme occurence
            retour = retour.Substring(i + 1);
            i = retour.IndexOf('_');//3eme occurence
            retour = retour.Substring(i + 1);
            retour = retour.Replace("/", "");
            retour = retour.Replace(@"[^\w\.@-]", "");
            retour = retour.Substring(0, retour.Length - 1);
            Console.WriteLine(retour.Length);
            //retour = retour.Replace("\", "");
            return retour;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            listBox1.Items.Clear();

            //chaine = "https://api-football-beta.p.rapidapi.com/fixtures?from=2020-08-29&to=2020-09-01&league=61&season=2020&status=FT";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/leagues/country/France/2020";
            // chaine="https://api-football-v1.p.rapidapi.com/v2/leagues/country/England/2020";
            // chaine="https://api-football-v1.p.rapidapi.com/v2/leagues/country/Italy/2020";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/leagues/country/Spain/2020";
            //chaine="https://api-football-v1.p.rapidapi.com/v2/league=2755&season=2020";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/teams/league/2857";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/teams/league/2847";
            chaine = "https://api-football-v1.p.rapidapi.com/v2/teams/league/2652";
            var client = new RestClient(chaine);// "http
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
            IRestResponse response = client.Execute(request);
            string resultat = response.Content;
            textBox1.Text = resultat;


            //Rootobject
            var obj = JsonConvert.DeserializeObject<RootobjectTeam2>(resultat);

            //  var obj = JsonConvert.DeserializeObject<RootobjectRound>(resultat);
            // //initialisation de la ligne de titre
            //listBox1.Items.Add("ROUND_EN_COURS,DATE_MAJ");
            //ateTime now = DateTime.Now;
            foreach (var item in obj.api.teams)
            {
                //objet feature
                listBox1.Items.Add(
                    item.team_id + ";" +
                    item.name + ";" +
                    item.country + ";" +
                    "2020" + ";" +
                    "2946" + ";");

            }

        }

        private void StatTeamAlpha(string typegetapifootball, string idteam, string nameleague)
        {
            string typefeuillet = "stat_team";
            //calcul de l'ensemble des valeurs.
            Statsteams stt = new Statsteams();
            if ((typegetapifootball == "fixture") && (listBox1.Items.Count > 0))
            {
                stt.idteam = idteam;// "93";//Reims
                stt.nameteam = "NON RENSEIGNE";
                stt.league = league;// "61";//ok celui on la
                stt.name_league = nameleague;
                //recup de la refcellule
                stt.refcelluleidteam = GetRefCelluleId(service, stt.name_league + "_teams", stt.idteam);
                //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJson);
                var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(oFixtureJson);
                int compteur = 0;
                int max = listBox1.Items.Count - 1;

                // idteam = "93"; //reims
                int nbmatchjouesdomicile = 0;
                int nbmatchjouesexterieur = 0;
                int nbvictoiredomicile = 0;
                int nbdefaitedomicile = 0;
                int nbnuldomicile = 0;
                int nbzerozerodomicile = 0;
                int nbvictoireexterieur = 0;
                int nbdefaiteexterieur = 0;
                int nbnulexterieur = 0;
                int nbzerozeroexterieur = 0;
                int butmisdomicile = 0;
                int butprisdomicile = 0;
                int butmisexterieur = 0;
                int butprisexterieur = 0;
                //totalisation
                int nbmatchjoues = 0;
                int nbvictoire = 0;
                int nbdefaite = 0;
                int nbnul = 0;
                int nbzerozero = 0;
                int butmis = 0;
                int butpris = 0;

                //function non construite pour le moment
                //il faudra stocker les elements en faisant sortir de la pile les premier rentré au bout de 10 et 5 respectivement.
                //pour le  moment on stocke la totalisation sur l'ensemble de la saison...
                int nbnul10derniermatch = 0;
                int nbzerozero10derniermatch = 0;
                int nbnul5derniermatch = 0;
                int nbzerozero5derniermatch = 0;

                double ratiobutmis = 0;
                double ratiobutpris = 0;
                //var obj = JsonConvert.DeserializeObject<RootobjectFixture>(oFixtureJson);
                // obj.response[compteur].fixture.
                //if (obj.response[compteur].fixture.status.Short.ToString() != "TBD") //bref different de TBD...["FT", "CAN", "NS"]
                // {
                //   writeintoGooglesheet(obj.response[compteur].fixture.id.ToString(), oFixtureJson, compteur, championnat);

                // }
                while (compteur < max)

                {

                    //if (obj.api.fixtures[compteur].statusShort != "TBD") //bref different de TBD...["FT", "CAN", "NS"]

                    if (obj.api.fixtures[compteur].statusShort.ToString() == "FT")//uniquement sur les matchs joués..
                                                                                  //on commence par rechercher l'idteam
                    {
                        if (obj.api.fixtures[compteur].homeTeam.team_id.ToString() == stt.idteam)
                        {
                            //au premier passage on recupere le nom de l'equipe.
                            if (stt.nameteam == "NON RENSEIGNE")
                            {
                                stt.nameteam = obj.api.fixtures[compteur].homeTeam.team_name;
                            }
                            nbmatchjouesdomicile++;
                            //on regarde si c'est une victoire/defaite/nul
                            //VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString())
                            if ((VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "TRUE") && (VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE"))  //victoire
                            {
                                nbvictoiredomicile++;
                            }
                            if ((VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "TRUE"))  //defaite
                                                                                                                                                                                                                                                                                                                                                                                                      //if ((obj.response[compteur].teams.home.winner == false) && (obj.response[compteur].teams.away.winner == true))//defaite
                            {
                                nbdefaitedomicile++;
                            }
                            if ((VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE"))  //nul domicile
                            //    if ((obj.response[compteur].teams.home.winner == null) && (obj.response[compteur].teams.away.winner == null)) //nul
                            {
                                nbnuldomicile++;
                            }
                            //addition des buts...
                            butmisdomicile = butmisdomicile + int.Parse(obj.api.fixtures[compteur].goalsHomeTeam.ToString());
                            butprisdomicile = butprisdomicile + int.Parse(obj.api.fixtures[compteur].goalsAwayTeam.ToString());
                            //est ce un zero a zero
                            if ((int.Parse(obj.api.fixtures[compteur].goalsHomeTeam.ToString()) == 0) && (int.Parse(obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == 0))
                            {
                                nbzerozerodomicile++;
                            }

                        }
                        if (obj.api.fixtures[compteur].awayTeam.team_id.ToString() == stt.idteam)
                        {
                            //au premier passage on recupere le nom de l'equipe.
                            if (stt.nameteam == "NON RENSEIGNE")
                            {
                                stt.nameteam = obj.api.fixtures[compteur].awayTeam.team_name;
                            }
                            nbmatchjouesexterieur++;
                            //on regarde si c'est une victoire/defaite/nul
                            if ((VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "TRUE") && (VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE"))  //victoire exterieure
                            //    if ((obj.response[compteur].teams.away.winner == true) && (obj.response[compteur].teams.home.winner == false))  //victoire
                            {
                                nbvictoireexterieur++;
                            }
                            if ((VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE") && (VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "TRUE"))  //defaite
                            //    if ((obj.response[compteur].teams.away.winner == false) && (obj.response[compteur].teams.home.winner == true))//defaite
                            {
                                nbdefaiteexterieur++;
                            }
                            if ((VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE"))  //nul domicile
                            //    if ((obj.response[compteur].teams.away.winner == null) && (obj.response[compteur].teams.home.winner == null)) //nul
                            {
                                nbnulexterieur++;
                            }
                            //addition des buts...
                            butmisexterieur = butmisexterieur + int.Parse(obj.api.fixtures[compteur].goalsAwayTeam.ToString());
                            butprisexterieur = butprisexterieur + int.Parse(obj.api.fixtures[compteur].goalsHomeTeam.ToString());
                            //est ce un zero a zero
                            if ((int.Parse(obj.api.fixtures[compteur].goalsHomeTeam.ToString()) == 0) && (int.Parse(obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == 0))
                            //if ((obj.response[compteur].score.fulltime.away.Value == 0) && (obj.response[compteur].score.fulltime.home.Value == 0))
                            {
                                nbzerozeroexterieur++;
                            }

                        }

                    }
                    compteur = compteur + 1;
                }
                //boucle inversé pour recuperer les données uniquement des 5 et 10 derniers matchs.
                int compteurinverse = listBox1.Items.Count - 2;// 0;
                int maxinverse = 0;// listBox1.Items.Count - 1;
                                   //int nbmatchjouesinverse = 0;
                int nbmatchjouesdomicileinverse = 0;
                int nbmatchjouesexterieurinverse = 0;
                int nbvictoiredomicileinverse = 0;
                int nbdefaitedomicileinverse = 0;
                int nbvictoireexterieurinverse = 0;
                int nbdefaiteexterieurinverse = 0;
                int nbzerozerodomicileinverse = 0;
                int nbzerozeroexterieurinverse = 0;
                int nbnulexterieurinverse = 0;
                int nbnuldomicileinverse = 0;
                int butmisdomicileinverse = 0;
                int butmisexterieurinverse = 0;
                int butprisdomicileinverse = 0;
                int butprisexterieurinverse = 0;

                while (compteurinverse >= maxinverse)
                {
                    //Console.WriteLine(obj.response[compteurinverse].fixture.id.ToString());
                    if (obj.api.fixtures[compteurinverse].statusShort.ToString() == "FT")//uniquement sur les matchs joués..
                                                                                         //on commence par rechercher l'idteam
                    {
                        if (obj.api.fixtures[compteurinverse].homeTeam.team_id.ToString() == stt.idteam)
                        {
                            nbmatchjouesdomicileinverse++;
                            //on regarde si c'est une victoire/defaite/nul

                            if ((VictoireDomicile(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "TRUE") && (VictoireExterieure(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE"))  //victoire
                            {
                                nbvictoiredomicileinverse++;
                            }
                            if ((VictoireDomicile(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "TRUE"))  //defaite
                            {
                                nbdefaitedomicileinverse++;
                            }
                            if ((VictoireDomicile(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE"))  //nul domicile
                            {
                                nbnuldomicileinverse++;
                            }
                            //addition des buts...
                            butmisdomicileinverse = butmisdomicileinverse + int.Parse(obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString());
                            butprisdomicileinverse = butprisdomicileinverse + int.Parse(obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString());
                            //est ce un zero a zero
                            if ((int.Parse(obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString()) == 0) && (int.Parse(obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == 0))
                            {
                                nbzerozerodomicileinverse++;
                            }

                        }
                        if (obj.api.fixtures[compteurinverse].awayTeam.team_id.ToString() == stt.idteam)
                        {
                            nbmatchjouesexterieurinverse++;
                            //on regarde si c'est une victoire/defaite/nul
                            if ((VictoireExterieure(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "TRUE") && (VictoireDomicile(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE"))  //victoire exterieure
                                                                                                                                                                                                                                                                                                                                                                                                                                                //                                if ((obj.response[compteurinverse].teams.away.winner == true) && (obj.response[compteurinverse].teams.home.winner == false))  //victoire
                            {
                                nbvictoireexterieurinverse++;
                            }
                            if ((VictoireExterieure(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE") && (VictoireDomicile(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "TRUE"))  //defaite
                                                                                                                                                                                                                                                                                                                                                                                                                                                //if ((obj.response[compteurinverse].teams.away.winner == false) && (obj.response[compteurinverse].teams.home.winner == true))//defaite
                            {
                                nbdefaiteexterieurinverse++;
                            }
                            if ((VictoireDomicile(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteurinverse].statusShort, obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString(), obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == "FALSE"))  //nul domicile
                            {
                                nbnulexterieurinverse++;
                            }
                            //addition des buts...
                            butmisexterieurinverse = butmisexterieurinverse + int.Parse(obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString());
                            butprisexterieurinverse = butprisexterieurinverse + int.Parse(obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString());
                            //est ce un zero a zero
                            if ((int.Parse(obj.api.fixtures[compteurinverse].goalsHomeTeam.ToString()) == 0) && (int.Parse(obj.api.fixtures[compteurinverse].goalsAwayTeam.ToString()) == 0))
                            {
                                nbzerozeroexterieurinverse++;
                            }

                        }

                    }
                    //on totalise si il y a lieu en fonction du compteur match joué (5/10)
                    if ((nbmatchjouesdomicileinverse + nbmatchjouesexterieurinverse) <= 5)
                    {
                        nbnul5derniermatch = nbnulexterieurinverse + nbnuldomicileinverse;
                        nbzerozero5derniermatch = nbzerozerodomicileinverse + nbzerozeroexterieurinverse;
                    }

                    if ((nbmatchjouesdomicileinverse + nbmatchjouesexterieurinverse) <= 10)
                    {
                        nbnul10derniermatch = nbnulexterieurinverse + nbnuldomicileinverse;
                        nbzerozero10derniermatch = nbzerozerodomicileinverse + nbzerozeroexterieurinverse;

                    }

                    compteurinverse = compteurinverse - 1;
                }

                //totalisation
                nbmatchjoues = nbmatchjouesdomicile + nbmatchjouesexterieur;
                nbvictoire = nbvictoiredomicile + nbvictoireexterieur;
                nbdefaite = nbdefaitedomicile + nbdefaiteexterieur;
                nbnul = nbnuldomicile + nbnulexterieur;
                nbzerozero = nbzerozerodomicile + nbzerozeroexterieur;
                butmis = butmisdomicile + butmisexterieur;
                butpris = butprisdomicile + butprisexterieur;

                ratiobutmis = (float)butmis / (float)nbmatchjoues;
                ratiobutpris = (float)butpris / (float)nbmatchjoues;

                ratiobutmis = Math.Round(ratiobutmis, 2);
                ratiobutpris = Math.Round(ratiobutpris, 2);

                //function non construite pour le moment
                //pour le moment, on recupere les elements ici...(on pourra se passer des variables intermediares)
                stt.nbmatchjouesdomicile = nbmatchjouesdomicile;
                stt.nbmatchjouesexterieur = nbmatchjouesexterieur;
                stt.nbvictoiredomicile = nbvictoiredomicile;
                stt.nbdefaitedomicile = nbdefaitedomicile;
                stt.nbnuldomicile = nbnuldomicile;
                stt.nbzerozerodomicile = nbzerozerodomicile;
                stt.nbvictoireexterieur = nbvictoireexterieur;
                stt.nbdefaiteexterieur = nbdefaiteexterieur;
                stt.nbnulexterieur = nbnulexterieur;
                stt.nbzerozeroexterieur = nbzerozeroexterieur;
                stt.butmisdomicile = butmisdomicile;
                stt.butprisdomicile = butprisdomicile;
                stt.butmisexterieur = butmisexterieur;
                stt.butprisexterieur = butprisexterieur;
                //totalisation
                stt.nbmatchjoues = nbmatchjoues;
                stt.nbvictoire = nbvictoire;
                stt.nbdefaite = nbdefaite;
                stt.nbnul = nbnul;
                stt.nbzerozero = nbzerozero;
                stt.butmis = butmis;
                stt.butpris = butpris;
                stt.ratiobutmis = ratiobutmis;
                stt.ratiobutpris = ratiobutpris;

                stt.nbnul10derniermatch = nbnul10derniermatch;
                stt.nbzerozero10derniermatch = nbzerozero10derniermatch;
                stt.nbnul5derniermatch = nbnul5derniermatch;
                stt.nbzerozero5derniermatch = nbzerozero5derniermatch;
                /*

                Console.WriteLine("match joués domicile : "+nbmatchjouesdomicile);
                Console.WriteLine("match joués exterieur : " + nbmatchjouesexterieur);

                Console.WriteLine("victoires domicile  : " + nbvictoiredomicile);
                Console.WriteLine("defaites domicile  : " + nbdefaitedomicile);
                Console.WriteLine("nuls domicile  : " + nbnuldomicile);
                Console.WriteLine("zero zero domicile  : " + nbzerozerodomicile);

                Console.WriteLine("buts mis domicile  : " + butmisdomicile);
                Console.WriteLine("buts pris domicile  : " + butprisdomicile);


                Console.WriteLine("victoires exterieur  : " + nbvictoireexterieur);
                Console.WriteLine("defaites exterieur  : " + nbdefaiteexterieur);
                Console.WriteLine("nuls exterieur  : " + nbnulexterieur);
                Console.WriteLine("zero zero exterieur  : " + nbzerozeroexterieur);

                Console.WriteLine("buts mis exterieur  : " + butmisexterieur);
                Console.WriteLine("buts pris exterieur  : " + butprisexterieur);

                Console.WriteLine("totalisation");
                Console.WriteLine("match joués  : " + nbmatchjoues);
                Console.WriteLine("victoires  : " + nbvictoire);
                Console.WriteLine("defaites   : " + nbdefaite);
                Console.WriteLine("nuls   : " + nbnul);
                Console.WriteLine("zero zero  : " + nbzerozero);

                Console.WriteLine("buts mis   : " + butmis);
                Console.WriteLine("buts pris   : " + butpris);

                Console.WriteLine("ratios buts mis   : " + ratiobutmis);
                Console.WriteLine("ratios buts pris   : " + ratiobutpris);

                Console.WriteLine("nbnul10derniermatch   : " + nbnul10derniermatch);
                Console.WriteLine("nbzerozero10derniermatch   : " + nbzerozero);

                Console.WriteLine("nbnul5derniermatch   : " + nbnul5derniermatch);
                Console.WriteLine("nbzerozero5derniermatch   : " + nbzerozero5derniermatch);

                //on fait apparaitre le contenu de l'objet
                Console.WriteLine("equipe : " + stt.nameteam);
                Console.WriteLine("idequipe : " + stt.idteam);
                Console.WriteLine("match joués exterieur : " + stt.nbmatchjouesexterieur);

                Console.WriteLine("match joués domicile : " + stt.nbmatchjouesdomicile);
                Console.WriteLine("match joués exterieur : " + stt.nbmatchjouesexterieur);

                Console.WriteLine("victoires domicile  : " + stt.nbvictoiredomicile);
                Console.WriteLine("defaites domicile  : " + stt.nbdefaitedomicile);
                Console.WriteLine("nuls domicile  : " + stt.nbnuldomicile);
                Console.WriteLine("zero zero domicile  : " + stt.nbzerozerodomicile);

                Console.WriteLine("buts mis domicile  : " + stt.butmisdomicile);
                Console.WriteLine("buts pris domicile  : " + stt.butprisdomicile);


                Console.WriteLine("victoires exterieur  : " + stt.nbvictoireexterieur);
                Console.WriteLine("defaites exterieur  : " + stt.nbdefaiteexterieur);
                Console.WriteLine("nuls exterieur  : " + stt.nbnulexterieur);
                Console.WriteLine("zero zero exterieur  : " + stt.nbzerozeroexterieur);

                Console.WriteLine("buts mis exterieur  : " + stt.butmisexterieur);
                Console.WriteLine("buts pris exterieur  : " + stt.butprisexterieur);

                Console.WriteLine("totalisation");
                Console.WriteLine("match joués  : " + stt.nbmatchjoues);
                Console.WriteLine("victoires  : " + stt.nbvictoire);
                Console.WriteLine("defaites   : " + stt.nbdefaite);
                Console.WriteLine("nuls   : " + stt.nbnul);
                Console.WriteLine("zero zero  : " + stt.nbzerozero);

                Console.WriteLine("buts mis   : " + stt.butmis);
                Console.WriteLine("buts pris   : " + stt.butpris);

                Console.WriteLine("ratios buts mis   : " + stt.ratiobutmis);
                Console.WriteLine("ratios buts pris   : " + stt.ratiobutpris);

                Console.WriteLine("nbnul10derniermatch   : " + stt.nbnul10derniermatch);
                Console.WriteLine("nbzerozero10derniermatch   : " + stt.nbzerozero);

                Console.WriteLine("nbnul5derniermatch   : " + stt.nbnul5derniermatch);
                Console.WriteLine("nbzerozero5derniermatch   : " + stt.nbzerozero5derniermatch);
                */
                //mise a jour de la stat de l'equipe
                writeintoGooglesheet(stt.idteam, oFixtureJson, compteur, stt.name_league, typefeuillet, stt, new StatsLeagues("60", "bidon", 1, 1));
            }

        }

        private void StatLeaguesAlpha(string typegetapifootball, string idleague, string nameleague)
        {
            //on va calculer directement le currentround ou du moins le recuperer
            string getApifootballbefore = typeGetApiFootball;
            typeGetApiFootball = "roundencours";
            GetApifootballRoundencours(typeGetApiFootball, idleague, season);
            typeGetApiFootball = getApifootballbefore;
            string typefeuillet = "stat_leagues";
            //calcul de l'ensemble des valeurs.
            //on passe en dure la..
            int nbteam = 0;
            int nbday = 0;
            if ((idleague == "2664") || (idleague == "2652") || (idleague == "2790") || (idleague == "2857") || (idleague == "2946") || (idleague == "2833"))// ligues francaises et premier league / ligues italiennes
            {

                nbteam = 20;
                nbday = 38;
            }
            if (idleague == "2847")// laliga2.. 22 equipes.. 38 journées...
            {
                nbteam = 22;
                nbday = 38;
            }

            if (idleague == "2794")// championship.. 24 equipes.. 46 journées...
            {
                nbteam = 24;
                nbday = 46;
            }


            if ((idleague == "2755") || (idleague == "2743")) // ligues allemandes
            {
                nbteam = 18;
                nbday = 34;
            }

            StatsLeagues stl = new StatsLeagues(idleague, nameleague, nbday, nbteam);//gerer differement le nombre d'equipe et de round...




            stl.currentround = int.Parse(CurrentRound);//pas dans les regles de l'art, mais bon si pas initialisé alors ca souleve une exception.
            if ((typegetapifootball == "fixture") && (listBox1.Items.Count > 0))
            {
                //if (typeGetApiFootball == "fixture_FT_veille") //donc uniquement si if (typeGetApiFootball == "fixture_FT_veille")
                // Console.WriteLine(stl.idleague);// = idleague;// "61";//Ligue1
                // Console.WriteLine(stl.name_league);// = nameleague;
                //il faudrait initialiser églement les elements lié au nombre d'equipe etc. 
                //normalement cela devrait se faire a lcreation de l'objet...en allant recup les infos. 
                // il a deja cette info dans la classe fixture surement...
                //recup de la refcellule
                stl.refcelluleidleague = GetRefCelluleId(service, "championnatsleagues", stl.idleague);
                var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(oFixtureJson);
                int compteur = 0;
                int max = listBox1.Items.Count - 1;
                //matchs joué FT
                //NS pas demarré

                //bon pas de statut en cours... ca passe forcement pas l'analyse dateheure donc pour aller plus loin.
                //TDB  
                //en clair si j'ai 10 match joué (cas de la France) alors le round est complet...
                //on mets un compteur sur, on ne sors pas du probleme de la dimension. il faut gerer avec un index...38 dans le cas de la France...
                //stl.rounds[38].nbmatchsbyround =10;
                //stl.rounds[38].nbroundbyleague = 38;

                //int indexround = 0;
                //stl.rounds
                int currentround;
                while (compteur < max)
                {
                    //CurrentRound=
                    Console.WriteLine(obj.api.fixtures[compteur].round.ToString());
                    currentround = int.Parse(GetDay_Round_Int(obj.api.fixtures[compteur].round.ToString()));
                    //currentround = int.Parse(CurrentRound);// int.Parse(GetDay_Round_Int(obj.response[compteur].league.round));  // vraiment a ameliorer... pour eviter les requetes inutiles
                    {
                        if (obj.api.fixtures[compteur].statusShort.ToString() == "FT")//uniquement sur les matchs joués..
                        {
                            stl.rounds[currentround].nbmatchFT++;
                            //stl.nbmatchfinished = stl.nbmatchfinished++; // on cumul mal integré pour un objet mais bon..
                            //si c'est FT alors potentiellement un match nul
                            if ((VictoireDomicile(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE") && (VictoireExterieure(obj.api.fixtures[compteur].statusShort, obj.api.fixtures[compteur].goalsHomeTeam.ToString(), obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == "FALSE"))  //nul domicile
                                                                                                                                                                                                                                                                                                                                                                                                       //  if ((obj.response[compteur].teams.home.winner == null) && (obj.response[compteur].teams.away.winner == null)) //nul
                            {
                                stl.rounds[currentround].nbnul++;
                                //si c'est un nul on peux analyser le score pour voir si c'est un zero a zero
                                if ((int.Parse(obj.api.fixtures[compteur].goalsHomeTeam.ToString()) == 0) && (int.Parse(obj.api.fixtures[compteur].goalsAwayTeam.ToString()) == 0))
                                {
                                    stl.rounds[currentround].nbzerozero++;
                                }
                            }
                        }
                        if (obj.api.fixtures[compteur].statusShort.ToString() == "NS")
                        {
                            stl.rounds[currentround].nbmatchNS++;
                        }
                        if (obj.api.fixtures[compteur].statusShort.ToString() == "CANC")
                        {
                            stl.rounds[currentround].nbmatchCANC++;
                        }
                        if (obj.api.fixtures[compteur].statusShort.ToString() == "TDB")
                        {
                            stl.rounds[currentround].nbmatchTDB++;
                        }
                    }
                    // Console.WriteLine("round : " + obj.response[compteur].league.
                    //round.ToString());
                    compteur = compteur + 1;
                }
                /*
                for (int i = 1; i <= stl.nbrounds;i++)

                    {

                    Console.WriteLine("Numero :"+stl.rounds[i].Numero);
                    Console.WriteLine("nom : "+stl.rounds[i].Name_round);
                    Console.WriteLine("nbre matchs joués: "+stl.rounds[i].nbmatchFT);
                    Console.WriteLine("nbre matchs non commencés : " + stl.rounds[i].nbmatchNS);
                    Console.WriteLine("nbre matchs annulés: " + stl.rounds[i].nbmatchCANC);
                    Console.WriteLine("nbre matchs non planifiés: " + stl.rounds[i].nbmatchTDB);
                    Console.WriteLine("nbre matchs nuls: " + stl.rounds[i].nbnul);
                    Console.WriteLine("nbre matchs zero zero: " + stl.rounds[i].nbzerozero);
                    Console.WriteLine("journée courante: " + stl.currentround);

                    Console.WriteLine("nbre match nul round courant: " + stl.Calculnbnulcurrentround());
                    Console.WriteLine("nbre matchs nul round courant: " + stl.nbnulcurrentround);

                    Console.WriteLine("nbre match zerozero  round courant: " + stl.Calculnbzerozerocurrentround());
                    Console.WriteLine("nbre matchs zerozero round courant: " + stl.nbzerozerocurrentround);

                    Console.WriteLine("nbre match nul  3lastround : " + stl.Calculnbnul3lastrounds());
                    Console.WriteLine("nbre matchs nul 3lastround : " + stl.nbnul3lastrounds);

                    Console.WriteLine("nbre match zerozero  3lastround : " + stl.Calculnbzerozero3lastrounds());
                    Console.WriteLine("nbre matchs zerozero 3lastround : " + stl.nbzerozer3lastrounds);
                    //.Calculnbnulcurrentround.ToString());
                    //rounds[i].nbzerozero);
                    // Calculnbnulcurrentround
                }
                */
                // ok mais bon on a le detail, mais pas le cumul par journée...
                //mise a jour de la stat de l'equipe
                stl.Calculnbnulcurrentround();
                stl.Calculnbzerozerocurrentround();
                stl.Calculnbnul3lastrounds();
                stl.Calculnbzerozero3lastrounds();
                stl.Calcul();

                writeintoGooglesheet(stl.idleague, oFixtureJson, compteur, stl.name_league, typefeuillet, new Statsteams(), stl);
            }

        }

        private void GetApifootballRoundencours(string typegetapifootball, string championnat, string season)
        {
            textBox2.Text = "";
            listBox2.Items.Clear();
            var client_1 = new RestClient();
            var client = new RestClient();// "https://api-football-beta.p.rapidapi.com/fixtures?league=62&season=2018" + chaine);

            if (typeGetApiFootball == "roundencours")
            {
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    chaine = "https://api-football-v1.p.rapidapi.com/v2/fixtures/rounds/" + league + "/current";
                    //https://api-football-v1.p.rapidapi.com/v2/predictions/157462");
                    //"https://api-football-v1.p.rapidapi.com/v2/fixtures/rounds/2664/current");

                }
                else
                {
                    chaine = "https://api-football-beta.p.rapidapi.com/fixtures/rounds?current=true&league=" + league + "&season=" + season;
                }
                //chaine = "https://api-football-beta.p.rapidapi.com/fixtures/rounds?current=false&league=" + league + "&season=" + season;

                //a traiter donc...
                client_1 = new RestClient(chaine);
                //il faut creer la classe correspondante.
            }
            client_1 = new RestClient(chaine);
            client = client_1;

            //avant d'appeler on regarde si la valeur n'a pas déjà eté recuperé aujourd'hui
            if (WriteAppconfigBeforeRequete("CurrentRound", league, "ras") != "OK")  //on a pas la valeur.
            {
                var request = new RestRequest(Method.GET);
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
                    request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
                }
                else
                {
                    request.AddHeader("x-rapidapi-host", "api-football-beta.p.rapidapi.com");
                    request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
                }
                IRestResponse response = client.Execute(request);
                string resultat = response.Content;
                textBox1.Text = resultat;
                var json = resultat;
                //stockage dans la variable public
                oFixtureJson = resultat;
                if (typeGetApiFootball == "roundencours")
                {
                    if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                    {
                        CurrentRound = CurrentroundFromJson(json);
                    }
                    else //not alpha beta donc
                    {
                        var obj = JsonConvert.DeserializeObject<RootobjectRound>(json);
                        //initialisation de la ligne de titre
                        listBox1.Items.Add("ROUND_EN_COURS,DATE_MAJ");
                        DateTime now = DateTime.Now;
                        foreach (var item in obj.response)
                        //ResponseFixture)
                        {
                            //objet feature
                            listBox1.Items.Add(
                item.ToString() + ";" +
                GetDay_Round_Int(item.ToString())
               + ";"
               +
               now //date mise a jour de l'info
                             );
                            CurrentRound = GetDay_Round_Int(item.ToString());
                        }
                    }
                    //dans ce cas on met à jours la valeurs.
                    WriteAppconfigAfterRequete("CurrentRound", league, "ras");
                }
            }


        }

        private void GetApifootball(string typegetapifootball, string championnat, string season)
        {

            textBox1.Text = "";
            listBox1.Items.Clear();

            var client_1 = new RestClient();
            var client = new RestClient();// "https://api-football-beta.p.rapidapi.com/fixtures?league=62&season=2018" + chaine);

            /*
            if (lb_typeGet.SelectedItem == "Liste matchs Championnat")
            {
                typeGetApiFootball = "fixture";
            }

            if (lb_typeGet.SelectedItem == "Liste matchs championnats finis depuis la veille")
            {
                typeGetApiFootball = "fixture_FT_veille";
            }

            */
            if (typeGetApiFootball == "roundencours")
            {
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    chaine = "https://api-football-v1.p.rapidapi.com/v2/fixtures/rounds/" + league + "/current";
                    //https://api-football-v1.p.rapidapi.com/v2/predictions/157462");
                    //"https://api-football-v1.p.rapidapi.com/v2/fixtures/rounds/2664/current");

                }
                else
                {
                    chaine = "https://api-football-beta.p.rapidapi.com/fixtures/rounds?current=true&league=" + league + "&season=" + season;
                }
                //chaine = "https://api-football-beta.p.rapidapi.com/fixtures/rounds?current=false&league=" + league + "&season=" + season;

                //a traiter donc...
                client_1 = new RestClient(chaine);
                //il faut creer la classe correspondante.
            }
            if (typeGetApiFootball == "fixture")
            {
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    // league = "2664";
                    //chaine = "https://api-football-v1.p.rapidapi.com/fixtures?league=" + league + "&season=" + season;
                    chaine = "https://api-football-v1.p.rapidapi.com/v2/fixtures/league/" + league;
                    // chaine = "https://api-football-v1.p.rapidapi.com/v2/leagues/country/france/2020";

                }
                else
                {
                    chaine = "https://api-football-beta.p.rapidapi.com/fixtures?league=" + league + "&season=" + season;
                    //chaine = "https://api-football-beta.p.rapidapi.com/fixtures?league=" + league + "&season=" + season+ "&status=TBD";

                    // chaine ="https://api-football-beta.p.rapidapi.com/fixtures?last=571682&league=61&season=2020&next=571576";
                }
                client_1 = new RestClient(chaine);
            }
            if (typeGetApiFootball == "fixture_FT_veille")
            {
                // chaine = "https://api-football-beta.p.rapidapi.com/fixtures?league=" + league + "&season=" + season;
                //chaine = "https://api-football-beta.p.rapidapi.com/fixtures?to=2020-08-29&league=" + league + "&season=" + season +"&status=FT&date=2020-02-06";
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    chaine = "https://api-football-v1.p.rapidapi.com/fixtures?from=2020-08-29&to=2020-09-01&league=61&season=2020&status=FT";
                }
                else
                {
                    chaine = "https://api-football-beta.p.rapidapi.com/fixtures?from=2020-08-29&to=2020-09-01&league=61&season=2020&status=FT";
                }

                client_1 = new RestClient(chaine);
            }
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures?league=62&season=2018"+chaine);
            client = client_1;
            // if lb
            // .SetSelected(1, true);
            // var client = new RestClient("https://api-football-beta.p.rapidapi.com/timezone");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/leagues");
            //country name = France
            //league name : Ligue 1 / 61 c'est l'id de la Ligue/championnat
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/leagues?name=Ligue%201&country=France");
            //seaisons =2019
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/leagues?name=Ligue%201&season=2020&country=France");
            ///ok on essaye d'avoir la liste des equipes de ligue 1 pour la saison 2019-2020
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/teams?league=62&season=2019");
            //ok donc 85 c'est l'id du PSG.
            /*
            // get("https://api-football-v1.p.rapidapi.com/v2/fixtures/rounds/{league_id}");
             GET By league & currentGet current round from one {league}

            get("https://api-football-v1.p.rapidapi.com/v2/fixtures/rounds/{league_id}/current");
             */
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures/rounds?season=2020&league=61");
            //"https://api-football-beta.p.rapidapi.com/fixtures/rounds?season=2019&league=39"
            //
            // var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures/rounds?current=true&season=2019&league=61");
            //recup du match
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures/headtohead?season=2020&last=10&status=ft&h2h=106-92");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures?round=1&league=61&date=2020-02-06");
            //recup d'un objet avec match round etc
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures?round=1&league=61&season=2020");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures?league=62&season=2018");
            //var req = unirest("GET", "https://api-football-v1.p.rapidapi.com/v2/leagues/country/spain/2018");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/teams/statistics?team=85&season=2019&league=61");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures/statistics?team=85&type=Total%20Shots&fixture=37899");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/teams/statistics?team=85&season=2019&league=61");
            // var client = new RestClient("https://api-football-beta.p.rapidapi.com/leagues?&country=Spain");
            //var client = new RestClient("https://api-football-beta.p.rapidapi.com/fixtures/statistics?team=85&type=Total%20Shots&fixture=37499");
            //ok j'arrive a avoir les stats de l'equipe pour cette competition (nombre de victoire etc nul etc, pas de stat sur les matchs par contre avec cette api.
            //il me faudrais aussi formater ca avec du json en sortie
            //ok en fait il y a toute les stats sur api football nickel
            //ok test une autre api, avec des resultats en live (il faudra des stats de mi temps surtout...)
            //donc on va essayé de requeter sur le match championne ligue LYON BAYERN.
            //programme API
            //var client = new RestClient("https://api-football-v1.p.rapidapi.com/v2/predictions/157462");
            //https://api-football-v1.p.rapidapi.com/
            var request = new RestRequest(Method.GET);
            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
            {
                request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
            }
            else
            {
                request.AddHeader("x-rapidapi-host", "api-football-beta.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
            }
            IRestResponse response = client.Execute(request);
            //programme API BETA
            /*
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-beta.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
            c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a
            c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a
            // request.RequestFormat = 'Json';
            IRestResponse response = client.Execute(request);
            */
            //response.contact
            string resultat = response.Content;

            textBox1.Text = resultat;


            //      var json = "{\"response\":[{\"id\":269058571,\"first_name\":\"Name\",\"last_name\":\"LastName\",\"photo_50\":\"http://cs624717.vk.me/v624717571/21718/X8.jpg\"}]}";
            var json = resultat;
            //stockage dans la variable public
            oFixtureJson = resultat;
            if (typeGetApiFootball == "roundencours")
            {
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    // Console.WriteLine(json.ToString());
                    //var obj = JsonConvert.DeserializeObject<RootObjectApiRoundAlpha>(json);
                    //initialisation de la ligne de titre
                    //listBox1.Items.Add("ROUND_EN_COURS,DATE_MAJ");
                    // DateTime now = DateTime.Now;
                    // Console.WriteLine(CurrentroundFromJson(json));
                    // foreach (var item in obj.apiroundalpha.fixtures)
                    //)
                    //)
                    //fixtures)
                    //.fixtures)

                    //objet feature
                    /*
                    listBox1.Items.Add(
                          CurrentroundFromJson(json) //item.ToString() 
                          + ";" +
                          GetDay_Round_Int(CurrentroundFromJson(json))
                       + ";"
                       +
                       now //date mise a jour de l'info
                       );
                    */
                    CurrentRound = CurrentroundFromJson(json);
                }
                else //not alpha beta donc
                {
                    var obj = JsonConvert.DeserializeObject<RootobjectRound>(json);
                    //initialisation de la ligne de titre
                    listBox1.Items.Add("ROUND_EN_COURS,DATE_MAJ");
                    DateTime now = DateTime.Now;
                    foreach (var item in obj.response)
                    //ResponseFixture)
                    {
                        //objet feature
                        listBox1.Items.Add(
              item.ToString() + ";" +
              GetDay_Round_Int(item.ToString())
             + ";"
             +
             now //date mise a jour de l'info
                         );
                        CurrentRound = GetDay_Round_Int(item.ToString());
                    }
                }
                //   Console.WriteLine(json.ToString());

            }
            if ((typeGetApiFootball == "fixture") || (typeGetApiFootball == "fixture_FT_veille"))
            //   var obj = JsonConvert.DeserializeObject<RootobjectTeam>(json);
            {
                //  var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(json);
                //JsonConvert();

                //Object();
                //   Console.WriteLine(json.ToString());
                if (MYGlobalVars.APIFOOTBALL == "ALPHA")
                {
                    var obj = JsonConvert.DeserializeObject<RootObjectFixtureAlpha>(json);
                    //initialisation de la ligne de titre
                    listBox1.Items.Add("ID_MATCH;DATE_HEURE_MATCH;DAY_ROUND;DAY_ROUND_INT;STATUS_LONG;STATUTS_SHORT;ID_TEAM_HOME;NAME_TEAM_HOME;SCORE_HOME;SCORE_MITEMPS_HOME;SCORE_FINALE_HOME;ID_TEAM_AWAY;NAME_TEAM_AWAY;SCORE_AWAY;SCORE_MITEMPS_AWAY;SCORE_FINALE_AWAY;TIMEZONE;VICTOIRE_DOMICILE;VICTOIRE_EXTERIEURE;MATCH_NUL;MATCH_ZERO_ZERO;DATE_MAJ");
                    DateTime now = DateTime.Now;
                    foreach (var item in obj.api.fixtures)//                    )
                    {
                        //objet feature
                        listBox1.Items.Add(item.fixture_id //   item.fixture.id
                         + ";"
            +
            item.event_date    //    item.fixture.date //DATE_HEURE
                         + ";"
             +
             item.round  //    item.league.round   //DAY_ROUND
                        + ";"
            +
            GetDay_Round_Int(item.round) //item.league.round);// ;//  //DAY_ROUND
                        + ";"
            +//DAY_ROUND_INT
                        item.status//  item.fixture.status.Long  // "status long:"
                        + ";"
             +
             item.statusShort//  item.fixture.status.Short  // "status cours:" +
                        +
            ";"
            +
            item.homeTeam.team_id//    item.teams.home.id //id team home
                         + ";" +
             item.homeTeam.team_name //   item.teams.home.name //name team home
                         + ";" +
             item.goalsHomeTeam //  item.goals.home  //but domicile //c'est quoi la difference ...
                          + ";" +
            "scoremitempsdomicile"// item.score.halftime.home.Value//    ok il faut creer deux fonctions pour decouper les scores.                     .home//score mi temps domicile
                          + ";" +
             "scorefinalsdomicile"//                          item.score.fulltime.home.Value //score finale domicile
                             + ";" +

            item.awayTeam.team_id//    item.teams.home.id //id team home
                         + ";" +
             item.awayTeam.team_name //   item.teams.home.name //name team home
                          + ";" +
             item.goalsAwayTeam  //but exterieur
                                  + ";" +
                         //"score mitemps exterieur"//
                         item.score.fulltime
                          //halftime.away.Value//score mi temps exterieur
                          + ";" +
            "score finale exterieur"//  item.score.fulltime.away.Value //score finale exterieur
                          + ";" +
             "pas trouvé d'equivalent..."//item.//                         fixture.timezone //timezone (ca c'est important pour recalculer l'horaire des matchs pour la france)
                          + ";" +
             VictoireDomicile(item.statusShort, item.goalsHomeTeam.ToString(), item.goalsAwayTeam.ToString())
                         //item.score.fulltime.home.ToString(), item.score.fulltime.away.ToString())
                         //teams.home.winner //victoire domicile
                         + ";" +
             VictoireExterieure(item.statusShort, item.goalsHomeTeam.ToString(), item.goalsAwayTeam.ToString())
                         //item.teams.away.winner //victoire exterieur
                         + ";" +
             GetNulOrZero(item.statusShort, item.goalsHomeTeam.ToString(), item.goalsAwayTeam.ToString())
             +
             now //date mise a jour de l'info
                         ); ;
                    }
                }
                else //donc pas alpha mais api beta
                {
                    var obj = JsonConvert.DeserializeObject<RootobjectFixture>(json);
                    //initialisation de la ligne de titre
                    listBox1.Items.Add("ID_MATCH;DATE_HEURE_MATCH;DAY_ROUND;DAY_ROUND_INT;STATUS_LONG;STATUTS_SHORT;ID_TEAM_HOME;NAME_TEAM_HOME;SCORE_HOME;SCORE_MITEMPS_HOME;SCORE_FINALE_HOME;ID_TEAM_AWAY;NAME_TEAM_AWAY;SCORE_AWAY;SCORE_MITEMPS_AWAY;SCORE_FINALE_AWAY;TIMEZONE;VICTOIRE_DOMICILE;VICTOIRE_EXTERIEURE;MATCH_NUL;MATCH_ZERO_ZERO;DATE_MAJ");
                    DateTime now = DateTime.Now;
                    foreach (var item in obj.response)   //ResponseFixture)
                    {
                        //objet feature
                        listBox1.Items.Add(
              item.fixture.id
             + ";"
            +
             item.fixture.date //DATE_HEURE
                         + ";"
             +
            item.league.round   //DAY_ROUND
                        + ";"
            +
            GetDay_Round_Int(item.league.round)  //DAY_ROUND
                        + ";"
            +//DAY_ROUND_INT
                              item.fixture.status.Long  // "status long:"
                        + ";"
             +
            item.fixture.status.Short  // "status cours:" +
                        +
            ";"
            +
            item.teams.home.id //id team home
                         + ";" +
             item.teams.home.name //name team home
                         + ";" +
             item.goals.home  //but domicile //c'est quoi la difference ...
                          + ";" +
             item.score.halftime.home//score mi temps domicile
                          + ";" +
             item.score.fulltime.home //score finale domicile
                             + ";" +
            item.teams.away.id //id team away
                         + ";" +
             item.teams.away.name //name team away
                          + ";" +
             item.goals.away  //but exterieur
                                  + ";" +
             item.score.halftime.away//score mi temps exterieur
                          + ";" +
             item.score.fulltime.away //score finale exterieur
                          + ";" +
             item.fixture.timezone //timezone (ca c'est important pour recalculer l'horaire des matchs pour la france)
                          + ";" +
             item.teams.home.winner //victoire domicile
                         + ";" +
             item.teams.away.winner //victoire exterieur
                         + ";" +
             GetNulOrZero(item.fixture.status.Short, item.score.fulltime.home.ToString(), item.score.fulltime.away.ToString())
             +
             now //date mise a jour de l'info
                         );
                    }
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();
            /*
             * 1m
5m
15m
30m
1h
2h
4h
12h
1d
1w*/
            string AffichagePrix = "";
            string AffichageRSI14 = "";
            string AffichageRSI5 = "";
            string Indice = "prix";
            string Symbol = "";
            string AffichagePriceDirectionBTC;
            string AffichagePriceDirectionSymbol;
            //string Interval;
            //listBox1.SelectedIndex
            if (Interval=="") { Interval = "15m"; };
           // Interval =lb_typeGet.SelectedItem.ToString();
           // Interval
            Symbol = listBox1.Items[listBox1.SelectedIndex].ToString();
            //textBox1.AppendText(Symbol+listBox1.Items[listBox1.SelectedIndex].ToString());
            //textBox1.AppendText("\r\n");
           // Interval = "15m";
         /*
            if (Interval == "1h")
            {
                Interval = "15m";
            }
            else
            {
                Interval = "1h";
            }
         */
            //Symbol = "ETH/USDT";
            //le calcul du RSI n'est pas bon du tout sur l'api...pfff.
            try
            {

                // Symbol = "UNISUSDT";
                string Weburl = "https://api.taapi.io/wclprice?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=" + Symbol + "&interval=1m";
                // APICall.RunAsync(Weburl,"GET");
                RestClient clientRest1 = new RestClient(Weburl);
                var request = new RestRequest(Method.GET);
                IRestResponse response = clientRest1.Execute(request);
                string resultat = response.Content;
                JObject n = JObject.Parse(resultat);
                string valeur = (string)n["value"];
                decimal prix = Decimal.Parse(valeur.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                prix = decimal.Round(prix, 5);
                AffichagePrix = Symbol + "/" + Indice + ":" + prix;
                textBox1.AppendText(AffichagePrix);
                textBox1.AppendText("\r\n");

                //Symbol = "UNISUSDT";
                Indice = "RSI_14_" + Interval;
                Weburl = "https://api.taapi.io/rsi?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=" + Symbol + "&interval=" + Interval + "&optInTimePeriod=14";
                // APICall.RunAsync(Weburl,"GET");
                clientRest1 = new RestClient(Weburl);
                request = new RestRequest(Method.GET);
                response = clientRest1.Execute(request);
                resultat = response.Content;
                JObject o = JObject.Parse(resultat);
                valeur = (string)o["value"];
                decimal RSI14 = Decimal.Parse(valeur.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                RSI14 = decimal.Round(RSI14, 2);

                AffichageRSI14 = Symbol + "/" + Indice + ":" + RSI14;
                textBox1.AppendText(AffichageRSI14);

                textBox1.AppendText("\r\n"); 
                Indice = "RSI_5_" + Interval;
                //Weburl = "https://api.taapi.io/stochrsi?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=" + Symbol + "&interval=" + Interval + "& optInTimePeriod=5";
                Weburl = "https://api.taapi.io/rsi?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=" + Symbol + "&interval=" + Interval + "&optInTimePeriod=5";
                // APICall.RunAsync(Weburl,"GET");
                clientRest1 = new RestClient(Weburl);
                request = new RestRequest(Method.GET);
                response = clientRest1.Execute(request);
                resultat = response.Content;
                JObject p = JObject.Parse(resultat);
                valeur = (string)p["value"];
                decimal RSI5 = Decimal.Parse(valeur.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                RSI5 = decimal.Round(RSI5, 2);
                AffichageRSI5 = Symbol + "/" + Indice + ":" + RSI5;
                textBox1.AppendText(AffichageRSI5);
                textBox1.AppendText("\r\n");

                textBox1.AppendText("\r\n");
                Indice = "Price_direction_Symbol" + Interval;
                Weburl = "https://api.taapi.io/pd?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=" + Symbol + "&interval=" + Interval;
                // APICall.RunAsync(Weburl,"GET");
                clientRest1 = new RestClient(Weburl);
                request = new RestRequest(Method.GET);
                response = clientRest1.Execute(request);
                resultat = response.Content;
                JObject v = JObject.Parse(resultat);
                valeur = (string)v["value"];
                decimal PricedirectionSymbol = Decimal.Parse(valeur.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                PricedirectionSymbol = decimal.Round(PricedirectionSymbol, 2);

                AffichagePriceDirectionSymbol = Symbol + "/" + Indice + ":" + PricedirectionSymbol;
                textBox1.AppendText(AffichagePriceDirectionSymbol);


                textBox1.AppendText("\r\n");
                Indice = "Price_direction_BTC" + Interval;
                Weburl = "https://api.taapi.io/pd?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBoaWxpcHBlLmFtYXJ5QGdtYWlsLmNvbSIsImlhdCI6MTYyMTI3NDk4MiwiZXhwIjo3OTI4NDc0OTgyfQ.9smxGMuHVwT4mAYPKX7QWC2I_NX3kVaN1HS7G0LlMd8&exchange=binance&symbol=BTC/USDT&interval=" + Interval;
                // APICall.RunAsync(Weburl,"GET");
                clientRest1 = new RestClient(Weburl);
                request = new RestRequest(Method.GET);
                response = clientRest1.Execute(request);
                resultat = response.Content;
                JObject q = JObject.Parse(resultat);
                valeur = (string)q["value"];
                decimal PricedirectionBTC = Decimal.Parse(valeur.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                PricedirectionBTC = decimal.Round(PricedirectionBTC, 2);

                AffichagePriceDirectionBTC = Symbol + "/" + Indice + ":" + PricedirectionBTC;
                textBox1.AppendText(AffichagePriceDirectionBTC);

                Model1Container db = new Model1Container();
                //analyse a l'arrache...
                //on regarde si RSI14<30 ET RSI5>RSI14 (dejà)
                //on regardera ensuite si prix est vers la bande lower de bollinger.
                //si c'est le cas, on inscrit l'opportunité dans une textbox.


                //int nbevent = db.EvenementSet.Count();
                //int nbessai = db.EssaiSet.Count();


                /*
                Essai MyEssai = new Essai();// (nom = "TEST1");
                //MyEssai.Id = 3;
                MyEssai.nom = "TEST1";
                db.EssaiSet.Add(MyEssai);
                db.SaveChanges();
                */
                //RSI14 = 20;
                // RSI5 = 30;

                // enregistrement du signal pour stat //refaire plus correctement l'implementation
                SuiviSignal MySuiviSignal = new SuiviSignal();
                MySuiviSignal.DateHeure= DateTime.Now;
                MySuiviSignal.Symbol = Symbol;
                MySuiviSignal.Prix = prix;
                MySuiviSignal.RSI14 = RSI14;
                MySuiviSignal.RSI5 = RSI5;
                MySuiviSignal.Interval = Interval;
                MySuiviSignal.PricedirectionBTC = PricedirectionBTC;
                MySuiviSignal.PriceDirectionSymbol = PricedirectionSymbol;
                db.SuiviSignalSet.Add(MySuiviSignal);

                //TEST Methode
                //RSI14<50
                //RSI5>RSI14
                //(RSI5 - RSI14) >= 1) deja signal affirmé
                //(RSI5 - RSI14) <= 2) au debut du decollage

                if ((RSI14 < 60) && (RSI5 > RSI14))  // METHODE NEW1
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);

                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";

                    //                        = DateTime.Now;
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if (((RSI5 - RSI14) >= 1) && ((RSI5 - RSI14) <= 2))
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14 mais inferieur a 2 point");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14  et inferieur a 2 point" + Symbol + " sur " + Interval;
                        //appel a a une fonction de trade (enregistrement dans la table et futur passage d'ordre en réel.)

                        // Trade MyTrade = new Trade();
                        // MyTrade = TradingServices.InitializeTrade(MyEvent);
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        //MethodeTrading
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "NEW1");
                        /*
                        int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade=="NEW1").Count();// il faudra ajouter les futurs statuts également
                        if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                        {
                            int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW1").Count();
                            if (tradedejaencours == 0)
                            {
                                
                                db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent,"NEW1"));
                            }
                        }
                        */
                    }

                    db.EvenementSet.Add(MyEvent);

                }

//debut  methode NEW2
                if ((RSI14 < 60) && (RSI5 > RSI14) && PricedirectionBTC > 0)  // METHODE NEW2
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if (((RSI5 - RSI14) >= 1) && ((RSI5 - RSI14) <= 2))
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14 mais inferieur a 2 point");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14  et inferieur a 2 point" + Symbol + " sur " + Interval;
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "NEW2");
                        /*
                        int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW2").Count();// il faudra ajouter les futurs statuts également
                        if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                        {
                            int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW2").Count();
                            if (tradedejaencours == 0)
                            {
                                db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent, "NEW2"));
                            }
                        }
                        */
                    }
                    db.EvenementSet.Add(MyEvent);
                }
                //fin methode NEW3
                //debut  methode NEW3
                if ((RSI14 < 60) && (RSI5 > RSI14) && PricedirectionSymbol > 0)  // METHODE NEW3
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if (((RSI5 - RSI14) >= 1) && ((RSI5 - RSI14) <= 2))
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14 mais inferieur a 2 point");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14  et inferieur a 2 point" + Symbol + " sur " + Interval;
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "NEW3");
                        /*
                        int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW3").Count();// il faudra ajouter les futurs statuts également
                        if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                        {
                            int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW3").Count();
                            if (tradedejaencours == 0)
                            {
                                db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent, "NEW3"));
                            }
                        }
                        */
                    }
                    db.EvenementSet.Add(MyEvent);
                }
                //fin methode NEW3

                //debut  methode NEW4
                if ((RSI14 < 60) && (RSI5 > RSI14) && (PricedirectionSymbol > 0) && (PricedirectionBTC > 0))  // METHODE NEW4
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if (((RSI5 - RSI14) >= 1) && ((RSI5 - RSI14) <= 2))
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14 mais inferieur a 2 point");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14  et inferieur a 2 point" + Symbol + " sur " + Interval;
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "NEW4");
                        /*
                         int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW4").Count();// il faudra ajouter les futurs statuts également
                         if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                         {
                             int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "NEW4").Count();
                             if (tradedejaencours == 0)
                             {
                                 db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent, "NEW4"));
                             }
                         }
                        */
                    }
                    db.EvenementSet.Add(MyEvent);
                }
                //fin methode NEW3


                //////methode 
                if ((RSI14 < 30) && (RSI5 > RSI14) && PricedirectionBTC > 0) //methode TRADI1
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";


                    //                        = DateTime.Now;
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if ((RSI5 - RSI14) >= 1)
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14 " + Symbol + " sur " + Interval;
                        //appel a a une fonction de trade (enregistrement dans la table et futur passage d'ordre en réel.)

                        // Trade MyTrade = new Trade();
                        // MyTrade = TradingServices.InitializeTrade(MyEvent);
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "TRADI1");
                        /*
                         int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI1").Count();// il faudra ajouter les futurs statuts également
                         if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                         {
                             int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI1").Count();
                             if (tradedejaencours == 0)
                             {
                                 db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent,"TRADI1"));
                             }
                         }
                        */
                    }     
                        
                        db.EvenementSet.Add(MyEvent);


                    
                } // fin methode

                //////methode TRADI2
                if ((RSI14 < 30) && (RSI5 > RSI14) && PricedirectionBTC>0) //methode TRADI2
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if ((RSI5 - RSI14) >= 1)
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14 " + Symbol + " sur " + Interval;
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "TRADI2");
                        /*
                        int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI2").Count();// il faudra ajouter les futurs statuts également
                        if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                        {
                            int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI2").Count();
                            if (tradedejaencours == 0)
                            {
                                db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent, "TRADI2"));
                            }
                        }
                        */
                    }
                   db.EvenementSet.Add(MyEvent);
                } // fin methode

                //////methode TRADI3
                if ((RSI14 < 30) && (RSI5 > RSI14) && PricedirectionSymbol > 0) //methode TRADI3
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if ((RSI5 - RSI14) >= 1)
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14 " + Symbol + " sur " + Interval;
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "TRADI3");
                        /*
                        int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI3").Count();// il faudra ajouter les futurs statuts également
                        if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                        {
                            int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI3").Count();
                            if (tradedejaencours == 0)
                            {
                                db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent, "TRADI3"));
                            }
                        }
                        */
                    }
                    db.EvenementSet.Add(MyEvent);
                } // fin methode

                //////methode TRADI4
                if ((RSI14 < 30) && (RSI5 > RSI14) && PricedirectionSymbol > 0 && PricedirectionBTC > 0) //methode TRADI4
                {
                    textBox2.AppendText("opportunité :" + Symbol + " sur " + Interval);
                    Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " opportunité :" + Symbol + "r\n" + AffichagePrix + "r\n" + AffichageRSI14 + "r\n" + AffichageRSI5);
                    Evenement MyEvent = new Evenement();
                    MyEvent.DateHeure = DateTime.Now;
                    MyEvent.Symbol = Symbol;
                    MyEvent.Eventlog = "opportunité :" + Symbol + " sur " + Interval;
                    MyEvent.RSI14 = RSI14;
                    MyEvent.RSI5 = RSI5;
                    MyEvent.valeur = prix;
                    MyEvent.typeintervaltime = Interval;
                    MyEvent.TypeOrder = "ACHAT";
                    //si au moins 1 point d'ecart entre les deux RSI alors on le precise
                    if ((RSI5 - RSI14) >= 1)
                    {
                        Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Super opportunité RSI5 superieur d'au moins 1 point a RSI14");
                        MyEvent.Eventlog = "Super opportunité RSI5 superieur d'au moins 1 point a RSI14 " + Symbol + " sur " + Interval;
                        //si la valeur est déja en cours de trade, on n'initialise pas un seconde trade sur la meme paire...
                        TradingServices.CreateAllPotentialTrades(db, MyEvent, "TRADI4");
                        /*
                        int tradeENCOURSALLSYMBOL = db.TradeSet.Where(TradeSet => TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI4").Count();// il faudra ajouter les futurs statuts également
                        if (tradeENCOURSALLSYMBOL < 5)  //money management pas plus de 5 trades en cours.
                        {
                            int tradedejaencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN" && TradeSet.CodeMethodeTriggerTrade == "TRADI4").Count();
                            if (tradedejaencours == 0)
                            {
                                db.TradeSet.Add(TradingServices.InitializeTrade(MyEvent, "TRADI4"));
                            }
                        }
                        */
                    }
                    db.EvenementSet.Add(MyEvent);
                } // fin methode




                //on ne fait du suivi de valeur que sur les valeurs avec des trades OPEN
                int tradeencours = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol &&  TradeSet.Statut == "OPEN").Count();
                if (tradeencours > 0)
                {
                    SuiviValeur MySuiviValeur = new SuiviValeur();
                    MySuiviValeur.DateHeure = DateTime.Now; ;
                    MySuiviValeur.Symbol = Symbol;
                    MySuiviValeur.valeur = prix;
                    db.SuiviValeurSet.Add(MySuiviValeur);
                    // pour simplifier le modele on va stocker le prix courant de la valeur dans la table trade.
                    //on statue si on vends (R0 touché) ou R2 touché.

                    foreach (Trade MYtradeSet in db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN").AsEnumerable())
                    {
                        // db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN").GetEnumerator();
                        //  Trade MYtradeSet = db.TradeSet.Where(TradeSet => TradeSet.Symbol == Symbol && TradeSet.Statut == "OPEN").First();
                        MYtradeSet.ValeurCourante = prix;
                        if (decimal.Compare(prix, MYtradeSet.STOP_COURANT.Value) <= 0)  // si le prix est inferieur au stop courant alors on vends.
                                                                                        // LessThan = -1, Equals = 0, GreaterThan = 1 }
                        {
                            //on vends stop loss
                            TradingServices.CloseTrade(MYtradeSet, prix);
                        }
                        else
                        {
                            //on passe au mode trailling au franchissement du R2
                            //on passe en mode trailling. donc on doit tester aussi le cliquet R2...
                            if (decimal.Compare(MYtradeSet.R2.Value, prix) < 0)  // LessThan = -1, Equals = 0, GreaterThan = 1 }
                            {
                                //on verifie si on pas deja en mode trailling.., dans ce cas, on réevalue simplement la valeur du stop courant rapport.
                                if (MYtradeSet.STOP_COURANT >= MYtradeSet.R2Trailling.Value)//si stop courant superieur ou egal a R2trailling alors on deja en mode trailling.
                                {
                                    //on se contente de réevaluer au besoin le stop (en fonction de la valeur courante.
                                    decimal TraillingRecalcul = TradingServices.CalculTrailling(prix);
                                    if (TraillingRecalcul > MYtradeSet.STOP_COURANT)
                                    { MYtradeSet.STOP_COURANT = TraillingRecalcul; }
                                }
                                else
                                { MYtradeSet.STOP_COURANT = MYtradeSet.R2Trailling.Value; }
                                //comment on reevalue le stop trailling ???
                            }
                        }
                    }
                }
                db.SaveChanges();

                //analyse et statuer des trades en cours en fonction des valeurs d'entrées, du niveau d'expo au risque et de la valeur courante...
                //pour le moment, soit on vends, soit on ne vends pas... 
                //par contre il faut un point de vente, meme en suivi de tendance (stop trailling)... pour le moment on ne calcul pas ?
                //il faut stocker dans la table TRADE (evidemment)
                //R0 (point d'entrée ou point de protection ??
                

            }
            catch (Exception ex)
            { }

            //else
            // {// Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")+" pas d'opportunité :" + Symbol);
            // }
            if (listBox1.Items.Count > listBox1.SelectedIndex + 1)
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }
            else
            {
                if (Interval == "15m") { Interval = "1h"; }
                else
                { Interval = "15m"; };
                listBox1.SelectedIndex = 0;
            }
            
            /*
            if (param1 == "auto")
            {
                GestionModeAuto(param2, param3);
            }
            */
        }

        public string GetMatchNext24h(SheetsService myss, string feuillet, string Id)
        {
            //Console.WriteLine("recherche ref cellule");
            //String spreadsheetId = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
            String spreadsheetId = "1OFa9S4sh2jJFsdiA9GZyCABn5rE2pg4uP3RBGNVL6as";//classeur championnats2020 .feuillet Ligue1
                                                                                  //String range = "Class Data!A2";  // single cell D5
                                                                                  //range par feuillet./championnat ou equipe
                                                                                  //range par feuillet./championnat ou equipe
            String range = "";

            //on a besoin de recuperer l'IDmatch,la refligne/le dateheure/le statuts =NS / pour le moment c'est en France donc bon.

            if (feuillet == "Ligue1")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                //range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                //String myNewCellValue = "Alexandrie";
                //range = feuillet + "!A2:M2"; //pour le moment on ne recupere qu'une ligne'
                range = feuillet + "!A2:M381"; //pour le moment on recupere les x lignes pour voir.
            }
            if (feuillet == "Ligue2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:M381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:M307";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Bundesliga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:M307";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Premier_league")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                //range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                //String myNewCellValue = "Alexandrie";
                //range = feuillet + "!A2:M2"; //pour le moment on ne recupere qu'une ligne'
                range = feuillet + "!A2:M381"; //pour le moment on recupere les x lignes pour voir.
            }
            if (feuillet == "Championship")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:M553";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                              //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                              //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "SerieA")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                //range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                //String myNewCellValue = "Alexandrie";
                //range = feuillet + "!A2:M2"; //pour le moment on ne recupere qu'une ligne'
                range = feuillet + "!A2:M381"; //pour le moment on recupere les x lignes pour voir.
            }
            if (feuillet == "SerieB")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                //range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                //String myNewCellValue = "Alexandrie";
                //range = feuillet + "!A2:M2"; //pour le moment on ne recupere qu'une ligne'
                range = feuillet + "!A2:M381"; //pour le moment on recupere les x lignes pour voir.
            }

            if (feuillet == "LaLiga")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                //range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                //String myNewCellValue = "Alexandrie";
                //range = feuillet + "!A2:M2"; //pour le moment on ne recupere qu'une ligne'
                range = feuillet + "!A2:M381"; //pour le moment on recupere les x lignes pour voir.
            }
            if (feuillet == "LaLiga2")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                //range = feuillet + "!A1:A381";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                //String myNewCellValue = "Alexandrie";
                //range = feuillet + "!A2:M2"; //pour le moment on ne recupere qu'une ligne'
                range = feuillet + "!A2:M463"; //pour le moment on recupere les x lignes pour voir.
            }


            if (feuillet == "Ligue1_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Ligue2_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga_teams")
            {
                range = feuillet + "!A1:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Bundesliga2_teams")
            {
                range = feuillet + "!A1:A19";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "Premier_league_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }
            if (feuillet == "Championship_teams")
            {
                range = feuillet + "!A1:A25";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "SerieA_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "SerieB_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "LaLiga_teams")
            {
                range = feuillet + "!A1:A21";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "LaLiga2_teams")
            {
                range = feuillet + "!A1:A23";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            if (feuillet == "championnatsleagues")  // a ameliorer c'est bof ca..mais je connais pas la syntaxe en c#...pour chercher dans un ensemble
            {
                range = feuillet + "!A1:A11";// + refcellule;  // single cell D5  en dur pour le moment, il s'agit de la colonne de l'ID.
                                             //on va egalement devoir recuperer cça dynamiquement, en utilisant uniquement le titre. c'est le seul element qui doit etre fixe.
                                             //String myNewCellValue = "Alexandrie";
            }

            ValueRange valueRange = new ValueRange();
            string resultat = "NON TROUVE";
            // SpreadsheetsResource.CreateRequest.
            valueRange.MajorDimension = "ROWS";// "COLUMNS";//"ROWS";//COLUMNS

            SpreadsheetsResource.ValuesResource.GetRequest quest = myss.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange recupQuest = quest.Execute();

            IList<IList<Object>> values = recupQuest.Values;

            int i;
            i = 0;
            int z = 1;
            string idmatch;
            string statut;
            string teamhome;
            string teamaway;
            DateTime dateheurematch;
            //DateTime datejour;
            string s2 = Id;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Console.WriteLine(row[i].ToString());
                    idmatch = row[i].ToString();
                    //ok donc la je recupere la ligne et i = numero de colonne. nickel
                    //soit je recupere dans une classe, soit j'analyse tout et je programme en fonction via une fonction
                    statut = row[5].ToString();
                    if (statut == "NS")  // alors on analyse si le match a lieu dans les prochaine 24h.
                    {
                        dateheurematch = Convert.ToDateTime(row[1].ToString());
                        //Console.WriteLine(dateheurematch);
                        //Console.WriteLine(dateheurematch.Date);
                        if ((dateheurematch.Date == DateTime.Today) | (dateheurematch.Date == DateTime.Today.AddDays(1)))
                        {
                            Console.WriteLine("match candidat : " + dateheurematch);  //appel a une fonction ? on on charge un objet ? là en l'occurence on a toutes les infos necessaire.
                            //sans doute nettoyage des alarmes peut etre ?
                            teamhome = row[7].ToString();
                            teamaway = row[12].ToString();
                            //on desactive l' ajout des matchs ...
                            //Addmatchoncalendar(idmatch, dateheurematch, feuillet, teamhome, teamaway);
                            //on laisse l'appconfig pour gerer le scan des matchs
                            //pour le moment on aura un appel sur tous les matchs sans le scan prealable de la stat... 
                            AddmatchonAppconfig(idmatch, dateheurematch, feuillet, teamhome, teamaway);
                            //on enregistre aussi dans ce cas dans le fichier appconfig (il faudra penser a virer les valeurs du reste...)
                            //pour le moment pas de fonction nettoyage.. a mettre en place
                        }
                        //   datejour = DateTime.Today;
                        // Console.WriteLine(datejour);
                        ///def prochaine 24h dans le contexte///
                        ///soit aujourdhui (quelquesoit l'heure) soit demain quelquesoit l'heure)
                        //  if dateheurematch.da
                        //     betwe
                    }
                    /*
                    if (IsFind(s1, s2, z) == true)
                    {
                        resultat = String.Concat("", z);// Convert.ToString(i);    
                    }
                    if (IsFind(s1, s2, z) == true)
                    {
                        resultat = String.Concat("", z);// Convert.ToString(i);    
                    }
                    */
                    z = z + 1;
                }

            }
            //Console.WriteLine(recupQuest.Values[0].ToString());
            return resultat;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ///purge
            NettoyageAgendaAppConfig();
            //on archive les matchs du classeur match courant et on vide le feuillet courant.
            CelluleSheet servicecellulesheet = new CelluleSheet();
            //servicecellulesheet.DuplicateSheet(service, "IDMATCH_592917", "rien", "rien");
            servicecellulesheet.Duplicatanddeletesheetfichesmatchs(service);
            GetMatchNext24h(service, "Ligue1", "");
            GetMatchNext24h(service, "Ligue2", "");
            GetMatchNext24h(service, "Bundesliga", "");
            GetMatchNext24h(service, "Bundesliga2", "");
            GetMatchNext24h(service, "Premier_league", "");
            GetMatchNext24h(service, "Championship", "");
            GetMatchNext24h(service, "SerieA", "");
            GetMatchNext24h(service, "SerieB", "");
            GetMatchNext24h(service, "LaLiga", "");
            GetMatchNext24h(service, "LaLiga2", "");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //

            // Define parameters of request.
            //string[] Calendriers =
            //Console.WriteLine(
            //Items();
            //Items;
            //Items();

            // for (CalendarListEntry calendarListEntry  items)
            //  {
            //Console.WriteLine(calendarListEntry.getSummary());
            //System.out.println(calendarListEntry.getSummary());
            //   }
            //Items();
            //getItems();

            // CalendarListEntry calendarListEntry = servicecalendar.CalendarList.List;
            //calendarListEntry.Id.
            //string [] calendriers= 
            //  servicecalendar.CalendarList.
            //.ToString());
            //ToString());
            //CalendarList.ToString();
            /*
            EventsResource.ListRequest request = servicecalendar.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            //events.Items
           // servicecalendar.Events.Insert()

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            */
            Console.Read();
            ///

            //CalendarList calendarList = servicecalendar.CalendarList.List().Execute();
            //List<CalendarListEntry> items = calendarList.Items[];
            //  Console.WriteLine(calendarList.Items[0].Id);

            Event newEvent = new Event()
            {
                Summary = " TEST match3 ",
                Location = " Localisation",
                Description = "A chance to hear more about Google's developer products.",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2020-09-10T09:00:00-06:00"),
                    TimeZone = "Europe/Paris",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2020-09-10T17:00:00-07:00"),
                    TimeZone = "Europe/Paris",
                }
            };
            //  }
            //newEvent.Reminders.
            //  Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
            //  Attendees = new EventAttendee[] {
            // new EventAttendee() { Email = "lpage@example.com" },
            // new EventAttendee() { Email = "sbrin@example.com" },
            //},
            //          Reminders = new Event.RemindersData()
            //        {
            //            UseDefault = false,
            //            Overrides = new EventReminder[] {
            //    new EventReminder() { Method = "email", Minutes = 24 * 60 },
            //    new EventReminder() { Method = "sms", Minutes = 10 },
            //  }
            //   }
            //};

            String calendarId = "primary";
            EventsResource.InsertRequest request2 = servicecalendar.Events.Insert(newEvent, calendarId);
            Event createdEvent = request2.Execute();
            //Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);
        }

        private void Addmatchoncalendar(string idmatch, DateTime dateheurematch, string feuillet, string teamhome, string teamaway)
        {
            //Addmatchoncalendar(idmatch, dateheurematch, feuillet, teamhome, teamaway);
            //il faudra ajouter un controle pour eviter de doublonner les rdv.  par contre c'est nickel pas d'appel a des requetes supplementaires.
            //on mettra l'id du match.
            //et on bouclera pour voir si le match est deja referencé sur cette plage la... dans ce cas on ne cree pas.

            //il faudra ajouter un systeme via sms ou autre, afin d'avoir des news sur les matchs candidats a la mi temps
            if (IsMatchAlreadyScheduled(idmatch) == false)
            {
                Event newEvent = new Event()
                {
                    Summary = feuillet + " " + teamhome + "/" + teamaway,
                    Location = "MATCHID:" + idmatch,// teamhome,// on va remplacer la localisation par l'IDMATCH, ce qui permettra de voir si on a pas deja le match de programmé...
                    Description = teamhome + "/" + teamaway,
                    Start = new EventDateTime()
                    {
                        DateTime = dateheurematch.AddMinutes(45),// DateTime.Parse("2020-09-10T09:00:00-06:00"),
                        TimeZone = "Europe/Paris",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = dateheurematch.AddMinutes(55),//  DateTime.Parse("2020-09-10T17:00:00-07:00"),
                        TimeZone = "Europe/Paris",
                    },
                    Reminders = new Event.RemindersData()
                    {
                        UseDefault = false,
                        Overrides = new EventReminder[]
                                {
                   // new EventReminder() { Method = "email", Minutes = 24 * 60 },
                    new EventReminder() { Method = "popup", Minutes = 0 }
                                }
                    }
                };
                String calendarId = "primary";
                EventsResource.InsertRequest request = servicecalendar.Events.Insert(newEvent, calendarId);
                Event createdEvent = request.Execute();
                Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);
            } //fin condition deja programmé... on pourra encore amélioré le truc, c'est de supprimer l'evenement afin de le recreer..; si il y a eu des mises a jour entretemps
        }

        private void AddmatchonAppconfig(string idmatch, DateTime dateheurematch, string feuillet, string teamhome, string teamaway)
        {
            //il faudra ajouter un controle pour eviter de doublonner les rdv.  par contre c'est nickel pas d'appel a des requetes supplementaires.
            //on mettra l'id du match.
            //et on bouclera pour voir si le match est deja referencé sur cette plage la... dans ce cas on ne cree pas.

            //il faudra ajouter un systeme via sms ou autre, afin d'avoir des news sur les matchs candidats a la mi temps
            if (ConfigurationManager.AppSettings["MATCHID_" + idmatch] != idmatch) // ca veut dire que le match n'est pas enregistré
                                                                                   //if (IsMatchAlreadyScheduled(idmatch) == false)
            {
                config.AppSettings.Settings.Add("MATCHID_" + idmatch, idmatch);
                config.AppSettings.Settings.Add("DATEHEURE_" + idmatch, dateheurematch.ToString("dd/MM/yyyy HH:mm"));// ;
                                                                                                                     // DateTime = dateheurematch.AddMinutes(45),// DateTime.Parse("2020-09-10T09:00:00-06:00"),
                config.AppSettings.Settings.Add("LEAGUE_" + idmatch, feuillet);
                config.AppSettings.Settings.Add("TEAMHOME_" + idmatch, teamhome);
                config.AppSettings.Settings.Add("TEAMAWAY_" + idmatch, teamaway);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }

            //} //fin condition deja programmé... on pourra encore amélioré le truc, c'est de supprimer l'evenement afin de le recreer..; si il y a eu des mises a jour entretemps
        }



        private Boolean IsMatchAlreadyScheduled(string idmatch)
        {
            bool retour = false;
            EventsResource.ListRequest request = servicecalendar.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 20;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            // List events.
            Events events = request.Execute();
            // Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(eventItem.Location) == false)
                    {
                        if (eventItem.Location.ToString().Contains("MATCHID:"))
                        {
                            //dans ce cas on analyse si c'et l'id du match indiqué
                            if (eventItem.Location.ToString() == "MATCHID:" + idmatch)
                            {
                                retour = true;
                            }
                            //     Console.WriteLine(eventItem.Summary+retour.ToString());
                        }
                    }
                    // Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }

            return retour;


        }

        private void button13_Click(object sender, EventArgs e)
        {
            IsMatchAlreadyScheduled("452533");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            scan_matchs();
        }

        public void scan_matchs()
        {
            ////au chargement de l'appli on recupere l'ensemble des taux par championnats ? ben oui.
            ///denomination?
            ///
            string Debugencours = "NON"; // a desactiver pour passer en prod (
            //test debug

            for (int i = 0; i < ConfigurationManager.AppSettings.Keys.Count;)
            {
                if (ConfigurationManager.AppSettings.Keys[i].Contains("MATCHID_") == true)
                {
                    //ok donc la on peux recuperer l'id
                    string recupid = ConfigurationManager.AppSettings.Keys[i].ToString().Replace("MATCHID_", "");
                    //on analyse alors l'heure de la mi temps
                    string recupdateheureST = ConfigurationManager.AppSettings["DATEHEURE_" + recupid].ToString();
                    //Console.WriteLine(recupdateheureST);
                    //string valeursaisonnulencoursST = "";

                    DateTime recupdateheure = DateTime.Parse(recupdateheureST);
                    if ((DateTime.Compare(recupdateheure.Date, DateTime.Today) == 0) ||(Debugencours=="OUI")) //on analyse si on est le meme jour deja (puique 24h de matchs)
                    {
                        Console.WriteLine("on a au moins un match ce jour ");
                        if ((DateTime.Compare(recupdateheure.AddMinutes(49), DateTime.Now) < 0))// || (Debugencours == "OUI"))//plus 7 mn apres debut de la mitemps potentiel ?
                        {
                            Console.WriteLine("on a au moins sun match déjà demarré ou finie ");
                            if (DateTime.Compare(recupdateheure.AddMinutes(65), DateTime.Now) > 0 )//|| (Debugencours == "OUI"))// on lache l'affaire si le match a reprise a la 65ieme
                                                                                                  //on passera aussi le nettoyage lors de relancement de la fonction agenda
                            // on lache l'affaire si le match a reprise a la 65ieme 
                            {
                                string championnat;
                                //if (Debugencours == "OUI") { championnat="Ligue1"; }
                                //        else
                                    {
                                         championnat = ConfigurationManager.AppSettings["LEAGUE_" + recupid].ToString();
                                    }
                                //on va annuler l'appel si on a deja une alerte dans l'agenda...pour limiter les appels aux requetes
                                //on ajoute encore un critere qui evite d'alerter pour rien et qui evite des requetes stats pour rien
                                //evaluation de la stat long terme... si le championnat n'as pas la bonne stat...
                                //pas la peine d'aller plus loin.

                                //futur appel a la stat pour comparer, en gros si ni le taux nul ni le taux 0-0 n'est pertinent..
                                //si l'un ou l'autre, l'es, alors il faut aller evaluer le score...
                                //on se contente de recuperer la valeur stockée dans le fichier config. deux taux par championnat.
                                //si les deux taux ne sont pas interessant, alors on appel meme pas.
                                //  GetvaluefromStat_saison_encoursFromAppSettingorCell(this.nom_championnat.value, "RATIO_ZEROZERO"); }
                                //  GetvaluefromStat_saison_encoursFromAppSettingorCell(this.nom_championnat.value, "RATIO_NULS"); }
                                Criteres criteres_services = new Criteres();
                                string RATIO_NULS_evaluation = "";
                                string RATIO_ZEROZERO_evaluation = "";
                                string valeursaisonnulencoursST = criteres_services.GetvaluefromStat_saison_encoursFromAppSettingorCell(championnat, "RATIO_NULS");
                                string valeursaisonnulencours = valeursaisonnulencoursST.Replace("%", "");
                                valeursaisonnulencours = valeursaisonnulencours.Replace(",", ".");
                                double valeurreferencenul = 3;
                                double valeursaisonnulencoursfloat = Double.Parse(valeursaisonnulencours.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                                if (valeursaisonnulencoursfloat < valeurreferencenul) { RATIO_NULS_evaluation = "OUI"; }
                                else { RATIO_NULS_evaluation = "NON"; }
                                string valeursaisonzeroencoursST = criteres_services.GetvaluefromStat_saison_encoursFromAppSettingorCell(championnat, "RATIO_ZEROZERO");
                                string valeursaisonzeroencours = valeursaisonzeroencoursST.Replace("%", "");
                                valeursaisonzeroencours = valeursaisonzeroencours.Replace(",", ".");
                                double valeurreferencezero = 1.5;
                                double valeursaisonzeroencoursfloat = Double.Parse(valeursaisonzeroencours.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                                if (valeursaisonzeroencoursfloat > valeurreferencezero) { RATIO_ZEROZERO_evaluation = "OUI"; }
                                else { RATIO_ZEROZERO_evaluation = "NON"; }

                                if ((RATIO_ZEROZERO_evaluation == "OUI") || (RATIO_NULS_evaluation == "OUI"))  //si on au moins un sur deux, alors il faut le score pour analyser plus finement
                                {
                                    //if (IsAlerteMatchAlreadyScheduled(recupid) == false) 
                                    //peut etre on verifie si le statut est bien mi temps en cours ? bon pour ça faudra testé avec des matchs en face.
                                   // if (Debugencours == "OUI") { StatMitemps(championnat, "571474"); }
                                   //     else
                                    { StatMitemps(championnat, recupid); }
                                }
                                //on va mettre une pause pour eviter de saturer l'api en cas de relancement d'une fiche.2 secondes
                                System.Threading.Thread.Sleep(5000);// pour une pause de 5 secondes.
                                                                    // Console.WriteLine("on est pleine mitemps...de ");  //les conditions sont reunis pour recuper les states et mettre a jour l'evenement
                                                                    //Console.WriteLine(recupid);
                            }
                        }
                    }

                }
                i++;
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            //create_fiche_match mycellulesheet = new 
            //string idmatch, string json_in, string championnat)
            //  create_fiche_match("19999","bidon","Ligue1");
            //Critere critere = new Critere();
            //Console.WriteLine(critere.Getvaluefromleague_teams("78", "Ligue1", "MOYENNE_BUTS_MARQUES"));
            // Analyzestat.Analyse(idmatch, resultat, championnat);
            //Console.WriteLine(critere.GetColonneID("teams", "Ligue1", "NB_ZEROZERO_5DMATCHS"));
            // StatMitemps("Ligue1", "571475"); //type lay 0-0
           //  StatMitemps("Ligue1", "571474"); //type lay 0-1


            //NettoyageAgendaAppConfig();
            CelluleSheet servicecellulesheet = new CelluleSheet();
            string nomfiche = "MODELE2";
            if (servicecellulesheet.Isfichematchalreadyexist(service, nomfiche) == true)
                 {textBox1.Text="la fiche "+ nomfiche+" existe "; }
            else
            { textBox1.Text="la fiche " + nomfiche+" n 'existe pas "; }
            //servicecellulesheet.DuplicateSheet(service, "IDMATCH_592917", "rien", "rien");
            //servicecellulesheet.Duplicatanddeletesheetfichesmatchs(service);
            //servicecellulesheet.DeleteSheet(service, "IDMATCH_627980", "rien", "rien");
            //DeleteSheet
            // servicecellulesheet.FormatEvaluation("IDMATCH_568088");
            // GestionModeAuto("statteams", "ALL");
            //servicecellulesheet.CelluleColor(service, "IDMATCH_568088", "3","rien");
            // textBox1.Text = "";
            // listBox1.Items.Clear();
            // scan_matchs();
            //StatMitemps
            //StatMitemps("Ligue1", "571493");
            /*((((((((((((((((((((((((((((((((((((((((((
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/fixtures/id/571493";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/fixture/571493";
            //get("https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/{fixture_id}");
            //  chaine="https://api-football-v1.p.rapidapi.com/v2/fixtures/id/571493"
            //571493
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/571495";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/571493";
            //match en cours

            //chaine = "https://api-football-beta.p.rapidapi.com/fixtures?from=2020-08-29&to=2020-09-01&league=61&season=2020&status=FT";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/leagues/country/France/2020";
            var client = new RestClient(chaine);// "http
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
            IRestResponse response = client.Execute(request);
            string resultat = response.Content;
            textBox1.Text = resultat;
            
            
            //le systeme ci dessous, permet de taper dans une copie du json plutot que de multiplier les appels pour la mise au point...
            
            FileStream fs = new FileStream("C:\\tom\\jsonsource_fixture_id.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string myText = string.Empty;
            while (!sr.EndOfStream)
            {
                myText += sr.ReadLine();
            }
            sr.Close();
           
            string resultat = myText;
            textBox1.Text = resultat;  
            //System.IO.File.WriteAllText("C:\\tom\\jsonsource.txt", resultat);
            
            //on remplace null par 0...
            // string s= "\((((((("0\"";
            // string s = "0";
           
            resultat = resultat.Replace("null", "0");  //valeur non define = zero dans le contexte, sinon ca explose.
            resultat = resultat.Replace("Passes %", "Passes");  //probleme sur Passes %, car % est un caractere reservé en c#
            var obj = JsonConvert.DeserializeObject<RootobjectStatistics>(resultat.Replace(" ", ""));             //ppff c'est ca... il pouvait pas matcher a cause des espaces
            listBox1.Items.Add(obj.api.fixtures[0].statistics.ShotsonGoal.away);
            listBox1.Items.Add(obj.api.fixtures[0].statusShort);
            listBox1.Items.Add(obj.api.fixtures[0].goalsHomeTeam);
                //.statistics.ShotsonGoal.away);
            //statistics.ShotsoffGoal.away);
            // listBox1.Items.Add(obj.statistics.ShotsoffGoal.home);
            /*
            listBox1.Items.Add(obj.api.statistics.ShotsonGoal.home);
            listBox1.Items.Add(obj.api.statistics.ShotsonGoal.away);
            listBox1.Items.Add(obj.api.statistics.ShotsoffGoal.home);
            listBox1.Items.Add(obj.api.statistics.ShotsoffGoal.away);
            listBox1.Items.Add(obj.api.statistics.TotalShots.home);
            listBox1.Items.Add(obj.api.statistics.TotalShots.away);
            listBox1.Items.Add(obj.api.statistics.BlockedShots.home);
            listBox1.Items.Add(obj.api.statistics.BlockedShots.away);
            listBox1.Items.Add(obj.api.statistics.Shotsinsidebox.home);
            listBox1.Items.Add(obj.api.statistics.Shotsinsidebox.away);
            listBox1.Items.Add(obj.api.statistics.Shotsoutsidebox.home);
            listBox1.Items.Add(obj.api.statistics.Shotsoutsidebox.away);
            listBox1.Items.Add(obj.api.statistics.Fouls.home);
            listBox1.Items.Add(obj.api.statistics.Fouls.away);
            listBox1.Items.Add(obj.api.statistics.CornerKicks.home);
            listBox1.Items.Add(obj.api.statistics.CornerKicks.away);
            listBox1.Items.Add(obj.api.statistics.Offsides.home);
            listBox1.Items.Add(obj.api.statistics.Offsides.away);
            listBox1.Items.Add(obj.api.statistics.BallPossession.home);
            listBox1.Items.Add(obj.api.statistics.BallPossession.away);
            listBox1.Items.Add(obj.api.statistics.YellowCards.home);
            listBox1.Items.Add(obj.api.statistics.YellowCards.away);
            listBox1.Items.Add(obj.api.statistics.RedCards.home);
            listBox1.Items.Add(obj.api.statistics.RedCards.away);
            listBox1.Items(.Add(obj.api.statistics.GoalkeeperSaves.home);
            listBox1.Items.Add(obj.api.statistics.GoalkeeperSaves.away);
            listBox1.Items.Add(obj.api.statistics.Totalpasses.home);
            listBox1.Items.Add(obj.api.statistics.Totalpasses.away);
            listBox1.Items.Add(obj.api.statistics.Passesaccurate.home);
            listBox1.Items.Add(obj.api.statistics.Passesaccurate.away);
            listBox1.Items.Add(obj.api.statistics.Passes.home);
            listBox1.Items.Add(obj.api.statistics.Passes.away);
            //on enregistre direct dans le feuillet de championnat //pour le moment on pas pas précisé le championnat
            /*
                                  //  Shotsoutsidebox.away;
                        //listBox1.Items.Add(obj.Shotsinsidebox.away.ToString());
                        //            listBox1.Items.Add(obj.api.statistics.BallPossession.home.ToString());
                        //listBox1.Items.Add(obj.api.statistics.BallPossession.away);
                        // listBox1.Items.Add(obj.api.statistics.BlockedShots.home);
                        // listBox1.Items.Add(obj.api.statistics.BlockedShots.away);
                        // Fouls.away.ToString());
                        // if (String.IsNullOrEmpty("ss")==false)

                        {
                            listBox1.Items.Add(obj.api.statistics.Fouls.home.ToString());
                            }
                        if (String.IsNullOrEmpty(obj.api.statistics.Fouls.away.ToString()) == false)
                        {
                            listBox1.Items.Add(obj.api.statistics.Fouls.away.ToString());
                        }
                        try
                        {
                            listBox1.Items.Add(obj.api.statistics.YellowCards.home);
                        }
                        catch
                        { }
                        finally
                        {
                            listBox1.Items.Add("pas de valeur definie");
                        }
                        try
                        {(
                            listBox1.Items.Add(obj.api.statistics.YellowCards.away.ToString());
                        }
                        catch
                        { }
                        finally
                        {
                            listBox1.Items.Add("pas de valeur definie");
                        }
                       // if (String.IsNullOrEmpty(obj.api.statistics.YellowCards.away.ToString()) == false)
                       // {
                       //     listBox1.Items.Add(obj.api.statistics.YellowCards.away.ToString());
                       /// }
                       /// 
                       /// 
                       /// 
                       */
            //IsNullOrEmpty
            // listBox1.Items.Add(obj.api.statistics.Fouls.home.ToString());
            //listBox1.Items.Add(obj.api.statistics.Fouls.away.ToString());
            // listBox1.Items.Add(obj.api.statistics.YellowCards.home.ToString());
            // listBox1.Items.Add(obj.api.statistics.YellowCards.away.ToString());

            // listBox1.Items.Add(obj.api.statistics.RedCards.home.ToString());
            // listBox1.Items.Add(obj.api.statistics.RedCards.away.ToString());
            // listBox1.Items.Add(obj.api.statistics.BallPossession.home.ToString());
            //  listBox1.Items.Add(obj.api.statistics.BallPossession.away.ToString());
            // Fouls.away.ToString()); ;
            //(obj.Api.statistics.
            //Shotsoutsidebox.away);
            //foreach (var item in obj.Api.statistics)
            //ToString())


        }

        private void StatMitemps(string championnat, string idmatch)
        {

            textBox1.Text = "";
            listBox1.Items.Clear();

            //get("https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/{fixture_id}");
            //571493
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/571495";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/"+idmatch;
            //match en cours
            chaine = "https://api-football-v1.p.rapidapi.com/v2/fixtures/id/" + idmatch;// 571493";

            //chaine = "https://api-football-beta.p.rapidapi.com/fixtures?from=2020-08-29&to=2020-09-01&league=61&season=2020&status=FT";
            //chaine = "https://api-football-v1.p.rapidapi.com/v2/leagues/country/France/2020";
            var client = new RestClient(chaine);// "http
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "c98b841954msh187253efecb9cf7p111a96jsn7b037ef9d85a");
            IRestResponse response = client.Execute(request);
            string resultat = response.Content;

            /*  
            FileStream fs = new FileStream("C:\\tom\\jsonsource_fixture_id.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string myText = string.Empty;
            while (!sr.EndOfStream)
            {
                myText += sr.ReadLine();
            }
            sr.Close();
           
            string resultat = myText;
            textBox1.Text = resultat;  
                 var oblist = new List<object>();
            if (MYGlobalVars.APIFOOTBALL == "ALPHA")
            {
                oFixtureJsoncourant = oFixtureJsoncourant.Replace("null", "0");  //valeur non define = zero dans le contexte, sinon ca explose.
                oFixtureJsoncourant = oFixtureJsoncourant.Replace("Passes %", "Passes");  //probleme sur Passes %, car % est un caractere reservé en c#
                var obj = JsonConvert.DeserializeObject<RootobjectStatistics>(oFixtureJsoncourant.Replace(" ", "")); //ppff c'est ca... il pouvait pas matcher a cause des espaces
                oblist = new List<object>()
                {
                      obj.api.fixtures[0].statusShort,
       
            */
                string oFixtureJsoncourant = resultat;
                oFixtureJsoncourant = oFixtureJsoncourant.Replace("null", "0");  //valeur non define = zero dans le contexte, sinon ca explose.
                oFixtureJsoncourant = oFixtureJsoncourant.Replace("Passes %", "Passes");  //probleme sur Passes %, car % est un caractere reservé en c#
                var obj = JsonConvert.DeserializeObject<RootobjectStatistics>(oFixtureJsoncourant.Replace(" ", "")); //ppff c'est ca... il pouvait pas matcher a cause des espaces                                                                                                         //{
                string statusmatch = obj.api.fixtures[0].statusShort;
            
                //on teste si on est bien à la mi temps et pas dans les arrets de jeux
                if ((statusmatch == "HT"))// ||(idmatch== "571474")) // si on est pas vraiment a la mi temps, on s'arrete la //pour l'id, c'est le premier ligue 1 pour test donc
                {
                    writeintoGooglesheet(idmatch, resultat, 1, championnat, "stat_mitemps", new Statsteams(), new StatsLeagues("0", "0", 0, 0));
                    ///apres le calcul des stats
                    // on pars directe sur l'analyse des stats pour voir si on signale le match ou pas (construction de la fiche recap en parallele)
                    Analyzestat.Analyse(idmatch, resultat, championnat);
                }
        }

        public void AddAlertemitempsmatchoncalendar(string idmatch, string feuillet, string teamhome, int scorehome, string teamaway, int scoreaway, int cartonrouge, string url)
        {
            //Addmatchoncalendar(idmatch, dateheurematch, feuillet, teamhome, teamaway);
            //il faudra ajouter un controle pour eviter de doublonner les rdv.  par contre c'est nickel pas d'appel a des requetes supplementaires.
            //on mettra l'id du match.
            //et on bouclera pour voir si le match est deja referencé sur cette plage la... dans ce cas on ne cree pas.
            //comment gerer le cas des alertes, dans un cycle de 3 mn, on risque de remettre plusieurs alarmes, faut juste eviter.
            //il faudra ajouter un systeme via sms ou autre, afin d'avoir des news sur les matchs candidats a la mi temps
            if (IsAlerteMatchAlreadyScheduled(idmatch) == false)  //on ajoute pas d'evenement si on l'a deja fait pour ce match
            {
                Event newEvent = new Event()
                {
                    Summary = feuillet + " " + teamhome + "/" + teamaway,
                    Location = "ALERTEMATCHID:" + idmatch,// teamhome,// on va remplacer la localisation par l'IDMATCH, ce qui permettra de voir si on a pas deja le match de programmé...
                    Description = teamhome + ": " + scorehome + "/" + teamaway + ": " + scoreaway + "/carton(s) rouge(s):" + cartonrouge+" "+url,// + "zriojh   ihjiohj erijrij rhijreihje",
                    //ajout du lien vers la fiche match dans la description donc.

                    Start = new EventDateTime()
                    {
                        DateTime = DateTime.Now.AddMinutes(1),  //on veux signaler le truc immediatement
                        TimeZone = "Europe/Paris",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = DateTime.Now.AddMinutes(10),  //on veux signaler le truc immediatement
                        TimeZone = "Europe/Paris",
                    },
                    Reminders = new Event.RemindersData()
                    {
                        UseDefault = false,
                        Overrides = new EventReminder[]
                                {
                   // new EventReminder() { Method = "email", Minutes = 24 * 60 },
                    new EventReminder() { Method = "popup", Minutes = 0 }
                                }
                    }
                };
                String calendarId = "primary";
                EventsResource.InsertRequest request = servicecalendar.Events.Insert(newEvent, calendarId);
                Event createdEvent = request.Execute();
                Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);
            }
        } //fin condition deja programmé... on pourra encore amélioré le truc, c'est de supprimer l'evenement afin de le recreer..; si il y a eu des mises a jour entretemps


        public Boolean IsAlerteMatchAlreadyScheduled(string idmatch)
        {
            bool retour = false;
            EventsResource.ListRequest request = servicecalendar.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 20;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            // List events.
            Events events = request.Execute();
            // Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(eventItem.Location) == false)
                    {
                        if (eventItem.Location.ToString().Contains("ALERTEMATCHID:"))
                        {
                            //dans ce cas on analyse si c'et l'id du match indiqué
                            if (eventItem.Location.ToString() == "ALERTEMATCHID:" + idmatch)
                            {
                                retour = true;
                            }
                            //     Console.WriteLine(eventItem.Summary+retour.ToString());
                        }
                    }
                    // Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }

            return retour;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MajChampionnat(typeGetApiFootball, lb_league.SelectedItem.ToString(), "TOTAL");
        }

        /*
        public void create_fiche_match(string idmatch, Criteres critere_in = new Criteres(), string championnat)
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
            string destinationSpreadsheetId="";
            //int sheetId = 0;  // TODO: Update placeholder value.
            // The ID of the spreadsheet to copy the sheet to.
            //creation du feuillet
            if (championnat == "Ligue1")
                {
                destinationSpreadsheetId = "1fVG_LI5C5ThcRKXJEbLgtrW2qCxGhL88UYkZhHziJJs";  // TODO: Update placeholder value. 
                }
                //il faudra un classeur par championnat
                // TODO: Assign values to desired properties of `requestBody`:

            var addSheetRequest = new AddSheetRequest();
            addSheetRequest.Properties = new SheetProperties();
            addSheetRequest.Properties.Title = "IDMATCH_"+idmatch;
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { AddSheet = addSheetRequest });
            var batchUpdateRequest = service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, destinationSpreadsheetId);
            batchUpdateRequest.Execute();

            //
            //on devra également preparer l'ensemble des valeurs pour les enregistrer ailleurs.. (passes t on par un objet ???)
            //clairement, on a des elements a recuperer dans tous les sens...
            //ecriture des criteres
            String range = "IDMATCH_" + idmatch+ "!A1";  // single cell D5
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
            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, destinationSpreadsheetId, range);
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
            SpreadsheetsResource.ValuesResource.UpdateRequest updateValeurs = service.Spreadsheets.Values.Update(valueRangeValeurs, destinationSpreadsheetId, range);
            updateValeurs.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse resultValeurs = updateValeurs.Execute();

            // on fait une pause d'une seconde dans le code ?
            System.Threading.Thread.Sleep(2000);// pour une pause de 1 seconde.pour eviter de saturer la quota google
                                                // Console.WriteLine("fait");                                                

        }
        */

        public void NettoyageAgendaAppConfig()
        {
            /*
            < add key = "MATCHID_568122" value = "568122" />   
            < add key = "DATEHEURE_568122" value = "21/09/2020 20:45" />
            < add key = "LEAGUE_568122" value = "Ligue2" />     
            < add key = "TEAMHOME_568122" value = "Auxerre" />
            < add key = "AWAYHOME_568122" value = "Estac Troyes" />
            */
            //si ca commence par MATCHID_ alors on recupere le reste de la chaine, si on a une serie de 6 chiffes alors on l'extrait et on remove les chaines.
            for (int i = 0; i < ConfigurationManager.AppSettings.Keys.Count;)
            {
                if (ConfigurationManager.AppSettings.Keys[i].Contains("MATCHID_") == true)
                {
                    //ok donc la on peux recuperer l'id
                    string recupid = ConfigurationManager.AppSettings.Keys[i].ToString().Replace("MATCHID_", "");
                    //on retire tous les elements en rapport
                    config.AppSettings.Settings.Remove("MATCHID_" + recupid);
                    config.AppSettings.Settings.Remove("DATEHEURE_" + recupid);
                    config.AppSettings.Settings.Remove("LEAGUE_" + recupid);
                    config.AppSettings.Settings.Remove("TEAMHOME_" + recupid);
                    config.AppSettings.Settings.Remove("AWAYHOME_" + recupid);  // a corriger...
                    config.AppSettings.Settings.Remove("TEAMAWAY_" + recupid);
                    config.Save(ConfigurationSaveMode.Modified);
                    //ConfigurationManager.RefreshSection("appSettings");
                }
                i++;
            }
        }

        public void NettoyageRatiosConfig(string championnat)
        {
            //on reinitialise les valeurs stockées apres chaque mise a jour du championnat concernée
                  config.AppSettings.Settings.Remove("RATIO_NULS_" + championnat);
                  config.AppSettings.Settings.Remove("RATIO_ZEROZERO_" + championnat);
                  config.Save(ConfigurationSaveMode.Modified);
        }

        public int CountmatchAgendaAppConfig()
        {
            /*
            < add key = "MATCHID_568122" value = "568122" />   
            < add key = "DATEHEURE_568122" value = "21/09/2020 20:45" />
            < add key = "LEAGUE_568122" value = "Ligue2" />     
            < add key = "TEAMHOME_568122" value = "Auxerre" />
            < add key = "AWAYHOME_568122" value = "Estac Troyes" />
            */
            //si ca commence par MATCHID_ alors on recupere le reste de la chaine, si on a une serie de 6 chiffes alors on l'extrait et on remove les chaines.
            int comptage = 0;
            for (int i = 0; i < ConfigurationManager.AppSettings.Keys.Count;)
            {
                if (ConfigurationManager.AppSettings.Keys[i].Contains("MATCHID_") == true)
                {
                    //ok compte alors
                    comptage++;
                }
                i++;
            }
            return comptage;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            CelluleSheet servicecellulesheet = new CelluleSheet();
            //servicecellulesheet.DuplicateSheet(service, "IDMATCH_592917", "rien", "rien");
            servicecellulesheet.Purge_archivefiche_matchs(service);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    static class MYGlobalVars
    {
        public static string APIFOOTBALL = "ALPHA";
    }


}


//}