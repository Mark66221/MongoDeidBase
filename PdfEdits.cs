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

    public class PdfEdits {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_PdfEdits(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string badge_firstname = "";
            string badge_lastname = "";
            string phone = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine badge_firstname value
                    if (obj.sampleData.badge_firstname == null) {
                        badge_firstname = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "badge_firstname";
                        colInfo.DataType = "FirstName";
                        badge_firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.sampleData.badge_firstname = badge_firstname;
                    }

                    //Determine badge_lastname value
                    if (obj.sampleData.badge_lastname == null) {
                        badge_lastname = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "badge_lastname";
                        colInfo.DataType = "LastName";
                        badge_lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.sampleData.badge_firstname = badge_lastname;
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
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class CurrentRevisionId {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }
        public class SampleData {
            [JsonPropertyName("$oid")]
            public string uid { get; set; }
            public string barcode { get; set; }
            public string badge_category { get; set; }
            public string badge_group { get; set; }
            public string category { get; set; }
            public string badge_firstname { get; set; }
            public string badge_lastname { get; set; }
            public string badge_company { get; set; }
            public string badge_reprint { get; set; }
            public int print_count { get; set; }
            public string user_cityst { get; set; }
            public string user_country { get; set; }
            public string country_code { get; set; }
            public int conference_nid { get; set; }
            public string conference_title { get; set; }
            public List<object> sessions { get; set; }

        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public SampleData sampleData { get; set; }
            public string updatedOn { get; set; }
            public CurrentRevisionId currentRevisionId { get; set; }
            public string currentPath { get; set; }
        }


    }
}

