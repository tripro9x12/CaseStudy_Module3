using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Repositories
{
    public interface IEpisodeRepository
    {
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisode();
        int CreateEpisode(Episode model);
        int EditEpisode(Episode model);
        int DeleteEpisode(int id);
    }
}
