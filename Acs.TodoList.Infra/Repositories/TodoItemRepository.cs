using Acs.TodoList.Domain.Dtos;
using Acs.TodoList.Domain.Entities;
using Acs.TodoList.Domain.Interfaces.Repositories;
using Acs.TodoList.Infra.SqlStatements;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Acs.TodoList.Infra.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IConfiguration _configuracoes;
        private readonly string? _connectionString;

        public TodoItemRepository(IConfiguration configuration)
        {
            _configuracoes = configuration;
            _connectionString = _configuracoes.GetConnectionString("DbAcsTodoList");
        }

        public async Task Add(Item item)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(ItemSqlStatements.InsertItem, item);        
        }

        public async Task<ResponseDto> GetAllWithPagination(int limit, int offset)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync(ItemSqlStatements.GetItemWithPagination, new { limit, offset }))
                {
                    var responseDto = new ResponseDto();
                    responseDto = await multi.ReadFirstAsync<ResponseDto>();
                    responseDto.Content = (await multi.ReadAsync<Item>()).ToList();
                    
                    return responseDto;
                }
            }            
        }

        public async Task<Item?> GetbyId(Guid id) 
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Item>(ItemSqlStatements.GetById, new { id });
        }

        public async Task Update(Item item)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(ItemSqlStatements.UpdateItem, item);
        }      
    }
}
