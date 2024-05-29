using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using CleanArc.Application.Contracts.Persistence;

namespace CleanArc.Infrastructure.Persistence.Repositories.Common;

    public class DapperORM : IDapperORM
{
        public IConfiguration _configuration;
        ////private static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TTRCon"].ConnectionString;
        private string ConnectionString= @"Data Source=DESKTOP-B9LPGU3;Initial Catalog=LLP;Integrated Security=true;Encrypt=False; MultipleActiveResultSets=true;";
        //private static string ConnectionString = "Data Source=66.219.22.206;Initial Catalog=imgmtor1;Persist Security Info=True;User ID=imgmtor1;Password=K%8vF7w5mZW5k#";
        //static DapperORM()
        //{
        //    ConnectionString= _configuration.GetConnectionString("DefaultConnection");
        //}
        public async Task ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
               await conn.ExecuteAsync(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        //DapperORM.ReturnList<RegisterModel><=IEnumerable<RegisterModel>;
        public async Task<IEnumerable<T>> ReturnList<T>(string procedureName, DynamicParameters param=null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
              return result;
            }
        }

        public async Task<(List<T1>,List<T2>)> ReturnMultipleList<T1,T2>(string procedureName, DynamicParameters param=null)
        {
            List<T1> result1 = null;
            List<T2> result2 = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.QueryMultipleAsync(procedureName, param, commandType: CommandType.StoredProcedure);
                result1 = result.Read<T1>().ToList();
                result2 = result.Read<T2>().ToList();
            }
            return (result1, result2);
        }
        public async Task<(List<T1>,List<T2>,List<T3>)> ReturnMultipleList<T1,T2,T3>(string procedureName, DynamicParameters param=null)
        {
            List<T1> result1 = null;
            List<T2> result2 = null;
            List<T3> result3 = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result =await conn.QueryMultipleAsync(procedureName, param, commandType: CommandType.StoredProcedure);
                result1 = result.Read<T1>().ToList();
                result2 = result.Read<T2>().ToList();
                result3 = result.Read<T3>().ToList();
            }
            return (result1, result2,result3);
        }
        //DapperORM.ExecuteReturnScalar<int>(_,_);
        public async Task<T> ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result =await conn.ExecuteScalarAsync(procedureName, param, commandType: CommandType.StoredProcedure);
              return (T)Convert.ChangeType(result,typeof(T));
            }
        }
        public async Task<IEnumerable<T>> ReturnPK<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);

            }
        }
    } 
