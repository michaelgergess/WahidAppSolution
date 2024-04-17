using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WorldChat : BaseEntity
    {
        [Required]
        public string Message { get; set; }
        [ForeignKey("User")]
        public string UserName { get; set; }
        public AppUser User { get; set; }
    }
}
