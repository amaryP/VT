using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Fixture
{
    public class RootobjectFixture
    {
        public string get { get; set; }
        public Parameters parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public Paging paging { get; set; }
        public Response[] response { get; set; }
    }

    public class Parameters
    {
        public string league { get; set; }
        public string season { get; set; }
    }

    public class Paging
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class Response
    {
        //  public Response fixture { get; set; }

        public Fixture fixture { get; set; }
        public League league { get; set; }
        public Teams teams { get; set; }
        public Goals goals { get; set; }
        public Score score { get; set; }
    }



    public class Fixture
    {
        public int id { get; set; }
        public string referee { get; set; }
        public string timezone { get; set; }
        public DateTime date { get; set; }
        public int timestamp { get; set; }
        public Periods periods { get; set; }
        public Venue venue { get; set; }
        public Status status { get; set; }
    }

    public class Periods
    {
        public int? first { get; set; }
        public int? second { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
    }

    public class Status
    {


        public string Long { get; set; }
        public string Short { get; set; }
        public int? elapsed { get; set; }
    }

    public class League
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string logo { get; set; }
        public string flag { get; set; }
        public int season { get; set; }
        public string round { get; set; }
    }

    public class Teams
    {
        public Home home { get; set; }
        public Away away { get; set; }
    }

    public class Home
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public bool? winner { get; set; }
    }

    public class Away
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public bool? winner { get; set; }
    }

    public class Goals
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class Score
    {
        public Halftime halftime { get; set; }
        public Fulltime fulltime { get; set; }
        public Extratime extratime { get; set; }
        public Penalty penalty { get; set; }
    }

    public class Halftime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class Fulltime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class Extratime
    {
        public object home { get; set; }
        public object away { get; set; }
    }

    public class Penalty
    {
        public object home { get; set; }
        public object away { get; set; }
    }


    public class Statsteams
    {
        public string idteam { get; set; } //= "93"; //reims
        public string nameteam { get; set; }
        public string league { get; set; }
        public string name_league { get; set; }

        public string refcelluleidteam { get; set; }
        public int nbmatchjouesdomicile { get; set; }
        public int nbmatchjouesexterieur { get; set; }
        public int nbvictoiredomicile { get; set; }
        public int nbdefaitedomicile { get; set; }
        public int nbnuldomicile { get; set; }
        public int nbzerozerodomicile { get; set; }
        public int nbvictoireexterieur { get; set; }
        public int nbdefaiteexterieur { get; set; }
        public int nbnulexterieur { get; set; }
        public int nbzerozeroexterieur { get; set; }
        public int butmisdomicile { get; set; }
        public int butprisdomicile { get; set; }
        public int butmisexterieur { get; set; }
        public int butprisexterieur { get; set; }
        //totalisation
        public int nbmatchjoues { get; set; }
        public int nbvictoire { get; set; }
        public int nbdefaite { get; set; }
        public int nbnul { get; set; }
        public int nbzerozero { get; set; }
        public int butmis { get; set; }
        public int butpris { get; set; }
        public int nbnul10derniermatch { get; set; }
        public int nbzerozero10derniermatch { get; set; }
        public int nbnul5derniermatch { get; set; }
        public int nbzerozero5derniermatch { get; set; }
        public double ratiobutmis { get; set; }
        public double ratiobutpris { get; set; }


    }
    public class StatsLeagues
    {
        public string idleague { get; set; }
        public string name_league { get; set; }

        public string refcelluleidleague { get; set; }
        public int nbteams { get; set; }
        public int nbrounds { get; set; }

        public int nbmatchbyleague { get; set; }
        public int nbmatchbyround { get; set; }
        public int nbroundfinished { get; set; }
        public int nbmatchfinished { get; set; }
        public int currentround { get; set; }
        public int nextround { get; set; }
        public int lastround { get; set; }
        public int nbnulcurrentround { get; set; }
        public int nbzerozerocurrentround { get; set; }
        public int nbnul3lastrounds { get; set; }
        public int nbzerozer3lastrounds { get; set; }

        public void Calcul()
        {
            //on recalcul tout...
            //int test =int.Parse(this.currentround);
            //calcul du last
            if (this.currentround > 1)
            {
                this.lastround = this.currentround - 1;
            }
            else
            {
                this.lastround = 1;
            }
            //calcul du next
            if (this.currentround < this.nbrounds)
            {
                this.nextround = this.currentround + 1;
            }
            else
            {
                this.nextround = this.nbrounds;
            }

        }

        public int Calculnbnulcurrentround()
        {
            this.nbnulcurrentround = rounds[this.currentround].nbnul;
            return this.nbnulcurrentround;
        }

        public int Calculnbnul3lastrounds()
        {
            //on voit si on a deja au moins 3 journées precedentes...sinon on cumule le total qu'on a (courant,1,2,3)...
            if (this.currentround <= 3)
            {
                this.nbnul3lastrounds = rounds[1].nbnul + rounds[2].nbnul + rounds[3].nbnul;
            }
            else
            {
                this.nbnul3lastrounds = rounds[(this.currentround - 1)].nbnul + rounds[(this.currentround - 2)].nbnul + rounds[(this.currentround - 3)].nbnul;
            }
            return this.nbnul3lastrounds;
        }

        public int Calculnbzerozero3lastrounds()
        {
            //on voit si on a deja au moins 3 journées precedentes...sinon on cumule le total qu'on a (courant,1,2,3)...
            if (this.currentround <= 3)
            {
                this.nbzerozer3lastrounds = rounds[1].nbzerozero + rounds[2].nbzerozero + rounds[3].nbzerozero;
            }
            else
            {
                this.nbzerozer3lastrounds = rounds[(this.currentround - 1)].nbzerozero + rounds[(this.currentround - 2)].nbzerozero + rounds[(this.currentround - 3)].nbzerozero;
            }
            return this.nbzerozer3lastrounds;
        }
        public int Calculnbzerozerocurrentround()
        {
            this.nbzerozerocurrentround = rounds[this.currentround].nbzerozero;
            return this.nbzerozerocurrentround;
        }

        public List<Round> rounds = new List<Round>();

        public StatsLeagues(string idleague, string nameleague, int nbrounds, int nbteams)
        {
            this.idleague = idleague;
            this.name_league = nameleague;
            this.nbrounds = nbrounds;
            this.nbteams = nbteams;
            //initialisation
            this.nbroundfinished = 0;
            this.nbmatchfinished = 0;
            //on n'a pas verifé les valeurs mais bon...
            this.nbmatchbyleague = (nbteams * nbrounds / 2);
            this.nbmatchbyround = (nbteams / 2);
            //creation de la liste rounds 
            int compteur = 1;
            int max = nbrounds;
            //le premier sert a rien juste les titres, afin que index=numero
            rounds.Add(new Round(0, nbmatchbyround, nbrounds));
            while (compteur <= max)
            {
                rounds.Add(new Round(compteur, nbmatchbyround, nbrounds));
                compteur++;
            }

            // Public void Calculnbnulcurrentround()
            //  {

            // }
        }
    }
    public class Round
    {
        public string Numero { get; set; }
        public string Name_round { get; set; }
        public int nbmatchFT { get; set; }
        public int nbmatchNS { get; set; }
        public int nbmatchTDB { get; set; }
        public int nbmatchCANC { get; set; }
        public int nbnul { get; set; }
        public int nbzerozero { get; set; }
        public int nbmatchsbyround { get; set; }
        public int nbroundbyleague { get; set; }

        public Round(int numero, int nbmatchbyround, int nbroundbyleague)
        {
            //Console.WriteLine("Nouvelle instance round créée.");
            this.Numero = numero.ToString();
            this.Name_round = "Numero : " + numero.ToString();
            this.nbmatchFT = 0;
            this.nbmatchNS = 0;
            this.nbmatchTDB = 0;
            this.nbmatchCANC = 0;
            this.nbnul = 0;
            this.nbzerozero = 0;
            this.nbmatchsbyround = nbmatchbyround;
            this.nbroundbyleague = nbroundbyleague;
        }

    }


    public class RootobjectRound
    {
        public string get { get; set; }
        public ParametersRound parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public PagingRound paging { get; set; }
        public string[] response { get; set; }
    }

    public class ParametersRound
    {
        public string current { get; set; }
        public string league { get; set; }
        public string season { get; set; }
    }

    public class PagingRound
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class RootObjectFixtureAlpha
    {
        public Api api { get; set; }
    }

    public class Api
    {
        public int results { get; set; }
        public FixtureAlpha[] fixtures { get; set; }
    }

    public class FixtureAlpha
    {
        public int fixture_id { get; set; }
        public int league_id { get; set; }
        public LeagueAlpha league { get; set; }
        public DateTime event_date { get; set; }
        public int event_timestamp { get; set; }
        public int? firstHalfStart { get; set; }
        public int? secondHalfStart { get; set; }
        public string round { get; set; }
        public string status { get; set; }
        public string statusShort { get; set; }
        public int elapsed { get; set; }
        public string venue { get; set; }
        public string referee { get; set; }
        public Hometeam homeTeam { get; set; }
        public Awayteam awayTeam { get; set; }
        public int? goalsHomeTeam { get; set; }
        public int? goalsAwayTeam { get; set; }
        public ScoreAlpha score { get; set; }
    }

    public class LeagueAlpha
    {
        public string name { get; set; }
        public string country { get; set; }
        public string logo { get; set; }
        public string flag { get; set; }
    }

    public class Hometeam
    {
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string logo { get; set; }
    }

    public class Awayteam
    {
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string logo { get; set; }
    }

    public class ScoreAlpha
    {
        public string halftime { get; set; }
        public string fulltime { get; set; }
        public string extratime { get; set; }
        public string penalty { get; set; }
    }

    public class Rootobject
    {
        public Api api { get; set; }
    }


    public class RootObjectApiRoundAlpha
    {
        public ApiRoundAlpha apiroundalpha { get; set; }
    }

    public class ApiRoundAlpha
    {
        public int results { get; set; }
        public string[] fixtures { get; set; }
        //{"api":{"results":1,"fixtures":["Regular_Season_-_2"]}}
    }

    /*
    public class RootobjectStatistics
    {
        public ApiStatistics Api { get; set; }
    }

    public class ApiStatistics
    {
        public int results { get; set; }
        public Statistics statistics { get; set; }
    }

    public class Statistics
    {
        public ShotsOnGoal ShotsonGoal { get; set; }
        public ShotsOffGoal ShotsoffGoal { get; set; }
        public TotalShots TotalShots { get; set; }
        public BlockedShots BlockedShots { get; set; }
        public ShotsInsidebox Shotsinsidebox { get; set; }
        public ShotsOutsidebox Shotsoutsidebox { get; set; }
        public Fouls Fouls { get; set; }
        public CornerKicks CornerKicks { get; set; }
        public Offsides Offsides { get; set; }
        public BallPossession BallPossession { get; set; }
        public YellowCards YellowCards { get; set; }
        public RedCards RedCards { get; set; }
        public GoalkeeperSaves GoalkeeperSaves { get; set; }
        public TotalPasses Totalpasses { get; set; }
        public PassesAccurate Passesaccurate { get; set; }
        public Passes Passes { get; set; }
    }

    public class ShotsOnGoal
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class ShotsOffGoal
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class TotalShots
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class BlockedShots
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class ShotsInsidebox
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class ShotsOutsidebox
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class Fouls
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class CornerKicks
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class Offsides
    {
        public object home { get; set; }
        public object away { get; set; }
    }

    public class BallPossession
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class YellowCards
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class RedCards
    {
        public object home { get; set; }
        public object away { get; set; }
    }

    public class GoalkeeperSaves
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class TotalPasses
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class PassesAccurate
    {
        public string home { get; set; }
        public string away { get; set; }
    }

    public class Passes
    {
        public string home { get; set; }
        public string away { get; set; }
    }
    */
}   


