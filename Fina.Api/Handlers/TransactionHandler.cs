using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Fina.Api.Data.Mappings;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<List<Transaction>?>> GetByPeriod(GetTransactionsByPeriodRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}