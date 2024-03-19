using DeidentifyLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;

namespace MongoDeidBase {
    public abstract class Utilities {
        string ProgramUsersFileName = "\\ProgramUsers.json";
        int nbrRowsChanged = 0;
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public static string RemoveSpecialCharacters(string str) {
            if (System.String.IsNullOrEmpty(str))
                return str;
            else
                return Regex.Replace(str, "[^@a-zA-Z0-9_\\\\$/:. ']+", "", RegexOptions.Compiled);
        }
        public static bool WriteDeIdentifiedFile(string path, string filename, dynamic jsonObj) {
            try {
                string newFilePath = path + filename.Replace(".json", "_Deidentified.json");
                var jsonFile = jsonObj.ToString();
                File.WriteAllText(newFilePath, System.Text.Json.JsonSerializer.Serialize(jsonObj));

                Console.WriteLine($"Modified JSON file {filename} has been created successfully.");
                return (true);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return (false);
            }
        }
        public static bool InsertUserTable(int userId, string firstName, string lastName, string email,
            string createdOn, string updatedOn, bool missing, int subdomId) {

            bool Debug = false;
            string _id = "";
            string phone = "";
            string street1 = "";
            string street2 = "";
            string city = "";
            string province = "";
            string postal_code = "";
            string ip = "127.0.0.1";
            ColumnInformation colInfo = new ColumnInformation();

            string connectionString = "Data Source=ASC-DEV-SQL42;Initial Catalog=DeityResults;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var randomData = new RandomData();

            try {
                if (Debug) {
                    Console.WriteLine("userId " + userId);
                    Console.WriteLine("firstName " + firstName);
                    Console.WriteLine("lastName " + lastName);
                    Console.WriteLine("email " + email);
                    Console.WriteLine("createdOn " + createdOn);
                    Console.WriteLine("updatedOn " + updatedOn);
                    Console.WriteLine("missing " + missing);
                    Console.WriteLine("subdomId " + subdomId);
                }
                //Determine Phone value
                colInfo = new ColumnInformation();
                colInfo.Name = "Phone";
                colInfo.DataType = "PhoneNumber";
                phone = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                if (Debug) Console.WriteLine("Phone " + phone);

                //Determine Street1 value
                colInfo = new ColumnInformation();
                colInfo.Name = "Street1";
                colInfo.DataType = "StreetAddress";
                street1 = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                if (Debug) Console.WriteLine("Street1 " + street1);

                //Determine Street2 value
                colInfo = new ColumnInformation();
                colInfo.Name = "Street2";
                colInfo.DataType = "PlainText";
                colInfo.Value = $"De-identified  {DateTime.Today}";
                street2 = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                if (Debug) Console.WriteLine("Street2 " + street2);

                //Determine City value
                colInfo = new ColumnInformation();
                colInfo.Name = "city";
                colInfo.DataType = "City";
                city = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                if (Debug) Console.WriteLine("Province " + province);

                //Determine Province value
                colInfo = new ColumnInformation();
                colInfo.Name = "Province";
                colInfo.DataType = "State";
                province = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                if (Debug) Console.WriteLine("Province " + province);

                //Determine Postal_Code value
                colInfo = new ColumnInformation();
                colInfo.Name = "Postal_Code";
                colInfo.DataType = "Zip";
                postal_code = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                if (Debug) Console.WriteLine("Postal_Code " + postal_code);

                string sqlInsertQuery = "INSERT INTO dbo.MongoProgramUsers ([userId],[firstname],[lastname],[street1],[street2],[city],[province],[postal_code],[phone],[email]," +
                    "[fullname],[ip],[createdOn],[updatedOn],[_id],[missing],[subdomId])" +
                " VALUES (@userId,@firstname,@lastname,@street1,@street2,@city,@province,@postal_code,@phone,@email," +
                "@fullname,@ip,@createdOn,@updatedOn,@_id,@missing,@subdomId)";
                SqlCommand command = new SqlCommand(sqlInsertQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@firstname", firstName);
                command.Parameters.AddWithValue("@lastname", lastName);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@street1", street1);
                command.Parameters.AddWithValue("@street2", street2);
                command.Parameters.AddWithValue("@city", city);
                command.Parameters.AddWithValue("@province", province);
                command.Parameters.AddWithValue("@postal_code", postal_code);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@fullname", firstName + " " + lastName);
                command.Parameters.AddWithValue("@ip", "127.0.0.1");
                command.Parameters.AddWithValue("@createdOn", createdOn);
                command.Parameters.AddWithValue("@updatedOn", updatedOn);
                command.Parameters.AddWithValue("@_id", _id);
                command.Parameters.AddWithValue("@missing", missing);
                command.Parameters.AddWithValue("@subdomId", subdomId);
                int rowsAffected = command.ExecuteNonQuery();
                if (Debug) Console.WriteLine($"Rows Affected: {rowsAffected}");

                return (true);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return (false);
            }
        }
    }
}

