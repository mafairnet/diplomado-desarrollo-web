using ControlEscolaApi.Authorization;
using ControlEscolaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Agregando conectividad con BD
            builder.Services.AddDbContext<CatalogoEscolarContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogoEscolarContext")));

            builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();

            builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
        }
    }
}