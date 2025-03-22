using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Company_Profiles] 
                        ([Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo]) 
                        VALUES 
                        (@Id, @RegistrationDate, @CompanyWebsite, @ContactPhone, @ContactName, @CompanyLogo)";

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
                            command.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                            command.Parameters.AddWithValue("@CompanyWebsite", (object)item.CompanyWebsite ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                            command.Parameters.AddWithValue("@ContactName", (object)item.ContactName ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@CompanyLogo", (object)item.CompanyLogo ?? DBNull.Value);
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IList<CompanyProfilePoco> CompanyProfilePocoList = new List<CompanyProfilePoco>();
            string query = @"SELECT 
                    [Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo]
                    FROM [dbo].[Company_Profiles]";
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
                                var companyProfile = new CompanyProfilePoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    RegistrationDate = reader.GetDateTime(reader.GetOrdinal("Registration_Date")),
                                    CompanyWebsite = reader.IsDBNull(reader.GetOrdinal("Company_Website")) ? null : reader.GetString(reader.GetOrdinal("Company_Website")),
                                    ContactPhone = reader.GetString(reader.GetOrdinal("Contact_Phone")),
                                    ContactName = reader.IsDBNull(reader.GetOrdinal("Contact_Name")) ? null : reader.GetString(reader.GetOrdinal("Contact_Name")),
                                    CompanyLogo = reader.IsDBNull(reader.GetOrdinal("Company_Logo")) ? null : (byte[])reader["Company_Logo"]
                                };
                                CompanyProfilePocoList.Add(companyProfile);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyProfilePocoList;
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Company_Profiles]
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

        public void Update(params CompanyProfilePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Company_Profiles] 
                        SET 
                            [Registration_Date] = @RegistrationDate,
                            [Company_Website] = @CompanyWebsite,
                            [Contact_Phone] = @ContactPhone,
                            [Contact_Name] = @ContactName,
                            [Company_Logo] = @CompanyLogo
                        WHERE Id = @Id";

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
                            command.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                            command.Parameters.AddWithValue("@CompanyWebsite", (object)item.CompanyWebsite ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                            command.Parameters.AddWithValue("@ContactName", (object)item.ContactName ?? DBNull.Value); 
                            command.Parameters.AddWithValue("@CompanyLogo", (object)item.CompanyLogo ?? DBNull.Value); 

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
