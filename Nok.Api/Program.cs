using Microsoft.EntityFrameworkCore;
using Nok.Infrastructure.Data;

namespace Nok.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalDev",
                    builder => builder.WithOrigins("http://localhost:4200") // Replace with your client app's URL
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            builder.Services.AddDbContext<DatabaseContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)); });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}



            //if (app.Environment.IsDevelopment())
            //{
            // Use CORS middleware in development environment
            app.UseCors("AllowLocalDev");
            //}

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
