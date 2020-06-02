using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface Isp_call : IDisposable
    {
        //StroredProcedure Calls
        //using Dapper for passing all parameters...
        //Bilal Khan
        T Single<T>(string procedureName, DynamicParameters param = null);//Single Value return (like count , int ot bool value)
        void Execute(string procedureName, DynamicParameters param = null);//create,remove,updating for exuting into the database

        T OneRecord<T>(string procedureName, DynamicParameters param = null);//get complete row one record of any item 

        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);// Get allthe rows or data 

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);//get two table
            
     }
}
