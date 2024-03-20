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

    public class EmailErrors {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_EmailErrors(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();
            string firstname = "";
            string lastname = "";
            string toEmail = "";
            string body = "\"body\": \"<!DOCTYPE html PUBLIC \\\"-//W3C//DTD HTML 4.0 Transitional//EN\\\" \\\"http://www.w3.org/TR/REC-html40/loose.dtd\\\">\\n<html><body>\\n<p>Deidentified</p>\\n</body></html>\\n\",";

            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    // toEmail, body 
                    // emailData: firstname, lastname, email, fullName
                    if (obj.emailData != null) {
                        if (obj.emailData.data != null) {
                            if (obj.emailData.data.firstname == null) {
                                firstname = "";
                            }
                            else {
                                colInfo = new ColumnInformation();
                                colInfo.Name = "firstname";
                                colInfo.DataType = "FirstName";
                                firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                obj.emailData.data.firstname = firstname;
                            }
                            if (obj.emailData.data.lastname == null) {
                                lastname = "";
                            }
                            else {
                                colInfo = new ColumnInformation();
                                colInfo.Name = "lastname";
                                colInfo.DataType = "LastName";
                                lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                obj.emailData.data.lastname = lastname;
                            }
                            if (obj.emailData.data.fullName == null) {
                                toEmail = "";
                            }
                            else {
                                if (firstname == "") {
                                    colInfo = new ColumnInformation();
                                    colInfo.Name = "firstname";
                                    colInfo.DataType = "FirstName";
                                    firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                }
                                if (lastname == "") {
                                    colInfo = new ColumnInformation();
                                    colInfo.Name = "lastname";
                                    colInfo.DataType = "LastName";
                                    lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                }
                                obj.emailData.data.fullName = $"{firstname} {lastname}";
                            }
                            if (obj.emailData.data.email == null) {
                                toEmail = "";
                            }
                            else {
                                if (firstname == "") {
                                    colInfo = new ColumnInformation();
                                    colInfo.Name = "firstname";
                                    colInfo.DataType = "FirstName";
                                    firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                }
                                if (lastname == "") {
                                    colInfo = new ColumnInformation();
                                    colInfo.Name = "lastname";
                                    colInfo.DataType = "LastName";
                                    lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                                }
                                obj.emailData.data.email = $"{firstname}.{lastname}{emailSuffix}";
                            }
                        }
                    }

                    //Determine toEmail value
                    if (obj.toEmail == null) {
                        toEmail = "";
                    }
                    else {
                        if (firstname == "") {
                            colInfo = new ColumnInformation();
                            colInfo.Name = "firstname";
                            colInfo.DataType = "FirstName";
                            firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        }
                        if (lastname == "") {
                            colInfo = new ColumnInformation();
                            colInfo.Name = "lastname";
                            colInfo.DataType = "LastName";
                            lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                        }
                        toEmail = $"{firstname}.{lastname}{emailSuffix}";
                        obj.toEmail = toEmail;
                    }
                    //Determine body value
                    if (!(obj.body == null)) { obj.body = body; }
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
        public class Data {
            public string className { get; set; }
            public string courseTitle { get; set; }
            public string courseUrl { get; set; }
            public string subdomain { get; set; }
            public string siteName { get; set; }
            public string siteEmail { get; set; }
            public string loginUrl { get; set; }
            public string siteUrl { get; set; }
            public string salutation { get; set; }
            public Variables variables { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string fullName { get; set; }
            public int userId { get; set; }
            public string certificateUrl { get; set; }
            public string days { get; set; }
            public string enrolledUrl { get; set; }
            public string loginKey { get; set; }
            public string myCoursesUrl { get; set; }
            public string fromAddr { get; set; }
        }

        public class EmailData {
            public string className { get; set; }
            public Data data { get; set; }
            public string contextId { get; set; }
            public object perms { get; set; }
            public string contentId { get; set; }
        }

        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public string emailClassName { get; set; }
            public string subdomain { get; set; }
            public string createdOn { get; set; }
            public string sentOn { get; set; }
            public string toEmail { get; set; }
            public string fromEmail { get; set; }
            public string fromName { get; set; }
            public string subject { get; set; }
            public string body { get; set; }
            public bool html { get; set; }
            public int failCount { get; set; }
            public List<string> errors { get; set; }
            public EmailData emailData { get; set; }
            public string templateId { get; set; }
            public string bcc { get; set; }
        }

        public class Variables {
            public string moduleCertificateUrl1 { get; set; }
        }

    }
}

