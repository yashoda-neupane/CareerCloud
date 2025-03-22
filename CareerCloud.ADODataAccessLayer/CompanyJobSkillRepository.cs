using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        protected readonly string _connStr = string.Empty;
        public CompanyJobSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Company_Job_Skills]
                            ([Id], [Job], [Skill], [Skill_Level], [Importance]) 
                           VALUES(@Id, @Job, @Skill, @Skill_Level, @Importance)";
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
                            command.Parameters.AddWithValue("@Skill", item.Skill);
                            command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IList<CompanyJobSkillPoco> CompanyJobSkillPocoList = new List<CompanyJobSkillPoco>();
            string query = @"SELECT [Id], [Job], [Skill], [Skill_Level], [Importance], [Time_Stamp]
                            FROM Company_Job_Skills";
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
                                var poco = new CompanyJobSkillPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Job = reader.GetGuid(reader.GetOrdinal("Job")),
                                    Skill = reader.GetString(reader.GetOrdinal("Skill")),
                                    SkillLevel = reader.GetString(reader.GetOrdinal("Skill_Level")),
                                    Importance = reader.GetInt32(reader.GetOrdinal("Importance")),
                                    TimeStamp = reader["Time_Stamp"] as byte[] // Handle timestamp (rowversion) column
                                };
                                CompanyJobSkillPocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CompanyJobSkillPocoList;
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Company_Job_Skills]
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

        public void Update(params CompanyJobSkillPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Company_Job_Skills] 
                            SET 
                                [Job] = @Job, 
                                [Skill] = @Skill,
                                [Skill_Level] = @Skill_Level, 
                                [Importance] = @Importance
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
                            command.Parameters.AddWithValue("@Job", item.Job);
                            command.Parameters.AddWithValue("@Skill", item.Skill);
                            command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
