using Ardalis.Specification.EntityFrameworkCore;
using CustomerApp.Core.Contracts;
using CustomerApp.Infrastructure.DataAccess;
using Loyalty.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.Data
{
    // This is abstract repository if you want to utilize the Specification pattern.
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T: class, IAggregateRoot
    {
        public Repository(LoyaltyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
