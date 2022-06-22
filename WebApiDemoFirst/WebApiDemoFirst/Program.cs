using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWorks;
using NLayerRepository;
using NLayerRepository.Repositories;
using NLayerRepository.UnitOfWorks;
using NLayerService.Mapping;
using NLayerService.Services;
using NLayerService.Validators;
using WebApiDemoFirst.Filters;
using WebApiDemoFirst.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Filterları her controllera tek tek eklemektense burada controllerları DI'a register ettiğim yerde options vererek configure edebilirim
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// Önemli : Api tarafında Fluent kendi response modelini döner, ben yukarıda ne kadar Filter configure etsem de Fluent'in response modelini baskılamadan kendi filterımın sonucunu göremem!!
builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddDbContext<AppDbContext>(x=> x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
    options =>
    {
        //tip güvenli olarak preojenin ismini aldım. AppDbContext classının bulunduğu projeyi çektim
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))?.GetName().Name);
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();