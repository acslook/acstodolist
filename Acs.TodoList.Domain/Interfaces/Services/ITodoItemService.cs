using Acs.TodoList.Domain.Dtos;
using Acs.TodoList.Domain.Entities;

namespace Acs.TodoList.Domain.Interfaces.Services
{
    public interface ITodoItemService
    {
        public Task Add(Item item);
        public Task Update(Item item);
        public Task<ResponseDto> GetAllWithPagination(int limit, int offset);
        public Task<Item> GetbyId(Guid id);
        
    }
}
