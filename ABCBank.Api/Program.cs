using ABCBank.Application.Interfaces;
using ABCBank.Application.Services;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Implementations.Services;
using ABCBank.Infrastructure.Data;
using ABCBank.Infrastructure.Implementations.GenericRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ABCBankDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("OFFLINE"),b=>b.MigrationsAssembly("ABCBank.Api"));
}); //THIS IS FOR  THE OFFLINE DATABASE

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddScoped<ITransactionService,TransactionService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddCors(x=>{
    x.AddDefaultPolicy(x=>x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseCors();
app.Run();