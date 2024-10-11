using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trophy_library
{
    public class TrophiesRepository : ITrophiesRepository
    {
        private int _nextId = 1;
        private readonly List<Trophy> _trophies = new();

        public Trophy Add(Trophy trophy)
        {
            trophy.Validate();
            trophy.Id = _nextId++;
            _trophies.Add(trophy);
            return trophy;
        }

        public IEnumerable<Trophy> Get(int? year = null, string? competitionincludes = null, string orderBy = null)
        {
            IEnumerable<Trophy> result =new List<Trophy>( _trophies); //Copy constructor to make sure the get method cannot access in the original list
            //filtering

            if (year != null)
            { result = result.Where(t => t.Year >= year); } //Filters out trophies with a year less than the year parameter
            if (competitionincludes != null)
            { result = result.Where(t => t.Competition.Contains(competitionincludes)); } //Filters out trophies that does not contain competitionincludes parameter

            if (orderBy != null)
            {
              orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "year": //Case where it contains year
                    case "year asc": //either year asc

                        result = result.OrderBy(t => t.Year);
                        break;

                    case "year desc": //or year desc
                        result = result.OrderByDescending(t => t.Year);
                        break;


                    case "competition": //Case where it contains competition
                    case "competition asc": //either competition asc
                        result = result.OrderBy(t => t.Competition);
                        break;

                    case "competition desc": //or competition desc
                        result = result.OrderByDescending(t => t.Competition);
                        break;

                    default:
                        throw new ArgumentException("Invalid orderBy parameter");
                }
            }

            return result;
        }

        public Trophy? GetById(int id)
        {
            Trophy? trophy = _trophies.Find(t => t.Id == id);
            return trophy;
        }

        public Trophy? Remove(int id)
        {
           Trophy? trophy = GetById(id);
            if (trophy != null)
            {
                _trophies.Remove(trophy);
            }
            return trophy;
        }

        public Trophy? Update(int id, Trophy trophy)
        {
            trophy.Validate();
            Trophy? oldTrophy = GetById(id);
            if (oldTrophy != null)
            {
                oldTrophy.Competition = trophy.Competition;
                oldTrophy.Year = trophy.Year;
            }
            return oldTrophy;
        }
    }
}
