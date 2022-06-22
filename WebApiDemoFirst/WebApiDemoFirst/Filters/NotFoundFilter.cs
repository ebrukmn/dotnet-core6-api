using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Services;

namespace WebApiDemoFirst.Filters;

public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
{
    private readonly IService<T> _service;

    public NotFoundFilter(IService<T> service)
    {
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idParameter = context.ActionArguments.Values.FirstOrDefault();

        if (idParameter == null)
        {
            await next.Invoke();
            return;
        }

        var idValue = (int)idParameter;
        var isExists = await _service.AnyAsync(x => x.Id == idValue);
        
        if (isExists)
        {
            await next.Invoke();
            return;
        }

        context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({idValue}) Not Found !"));
    }
}