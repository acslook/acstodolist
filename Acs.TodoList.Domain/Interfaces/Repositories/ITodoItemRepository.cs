using Acs.TodoList.Domain.Dtos.ItemEntity.Response;
using Acs.TodoList.Domain.Entities;

namespace Acs.TodoList.Domain.Interfaces.Repositories
{
    public interface ITodoItemRepository
    {
        public Task Add(Item item);
        public Task Update(Item item);
        public Task<ResponseDto> GetAllWithPagination(int limit, int offset);
        public Task<Item?> GetbyId(Guid id);
    }
}
