using System;


namespace statisticsmatch
{

    //public class StatisticsC
    //{
    public class RootobjectStatistics
    {
        public Api api { get; set; }
    }

    public class Api
    {
        public int results { get; set; }
        public Fixture[] fixtures { get; set; }
    }

    public class Fixture //RootobjectStatistics
    {
        public int fixture_id { get; set; }
        public int league_id { get; set; }
        public League league { get; set; }
        public DateTime event_date { get; set; }
        public int event_timestamp { get; set; }
        public int firstHalfStart { get; set; }
        public int secondHalfStart { get; set; }
        public string round { get; set; }
        public string status { get; set; }
        public string statusShort { get; set; }
        public int elapsed { get; set; }
        public string venue { get; set; }
        public string referee { get; set; }
        public Hometeam homeTeam { get; set; }
        public Awayteam awayTeam { get; set; }
        public int goalsHomeTeam { get; set; }
        public int goalsAwayTeam { get; set; }
        public Score score { get; set; }
       // public Event[] events { get; set; }
        //public Lineups lineups { get; set; }
        public Statistics statistics { get; set; }
        //public Player[] players { get; set; }
    }

}
/*
    public class RootobjectStatistics
    {
        public api api { get; set; }
        public string statusShort { get; set; }
        public Hometeam homeTeam { get; set; }
        public Awayteam awayTeam { get; set; }
        public int goalsHomeTeam { get; set; }
        public int goalsAwayTeam { get; set; }

        public statistics statistics { get; set; }
        //public Score score { get; set; }

    }

 public class api
    {
        public int results { get; set; }
        public statistics statistics { get; set; }
    }
*/




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
    
    /// ajout des elements de stat
    
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
public class Score
{
    public string halftime { get; set; }
    public string fulltime { get; set; }
    public object extratime { get; set; }
    public object penalty { get; set; }
}
public class League
{
    public string name { get; set; }
    public string country { get; set; }
    public string logo { get; set; }
    public string flag { get; set; }
}

//}

