using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        protected readonly string _connStr = string.Empty;
        public SecurityLoginsLogRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Security_Logins_Log]
                                ([Id], [Login], [Source_IP], [Logon_Date], [Is_Succesful])
                                VALUES
                                (@Id, @Login, @SourceIP, @LogonDate, @IsSuccesful)";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();
                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Login", item.Login);
                            command.Parameters.AddWithValue("@SourceIP", item.SourceIP);
                            command.Parameters.AddWithValue("@LogonDate", item.LogonDate);
                            command.Parameters.AddWithValue("@IsSuccesful", item.IsSuccesful);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IList<SecurityLoginsLogPoco> SecurityLoginsLogPocoList = new List<SecurityLoginsLogPoco>();
            string query = @"SELECT [Id], [Login], [Source_IP], [Logon_Date], [Is_Succesful] 
                            FROM [dbo].[Security_Logins_Log]";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var poco = new SecurityLoginsLogPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Login = reader.GetGuid(reader.GetOrdinal("Login")),
                                    SourceIP = reader.GetString(reader.GetOrdinal("Source_IP")),
                                    LogonDate = reader.GetDateTime(reader.GetOrdinal("Logon_Date")),
                                    IsSuccesful = reader.GetBoolean(reader.GetOrdinal("Is_Succesful"))
                                };

                                SecurityLoginsLogPocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return SecurityLoginsLogPocoList;
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Security_Logins_Log]
                                WHERE [Id] = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@Id", item.Id);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting data: " + ex.Message);
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Security_Logins_Log]
                                SET 
                                    [Login] = @Login,
                                    [Source_IP] = @SourceIP,
                                    [Logon_Date] = @LogonDate,
                                    [Is_Succesful] = @IsSuccesful
                                WHERE [Id] = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Clear(); 
                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Login", item.Login);
                            command.Parameters.AddWithValue("@SourceIP", item.SourceIP);
                            command.Parameters.AddWithValue("@LogonDate", item.LogonDate);
                            command.Parameters.AddWithValue("@IsSuccesful", item.IsSuccesful);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new InvalidOperationException($"The record with Id {item.Id} was not updated because it may have been modified by another user.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating data: " + ex.Message);
            }
        }
    }
}
