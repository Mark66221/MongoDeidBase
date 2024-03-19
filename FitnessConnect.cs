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

    public class FitnessConnect {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_FitnessConnect(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string firstName = "";
            string lastName = "";
            string email = "";
            string displayName = "";
            string publicPath = "";
            string description = "";
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

                    if (obj.displayName == null) {
                        displayName = "";
                    }
                    else {
                        colInfo = new ColumnInformation();
                        colInfo.Name = "lastName";
                        colInfo.DataType = "LastName";
                        displayName = $"{firstName} {lastName}";
                        obj.displayName = displayName;
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
                    //Determine publicPath value
                    if (obj.publicPath == null) {
                        publicPath = "";
                    }
                    else {
                        publicPath = $"De-identified";
                        obj.publicPath = publicPath;
                    }
                    //Determine description value
                    if (obj.description == null) {
                        description = "";
                    }
                    else {
                        description = $"De-identified";
                        obj.description = description;
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
        public class Activities {
            public string className { get; set; }
            public int start { get; set; }
            public int limit { get; set; }
            public int total { get; set; }
            public string entity { get; set; }
            public List<Item> items { get; set; }
            public int? next_start { get; set; }
        }

        public class Item {
            public string className { get; set; }
            public string tid { get; set; }
            public string vid { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string weight { get; set; }
            public string path { get; set; }
            public int id { get; set; }
            public string context { get; set; }
            public string created_on { get; set; }
            public string updated_on { get; set; }
            public string verified_on { get; set; }
            public string expires_on { get; set; }
            public int expiry_verified { get; set; }
            public int uid { get; set; }
            public int provider_id { get; set; }
            public int cert_id { get; set; }
            public object data { get; set; }
            public int visibility { get; set; }
            public string customer_number { get; set; }
            public string other_verification_value { get; set; }
            public string status { get; set; }
            public string verification_method { get; set; }
            public string approval_status { get; set; }
            public int source_provider_id { get; set; }
            public string renewal_status { get; set; }
            public string source { get; set; }
            public string subdomain { get; set; }
            public int? cert_type_id { get; set; }
            public int? verifier_agent { get; set; }
            public string cert_num { get; set; }
            public string uuid { get; set; }
            public string achieved_on { get; set; }
            public string provider_other { get; set; }
            public string cert_type_other { get; set; }
        }

        public class Location {
            public string className { get; set; }
            public int ref_id { get; set; }
            public string fitconnect_parent { get; set; }
            public string type { get; set; }
            public string location_source { get; set; }
            public string label { get; set; }
            public string radius { get; set; }
            public string visibility { get; set; }
            public string nid { get; set; }
            public int lid { get; set; }
            public string name { get; set; }
            public string street { get; set; }
            public string additional { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string postal_code { get; set; }
            public string country { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string source { get; set; }
            public bool is_primary { get; set; }
        }

        public class Professions {
            public string className { get; set; }
            public int start { get; set; }
            public int limit { get; set; }
            public int total { get; set; }
            public string entity { get; set; }
            public List<Item> items { get; set; }
            public int? next_start { get; set; }
        }

        public class ProfilePicture {
            public string className { get; set; }
            public string filepath { get; set; }
            public int? fid { get; set; }
            public int? nid { get; set; }
            public string filename { get; set; }
            public string filemime { get; set; }
            public int? filesize { get; set; }
        }

        public class Root {
            public int _id { get; set; }
            public string className { get; set; }
            public Professions professions { get; set; }
            public Specialties specialties { get; set; }
            public Activities activities { get; set; }
            public UserCertifications userCertifications { get; set; }
            public object userCertificationCategories { get; set; }
            public object settings { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string gender { get; set; }
            public int jobApplicantScore { get; set; }
            public int yogaJournalScore { get; set; }
            public string insuranceStatus { get; set; }
            public List<string> languagesSpoken { get; set; }
            public List<string> awardsAccomplishments { get; set; }
            public int userId { get; set; }
            public int nodeId { get; set; }
            public bool enabled { get; set; }
            public string publicPath { get; set; }
            public int completeness { get; set; }
            public bool completenessCheck { get; set; }
            public string status { get; set; }
            public double averageVotes { get; set; }
            public string updatedOn { get; set; }
            public int score { get; set; }
            public string description { get; set; }
            public int ratingCount { get; set; }
            public double ratingAverage { get; set; }
            public ProfilePicture profilePicture { get; set; }
            public List<Location> locations { get; set; }
            public string displayName { get; set; }
            public int profilePriority { get; set; }
            public string type { get; set; }
            public string profilePictureUrl { get; set; }
            public string website { get; set; }
            public string yearsExperience { get; set; }
        }

        public class Specialties {
            public string className { get; set; }
            public int start { get; set; }
            public int limit { get; set; }
            public int total { get; set; }
            public string entity { get; set; }
            public List<Item> items { get; set; }
            public int? next_start { get; set; }
        }

        public class UserCertifications {
            public string className { get; set; }
            public int start { get; set; }
            public int limit { get; set; }
            public int total { get; set; }
            public string entity { get; set; }
            public List<Item> items { get; set; }
            public int? next_start { get; set; }
        }


    }
}

