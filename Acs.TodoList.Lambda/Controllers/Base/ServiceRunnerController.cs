using Acs.TodoList.Domain.Notifications;
using Amazon.Lambda.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acs.TodoList.Lambda.Controllers.Base
{
    public class ServiceRunnerController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Object> _logger;
        private readonly NotificationContext _notificationContext;

        public ServiceRunnerController(            
            IServiceProvider serviceProvider, 
            ILogger<Object> logger, 
            NotificationContext notificationContext)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        protected async Task<IActionResult> RunStatus<TRequest, TResponse>(Func<TRequest, Task<TResponse>> serviceMethod, TRequest dataRequest, HttpStatusCode expectedStatusResult = HttpStatusCode.OK)
        {
            using var providerScope = _serviceProvider.CreateScope();
            try
            {
                _logger.LogInformation($"{JsonSerializer.Serialize(HttpContext?.Request.Headers)}");
                _logger.LogInformation($"running request.");

                var validator = providerScope.ServiceProvider.GetService<IValidator<TRequest>>();
                var validatorResult = validator?.Validate(dataRequest);

                if(validatorResult != null && !validatorResult.IsValid)
                {
                    var errors = string.Join("\n", validatorResult.Errors.Select(s => s.ErrorMessage));
                    return BadRequest(errors);
                }

                var response = await serviceMethod(dataRequest);

                if (_notificationContext.HasNotifications)
                {
                    _logger.LogWarning(JsonSerializer.Serialize(_notificationContext.Notifications));
                    return StatusCode((int)HttpStatusCode.BadRequest, _notificationContext.Notifications);
                }

                return StatusCode((int)expectedStatusResult, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{GetType().BaseType} ending request with exception.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error during processing of the request.");
            }
        }
    }
}
