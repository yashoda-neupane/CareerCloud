using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"
                            INSERT INTO [dbo].[Company_Descriptions] ([Id], [Company], [LanguageID], [Company_Name], [Company_Description])
                            VALUES (@Id, @Company, @LanguageID, @CompanyName, @CompanyDescription)";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();
                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Company", item.Company);
                            command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                            command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                            command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IList<CompanyDescriptionPoco> CompanyDescriptionPocoList = new List<CompanyDescriptionPoco>();
            string query = @"SELECT [Id], [Company], [LanguageID], [Company_Name], [Company_Description], [Time_Stamp]
                            FROM [dbo].[Company_Descriptions]";

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
                                var companyDescription = new CompanyDescriptionPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Company = reader.GetGuid(reader.GetOrdinal("Company")),
                                    LanguageId = reader.GetString(reader.GetOrdinal("LanguageID")),
                                    CompanyName = reader.GetString(reader.GetOrdinal("Company_Name")),
                                    CompanyDescription = reader.GetString(reader.GetOrdinal("Company_Description")) 
                                };
                                CompanyDescriptionPocoList.Add(companyDescription);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyDescriptionPocoList;
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Company_Descriptions] WHERE [Id] = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
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

        public void Update(params CompanyDescriptionPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Company_Descriptions]
                            SET 
                                [Company] = @Company,
                                [LanguageID] = @LanguageID,
                                [Company_Name] = @CompanyName,
                                [Company_Description] = @CompanyDescription
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
                            command.Parameters.AddWithValue("@Company", item.Company);
                            command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                            command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                            command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription);

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
