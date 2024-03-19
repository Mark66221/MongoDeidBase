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

    public class Waitlist {
        //string ProgramUsersFileName = "\\Waitlist.json";
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_Waitlist(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string firstName = "";
            string lastName = "";
            string email = "";
            string phone = "";
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
                    //Determine phone value
                    if (obj.phone == null) {
                        phone = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "phone";
                        colInfo.DataType = "PhoneNumber";
                        phone = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.phone = phone;
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
        public class Context {
            public string className { get; set; }
            public int nodeId { get; set; }
            public string context { get; set; }
        }
        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }
        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public Context context { get; set; }
            public int userId { get; set; }
            public string status { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
        }
    }
}

