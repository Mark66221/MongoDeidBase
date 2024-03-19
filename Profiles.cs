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
    public class Profiles {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";
        public int Deidentify_Profiles(string filepath, string filename) {
            var randomData = new RandomData();
            ColumnInformation colInfo = new ColumnInformation();
            try {
                string description = "";
                string name = "";
                string firstName = "";
                string lastName = "";
                string user_firstName = "";
                string user_lastName = "";
                string data = "";

                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);
                int NbrRows = 0;

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    if (obj.data == null) {
                        data = "";
                    }
                    else {
                        int a = 0;
                        //string[] xx = new string[] { };
                        foreach (var dataobj in obj.data) {
                            colInfo = new ColumnInformation();
                            switch (dataobj._id) {
                                case "name_firstName":
                                    colInfo.Name = "firstName";
                                    colInfo.DataType = "FirstName";
                                    firstName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                    dataobj.value = firstName;
                                    break;
                                case "name_lastName":
                                    colInfo.Name = "lastName";
                                    colInfo.DataType = "LastName";
                                    lastName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                    dataobj.value = lastName;
                                    break;
                                case "user_firstName":
                                    user_firstName = firstName;
                                    dataobj.value = user_firstName;
                                    break;
                                case "user_lastName":
                                    user_lastName = lastName;
                                    dataobj.value = user_lastName;
                                    break;
                                case "name":
                                    name = $"{firstName} {lastName})";
                                    dataobj.value = name;
                                    break;
                                case "description":
                                    colInfo.Name = "description";
                                    colInfo.DataType = "PlainText";
                                    colInfo.Value = $"De-identified {DateTime.Today}";
                                    description = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                    dataobj.value = description;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (obj.name == null) {
                        name = "";
                    }
                    else {
                        name = $"{firstName} {lastName})";
                        obj.name = name;
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
        public class Datum {
            public string className { get; set; }
            public object value { get; set; }
            public string _id { get; set; }
        }
        public class Id {
            public object oid { get; set; }
        }
        public class Owner {
            public string className { get; set; }
            public int userId { get; set; }
        }
        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public string profileTypeId { get; set; }
            public Owner owner { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public List<string> permissions { get; set; }
            public List<string> subdomains { get; set; }
            public List<Datum> data { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public bool missing { get; set; }
            public int subdomId { get; set; }
            public object firstName { get; set; }
            public object lastName { get; set; }
            public object email { get; set; }
            public int userId { get; set; }
        }
    }
}
