using System;
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
    public class GetTransactionsByDateRangeQuery : IRequest<List<TransactionResponseModel>>
    {
        public string Start { get; set; }

        public string End { get; set; }
    }
    
    
    public class GetTransactionsByDateRangeQueryHandler : IRequestHandler<GetTransactionsByDateRangeQuery, List<TransactionResponseModel>>
    {
        private readonly TransactionDbContext _context;

        public GetTransactionsByDateRangeQueryHandler(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionResponseModel>> Handle(GetTransactionsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            DateTime start = DateTime.UtcNow;
            DateTime end = DateTime.UtcNow;

            if (DateTime.TryParse(request.Start, out var outStart))
            {
                start = outStart;
            }

            if (DateTime.TryParse(request.Start, out var outEnd))
            {
                end = outEnd;
            }

            var transactions = await _context.Transactions
                .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
                .ToListAsync(cancellationToken: cancellationToken);

            return transactions.Adapt<List<TransactionResponseModel>>();
        }
    }
}