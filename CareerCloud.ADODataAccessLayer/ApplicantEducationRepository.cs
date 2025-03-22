using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        protected readonly string _connStr = string.Empty;
        public ApplicantEducationRepository ()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantEducationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to add.");

            string query = @"INSERT INTO [dbo].[Applicant_Educations]
                    ([Id], [Applicant], [Major], [Certificate_Diploma], [Start_Date], [Completion_Date], [Completion_Percent])
                    VALUES
                    (@Id, @Applicant, @Major, @CertificateDiploma, @StartDate, @CompletionDate, @CompletionPercent)";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    // Iterate through each item and insert it
                    foreach (var item in items)
                    {
                        // Create a command for each item to insert it
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Set up the parameters for the SQL command
                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Applicant", item.Applicant);
                            command.Parameters.AddWithValue("@Major", item.Major);
                            command.Parameters.AddWithValue("@CertificateDiploma", (object)item.CertificateDiploma ?? DBNull.Value);
                            command.Parameters.AddWithValue("@StartDate", (object)item.StartDate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CompletionDate", (object)item.CompletionDate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CompletionPercent", (object)item.CompletionPercent ?? DBNull.Value);
                            
                            // Execute the command to insert the record
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting data: " + ex.Message);
            }
        }


        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            // This will be implemented during C# Advanced course
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IList<ApplicantEducationPoco> educationList = new List<ApplicantEducationPoco>();

            // Define the SQL query to get all ApplicantEducationPoco records
            string query = @"SELECT [Id], [Applicant], [Major], [Certificate_Diploma], [Start_Date], [Completion_Date], [Completion_Percent], Time_Stamp
                            FROM [dbo].[Applicant_Educations]";
            try
            {
                // Set up the SQL connection using a connection string 
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    // Create a command object to execute the SQL query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and obtain the data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the data from the SqlDataReader and map it to the ApplicantEducationPoco object
                            while (reader.Read())
                            {
                                var poco = new ApplicantEducationPoco
                                {
                                    Id = reader.GetGuid(0),
                                    Applicant = reader.GetGuid(1),
                                    Major = reader.GetString(2),
                                    CertificateDiploma = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    StartDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                    CompletionDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                    CompletionPercent = reader.IsDBNull(6) ? (byte?)null : reader.GetByte(6),
                                    TimeStamp = (byte[])reader[7]
                                };
                                // Add the populated POCO object to the list
                                educationList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return educationList;
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            // This will be implemented during C# Advanced course
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.FirstOrDefault(where);
        }


        public void Remove(params ApplicantEducationPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove");

            string query = @"DELETE FROM [dbo].[Applicant_Educations]
                        WHERE [Id] = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        // Create a command for each item to delete it
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add the parameter for the SQL query
                            command.Parameters.AddWithValue("@Id", item.Id);

                            // Execute the command to delete the record
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


        public void Update(params ApplicantEducationPoco[] items)
        {
            string query = @"
                    UPDATE [dbo].[Applicant_Educations]
                    SET 
                        [Applicant] = @Applicant,
                        [Major] = @Major,
                        [Certificate_Diploma] = @CertificateDiploma,
                        [Start_Date] = @StartDate,
                        [Completion_Date] = @CompletionDate,
                        [Completion_Percent] = @CompletionPercent
                    WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        // Create a command for each item to update it
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            
                            // Add parameters for the SQL query
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Applicant", item.Applicant);
                            command.Parameters.AddWithValue("@Major", item.Major);
                            command.Parameters.AddWithValue("@CertificateDiploma", (object)item.CertificateDiploma ?? DBNull.Value);
                            command.Parameters.AddWithValue("@StartDate", (object)item.StartDate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CompletionDate", (object)item.CompletionDate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CompletionPercent", (object)item.CompletionPercent ?? DBNull.Value);
                      
                            // Execute the command to update the record
                            int rowsAffected = command.ExecuteNonQuery();

                            // If no rows were affected, it means the record was possibly modified by someone else
                            if (rowsAffected == 0)
                            {
                                throw new InvalidOperationException($"Record with Id {item.Id} was modified by another user.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., log the error or rethrow)
                Console.WriteLine("Error updating data: " + ex.Message);
            }
        }
    }
}
