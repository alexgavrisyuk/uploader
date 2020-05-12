using System.Linq;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Uploader.Core.Commands;
using Uploader.Core.Factories;
using Uploader.Core.Helpers;
using Uploader.Core.ResponseModels;
using Uploader.Domain.Entities;
using Uploader.Infrastructure;

namespace Uploader.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Uploader API", Version = "v1" });
            });
        }
        
        public static void ConfigureMapster(this IServiceCollection service)
        {
            TypeAdapterConfig<Transaction, TransactionResponseModel>
                .NewConfig()
                .Map(dest => dest.Payment, src => $"{src.Amount} {src.CurrencyCode}")
                .Map(dest => dest.Status, src => 
                    Constants.StatusMap.First(s => s.Key.Contains(src.Status)).Value);
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<TransactionFileParserFactory>();
        }
        
        public static void AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMediatR(typeof(UploadFileCommand).Assembly);
        }

        public static void AddDb(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetValue("ConnectionStrings:DefaultDb", string.Empty);

            services.AddDbContext<TransactionDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("Uploader.Api"));
                opt.EnableSensitiveDataLogging();
            });
        }
    }
}