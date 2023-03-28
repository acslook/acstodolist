using Acs.TodoList.Domain.Dtos.ItemEntity.common;
using Acs.TodoList.Domain.Dtos.ItemEntity.Response;
using Acs.TodoList.Domain.Entities;

namespace Acs.TodoList.Domain.Interfaces.Services
{
    public interface ITodoItemService
    {
        public Task Add(Item item);
        public Task Update(Item item);
        public Task<ResponseDto> GetAllWithPagination(PaginationRequestModel model);
        public Task<Item> GetbyId(Guid id);
        
    }
}
