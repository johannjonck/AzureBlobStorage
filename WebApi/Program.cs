using Application.Logic.Document.Requests;
using Infrastructure.Assemblers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using FluentValidation.AspNetCore;
using Application.Logic.Employee.Validators;
using Application.Logic.Employee.Requests;
using FluentValidation;
using Application.Logic.Document.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(ListDocumentRequest).Assembly);
builder.Services.AddAutoMapper(typeof(DocumentMappingProfile));
builder.Services.AddControllers().AddNewtonsoftJson();

//builder.Services.AddValidatorsFromAssembly(typeof(AddDocumentRequest).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
AssemblyScanner.FindValidatorsInAssembly(typeof(AddDocumentValidator).Assembly)
.ForEach(result =>
{
    builder.Services.AddTransient(result.InterfaceType, result.ValidatorType);
});

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


// Add DbContext using SQL Server Provider for InMemory database
builder.Services.AddDbContext<IDbContext, DataContext>(options =>
{
    options.UseInMemoryDatabase("QBlobStorageDB");
});

// Add DbContext using SQL Server Provider for local database
//builder.Services.AddDbContext<IDbContext, DataContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOption => sqlOption.UseNetTopologySuite());
//});

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
