using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace flat_vs_fk.Models
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }

        public string Name { get; set; }
    }
}
