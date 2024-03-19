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

    public class EngageChatApiCalls {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_EngageChatApiCalls(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string firstName = "";
            string lastName = "";
            string email = "";
            string notes = "";
            string homePhone = "";
            string mobile = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine firstName value
                    if (obj.request.firstName == null) {
                        firstName = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "firstName";
                        colInfo.DataType = "FirstName";
                        firstName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.request.firstName = firstName;
                    }
                    //Determine lastName value
                    if (obj.request.lastName == null) {
                        lastName = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "lastName";
                        colInfo.DataType = "LastName";
                        lastName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.request.lastName = lastName;
                    }

                    //Determine email value
                    if (obj.request.email == null) {
                        email = "";
                    }
                    else {
                        if (obj.request.firstName == null || obj.request.lastName == null) {
                            colInfo = new ColumnInformation();
                            colInfo.Name = "email";
                            colInfo.DataType = "Email";
                            email = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                            obj.request.email = email;
                        }
                        else {
                            email = $"{firstName}.{lastName}{emailSuffix}";
                            obj.request.email = email;
                        }
                    }
                    //Determine homePhone value
                    if (obj.request.homePhone == null) {
                        homePhone = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "homePhone";
                        colInfo.DataType = "PhoneNumber";
                        homePhone = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.request.homePhone = homePhone;
                    }
                    //Determine mobile value
                    if (obj.request.mobile == null) {
                        mobile = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "mobile";
                        colInfo.DataType = "PhoneNumber";
                        mobile = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.request.mobile = mobile;
                    }
                    //Determine notes value
                    if (obj.request.notes == null) {
                        notes = "";
                    }
                    else {
                        notes = $"De-identified {DateTime.Now}";
                        obj.request.notes = notes;
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

        public class Request {
            public string email { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string homePhone { get; set; }
            public string source { get; set; }
            public string notes { get; set; }
            public string userId { get; set; }
            public string UUID { get; set; }
            public string mobile { get; set; }
            public string leadSourceDetails { get; set; }
        }

        public class Response {
            public object status { get; set; }
            public List<Response> responses { get; set; }
        }

        public class Response2 {
            public string message { get; set; }
            public string value { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public string contextId { get; set; }
            public Request request { get; set; }
            public Response response { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
        }

    }
}

