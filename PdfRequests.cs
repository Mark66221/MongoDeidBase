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

    public class PdfRequests {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_PdfRequests(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string email = "";
            string firstname = "";
            string lastname = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine firstname value
                    if (obj.variables.firstname == null) {
                        firstname = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "firstname";
                        colInfo.DataType = "FirstName";
                        firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.variables.firstname = firstname;
                    }
                    //Determine lastname value
                    if (obj.variables.lastname == null) {
                        lastname = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "lastname";
                        colInfo.DataType = "FirstName";
                        lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.variables.lastname = lastname;
                    }

                    //Determine email value
                    if (obj.variables.email == null) {
                        email = "";
                    }
                    else {
                        email = $"{firstname}.{lastname}{emailSuffix}";
                        obj.variables.email = email;
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
            public int badgeId { get; set; }
            public string sortOrder { get; set; }
            public int reportId { get; set; }
            public int userId { get; set; }
            public Variables variables { get; set; }
            public string createdOn { get; set; }
            public string status { get; set; }
            public int printerId { get; set; }
        }

        public class Variables {
            public string id { get; set; }
            public string barcode { get; set; }
            public string conference_nid { get; set; }
            public string created_on { get; set; }
            public string category { get; set; }
            public string ref_type { get; set; }
            public string ref_id { get; set; }
            public string sort_by { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public string terms { get; set; }
            public string waiver { get; set; }
            public string waiver_date { get; set; }
            public string print_count { get; set; }
            public string company { get; set; }
        }


    }
}

