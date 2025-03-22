using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyLocationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Company_Locations] ([Id], [Company], [Country_Code], [State_Province_Code],
                            [Street_Address], [City_Town], [Zip_Postal_Code])
                            VALUES (@Id, @Company, @CountryCode, @Province, @Street, @City, @PostalCode)";
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
                            command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                            command.Parameters.AddWithValue("@Province", (object)item.Province ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@Street", (object)item.Street ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@City", (object)item.City ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                            
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IList<CompanyLocationPoco> CompanyLocationPocoList = new List<CompanyLocationPoco>();
            string query = @"SELECT [Id], [Company], [Country_Code], [State_Province_Code], [Street_Address], 
                           [City_Town], [Zip_Postal_Code] 
                           FROM [dbo].[Company_Locations]";
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
                                var poco = new CompanyLocationPoco
                                {
                                    Id = reader.GetGuid(0),
                                    Company = reader.GetGuid(1),
                                    CountryCode = reader.GetString(2),
                                    Province = (reader["State_Province_Code"] ?? "").ToString(),
                                    Street = (reader["Street_Address"] ?? "").ToString(),
                                    City = (reader["City_Town"] ?? "").ToString(),
                                    PostalCode = (reader["Zip_Postal_Code"] ?? "").ToString()
                                };
                                CompanyLocationPocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyLocationPocoList;
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Company_Locations]
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

        public void Update(params CompanyLocationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query =  @" UPDATE [dbo].[Company_Locations] 
                        SET 
                            [Company] = @Company,
                            [Country_Code] = @CountryCode,
                            [State_Province_Code] = @Province,
                            [Street_Address] = @Street,
                            [City_Town] = @City,
                            [Zip_Postal_Code] = @PostalCode
                        WHERE 
                            [Id] = @Id";

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
                            command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                            command.Parameters.AddWithValue("@Province", (object)item.Province ?? DBNull.Value); // Handle nullable Province
                            command.Parameters.AddWithValue("@Street", (object)item.Street ?? DBNull.Value); // Handle nullable Street
                            command.Parameters.AddWithValue("@City", (object)item.City ?? DBNull.Value); // Handle nullable City
                            command.Parameters.AddWithValue("@PostalCode", item.PostalCode);

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
