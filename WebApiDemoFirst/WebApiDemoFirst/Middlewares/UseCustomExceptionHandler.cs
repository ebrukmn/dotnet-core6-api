using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using NLayerCore.DTOs;
using NLayerService.Exceptions;

namespace WebApiDemoFirst.Middlewares;

public static class UseCustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        //Exception middleware'ine configürasyon tanımlıyorum
        app.UseExceptionHandler(config =>
        {
            //IApplicationBuilder'ın Run methodu request/response pipeline'ını terminate eder. Run çalıştığı anda
            //bu middleware'den sonraki akışa devam edilmez.
            
            config.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                //server ve middleware tarafından sağlanan ilgili requestteki feature'ları getirir.
                //Ben burada exception feature'ını getirmek istediğim için TFeature'ı generic olarak IExceptionHandlerFeature tanımladım
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                
                //error ile ilgili detaylar bu feature içerisinde yer alır.

                //Errorun Clienttan mı yoksa serverdan mı kaynaklandığını anlamak için ClientSide custom exception classını oluşturdum.
                //exception feature ClientSide exception'ı ise status Code'u 400 değilse 500 olarak basıyorum.
                var statusCode = exceptionFeature.Error switch
                {
                    ClientSideException => 400,
                    NotFoundException => 404,
                    _ => 500
                };

                //Burada context'in response'unun status Code'u da set edilmelidir. CustomResponseDto'dan serialize olmuyor.
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                
                //controller içerisindeki yapılarda serialize işlemi otomatik yapılır fakar middleware için response serialize edilmelidir.
                
                var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                
                //Burada kullanılan JsonSerializer .net core 3.1 ile gelen built-in bir functionality'dir. 
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });
    }
}