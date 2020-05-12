using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Uploader.Core.ResponseModels;
using Uploader.Domain.Entities;
using Uploader.Infrastructure;

namespace Uploader.Core.Queries
{
    public class GetTransactionsByStatusQuery : IRequest<List<TransactionResponseModel>>
    {
        public string Status { get; set; }
    }
    
    
    public class GetTransactionsByStatusQueryHandler : IRequestHandler<GetTransactionsByStatusQuery, List<TransactionResponseModel>>
    {
        private readonly TransactionDbContext _context;

        public GetTransactionsByStatusQueryHandler(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionResponseModel>> Handle(GetTransactionsByStatusQuery request, CancellationToken cancellationToken)
        {
            var transactions = _context.Transactions
                .AsEnumerable()
                .Where(t => t.Status.ToLowerInvariant().Contains(request.Status.ToLowerInvariant()))
                .ToList();

            return transactions.Adapt<List<TransactionResponseModel>>();
        }
    }
}