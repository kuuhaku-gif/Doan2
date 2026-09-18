using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Helper
{
    public interface IDatabaseHelper
    {
        DataTable ExecuteSProcedureReturnDataTable(out string msgError, string sprocedureName, params object[] paramObjects);
        int ExecuteNonQuery(out string msgError, string sprocedureName, params object[] paramObjects);
        string ExecuteScalar(out string msgError, string sprocedureName, params object[] paramObjects);
    }

    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataTable ExecuteSProcedureReturnDataTable(out string msgError, string sprocedureName, params object[] paramObjects)
        {
            msgError = "";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sprocedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (paramObjects != null)
                        {
                            for (int i = 0; i < paramObjects.Length; i += 2)
                            {
                                string paramName = paramObjects[i].ToString();
                                object paramValue = paramObjects[i + 1] ?? DBNull.Value;
                                cmd.Parameters.AddWithValue(paramName, paramValue);
                            }
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgError = ex.Message;
            }
            return dt;
        }

        public int ExecuteNonQuery(out string msgError, string sprocedureName, params object[] paramObjects)
        {
            msgError = "";
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sprocedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (paramObjects != null)
                        {
                            for (int i = 0; i < paramObjects.Length; i += 2)
                            {
                                string paramName = paramObjects[i].ToString();
                                object paramValue = paramObjects[i + 1] ?? DBNull.Value;
                                cmd.Parameters.AddWithValue(paramName, paramValue);
                            }
                        }
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                msgError = ex.Message;
            }
            return result;
        }

        public string ExecuteScalar(out string msgError, string sprocedureName, params object[] paramObjects)
        {
            msgError = "";
            string result = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sprocedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (paramObjects != null)
                        {
                            for (int i = 0; i < paramObjects.Length; i += 2)
                            {
                                string paramName = paramObjects[i].ToString();
                                object paramValue = paramObjects[i + 1] ?? DBNull.Value;
                                cmd.Parameters.AddWithValue(paramName, paramValue);
                            }
                        }
                        var val = cmd.ExecuteScalar();
                        result = val != null ? val.ToString() : "";
                    }
                }
            }
            catch (Exception ex)
            {
                msgError = ex.Message;
            }
            return result;
        }
    }
}