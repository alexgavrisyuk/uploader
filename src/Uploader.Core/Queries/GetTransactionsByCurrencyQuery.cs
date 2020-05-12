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
    public class GetTransactionsByCurrencyQuery : IRequest<List<TransactionResponseModel>>
    {
        public string Currency { get; set; }
    }
    
    
    public class GetTransactionsByCurrencyQueryHandler : IRequestHandler<GetTransactionsByCurrencyQuery, List<TransactionResponseModel>>
    {
        private readonly TransactionDbContext _context;

        public GetTransactionsByCurrencyQueryHandler(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionResponseModel>> Handle(GetTransactionsByCurrencyQuery request, CancellationToken cancellationToken)
        {
            var transactions = _context.Transactions
                .AsEnumerable()
                .Where(t =>
                    t.CurrencyCode.ToLowerInvariant().Contains(request.Currency.ToLowerInvariant()))
                .ToList();

            return transactions.Adapt<List<TransactionResponseModel>>();
        }
    }
}