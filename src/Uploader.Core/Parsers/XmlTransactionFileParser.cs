using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Uploader.Core.Interfaces;
using Uploader.Domain.Entities;

namespace Uploader.Core.Parsers
{
    public class XmlTransactionFileParser : ITransactionFileParser
    {
        public IEnumerable<Transaction> ParseAll(IFormFile file)
        {
            return new List<Transaction>();
        }
    }
}