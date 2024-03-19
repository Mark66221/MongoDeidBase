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

    public class Leads {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_Leads(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string email = "";
            string phone = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine phone value
                    if (obj.fields.phone == null) {
                        phone = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "phone";
                        colInfo.DataType = "PhoneNumber";
                        phone = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.fields.phone = phone;
                    }

                    //Determine email value
                    if (obj.email == null) {
                        email = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "email";
                        colInfo.DataType = "Email";
                        email = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
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
        public class Fields {
            public string className { get; set; }
            public Fields fields { get; set; }
            public Id _id { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public string om_optin_id { get; set; }
            public string phone { get; set; }
            public string company { get; set; }
            public string comment { get; set; }
            public string website { get; set; }
            public string sellToClubs { get; set; }
            public string sellToPros { get; set; }
            public string termsAndConditions { get; set; }
            public bool? agree { get; set; }
            public string utmSource { get; set; }
            public string utmMedium { get; set; }
            public string utmCampaign { get; set; }
        }

        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public string source { get; set; }
            public string email { get; set; }
            public Fields fields { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
        }

    }
}

