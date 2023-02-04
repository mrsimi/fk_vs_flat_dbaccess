using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace flat_vs_fk.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
