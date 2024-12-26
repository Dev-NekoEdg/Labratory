using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Dtos
{
    public class ListsItemsDto
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string ListName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool Complete { get; set; }

    }

}
