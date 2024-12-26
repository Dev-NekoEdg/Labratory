using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Entities
{
    [Table("Lists_Items", Schema = "lab")]
    public class ListsItems
    {
        [Key]
        public int Id { get; set; }

        public int ListId { get; set; }
        
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool Complete { get; set; }
    }
}
