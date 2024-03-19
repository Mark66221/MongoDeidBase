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
using static MongoDeidBase.EngageChats;
using System.Runtime.Remoting.Messaging;

namespace MongoDeidBase {

    public class EngageChats {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_EngageChats(string filepath, string filename) {
            DateTime date = DateTime.Now;
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string name = "";
            string phone = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {


                    NbrRows++;
                    // Obtain default values
                    string visitorName = "";
                    string visitorIp = "127.0.0.1";
                    string visitorId = "";
                    string ip = "127.0.0.1";
                    string agentName = "";
                    string agentId = "";
                    string authorName = "";
                    string city = "";
                    string region = "";

                    // visitorName
                    colInfo = new ColumnInformation();
                    colInfo.Name = "visitorName";
                    colInfo.DataType = "Name";
                    visitorName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";

                    // visitorId
                    colInfo = new ColumnInformation();
                    colInfo.Name = "visitorId";
                    colInfo.DataType = "Email";
                    agentId = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";

                    // agentName
                    colInfo = new ColumnInformation();
                    colInfo.Name = "agentName";
                    colInfo.DataType = "Name";
                    agentName = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";

                    // agentId
                    colInfo = new ColumnInformation();
                    colInfo.Name = "agentId";
                    colInfo.DataType = "Email";
                    agentId = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";

                    //Populate root values
                    if (obj.visitorName == null) {
                    }
                    else {
                        obj.visitorName = visitorName;
                    }
                    if (obj.visitorIp == null) {
                    }
                    else {
                        obj.visitorIp = visitorIp;
                    }

                    //Populate visitor values
                    if (obj.visitor == null) {
                    }
                    else {
                        if (obj.visitor.name == null) {
                        }
                        else {
                            obj.visitor.name = visitorName;
                        }
                        if (obj.visitor.ip == null) {
                        }
                        else {
                            obj.visitor.ip = ip;
                        }
                        if (obj.visitor.city == null) {
                            city = "";
                        }
                        else {
                            colInfo = new ColumnInformation();
                            colInfo.Name = "city";
                            colInfo.DataType = "City";
                            city = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                            obj.visitor.city = city;
                        }
                        //Determine visitor region value
                        if (obj.visitor.region == null) {
                            region = "";
                        }
                        else {
                            colInfo = new ColumnInformation();
                            colInfo.Name = "region";
                            colInfo.DataType = "State";
                            region = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                            obj.visitor.region = region;
                        }
                    }

                    //Determine agent Objects value
                    if (obj.agents == null) {
                    }
                    else {
                        foreach (var agent in obj.agents) {
                            if (agent.email == null) {
                            }
                            else {
                                agent.email = agentId;
                            }
                            if (agent.displayName == null) {
                            }
                            else {
                                agent.displayName = agentName;
                            }
                            if (agent.ip == null) {
                            }
                            else {
                                agent.ip = ip;
                            }
                        }
                    }
                    //Determine messages   Objects value
                    if (obj.messages == null) {
                    }
                    else {
                        foreach (var message in obj.messages) {
                            // message.authorName
                            if (message.authorName == null) {
                            }
                            else {
                                if (message.userType == "visitor")
                                    message.authorName = visitorName;
                                else
                                    message.authorName = agentName;
                            }
                            // message.agentId
                            if (message.agentId == null) {
                            }
                            else {
                                if (message.userType == "visitor")
                                    message.agentId = visitorId;
                                else
                                    message.agentId = agentId;
                                message.agentId = agentId;
                            }
                            // agent.text
                            if (message.text == null) {
                            }
                            else {
                                message.text = $"Deidentified {date.ToString()}";
                            }
                        }
                    }
                    //Determine events  Objects value
                    if (obj.events == null) {
                    }
                    else {
                        foreach (var objevent in obj.events) {
                            // objevent.authorName
                            if (objevent.authorName == null) {
                            }
                            else {
                                if (objevent.userType == "visitor")
                                    objevent.authorName = visitorName;
                                else
                                    objevent.authorName = agentName;
                            }
                            // objevent.agentId
                            if (objevent.agentId == null) {
                            }
                            else {
                                objevent.agentId = agentId;
                            }
                            // agent.text
                            if (objevent.text == null) {
                            }
                            else {
                                objevent.text = $"Deidentified {date.ToString()}";
                            }
                        }
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
        public class Agent {
            public string className { get; set; }
            public string displayName { get; set; } // firstname
            public string email { get; set; }       // email
            public string ip { get; set; }          // 127.0.0.2
        }

        public class Ended {
            [JsonPropertyName("$date")]
            public DateTime date { get; set; }
        }

        public class Event {
            public string className { get; set; }
            public string authorName { get; set; }  // name
            public string text { get; set; }        // Deidentified
            public DateTime date { get; set; }
            public string agentId { get; set; }     //email
            public string userType { get; set; }
            public string type { get; set; }
            public bool welcomeMessage { get; set; }
        }

        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Message {
            public string className { get; set; }
            public string authorName { get; set; }  // name
            public string text { get; set; }        // de-identify
            public DateTime date { get; set; }
            public string agentId { get; set; }     // email
            public string userType { get; set; }
            public string type { get; set; }
            public bool welcomeMessage { get; set; }
        }

        public class Root {
            public string _id { get; set; }
            public string className { get; set; }
            public string subdomainName { get; set; }
            public string type { get; set; }
            public List<object> tickets { get; set; }
            public string visitorId { get; set; }
            public string visitorIp { get; set; }   // 127.0.0.1
            public Visitor visitor { get; set; }
            public List<Agent> agents { get; set; }
            public List<object> supervisors { get; set; }
            public string rate { get; set; }
            public int duration { get; set; }
            public string chatStartUrl { get; set; }
            public string referrer { get; set; }
            public List<int> group { get; set; }
            public string pending { get; set; }
            public List<string> tags { get; set; }
            public string timezone { get; set; }
            public List<Message> messages { get; set; }
            public List<Event> events { get; set; }
            public string engagement { get; set; }
            public Started started { get; set; }
            public Ended ended { get; set; }
            public Stats stats { get; set; }
            public string visitorName { get; set; } // name
            public string updatedOn { get; set; }
            public bool active { get; set; }
            public string visitorTimezone { get; set; }
        }

        public class Started {
            [JsonPropertyName("$date")]
            public DateTime date { get; set; }
        }

        public class Stats {
            public string className { get; set; }
            public double responseTime { get; set; }
            public Id _id { get; set; }
            public int? firstResponseTime { get; set; }
        }

        public class Visitor {
            public string className { get; set; }
            public string id { get; set; }
            public string name { get; set; }    // name
            public string ip { get; set; }      //127.0.0.1
            public string city { get; set; }    // city
            public string region { get; set; }  //state
            public string country { get; set; }
            public string countryCode { get; set; }
            public string timezone { get; set; }
        }


    }
}

