using server_yoga.Repositories;
using server_yogaRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace server_yoga
{
    public class Program
    {

      
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<IRoutineRepository, RoutineRepository>();
            builder.Services.AddTransient<IPosesRepository, PosesRepository>();
            builder.Services.AddTransient<IRoutinePosesRepository, RoutinePosesRepository>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        }
    }
