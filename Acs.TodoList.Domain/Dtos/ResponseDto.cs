using Acs.TodoList.Domain.Entities;

namespace Acs.TodoList.Domain.Dtos
{
    public class ResponseDto
    {
        public long TotalItems { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public List<Item> Content { get; set; }
    }
}
