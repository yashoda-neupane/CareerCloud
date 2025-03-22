using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        protected readonly string _connStr = string.Empty;
        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            string query = @"INSERT INTO [dbo].[Security_Logins] ([Id], [Login], [Password], [Created_Date],
                                [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked], 
                            [Is_Inactive], [Email_Address], [Phone_Number], [Full_Name], [Force_Change_Password], [Prefferred_Language])
                            VALUES (@Id, @Login, @Password, @Created, @PasswordUpdate, @AgreementAccepted, @IsLocked,
                            @IsInactive, @Email, @PhoneNumber, @FullName, @ForceChangePassword, @PrefferredLanguage)";
                                                    
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
                            command.Parameters.AddWithValue("@Password", item.Password);
                            command.Parameters.AddWithValue("@Created", item.Created);
                            command.Parameters.AddWithValue("@PasswordUpdate", (object)item.PasswordUpdate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@AgreementAccepted", (object)item.AgreementAccepted ?? DBNull.Value );
                            command.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                            command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                            command.Parameters.AddWithValue("@Email", item.EmailAddress);
                            command.Parameters.AddWithValue("@PhoneNumber", (object)item.PhoneNumber ?? DBNull.Value);
                            command.Parameters.AddWithValue("@FullName", (object)item.FullName ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                            command.Parameters.AddWithValue("@PrefferredLanguage", (object)item.PrefferredLanguage ?? DBNull.Value);

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
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IList<SecurityLoginPoco> SecurityLoginPocoList = new List<SecurityLoginPoco>();

            string query = @"SELECT [Id], [Login], [Password], [Created_Date], [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked], 
                            [Is_Inactive], [Email_Address], [Phone_Number], [Full_Name], [Force_Change_Password], [Prefferred_Language]
                           FROM [dbo].[Security_Logins]";

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
                                var poco = new SecurityLoginPoco
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Login = reader.GetString(reader.GetOrdinal("Login")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Created = reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                                    PasswordUpdate = reader.IsDBNull(reader.GetOrdinal("Password_Update_Date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Password_Update_Date")),
                                    AgreementAccepted = reader.IsDBNull(reader.GetOrdinal("Agreement_Accepted_Date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Agreement_Accepted_Date")),
                                    IsLocked = reader.GetBoolean(reader.GetOrdinal("Is_Locked")),
                                    IsInactive = reader.GetBoolean(reader.GetOrdinal("Is_Inactive")),
                                    EmailAddress = reader.GetString(reader.GetOrdinal("Email_Address")),
                                    PhoneNumber = reader.IsDBNull(reader.GetOrdinal("Phone_Number")) ? null : reader.GetString(reader.GetOrdinal("Phone_Number")),
                                    FullName = reader.IsDBNull(reader.GetOrdinal("Full_Name")) ? null : reader.GetString(reader.GetOrdinal("Full_Name")),
                                    ForceChangePassword = reader.GetBoolean(reader.GetOrdinal("Force_Change_Password")),
                                    PrefferredLanguage = reader.IsDBNull(reader.GetOrdinal("Prefferred_Language")) ? null : reader.GetString(reader.GetOrdinal("Prefferred_Language"))
                                };

                                SecurityLoginPocoList.Add(poco);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return SecurityLoginPocoList;
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();

            return pocos.FirstOrDefault(where);
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("No items to remove");

            string query = @"DELETE FROM [dbo].[Security_Logins] 
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

        public void Update(params SecurityLoginPoco[] items)
        {
            string query = @"UPDATE [dbo].[Security_Logins]
                           SET 
                              [Login] = @Login,
                              [Password] = @Password,
                              [Created_Date] = @Created,
                              [Password_Update_Date] = @PasswordUpdate,
                              [Agreement_Accepted_Date] = @AgreementAccepted,
                              [Is_Locked] = @IsLocked,
                              [Is_Inactive] = @IsInactive,
                              [Email_Address] = @Email,
                              [Phone_Number] = @PhoneNumber,
                              [Full_Name] = @FullName,
                              [Force_Change_Password] = @ForceChangePassword,
                              [Prefferred_Language] = @PrefferredLanguage
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
                            command.Parameters.AddWithValue("@Password", item.Password);
                            command.Parameters.AddWithValue("@Created", item.Created);
                            command.Parameters.AddWithValue("@PasswordUpdate", (object)item.PasswordUpdate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@AgreementAccepted", (object)item.AgreementAccepted ?? DBNull.Value);
                            command.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                            command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                            command.Parameters.AddWithValue("@Email", item.EmailAddress);
                            command.Parameters.AddWithValue("@PhoneNumber", (object)item.PhoneNumber ?? DBNull.Value);
                            command.Parameters.AddWithValue("@FullName", (object)item.FullName ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                            command.Parameters.AddWithValue("@PrefferredLanguage", (object)item.PrefferredLanguage ?? DBNull.Value);

                            int rowsAffected = command.ExecuteNonQuery();

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
                Console.WriteLine("Error updating data: " + ex.Message);
            }
        }
    }
}
