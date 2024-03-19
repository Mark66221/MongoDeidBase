using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace DeidentifyLibrary {
    public partial class RandomData {
        private static bool _initialized = false;

        private static List<string> _tasks = new List<string>();
        private static List<string> _jobtitles = new List<string>();
        private static List<string> _futuredates = new List<string>();
        private static List<string> _countries = new List<string>();
        //private static List<string> _firstNames = new List<string>();
        //private static List<string> _lastNames = new List<string>();
        //private static List<string> _streets = new List<string>();
        //private static List<string> _cities = new List<string>();
        private static List<string> _states = new List<string>();
        private static List<string> _schoolNames = new List<string>();
        //private static List<string> _randomTexts = new List<string>();
        //private static List<string> _randomWesternTexts = new List<string>();
        //private static List<string> _hospitalNames = new List<string>();
        private static List<string> _stateAbbreviations = new List<string>();
        private Random _rnd = new Random(System.DateTime.Now.Millisecond);

        public static void Initialize() {
            if (_initialized)
                return;
            LoadFirstNames();
            LoadLastNames();
            LoadStreets();
            LoadCities();
            LoadStates();
            LoadStateAbbreviations();
            LoadCompanyNames();
            LoadTasks();
            LoadJobTitles();
            LoadFutureDates();
            LoadCountries();
            LoadHospitals();
            LoadSchools();
            LoadRandomText();
            LoadRandomWesternText();
            _initialized = true;
        }
        private static void LoadTasks() {
            _tasks.Add("Conduct Training, Train the team on making coffee");
            _tasks.Add("Conduct Training, Train the team on making coffee");
            _tasks.Add("Conduct Training, Train the team on making coffee");
            _tasks.Add("Conduct Training, Train the team on making coffee");
            _tasks.Add("Conduct Training, Train the team on making coffee");
            _tasks.Add("Conduct Training, Train the team on making coffee");
            _tasks.Add("Conduct Training, Train the team on making coffee");
        }
        private static void LoadJobTitles() {
            _jobtitles.Add("CEO");
            _jobtitles.Add("Clerk");
            _jobtitles.Add("Nurse");
            _jobtitles.Add("Doctor");
            _jobtitles.Add("Maintenance Engineer");
            _jobtitles.Add("President");
            _jobtitles.Add("Administrator");
        }
        private static void LoadFutureDates() {
            _futuredates.Add("10/20/2024");
            _futuredates.Add("11/21/2024");
            _futuredates.Add("12/22/2024");
            _futuredates.Add("5/1/2024");
            _futuredates.Add("10/20/2024");
        }
        private static void LoadCountries() {
            _countries.Add("United States");
            _countries.Add("Canada");
            _countries.Add("Denmark");
            _countries.Add("China");
            _countries.Add("England");
            _countries.Add("France");
            _countries.Add("Finland");
            _countries.Add("Hong Kong");
            _countries.Add("Israel");
            _countries.Add("Italy");
            _countries.Add("Mexico");
            _countries.Add("Netherlands");
            _countries.Add("New Zealand");
            _countries.Add("Northern Ireland");
            _countries.Add("Portugal");
            _countries.Add("Scotland");
            _countries.Add("Spain");
            _countries.Add("Sweden");
            _countries.Add("Tibet");
            _countries.Add("Turkey");
            _countries.Add("United Arab Emirates");
            _countries.Add("Venezuela");
            _countries.Add("Wales");
            _countries.Add("United Kingdom");
        }
        private static void LoadSchools() {
            _schoolNames.Add("The Art Institutes International");
            _schoolNames.Add("Associated Mennonite Biblical Seminary");
            _schoolNames.Add("Baker University");
            _schoolNames.Add("Barclay College");
            _schoolNames.Add("Benedictine College");
            _schoolNames.Add("Bethany College");
            _schoolNames.Add("Bethel College");
            _schoolNames.Add("Central Christian College");
            _schoolNames.Add("Cleveland University");
            _schoolNames.Add("Donnelly College");
            _schoolNames.Add("Friends University");
            _schoolNames.Add("Heritage Baptist College");
            _schoolNames.Add("Hesston College");
            _schoolNames.Add("Kansas Christian College");
            _schoolNames.Add("Kansas Wesleyan University");
            _schoolNames.Add("Manhattan Christian College");
            _schoolNames.Add("McPherson College");
        }

        private static void LoadStates() {
            _states.Add("Alabama");
            _states.Add("Alaska");
            _states.Add("Arizona");
            _states.Add("Arkansas");
            _states.Add("California");
            _states.Add("Colorado");
            _states.Add("Connecticut");
            _states.Add("Delaware");
            _states.Add("Florida");
            _states.Add("Georgia");
            _states.Add("Hawaii");
            _states.Add("Idaho");
            _states.Add("Illinois");
            _states.Add("Indiana");
            _states.Add("Iowa");
            _states.Add("Kansas");
            _states.Add("Kentucky");
            _states.Add("Louisiana");
            _states.Add("Maine");
            _states.Add("Maryland");
            _states.Add("Massachusetts");
            _states.Add("Michigan");
            _states.Add("Minnesota");
            _states.Add("Mississippi");
            _states.Add("Missouri");
            _states.Add("Montana");
            _states.Add("Nebraska");
            _states.Add("Nevada");
            _states.Add("New Hampshire");
            _states.Add("New Jersey");
            _states.Add("New Mexico");
            _states.Add("New York");
            _states.Add("North Carolina");
            _states.Add("North Dakota");
            _states.Add("Ohio");
            _states.Add("Oklahoma");
            _states.Add("Oregon");
            _states.Add("Pennsylvania");
            _states.Add("Rhode Island");
            _states.Add("South Carolina");
            _states.Add("South Dakota");
            _states.Add("Tennessee");
            _states.Add("Texas");
            _states.Add("Utah");
            _states.Add("Vermont");
            _states.Add("Virginia");
            _states.Add("Washington");
            _states.Add("West Virginia");
            _states.Add("Wisconsin");
            _states.Add("Wyoming");
            _states.Add("Washington DC");
        }
        private static void LoadStateAbbreviations() {
            _stateAbbreviations.Add("AL");
            _stateAbbreviations.Add("AK");
            _stateAbbreviations.Add("AZ");
            _stateAbbreviations.Add("AR");
            _stateAbbreviations.Add("CA");
            _stateAbbreviations.Add("CO");
            _stateAbbreviations.Add("CT");
            _stateAbbreviations.Add("DE");
            _stateAbbreviations.Add("FL");
            _stateAbbreviations.Add("GA");
            _stateAbbreviations.Add("HI");
            _stateAbbreviations.Add("ID");
            _stateAbbreviations.Add("IL");
            _stateAbbreviations.Add("IN");
            _stateAbbreviations.Add("IA");
            _stateAbbreviations.Add("KS");
            _stateAbbreviations.Add("KY");
            _stateAbbreviations.Add("LA");
            _stateAbbreviations.Add("ME");
            _stateAbbreviations.Add("MD");
            _stateAbbreviations.Add("MA");
            _stateAbbreviations.Add("MI");
            _stateAbbreviations.Add("MN");
            _stateAbbreviations.Add("MS");
            _stateAbbreviations.Add("MO");
            _stateAbbreviations.Add("MT");
            _stateAbbreviations.Add("NE");
            _stateAbbreviations.Add("NV");
            _stateAbbreviations.Add("NH");
            _stateAbbreviations.Add("NJ");
            _stateAbbreviations.Add("NM");
            _stateAbbreviations.Add("NY");
            _stateAbbreviations.Add("NC");
            _stateAbbreviations.Add("ND");
            _stateAbbreviations.Add("OH");
            _stateAbbreviations.Add("OK");
            _stateAbbreviations.Add("OR");
            _stateAbbreviations.Add("PA");
            _stateAbbreviations.Add("RI");
            _stateAbbreviations.Add("SC");
            _stateAbbreviations.Add("SD");
            _stateAbbreviations.Add("TN");
            _stateAbbreviations.Add("TX");
            _stateAbbreviations.Add("UT");
            _stateAbbreviations.Add("VT");
            _stateAbbreviations.Add("VA");
            _stateAbbreviations.Add("WA");
            _stateAbbreviations.Add("WV");
            _stateAbbreviations.Add("WI");
            _stateAbbreviations.Add("WY");
            _stateAbbreviations.Add("DC");
        }
        private Dictionary<string, string> _mostRecentRandomValues = new Dictionary<string, string>();
        public void AddUpdateMostRecentRandomValue(string ColumnName, string RandomValue) {
            if (_mostRecentRandomValues.ContainsKey(ColumnName))
                _mostRecentRandomValues[ColumnName] = RandomValue;
            else
                _mostRecentRandomValues.Add(ColumnName, RandomValue);
        }
        public string GetRandomValue(ColumnInformation ColumnInfo) {
            if (ColumnInfo.DataType is null)
                return "___";
            if (!_initialized) {
                RandomData.Initialize();
            }
            string returnValue = null;
            Regex regex = new Regex(@"\{(.+?)\}");
            Match match = regex.Match(""); //regex.Match();
            switch (ColumnInfo.DataType) {
                case "NULL":
                    returnValue = "null";
                    break;
                case "PlainText":
                    returnValue = ColumnInfo.Value;
                    break;
                case "IPAddress":
                    returnValue = "127.0.0.1";
                    break;
                case "Task":
                    returnValue = _tasks[_rnd.Next(0, _tasks.Count - 1)];
                    break;
                case "JobTitle":
                    returnValue = _jobtitles[_rnd.Next(0, _jobtitles.Count - 1)];
                    break;
                case "FutureDate":
                    returnValue = _futuredates[_rnd.Next(0, _futuredates.Count - 1)];
                    break;
                case "CompanyName":
                    returnValue = _companyNames[_rnd.Next(0, _companyNames.Count - 1)];
                    break;
                case "HospitalName":
                    returnValue = _hospitalNames[_rnd.Next(0, _hospitalNames.Count - 1)];
                    break;
                case "SchoolName":
                    returnValue = _schoolNames[_rnd.Next(0, _schoolNames.Count - 1)];
                    break;
                case "RandomText":
                    returnValue = _randomTexts[_rnd.Next(0, _randomTexts.Count - 1)];
                    break;
                case "RandomWesternText":
                    returnValue = _randomWesternTexts[_rnd.Next(0, _randomWesternTexts.Count - 1)];
                    break;
                case "FirstName":
                    returnValue = _firstNames[_rnd.Next(0, _firstNames.Count - 1)];
                    break;
                case "LastName":
                    returnValue = _lastNames[_rnd.Next(0, _lastNames.Count - 1)];
                    break;
                case "Name":
                    returnValue = _firstNames[_rnd.Next(0, _firstNames.Count - 1)] + " " + _lastNames[_rnd.Next(0, _lastNames.Count - 1)];
                    break;
                case "Initial":
                    returnValue = ((char)_rnd.Next(65, 90)).ToString();
                    break;
                case "PhoneNumber":
                    returnValue = _rnd.Next(100, 999).ToString() + "555" + _rnd.Next(1000, 9999).ToString();
                    break;
                case "StreetAddress":
                    returnValue = _rnd.Next(1000, 99999).ToString() + " " + _streets[_rnd.Next(0, _streets.Count - 1)];
                    break;
                case "City":
                    returnValue = _cities[_rnd.Next(0, _cities.Count - 1)];
                    break;
                case "State":
                    returnValue = _states[_rnd.Next(0, _states.Count - 1)];
                    break;
                case "Country":
                    returnValue = _countries[_rnd.Next(0, _countries.Count - 1)];
                    break;
                case "StateAbbreviation":
                    returnValue = _stateAbbreviations[_rnd.Next(0, _stateAbbreviations.Count - 1)];
                    break;
                case "Zip":
                    returnValue = _rnd.Next(10000, 99999).ToString();
                    break;
                case "Zip5Truncate":
                    if (String.IsNullOrEmpty(ColumnInfo.Value))
                        returnValue = "";
                    else
                        returnValue = ColumnInfo.Value.Substring(0, 5);
                    break;
                case "Address":
                    returnValue = _rnd.Next(1000, 99999).ToString() + " " + _streets[_rnd.Next(0, _streets.Count - 1)] + " " + _cities[_rnd.Next(0, _cities.Count - 1)] + ", " + _stateAbbreviations[_rnd.Next(0, _stateAbbreviations.Count - 1)] + " " + _rnd.Next(10000, 99999).ToString();
                    break;
                case "SSN":
                    returnValue = "123456789";
                    break;
                case "Email":
                    returnValue = _firstNames[_rnd.Next(0, _firstNames.Count - 1)] + "." +
                                  _lastNames[_rnd.Next(0, _lastNames.Count - 1)] + "@ascendlearning.com";
                    break;
                case "int":
                    var minInt = 1;
                    var maxInt = int.MaxValue;
                    if (!string.IsNullOrEmpty(ColumnInfo.Minimum))
                        minInt = int.Parse(ColumnInfo.Minimum);
                    if (!string.IsNullOrEmpty(ColumnInfo.Maximum))
                        maxInt = int.Parse(ColumnInfo.Maximum);
                    returnValue = _rnd.Next(minInt, maxInt).ToString();
                    break;
                case "long":
                    var minLong = 1L;
                    var maxLong = long.MaxValue;
                    if (!string.IsNullOrEmpty(ColumnInfo.Minimum))
                        minLong = long.Parse(ColumnInfo.Minimum);
                    if (!string.IsNullOrEmpty(ColumnInfo.Maximum))
                        maxLong = long.Parse(ColumnInfo.Maximum);
                    returnValue = (_rnd.NextDouble() * maxLong + minLong).ToString();
                    break;
                case "Date":
                    DateTime _minDate = DateTime.Now.AddYears(-1);
                    DateTime _maxDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(ColumnInfo.Minimum))
                        DateTime.TryParse(ColumnInfo.Minimum, out _minDate);
                    if (!string.IsNullOrEmpty(ColumnInfo.Maximum))
                        DateTime.TryParse(ColumnInfo.Maximum, out _maxDate);
                    var seconds = _maxDate.Subtract(_minDate).TotalSeconds;
                    returnValue = _minDate.AddSeconds(_rnd.NextDouble() * seconds).ToString("yyyy/MM/dd hh:mm:ss");
                    break;
                case "Text":
                    returnValue = ColumnInfo.Value;
                    //Look for call back values
                    // Regex regex = new Regex(@"\{(.+?)\}");
                    match = regex.Match(returnValue);
                    while (match.Success) {
                        string lookupName = match.Value.Substring(1, match.Value.Length - 2);
                        if (!_mostRecentRandomValues.ContainsKey(lookupName))
                            returnValue = returnValue.Replace(match.Value, "___");

                        //throw new ArgumentException($"Column {ColumnInfo.Name} Recent RandomValue'{lookupName}' lookup value not found in data type {ColumnInfo.DataType} {ColumnInfo.Value}");
                        else
                            returnValue = returnValue.Replace(match.Value, _mostRecentRandomValues[lookupName]);
                        match = match.NextMatch();
                    }
                    break;
                case "Text-ToUpper":
                    returnValue = ColumnInfo.Value;
                    //Look for call back values
                    //Regex regex1 = new Regex(@"\{(.+?)\}");
                    match = regex.Match(returnValue);
                    while (match.Success) {
                        string lookupName = match.Value.Substring(1, match.Value.Length - 2);
                        if (!_mostRecentRandomValues.ContainsKey(lookupName))
                            returnValue = returnValue.Replace(match.Value, "___");

                        //throw new ArgumentException($"Column {ColumnInfo.Name} Recent RandomValue'{lookupName}' lookup value not found in data type {ColumnInfo.DataType} {ColumnInfo.Value}");
                        else
                            returnValue = returnValue.Replace(match.Value, _mostRecentRandomValues[lookupName]).ToUpper();
                        match = match.NextMatch();
                    }
                    break;
                default:
                    //Determine if this is a list of pipe-delimited strings. If so, pick one, otherwise return the hard-coded string Type
                    if (ColumnInfo.DataType.Contains("|")) {
                        var options = ColumnInfo.DataType.Split('|');
                        returnValue = options[_rnd.Next(options.Length)];
                    }
                    else {
                        returnValue = ColumnInfo.DataType;
                        //Look for call back values
                        Regex regex2 = new Regex(@"\{(.+?)\}");
                        Match match2 = regex2.Match(returnValue);
                        while (match2.Success) {
                            string lookupName = match2.Value.Substring(1, match2.Value.Length - 2);
                            if (!_mostRecentRandomValues.ContainsKey(lookupName))
                                return string.Empty;
                            //throw new ArgumentException($"{lookupName} lookup value not found in data type {ColumnInfo.DataType}");
                            returnValue = returnValue.Replace(match2.Value,
                                _mostRecentRandomValues[lookupName]);

                            match2 = match2.NextMatch();
                        }
                    }
                    break;
            }
            if (_mostRecentRandomValues.ContainsKey(ColumnInfo.Name))
                _mostRecentRandomValues[ColumnInfo.Name] = returnValue;
            else
                _mostRecentRandomValues.Add(ColumnInfo.Name, returnValue);
            return returnValue;
        }
    }
    public class ColumnInformation {
        public string Name { get; set; }
        //public string ServiceObjectColumnName { get; set; }
        //public string SQLType { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
        //public string Filter { get; set; }
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        //public bool? Identity { get; set; }
        //public string ReferenceColumn { get; set; }
        //public bool Trim { get; set; } = false;
        //public string ReplaceOnlyIfNotEmpty { get; set; }
        //public string LeaveNulls { get; set; }
        //public string ReferenceDataSource { get; set; }
        //public string ReferenceDataColumn { get; set; }
    }

}

