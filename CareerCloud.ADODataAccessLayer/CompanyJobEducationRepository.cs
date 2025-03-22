using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyJobEducationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobEducationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Company_Job_Educations]
                            (Id, Job, Major, Importance)
                            VALUES (@Id, @Job, @Major, @Importance)";
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
                            command.Parameters.AddWithValue("@Major", item.Major);
                            command.Parameters.AddWithValue("@Importance", item.Importance);

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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IList<CompanyJobEducationPoco> CompanyJobEducationPocoList = new List<CompanyJobEducationPoco>();
            string query = "SELECT Id, Job, Major, Importance, Time_Stamp FROM Company_Job_Educations";
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
                                var poco = new CompanyJobEducationPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Job = reader.GetGuid(reader.GetOrdinal("Job")),
                                    Major = reader.IsDBNull(reader.GetOrdinal("Major")) ? null : reader.GetString(reader.GetOrdinal("Major")),
                                    Importance = reader.GetInt16(reader.GetOrdinal("Importance")),
                                    TimeStamp = reader.IsDBNull(reader.GetOrdinal("Time_Stamp")) ? null : (byte[])reader["Time_Stamp"]
                                };

                                CompanyJobEducationPocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyJobEducationPocoList;
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");
            string query = @"DELETE FROM [dbo].[Company_Job_Educations] 
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

        public void Update(params CompanyJobEducationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Company_Job_Educations]
                            SET [Job] = @Job, [Major] = @Major, [Importance] = @Importance 
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
                            command.Parameters.AddWithValue("@Major", item.Major);
                            command.Parameters.AddWithValue("@Importance", item.Importance);

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
