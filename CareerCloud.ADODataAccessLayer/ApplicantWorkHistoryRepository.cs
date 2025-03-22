using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        protected readonly string _connStr = string.Empty;
        public ApplicantWorkHistoryRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"
                            INSERT INTO [dbo].[Applicant_Work_History]
                            ([Id], [Applicant], [Company_Name], [Country_Code], [Location], [Job_Title], 
                            [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_Year])
                            VALUES
                            (@Id, @Applicant, @CompanyName, @CountryCode, @Location, @JobTitle, @JobDescription,
                            @StartMonth, @StartYear, @EndMonth, @EndYear)";
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
                            command.Parameters.AddWithValue("@Applicant", item.Applicant);
                            command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                            command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                            command.Parameters.AddWithValue("@Location", item.Location);
                            command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                            command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IList<ApplicantWorkHistoryPoco> ApplicantWorkHistoryPocoList = new List<ApplicantWorkHistoryPoco>();
            string query = @"SELECT 
                                Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, 
                                Start_Month, Start_Year, End_Month, End_Year, Time_Stamp
                            FROM Applicant_Work_History";
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
                                var workHistory = new ApplicantWorkHistoryPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                                    CompanyName = reader.GetString(reader.GetOrdinal("Company_Name")),
                                    CountryCode = reader.GetString(reader.GetOrdinal("Country_Code")),
                                    Location = reader.GetString(reader.GetOrdinal("Location")),
                                    JobTitle = reader.GetString(reader.GetOrdinal("Job_Title")),
                                    JobDescription = reader.GetString(reader.GetOrdinal("Job_Description")),
                                    StartMonth = reader.GetInt16(reader.GetOrdinal("Start_Month")),
                                    StartYear = reader.GetInt32(reader.GetOrdinal("Start_Year")),
                                    EndMonth = reader.GetInt16(reader.GetOrdinal("End_Month")),
                                    EndYear = reader.GetInt32(reader.GetOrdinal("End_Year")),
                                    TimeStamp = (byte[])reader["Time_Stamp"] // Handle byte array for timestamp
                                };

                                ApplicantWorkHistoryPocoList.Add(workHistory);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ApplicantWorkHistoryPocoList;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove.");

            string query = @"DELETE FROM [dbo].[Applicant_Work_History] 
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

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to update.");

            string query = @"UPDATE [dbo].[Applicant_Work_History]
                            SET 
                                [Company_Name] = @CompanyName,
                                [Country_Code] = @CountryCode,
                                [Location] = @Location,
                                [Job_Title] = @JobTitle,
                                [Job_Description] = @JobDescription,
                                [Start_Month] = @StartMonth,
                                [Start_Year] = @StartYear,
                                [End_Month] = @EndMonth,
                                [End_Year] = @EndYear
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
                            command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                            command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                            command.Parameters.AddWithValue("@Location", item.Location);
                            command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                            command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
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
