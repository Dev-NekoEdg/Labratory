using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Entities
{
    [Table("Lists", Schema = "lab")]
    public class Lists
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
