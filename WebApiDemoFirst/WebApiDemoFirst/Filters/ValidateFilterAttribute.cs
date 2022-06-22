using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerCore.DTOs;

namespace WebApiDemoFirst.Filters;

public class ValidateFilterAttribute : ActionFilterAttribute
{
    //Filterlar action çalışmadan önce veya çalıştıktan sonra herhangi bir işlem yapılması istendiğinde kullanılır.
    //Burada FluentValidation'ın döndüğü default validation response'unu değil Custom response'umu dönmek istediğim için
    //filter yazacağım. Actiondan önce çalışacak olan filter context invalid olduğunda kendi custom response'umu döneceğim.
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
        }
    }
}