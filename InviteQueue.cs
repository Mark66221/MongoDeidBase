using DeidentifyLibrary;
using System.Text.Json;
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
using System.Text.Json.Serialization;

namespace MongoDeidBase {

    public class InviteQueue {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_InviteQueue(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string firstName = "";
            string lastName = "";
            string email = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine firstName value
                    if (obj.firstName == null) {
                        firstName = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "firstName";
                        colInfo.DataType = "FirstName";
                        firstName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.firstName = firstName;
                    }
                    //Determine lastName value
                    if (obj.lastName == null) {
                        lastName = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "lastName";
                        colInfo.DataType = "LastName";
                        lastName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.lastName = lastName;
                    }
                    //Determine Email value
                    if (obj.email == null) {
                        email = "";
                    }
                    else {
                        email = $"{firstName}.{lastName}{emailSuffix}";
                        obj.email = email;
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
        public class Action {
            public string className { get; set; }
            public int graderId { get; set; }
            public Id _id { get; set; }
            public int? verifierId { get; set; }
            public string engageUserId { get; set; }
            public int? instructorId { get; set; }
            public string id { get; set; }
            public string context { get; set; }
        }
        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }
        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public string inviteName { get; set; }
            public string subdomain { get; set; }
            public string email { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string status { get; set; }
            public string destination { get; set; }
            public List<Action> actions { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
        }
    }
}

