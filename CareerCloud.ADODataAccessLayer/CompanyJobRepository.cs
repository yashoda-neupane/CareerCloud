using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyJobRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Company_Jobs] 
                            ([Id], [Company], [Profile_Created], [Is_Inactive], [Is_Company_Hidden])
                            VALUES (@Id, @Company, @ProfileCreated, @IsInactive, @IsCompanyHidden)";
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
                            command.Parameters.AddWithValue("@ProfileCreated", item.ProfileCreated);
                            command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                            command.Parameters.AddWithValue("@IsCompanyHidden", item.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IList<CompanyJobPoco> CompanyJobPocoList = new List<CompanyJobPoco>();
            string query = "SELECT Id, Company, Profile_Created, Is_Inactive, Is_Company_Hidden, Time_Stamp FROM Company_Jobs";
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
                                var companyJob = new CompanyJobPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Company = reader.GetGuid(reader.GetOrdinal("Company")),
                                    ProfileCreated = reader.GetDateTime(reader.GetOrdinal("Profile_Created")),
                                    IsInactive = reader.GetBoolean(reader.GetOrdinal("Is_Inactive")),
                                    IsCompanyHidden = reader.GetBoolean(reader.GetOrdinal("Is_Company_Hidden")),
                                    TimeStamp = reader.IsDBNull(reader.GetOrdinal("Time_Stamp")) ? null : (byte[])reader["Time_Stamp"]

                                };
                                CompanyJobPocoList.Add(companyJob);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyJobPocoList;
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Company_Jobs]
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

        public void Update(params CompanyJobPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Company_Jobs]
                            SET 
                                [Company] = @Company, 
                                [Profile_Created] = @ProfileCreated, 
                                [Is_Inactive] = @IsInactive, 
                                [Is_Company_Hidden] = @IsCompanyHidden
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
                            command.Parameters.AddWithValue("@ProfileCreated", item.ProfileCreated);
                            command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                            command.Parameters.AddWithValue("@IsCompanyHidden", item.IsCompanyHidden);
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
