using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        protected readonly string _connStr = string.Empty;
        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Applicant_Profiles] 
                            ([Id], [Login], [Current_Salary], [Current_Rate], [Currency], 
                            [Country_Code], [State_Province_Code], [Street_Address], 
                            [City_Town], [Zip_Postal_Code])
                            VALUES 
                            (@Id, @Login, @CurrentSalary, @CurrentRate, @Currency, 
                            @Country, @Province, @Street, @City, @PostalCode)";
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
                            command.Parameters.AddWithValue("@Login", item.Login);
                            command.Parameters.AddWithValue("@CurrentSalary", (object)item.CurrentSalary ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CurrentRate", (object)item.CurrentRate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Currency", (object)item.Currency ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Country", (object)item.Country ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Province", (object)item.Province ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Street", (object)item.Street ?? DBNull.Value);
                            command.Parameters.AddWithValue("@City", (object)item.City ?? DBNull.Value);
                            command.Parameters.AddWithValue("@PostalCode", (object)item.PostalCode ?? DBNull.Value);
                            
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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IList<ApplicantProfilePoco> ApplicantProfilePocoList = new List<ApplicantProfilePoco>();
            string query = @"SELECT [Id], [Login], [Current_Salary], [Current_Rate], [Currency], [Country_Code],
                   [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code]
                   FROM [dbo].[Applicant_Profiles]";

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
                                var poco = new ApplicantProfilePoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Login = reader.GetGuid(reader.GetOrdinal("Login")),
                                    CurrentSalary = reader.IsDBNull(reader.GetOrdinal("Current_Salary")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Current_Salary")),
                                    CurrentRate = reader.IsDBNull(reader.GetOrdinal("Current_Rate")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Current_Rate")),
                                    Currency = reader.IsDBNull(reader.GetOrdinal("Currency")) ? null : reader.GetString(reader.GetOrdinal("Currency")),
                                    Country = reader.IsDBNull(reader.GetOrdinal("Country_Code")) ? null : reader.GetString(reader.GetOrdinal("Country_Code")),
                                    Province = reader.IsDBNull(reader.GetOrdinal("State_Province_Code")) ? null : reader.GetString(reader.GetOrdinal("State_Province_Code")),
                                    Street = reader.IsDBNull(reader.GetOrdinal("Street_Address")) ? null : reader.GetString(reader.GetOrdinal("Street_Address")),
                                    City = reader.IsDBNull(reader.GetOrdinal("City_Town")) ? null : reader.GetString(reader.GetOrdinal("City_Town")),
                                    PostalCode = reader.IsDBNull(reader.GetOrdinal("Zip_Postal_Code")) ? null : reader.GetString(reader.GetOrdinal("Zip_Postal_Code")),

                                };
                                ApplicantProfilePocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ApplicantProfilePocoList;
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Applicant_Profiles] 
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

        public void Update(params ApplicantProfilePoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Applicant_Profiles] 
                           SET 
                                [Login] = @Login,
                                [Current_Salary] = @CurrentSalary,
                                [Current_Rate] = @CurrentRate,
                                [Currency] = @Currency,
                                [Country_Code] = @Country,
                                [State_Province_Code] = @Province,
                                [Street_Address] = @Street,
                                [City_Town] = @City,
                                [Zip_Postal_Code] = @PostalCode
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
                            command.Parameters.AddWithValue("@Login", item.Login);
                            command.Parameters.AddWithValue("@CurrentSalary", (object)item.CurrentSalary ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CurrentRate", (object)item.CurrentRate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Currency", (object)item.Currency ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Country", (object)item.Country ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Province", (object)item.Province ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Street", (object)item.Street ?? DBNull.Value);
                            command.Parameters.AddWithValue("@City", (object)item.City ?? DBNull.Value);
                            command.Parameters.AddWithValue("@PostalCode", (object)item.PostalCode ?? DBNull.Value);


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
