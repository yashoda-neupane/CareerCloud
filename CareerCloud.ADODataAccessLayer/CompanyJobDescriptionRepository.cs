using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyJobDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Company_Jobs_Descriptions] ([Id], [Job], [Job_Name], [Job_Descriptions])
                                                     VALUES (@Id, @Job, @JobName, @JobDescriptions)";

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
                            command.Parameters.AddWithValue("@Job", item.Job);
                            command.Parameters.AddWithValue("@JobName", item.JobName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions ?? (object)DBNull.Value);
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IList<CompanyJobDescriptionPoco> CompanyJobDescriptionPocoList = new List<CompanyJobDescriptionPoco>();
            string query = @"SELECT [Id], [Job], [Job_Name], [Job_Descriptions] FROM [dbo].[Company_Jobs_Descriptions]";
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
                                var poco = new CompanyJobDescriptionPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Job = reader.GetGuid(reader.GetOrdinal("Job")),
                                    JobName = reader.IsDBNull(reader.GetOrdinal("Job_Name")) ? null : reader.GetString(reader.GetOrdinal("Job_Name")),
                                    JobDescriptions = reader.IsDBNull(reader.GetOrdinal("Job_Descriptions")) ? null : reader.GetString(reader.GetOrdinal("Job_Descriptions"))
                                };

                                CompanyJobDescriptionPocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyJobDescriptionPocoList;
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");
            string query = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
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

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                SET 
                                    [Job] = @Job,
                                    [Job_Name] = @JobName,
                                    [Job_Descriptions] = @JobDescriptions
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
                            command.Parameters.AddWithValue("@Job", item.Job);
                            command.Parameters.AddWithValue("@JobName", item.JobName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions ?? (object)DBNull.Value);
                            
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
