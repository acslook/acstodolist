using Acs.TodoList.Domain.Interfaces.Repositories;
using Acs.TodoList.Domain.Interfaces.Services;
using Acs.TodoList.Infra.Repositories;
using Acs.TodoList.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Acs.TodoList.Infra.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            return services
                    .AddTransient<ITodoItemRepository, TodoItemRepository>()
                    .AddTransient<ITodoItemService, TodoItemService>();
            
        }
    }
}
