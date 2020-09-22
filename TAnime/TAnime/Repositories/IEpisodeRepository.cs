using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Episodes;

namespace TAnime.Repositories
{
    public interface IEpisodeRepository
    {
        IEnumerable<EpisodeViewModel> GetEpisodes();
        Episode GetEpisode(int id);
        int CreateEpisode(Episode model);
        int EditEpisode(Episode model);
        int DeleteEpisode(int id);
    }
}
