using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace flat_vs_fk.Models
{

    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        
       
        public int ArtistId { get; set; }

       
        public Artist Artist { get; set; }
    }
}