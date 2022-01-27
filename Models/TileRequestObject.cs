using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Models
{
    public class TileRequestObject
    {
        [Required]
        [DefaultValue("11,49,43,63")]
        public string TileId { get; set; }

        [Required]
        [DefaultValue("ADAM.DIVALENTINO@OTIS.COM")]
        public string UserName { get; set; }

    }
}
