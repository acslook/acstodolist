using Acs.TodoList.Domain.Dtos;
using Acs.TodoList.Domain.Entities;
using Acs.TodoList.Domain.Interfaces.Services;
using Acs.TodoList.Services;
using Microsoft.AspNetCore.Mvc;

namespace Acs.TodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ITodoItemService _todoItemService;
        
        public ItemController(
            ILogger<ItemController> logger,
            ITodoItemService todoItemService)
        {
            _logger = logger;
            _todoItemService = todoItemService;
        }

        [HttpGet]        
        public async Task<ResponseDto> GetWithPagination([FromQuery] int limit = 50, [FromQuery] int offset = 0)
        {
            return await _todoItemService.GetAllWithPagination(limit, offset);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Item> GetById([FromRoute] Guid id)
        {
            return await _todoItemService.GetbyId(id);
        }

        [HttpPost(Name = "AddItem")]
        public async Task Post(Item item)
        {
            await _todoItemService.Add(item);
        }

        [HttpPut(Name = "UpdateItem")]
        public async Task Put(Item item)
        {
            await _todoItemService.Update(item);
        }
    }
}
