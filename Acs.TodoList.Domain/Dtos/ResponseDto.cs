using Acs.TodoList.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Acs.TodoList.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ResponseDto
    {
        public long TotalItems { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public List<Item> Content { get; set; }
    }
}
