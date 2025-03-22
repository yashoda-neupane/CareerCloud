using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        protected readonly string _connStr = string.Empty;
        public SystemLanguageCodeRepository()
        {

            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SystemLanguageCodePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[System_Language_Codes]
                                ([LanguageId],[Name],[Native_Name])
                                 VALUES (@LanguageId, @Name, @NativeName)";
                    
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();
                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LanguageId", item.LanguageID);
                            command.Parameters.AddWithValue("@Name", item.Name);
                            command.Parameters.AddWithValue("@NativeName", item.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IList<SystemLanguageCodePoco> systemLanguageCodeList = new List<SystemLanguageCodePoco>();
            string query = @"SELECT 
                                   [LanguageId], [Name], [Native_Name]
                           FROM [dbo].[System_Language_Codes]";
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
                                var poco = new SystemLanguageCodePoco
                                {
                                    LanguageID = reader.GetString(reader.GetOrdinal("LanguageId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    NativeName = reader.GetString(reader.GetOrdinal("Native_Name"))
                                };

                                systemLanguageCodeList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return systemLanguageCodeList;
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            // This will be implemented during C# Advanced course
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[System_Language_Codes] 
                            WHERE [LanguageId] = @LanguageId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LanguageId", item.LanguageID);

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

        public void Update(params SystemLanguageCodePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[System_Language_Codes]
                           SET 
                              [Name] = @Name, [Native_Name] = @NativeName 
                              WHERE [LanguageId] = @LanguageId";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LanguageId", item.LanguageID);
                            command.Parameters.AddWithValue("@Name", item.Name);
                            command.Parameters.AddWithValue("@NativeName", item.NativeName);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                throw new InvalidOperationException($"Record with Id {item.LanguageID} was modified by another user.");
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
