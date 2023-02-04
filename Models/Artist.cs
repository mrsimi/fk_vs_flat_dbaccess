using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace flat_vs_fk.Models
{
    
    public class Artist
    {
       
        public int ArtistId { get; set; }

        
        public string Name { get; set; }
    }
}
