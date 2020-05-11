using CsvHelper.Configuration;
using Uploader.Domain.Entities;

namespace Uploader.Core.CsvMappers
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            
        }
    }
}