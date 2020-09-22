using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Episodes;

namespace TAnime.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext context;

        public EpisodeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public int CreateEpisode(Episode model)
        {
            context.Episodes.Add(model);
            return context.SaveChanges();
        }

        public int DeleteEpisode(int id)
        {
            var delEpi = GetEpisode(id);
            context.Episodes.Remove(delEpi);
            return context.SaveChanges();
        }

        public int EditEpisode(Episode model)
        {
            var editEpi = context.Episodes.Attach(model);
            editEpi.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public Episode GetEpisode(int id)
        {
            return context.Episodes.FirstOrDefault(e => e.EpisodeId == id);
        }

        public IEnumerable<EpisodeViewModel> GetEpisodes()
        {
            List<EpisodeViewModel> episodes = (from e in context.Episodes
                                               join m in context.Movies on e._MovieId equals m.MovieId
                                               select new EpisodeViewModel()
                                               {
                                                   EpisodeId = e.EpisodeId,
                                                   EpisodeCode = e.EpisodeCode,
                                                   EpisodeMovie = e.EpisodeMovie,
                                                   VideoPath = e.VideoPath,
                                                   _MovieName = m.MovieName
                                               }).ToList();
            return episodes;
        }
    }
}
