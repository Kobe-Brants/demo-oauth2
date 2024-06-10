using System.Text.Json.Serialization;
using Api.Middleware;
using Dal;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.RegisterDbContexts(builder.Configuration);
        builder.Services.RegisterRepositories();
        builder.Services.RegisterServices();

        builder.Services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication("Bearer", options =>
            {
                options.ApiName = "myApi";
                options.Authority = "https://localhost:7139";
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("read_access", policy =>
                policy.RequireClaim("scope", "myApi.read"))
            .AddPolicy("write_access", policy =>
                policy.RequireClaim("scope", "myApi.write"));
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("corsPolicy",
                policy =>
                {
                    policy.WithOrigins("https://localhost:5080");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("corsPolicy");

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.UseContentSecurityPolicy();
        app.Run();
    }
}