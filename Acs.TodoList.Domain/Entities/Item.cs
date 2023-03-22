using Acs.TodoList.Domain.Enums;

namespace Acs.TodoList.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public ItemStatus Status { get; set; }
        public bool Favorite { get; set; }
    }
}
