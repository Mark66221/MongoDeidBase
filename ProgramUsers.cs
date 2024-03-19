using DeidentifyLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using static MongoDeidBase.Utilities;

namespace MongoDeidBase {

    public class ProgramUsers {
        string ProgramUsersFileName = "\\ProgramUsers.json";
        int nbrRowsChanged = 0;
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_ProgramUsers(string filepath, string filename) {
            //string LogConnectionString = "server=ASC-DEV-SQL42; trusted_connection=true; database=DeityResults;";
            //string connectionString = "Data Source=ASC-DEV-SQL42;Initial Catalog=DeityResults;Integrated Security=True";
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string _id = "";
            bool missing;
            int subdomId = 0;
            int userId = 0;
            string createdOn;
            string updatedOn;
            string firstName = "";
            string lastName = "";
            string email = "";
            string phone = "";
            string street1 = "";
            string street2 = "";
            string city = "";
            string province = "";
            string postal_code = "";
            string fullname = "";
            string ip = "127.0.0.1";
            try {
                // Read the JSON file
                List<ProgramUsers.Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    int InsertRow = 0;
                    //Determine UserID value   Item[0]._id.$oid
                    //_id = obj._id.$oid;

                    if (obj.createdOn == null) createdOn = "";
                    else createdOn = obj.createdOn;

                    if (obj.updatedOn == null) updatedOn = "";
                    else updatedOn = obj.updatedOn;

                    if (obj.missing == null) missing = false;
                    else missing = obj.missing;

                    subdomId = obj.subdomId;
                    userId = obj.userId;

                    if (obj.firstName == null) {
                        firstName = "";
                    }
                    else {
                        InsertRow = 1;
                        colInfo = new ColumnInformation();
                        colInfo.Name = "FirstName";
                        colInfo.DataType = "FirstName";
                        firstName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.firstName = firstName;
                    }
                    //Determine LastName value
                    if (obj.lastName == null) {
                        lastName = "";
                    }
                    else {
                        InsertRow = 1;
                        colInfo = new ColumnInformation();
                        colInfo.Name = "LastName";
                        colInfo.DataType = "LastName";
                        lastName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.lastName = lastName;
                    }
                    //Determine Email value
                    if (obj.email == null) {
                        email = "";
                    }
                    else {
                        InsertRow = 1;
                        email = $"{firstName}.{lastName}{emailSuffix}";
                        obj.email = email;
                    }

                    // Now populate the other PII columns for the userId
                    if (InsertRow == 1) {
                        InsertUserTable(userId, firstName, lastName, email, createdOn, updatedOn, missing, subdomId);
                    }
                }
                // Write modified JSON to a new file
                var x = WriteDeIdentifiedFile(filepath, filename, jsonObj);
                return (NbrRows);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return (-1);
            }
        }
        public static List<Root> DeserializeJsonFile(string inputFilePath, string inputFile) {
            // Read the contents of the input file into a string
            try {
                var json = File.ReadAllText(inputFilePath + inputFile);

                // Deserialize the JSON string into a list of objects of type Root
                var jsonData = System.Text.Json.JsonSerializer.Deserialize<List<Root>>(json);

                // Return the deserialized JSON data
                return jsonData;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Item {
            public string className { get; set; }
            public bool inProgress { get; set; }
            public object _id { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public string type { get; set; }
            public bool? complete { get; set; }
        }

        public class Level {
            public string className { get; set; }
            public List<Item> items { get; set; }
            public object _id { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public bool? complete { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public string programId { get; set; }
            public int subdomId { get; set; }
            public string role { get; set; }
            public bool missing { get; set; }
            public int userId { get; set; }
            public List<Level> levels { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string currentLevelId { get; set; }
            public bool? archived { get; set; }
        }

    }
}

