using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uploader.Core.Commands;
using Uploader.Core.Factories;
using Uploader.Infrastructure;

namespace Uploader.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
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