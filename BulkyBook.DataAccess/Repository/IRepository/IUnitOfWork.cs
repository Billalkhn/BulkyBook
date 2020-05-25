using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
   public  interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        SP__Call SP__Call { get; }



    }
}
