using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uploader.Infrastructure;

namespace Uploader.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
        }

        public static void AddDb(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetValue("ConnectionStrings:DefaultConnection", string.Empty);

            services.AddDbContext<TransactionDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.EnableSensitiveDataLogging();
            });
        }
    }
}