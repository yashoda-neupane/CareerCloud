using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        protected readonly string _connStr = string.Empty;
        public ApplicantSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Applicant_Skills]
                            ([Id], [Applicant], [Skill], [Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year])
                            VALUES
                            (@Id, @Applicant, @Skill, @SkillLevel, @StartMonth, @StartYear, @EndMonth, @EndYear)";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();
                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Clear(); // Clear previous parameters
                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Applicant", item.Applicant);
                            command.Parameters.AddWithValue("@Skill", item.Skill);
                            command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                            command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                            command.Parameters.AddWithValue("@StartYear", item.StartYear);
                            command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                            command.Parameters.AddWithValue("@EndYear", item.EndYear);
                            
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IList<ApplicantSkillPoco> ApplicantSkillPocoList = new List<ApplicantSkillPoco>();
            string query = @"SELECT 
                                [Id], [Applicant], [Skill], [Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year]
                            FROM [dbo].[Applicant_Skills]";

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
                                var result = new ApplicantSkillPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                                    Skill = reader.GetString(reader.GetOrdinal("Skill")),
                                    SkillLevel = reader.GetString(reader.GetOrdinal("Skill_Level")),
                                    StartMonth = reader.GetByte(reader.GetOrdinal("Start_Month")),
                                    StartYear = reader.GetInt32(reader.GetOrdinal("Start_Year")),
                                    EndMonth = reader.GetByte(reader.GetOrdinal("End_Month")),
                                    EndYear = reader.GetInt32(reader.GetOrdinal("End_Year"))
                                };
                                ApplicantSkillPocoList.Add(result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ApplicantSkillPocoList;
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Applicant_Skills] 
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

        public void Update(params ApplicantSkillPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Applicant_Skills]
                            SET 
                                [Skill] = @Skill,
                                [Skill_Level] = @SkillLevel,
                                [Start_Month] = @StartMonth,
                                [Start_Year] = @StartYear,
                                [End_Month] = @EndMonth,
                                [End_Year] = @EndYear
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
                            command.Parameters.AddWithValue("@Skill", item.Skill);
                            command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                            command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                            command.Parameters.AddWithValue("@StartYear", item.StartYear);
                            command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                            command.Parameters.AddWithValue("@EndYear", item.EndYear);
                           
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
