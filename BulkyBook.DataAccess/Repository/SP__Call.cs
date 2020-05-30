﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class SP__Call : Isp_call
    {

        private readonly ApplicationDbContext _db;
        private static string ConnectionString = " ";
        public SP__Call(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;

        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                sqlcon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                var result = SqlMapper.QueryMultiple(sqlcon, procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<T1>().ToList();
                var item2 = result.Read<T2>().ToList();
                if (item1 != null && item2 == null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
                }
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());

            }

        }



        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                var value =  sqlcon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(value.FirstOrDefault(), typeof(T));
            }
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                return (T)Convert.ChangeType(sqlcon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure),typeof(T));
            }
        }
    }
}