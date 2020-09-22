using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Episodes
{
    public class EditEpisodeViewModel : CreateEpisodeViewModel
    {
        public int EpisodeId { get; set; }
    }
}
