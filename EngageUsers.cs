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

    public class EngageUsers {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_EngageUsers(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string firstname = "";
            string lastname = "";
            string email = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine firstname value
                    if (obj.firstname == null) {
                        firstname = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "firstname";
                        colInfo.DataType = "FirstName";
                        firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.firstname = firstname;
                    }
                    //Determine lastName value
                    if (obj.lastname == null) {
                        lastname = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "lastname";
                        colInfo.DataType = "LastName";
                        lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.lastname = lastname;
                    }
                    //Determine phone value
                     //Determine Email value
                    if (obj.email == null) {
                        email = "";
                    }
                    else {
                        email = $"{firstname}.{lastname}{emailSuffix}";
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
        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public int subdomId { get; set; }
            public int userId { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string role { get; set; }
            public string status { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public bool notifications { get; set; }
            public List<int> clubIds { get; set; }
        }


    }
}

