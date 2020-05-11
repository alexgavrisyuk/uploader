using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Uploader.Core.Factories;
using Uploader.Infrastructure;

namespace Uploader.Core.Commands
{
    public class UploadFileCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }
    }
    
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, bool>
    {
        private readonly TransactionFileParserFactory _factory;
        private readonly TransactionDbContext _context;

        public UploadFileCommandHandler(TransactionFileParserFactory factory, TransactionDbContext context)
        {
            _factory = factory;
            _context = context;
        }

        public async Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var parser = _factory.Create(request.File.FileName);

            var transactions = parser.ParseAll(request.File);

            await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}