using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using SoccerLeagueStats.Logic.FileManagement;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic
{
    public class SoccerLeagueStatsManager
    {
        private List<Club> allClubs = FileReader.GetAllClubs();
        private List<Competition> allCompetitions = FileReader.GetAllCompetitions();
        private List<Season> allSeasons = FileReader.GetAllSeasons();
        private List<Result> allResults = FileReader.GetAllResults();

        public IReadOnlyCollection<Club> GetAllClubsInCountry(string country)
        {
            return allClubs.Where(club => club.Country == country).OrderBy(club => club.Name).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Competition> GetAllCompetitionInCountry(string country)
        {
            return allCompetitions.Where(competition => competition.Country == country).OrderBy(competition => competition.Level).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Season> GetSeasonsForCompetition(string competitionId)
        {
            return allSeasons.Where(season => season.Competition.Id == competitionId).OrderBy(season => season.Name).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Result> GetResultsForSeason(string seasonId)
        {
            return allResults.Where(result => result.Season.Id == seasonId).OrderBy(result => result.Place).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Result> GetAllTimeRankingsForCompetition(string competitionId)
        {
            List<Season> seasonsInCompetition = allSeasons.Where(season => season.Competition.Id == competitionId).ToList();
            List<Club> clubsInCompetition = new List<Club>();
            foreach (Season season in seasonsInCompetition)
            {
                clubsInCompetition.AddRange(allResults.Where(result => result.Season == season).Select(result => result.Club));
            }

            List<Result> resultsPerClub = new List<Result>();
            foreach (Club club in clubsInCompetition)
            {
                List<Result> resultsForClub = allResults.Where(resultC => resultC.Club == club && seasonsInCompetition.Contains(resultC.Season)).ToList();
                resultsPerClub.Add(new Result(resultsForClub.Sum(resultD => resultD.Wins), resultsForClub.Sum(resultD => resultD.Draws), resultsForClub.Sum(resultD => resultD.Losses), resultsForClub.Sum(resultD => resultD.GoalsFor), resultsForClub.Sum(resultD => resultD.GoalsAgainst), club));
            }

            resultsPerClub = resultsPerClub.OrderByDescending(resultsE => resultsE.GetPoints(true)).ThenBy(resultsE => resultsE.GetNumberOfMatches()).ThenByDescending(resultsE => resultsE.GetGoalDifference()).ThenByDescending(resultsE => resultsE.GoalsFor).ThenBy(resultsE => resultsE.Club.Name).ToList();

            for (int i = 0; i <= resultsPerClub.Count; i++)
            {
                resultsPerClub[i].SetPlace(i + 1);
            }

            return resultsPerClub;
        }
    }
}
