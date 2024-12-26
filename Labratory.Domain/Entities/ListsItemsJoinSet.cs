using System.ComponentModel.DataAnnotations;

namespace Labratory.Domain.Entities
{
    public class ListsItemsJoinSet
    {
        [Key]
        public int Id { get; set; }

        public int ListId { get; set; }

        public string ListName { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool Complete { get; set; }
    }
}
