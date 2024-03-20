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
using System.Xml.Linq;

namespace MongoDeidBase {

    public class EmailsSent {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_EmailsSent(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string fromfirstname = "";
            string fromlastname = "";
            string fromName = "";
            string fromEmail = "";
            string tofirstname = "";
            string tolastname = "";
            string toName = "";
            string toEmail = "";
            string subject = "";
            string body = "";
            string html = "";

            DateTime date = DateTime.Now;
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;

                    colInfo = new ColumnInformation();
                    colInfo.Name = "fromfirstname";
                    colInfo.DataType = "FirstName";
                    fromfirstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    colInfo = new ColumnInformation();
                    colInfo.Name = "fromlastname";
                    colInfo.DataType = "LastName";
                    fromlastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    colInfo = new ColumnInformation();
                    colInfo.Name = "tofirstname";
                    colInfo.DataType = "FirstName";
                    tofirstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    colInfo = new ColumnInformation();
                    colInfo.Name = "tolastname";
                    colInfo.DataType = "LastName";
                    tolastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";

                    //Determine toEmail value
                    if (obj.toEmail == null) {
                        toEmail = "";
                    }
                    else {
                        toEmail = $"{tofirstname}.{tolastname}{emailSuffix}";
                        obj.toEmail = toEmail;
                    }
                    //Determine toName value
                    if (obj.toName == null) {
                        toEmail = "";
                    }
                    else {
                        toName = $"{tofirstname} {tolastname}";
                        obj.toName = toName;
                    }
                    //Determine fromEmail value
                    if (obj.fromEmail == null) {
                        fromEmail = "";
                    }
                    else {
                        fromEmail = $"{fromfirstname}.{fromlastname}{emailSuffix}";
                        obj.fromEmail = fromEmail;
                    }
                    //Determine fromName value
                    if (obj.fromName == null) {
                        fromName = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "fromName";
                        colInfo.DataType = "Name";
                        fromName = $"{fromfirstname} {fromlastname}";
                        obj.fromName = fromName;
                    }
                    //Determine subject value
                    if (obj.subject == null) {
                        subject = "";
                    }
                    else {
                        subject = $"Deidentified {date.ToString()}";
                        obj.subject = subject;
                    }
                    //Determine body value
                    if (obj.body == null) {
                        body = "";
                    }
                    else {
                        body = $"Deidentified {date.ToString()}";
                        obj.body = body;
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
            public string createdOn { get; set; }
            public string sentOn { get; set; }
            public string toEmail { get; set; }
            public string fromEmail { get; set; }
            public string fromName { get; set; }
            public string subject { get; set; }
            public string body { get; set; }
            public bool html { get; set; }
            public int failCount { get; set; }
            public List<object> errors { get; set; }
            public string toName { get; set; }
            public string replyTo { get; set; }
        }

    }
}

