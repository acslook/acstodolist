﻿using Acs.TodoList.Domain.Dtos;
using Acs.TodoList.Domain.Entities;
using Acs.TodoList.Domain.Interfaces.Repositories;
using Acs.TodoList.Domain.Interfaces.Services;

namespace Acs.TodoList.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task Add(Item item)
        {
            await _todoItemRepository.Add(item);
        }

        public async Task<ResponseDto> GetAllWithPagination(int limit, int offset)
        {
            return await _todoItemRepository.GetAllWithPagination(limit, offset);
        }

        public async Task<Item> GetbyId(Guid id)
        {
            return await _todoItemRepository.GetbyId(id);
        }

        public async Task Update(Item item)
        {
            await _todoItemRepository.Update(item);
        }
    }
}