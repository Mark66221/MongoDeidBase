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

    public class CommerceCarts {
        //string ProgramUsersFileName = "\\Waitlist.json";
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_CommerceCarts(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string email = "";
            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);

                foreach (var obj in jsonObj) {
                    NbrRows++;
                    //Determine Email value
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
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class Adjustment {
            public string className { get; set; }
            public List<object> permissions { get; set; }
            public bool restrictPurchase { get; set; }
            public List<object> permissionsOfViewer { get; set; }
            public string displayPosition { get; set; }
            public List<string> displayWhen { get; set; }
            public object description { get; set; }
            public Id _id { get; set; }
            public string type { get; set; }
            public string subscriptionId { get; set; }
            public bool? autoRenewal { get; set; }
            public bool? allowOptOut { get; set; }
        }

        public class Code {
            public string className { get; set; }
            public string code { get; set; }
            public string type { get; set; }
            public bool allowMultiple { get; set; }
            public string addedOn { get; set; }
        }

        public class CouponCodeApplication {
            public string className { get; set; }
            public string couponCode { get; set; }
            public int appliedAmount { get; set; }
            public int affectedItemCount { get; set; }
            public string message { get; set; }
        }

        public class Currency {
            public string className { get; set; }
            public string @base { get; set; }
            public string currency { get; set; }
            public int exchangeRate { get; set; }
            public bool final { get; set; }
        }

        public class Customer {
            public string className { get; set; }
            public string subdomain { get; set; }
            public int userId { get; set; }
            public string context { get; set; }
        }

        public class Id {
            [JsonPropertyName("$oid")]
            public string oid { get; set; }
        }

        public class Item {
            public string className { get; set; }
            public string sku { get; set; }
            public string title { get; set; }
            public double basePrice { get; set; }
            public List<Adjustment> adjustments { get; set; }
            public double price { get; set; }
            public int quantity { get; set; }
            public string addedOn { get; set; }
        }

        public class OrderProfile {
            public string className { get; set; }
            public TaxRate taxRate { get; set; }
            public string account { get; set; }
            public bool waiveShipping { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public List<object> cartAdjustments { get; set; }
            public List<CouponCodeApplication> couponCodeApplications { get; set; }
            public Customer customer { get; set; }
            public List<object> contexts { get; set; }
            public Shipping shipping { get; set; }
            public List<Code> codes { get; set; }
            public int totalTax { get; set; }
            public double subtotal { get; set; }
            public double total { get; set; }
            public List<Item> items { get; set; }
            public OrderProfile orderProfile { get; set; }
            public string subdomain { get; set; }
            public int userId { get; set; }
            public string email { get; set; }
            public Currency currency { get; set; }
            public List<string> attributes { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public string utmCodesId { get; set; }
        }

        public class Shipping {
            public string className { get; set; }
            public int amount { get; set; }
            public int shippableCount { get; set; }
            public int shippedCount { get; set; }
            public string method { get; set; }
        }

        public class TaxRate {
            public string className { get; set; }
        }


    }
}

