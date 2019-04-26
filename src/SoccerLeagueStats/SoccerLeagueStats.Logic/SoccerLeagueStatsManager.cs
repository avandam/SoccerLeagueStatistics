using System.Collections.Generic;
using System.Linq;
using SoccerLeagueStats.Logic.FileManagement;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic
{
    public class SoccerLeagueStatsManager
    {
        private readonly List<Club> allClubs = ClubFileHandler.GetAllClubs();
        private readonly List<Competition> allCompetitions = CompetitionFileHandler.GetAllCompetitions();
        private readonly List<Season> allSeasons = SeasonFileHandler.GetAllSeasons();
        private readonly List<Result> allResults = ResultFileHandler.GetAllResults();

        public IReadOnlyCollection<Club> GetAllClubsInCountry(string country)
        {
            return allClubs.Where(club => club.Country == country).OrderBy(club => club.Name).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Competition> GetAllCompetitionInCountry(string country)
        {
            return allCompetitions.Where(competition => competition.Country == country).OrderBy(competition => competition.Level).ThenBy(competition => competition.Name).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Season> GetSeasonsForCompetition(string competitionId)
        {
            return allSeasons.Where(season => season.Competition.Id == competitionId).OrderBy(season => season.Name).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Result> GetResultsForSeason(string seasonId)
        {
            return allResults.Where(result => result.Season.Id == seasonId).OrderBy(result => result.Place).ToList().AsReadOnly();
        }

        //TODO: Fix that the All* variables are used.
        public IReadOnlyCollection<Result> GetAllTimeRankingsForCompetition(string competitionId)
        {
            List<Season> seasonsInCompetition = allSeasons.Where(season => season.Competition.Id == competitionId).ToList();
            List<Club> clubsInCompetition = new List<Club>();
            foreach (Season season in seasonsInCompetition)
            {
                var clubs = allResults.Where(result => result.Season == season).Select(result => result.Club);
                foreach (var club in clubs)
                {
                    if (!clubsInCompetition.Exists(clubToCheck => clubToCheck.Name == club.Name))
                    {
                        clubsInCompetition.Add(club);
                    }
                }
            }

            List<Result> resultsPerClub = new List<Result>();
            foreach (Club club in clubsInCompetition)
            {
                List<Result> resultsForClub = allResults.Where(resultC => resultC.Club.Name == club.Name && seasonsInCompetition.Contains(resultC.Season)).ToList();
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
