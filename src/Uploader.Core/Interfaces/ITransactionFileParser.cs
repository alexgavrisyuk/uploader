using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Uploader.Domain.Entities;

namespace Uploader.Core.Interfaces
{
    public interface ITransactionFileParser
    {
        IEnumerable<Transaction> ParseAll(IFormFile file);
    }
}