using Acs.TodoList.Domain.Dtos;
using Acs.TodoList.Domain.Dtos.ItemEntity.common;
using Acs.TodoList.Domain.Entities;
using Acs.TodoList.Domain.Interfaces.Services;
using Acs.TodoList.Domain.Notifications;
using Acs.TodoList.Lambda.Controllers.Base;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Mvc;

namespace Acs.TodoList.Lambda.Controllers;

[Route("api/[controller]")]
public class ItemController : ServiceRunnerController
{
    private readonly ITodoItemService _todoItemService;

    public ItemController(
        IServiceProvider serviceProvider,
        ILogger<ItemController> logger,
        ITodoItemService todoItemService,        
        NotificationContext notificationContext)
        : base(serviceProvider, logger, notificationContext)
    {        
        _todoItemService = todoItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWithPagination([FromQuery] int limit = 50, [FromQuery] int offset = 0) =>
        await RunStatus(_todoItemService.GetAllWithPagination, new PaginationRequestModel(limit, offset));


    //{
    //    return await _todoItemService.GetAllWithPagination(limit, offset);
    //}

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