using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using pruebaNetOracle.Oracle;
namespace pruebaNetOracle.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        IConfiguration configuration;
        public EmployeeRepository(IConfiguration _configuration){
            configuration=_configuration;
        }
        public object GetEmployeeDetails(int empId){
            object result=null;
            try{
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("EMP_ID", OracleDbType.Int32, ParameterDirection.Input,empId);
                dyParam.Add("EMP_DETAIL_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn= this.GetConnection();
                if(conn.State == ConnectionState.Closed){
                    conn.Open();
                }

                if(conn.State == ConnectionState.Open){
                    var query = "USP_GETEMPLOYEEDETAILS";
                    result=SqlMapper.Query(conn,query,param: dyParam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public object GetEmpleados(string name) {
            object result = null;
            try {
                //var dyParam = new OracleDynamicParameters();
                //dyParam.Add("EMPCURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if(conn.State == ConnectionState.Closed) {
                    conn.Open();
                }

                if(conn.State == ConnectionState.Open) {
                    var query = String.Format("EMPLEADOS where name='{0}'",name);
                    result = SqlMapper.Query(conn,query, commandType:CommandType.TableDirect);
                }
            }
            catch (Exception ex){
                throw ex;
            }

            return result;
        }

        public object GetEmployeeList() {
            object result = null;
            try {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("EMPCURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if(conn.State == ConnectionState.Closed) {
                    conn.Open();
                }

                if(conn.State == ConnectionState.Open) {
                    var query = "USP_GETEMPLOYEES";

                    result = SqlMapper.Query(conn,query,param: dyParam, commandType:CommandType.StoredProcedure);
                }
            }
            catch (Exception ex){
                throw ex;
            }

            return result;
        }

        public IDbConnection GetConnection() {
            var connectionString = configuration.GetSection("ConnectionStrings")
            .GetSection("EmployeeConnection").Value;
            var conn = new OracleConnection(connectionString);   
            return conn;
        }
    }
}