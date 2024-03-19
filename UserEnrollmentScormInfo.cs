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

    public class UserEnrollmentScormInfo {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_UserEnrollmentScormInfo(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string student_name = "";
            try {
                // Read the JSON file
                List<Item> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine student_name value
                    if (obj.core.student_name == null) {
                        student_name = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "student_name";
                        colInfo.DataType = "Name";
                        student_name = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        obj.core.student_name = student_name;
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
        public static List<Item> DeserializeJsonFile(string inputFilePath, string inputFile) {
            // Read the contents of the input file into a string
            try {
                var json = File.ReadAllText(inputFilePath + inputFile);

                // Deserialize the JSON string into a list of objects of type Root
                var jsonData = System.Text.Json.JsonSerializer.Deserialize<List<Item>>(json);

                // Return the deserialized JSON data
                return jsonData;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public class Core {
            public string className { get; set; }
            public string student_id { get; set; }
            public string student_name { get; set; }
            public string lesson_status { get; set; }
            public string lesson_location { get; set; }
            public string lesson_mode { get; set; }
            public Score score { get; set; }
            public string credit { get; set; }
            public string entry { get; set; }
            public string exit { get; set; }
            public string session_time { get; set; }
            public string total_time { get; set; }
        }

        public class CreatedOn {
            [JsonPropertyName("$date")]
            public DateTime date { get; set; }
        }

        public class Interactions {
            public string className { get; set; }
            public List<object> items { get; set; }
        }

        public class Item {
            public string _id { get; set; }
            public string className { get; set; }
            public CreatedOn createdOn { get; set; }
            public UpdatedOn updatedOn { get; set; }
            public Core core { get; set; }
            public Objectives objectives { get; set; }
            public StudentData student_data { get; set; }
            public Interactions interactions { get; set; }
            public string comments { get; set; }
            public string launch_data { get; set; }
            public string suspend_data { get; set; }
        }

        public class Objectives {
            public string className { get; set; }
            public List<object> items { get; set; }
        }

        public class Root {
            public List<Item> Item { get; set; }
        }

        public class Score {
            public string className { get; set; }
            public decimal raw { get; set; }
            public int max { get; set; }
            public int? min { get; set; }
        }

        public class StudentData {
            public string className { get; set; }
        }

        public class UpdatedOn {
            [JsonPropertyName("$date")]
            public DateTime date { get; set; }
        }


    }
}

