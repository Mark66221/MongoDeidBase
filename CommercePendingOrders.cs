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

    public class CommercePendingOrders {
        public List<ColumnInformation> Columns { get; set; } = new List<ColumnInformation>();
        public static string emailSuffix = "@cc.com";

        public int Deidentify_CommercePendingOrders(string filepath, string filename) {
            var randomData = new RandomData();
            int NbrRows = 0;

            ColumnInformation colInfo = new ColumnInformation();

            string firstname = "";
            string firstName = "";
            string lastname = "";
            string lastName = "";
            string street1 = "";
            string street2 = "De-identified";
            string city = "";
            string province = "";
            string postal_code = "";
            string phone = "";
            string password = "De-identified";
            string email = "";

            try {
                // Read the JSON file
                List<Root> jsonObj = DeserializeJsonFile(filepath, filename);


                foreach (var obj in jsonObj) {
                    NbrRows++;
                    // Obtain randomised Client Base information
                    // firstname
                    colInfo = new ColumnInformation();
                    colInfo.Name = "firstname";
                    colInfo.DataType = "FirstName";
                    firstname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    // lastname
                    colInfo = new ColumnInformation();
                    colInfo.Name = "lastname";
                    colInfo.DataType = "LastName";
                    lastname = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    // street1
                    colInfo = new ColumnInformation();
                    colInfo.Name = "street1";
                    colInfo.DataType = "StreetAddress";
                    street1 = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    // city
                    colInfo = new ColumnInformation();
                    colInfo.Name = "city";
                    colInfo.DataType = "City";
                    city = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    // province
                    colInfo = new ColumnInformation();
                    colInfo.Name = "province";
                    colInfo.DataType = "State";
                    province = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    // postal_code
                    colInfo = new ColumnInformation();
                    colInfo.Name = "postal_code";
                    colInfo.DataType = "Zip";
                    postal_code = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";
                    // phone
                    colInfo = new ColumnInformation();
                    colInfo.Name = "phone";
                    colInfo.DataType = "PhoneNumber";
                    phone = $"{RemoveSpecialCharacters(randomData.GetRandomValue(colInfo))}";

                    //Determine firstName value
                    if (obj.firstName == null) {
                        firstName = "";
                    }
                    else {
                        firstName = firstname;
                        obj.firstName = firstName;
                    }

                    //Determine lastName value
                    if (obj.lastName == null) {
                        lastName = "";
                    }
                    else {
                        lastName = lastname;
                        obj.lastName = lastName;
                    }

                    //Determine email value
                    if (obj.email == null) {
                        email = "";
                    }
                    else {
                        email = $"{firstname}.{firstname}{emailSuffix}";
                        obj.email = email;
                    }

                    //do remainder of the orders if needed
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
        public class Address {
            public Shipping shipping { get; set; }
            public Billing billing { get; set; }
        }

        public class Billing {
            public string submitted { get; set; }
            public string company { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zip { get; set; }
            public string country { get; set; }
            public int savePrimaryAddress { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string type { get; set; }
            public string phone { get; set; }
            public string uid { get; set; }
            public string password { get; set; }
            public string aid { get; set; }
            public string updated_on { get; set; }
        }

        public class BillingAddress {
            public string className { get; set; }
            public int id { get; set; }
            public string ref_type { get; set; }
            public string ref_id { get; set; }
            public string created_on { get; set; }
            public string updated_on { get; set; }
            public string last_used_on { get; set; }
            public string hash { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string postal_code { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
        }

        public class Code {
            public string className { get; set; }
            public string code { get; set; }
            public string type { get; set; }
            public bool allowMultiple { get; set; }
            public string addedOn { get; set; }
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

        public class Order {
            public string className { get; set; }
            public int orderNumber { get; set; }
            public string orderedOn { get; set; }
            public string status { get; set; }
            public List<object> payments { get; set; }
            public bool guestCheckout { get; set; }
            public Customer customer { get; set; }
            public List<object> contexts { get; set; }
            public Shipping shipping { get; set; }
            public ShippingAddress shippingAddress { get; set; }
            public BillingAddress billingAddress { get; set; }
            public List<Code> codes { get; set; }
            public int totalTax { get; set; }
            public double subtotal { get; set; }
            public double total { get; set; }
            public object items { get; set; }
            public OrderProfile orderProfile { get; set; }
            public string subdomain { get; set; }
            public int userId { get; set; }
            public string email { get; set; }
            public Id _id { get; set; }
            public string createdOn { get; set; }
            public string updatedOn { get; set; }
            public int? txnid { get; set; }
            public string order_id { get; set; }
            public string tx_status { get; set; }
            public string pmt_status { get; set; }
            public string uid { get; set; }
            public string type { get; set; }
            public string mail { get; set; }
            public int? shipping_cost { get; set; }
            public string payment_method { get; set; }
            public int? payment_status { get; set; }
            public int? payment_date { get; set; }
            public int? workflow { get; set; }
            public int? gross { get; set; }
            public int? gross_usd { get; set; }
            public int? totalPaid { get; set; }
            public object subdom_id { get; set; }
            public string currency { get; set; }
            public int? created { get; set; }
            public int? changed { get; set; }
            public int? processed { get; set; }
            public string coupon { get; set; }
            public string tracking { get; set; }
            public string site { get; set; }
            public object agent { get; set; }
            public int? items_sold { get; set; }
            public int? waiver { get; set; }
            public string site_name { get; set; }
            public string payment_info { get; set; }
            public string sales_type { get; set; }
            public string sales_comment { get; set; }
            public int? refund_id { get; set; }
            public int? original_txnid { get; set; }
            public string token { get; set; }
            public object taxrate { get; set; }
            public object misc { get; set; }
            public Address address { get; set; }
            public bool? recalculate { get; set; }
            public bool? tax_exempt { get; set; }
            public string terms { get; set; }
            public object data { get; set; }
            public bool? pos { get; set; }
            public bool? card_required { get; set; }
            public bool? shipping_required { get; set; }
            public object billing_required { get; set; }
            public bool? no_receipt { get; set; }
            public int? taxAmount { get; set; }
            public object _baggage { get; set; }
            public bool? userCheckout { get; set; }
            public string source { get; set; }
            public string checkout_id { get; set; }
            public int? ccid { get; set; }
        }

        public class OrderProfile {
            public string className { get; set; }
            public TaxRate taxRate { get; set; }
            public string account { get; set; }
            public bool waiveShipping { get; set; }
        }

        public class Payment {
            public string className { get; set; }
            public double amount { get; set; }
            public string orderId { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public string encryptedCardNumber { get; set; }
            public string cardExpiration { get; set; }
            public string type { get; set; }
            public string mask { get; set; }
            public string suffix { get; set; }
            public Id _id { get; set; }
        }

        public class Root {
            public Id _id { get; set; }
            public string className { get; set; }
            public bool guest { get; set; }
            public Order order { get; set; }
            public bool createUser { get; set; }
            public object userData { get; set; }
            public List<Payment> payments { get; set; }
            public Step step { get; set; }
            public string email { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
        }

        public class Shipping {
            public string className { get; set; }
            public int amount { get; set; }
            public int shippableCount { get; set; }
            public int shippedCount { get; set; }
            public string method { get; set; }
            public string submitted { get; set; }
            public string company { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zip { get; set; }
            public string country { get; set; }
            public int savePrimaryAddress { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string type { get; set; }
            public string phone { get; set; }
            public string uid { get; set; }
            public string password { get; set; }
            public string aid { get; set; }
            public string updated_on { get; set; }
        }

        public class ShippingAddress {
            public string className { get; set; }
            public int id { get; set; }
            public string ref_type { get; set; }
            public string ref_id { get; set; }
            public string created_on { get; set; }
            public string updated_on { get; set; }
            public string last_used_on { get; set; }
            public string hash { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string postal_code { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
        }

        public class Step {
            public string className { get; set; }
        }

        public class TaxRate {
            public string className { get; set; }
        }


    }
}

