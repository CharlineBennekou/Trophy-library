using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trophy_library
{
    public interface ITrophiesRepository
    {
        Trophy Add(Trophy trophy);
        IEnumerable<Trophy> Get(int? year = null, string? competitionincludes = null, string orderBy = null); //Skal returnere en kopi af listen vha. copy constructor. Kan filtrere på year. Kan sortere på year eller comp.
        Trophy? GetById(int id); //returnerer trophy med det givne id eller null hvis det ikke findes
        Trophy? Remove(int id); // Fjerner og returnerer trophy med det givne id eller null hvis det ikke findes
        Trophy? Update(int id, Trophy trophy); //Opdaterer og returnerer trophy med det givne id eller null hvis det ikke findes
    }
}
