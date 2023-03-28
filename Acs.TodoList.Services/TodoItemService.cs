using Acs.TodoList.Domain.Dtos.ItemEntity.common;
using Acs.TodoList.Domain.Dtos.ItemEntity.Response;
using Acs.TodoList.Domain.Entities;
using Acs.TodoList.Domain.Interfaces.Repositories;
using Acs.TodoList.Domain.Interfaces.Services;
using Acs.TodoList.Domain.Notifications;
using FluentValidation;
using System.Text.Json;

namespace Acs.TodoList.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IValidator<PaginationRequestModel> _validator;
        private readonly NotificationContext _notificationContext;

        public TodoItemService(
            ITodoItemRepository todoItemRepository, 
            IValidator<PaginationRequestModel> validator,
            NotificationContext notificationContext)
        {            
            _todoItemRepository = todoItemRepository;
            _validator = validator;
            _notificationContext = notificationContext;
        }
        public async Task Add(Item item)
        {
            Console.WriteLine($"Saving {JsonSerializer.Serialize(item)}");
            await _todoItemRepository.Add(item);
        }

        public async Task<ResponseDto> GetAllWithPagination(PaginationRequestModel model)
        {
            Console.WriteLine($"Executing GetAllWithPagination");

            var validationResult = _validator.Validate(model);

            if (!validationResult.IsValid)
            {
                _notificationContext.AddNotifications(validationResult);
                return await Task.FromResult<ResponseDto>(null);
            }

            return await _todoItemRepository.GetAllWithPagination(model.Limit, model.Offset);
        }

        public async Task<Item> GetbyId(Guid id)
        {
            Console.WriteLine($"Searching id {id}");
            return await _todoItemRepository.GetbyId(id);
        }

        public async Task Update(Item item)
        {
            await _todoItemRepository.Update(item);
        }
    }
}