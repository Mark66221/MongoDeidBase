using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDeidBase {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "C:\\Users\\mark.finch\\OneDrive - Ascend Learning\\Desktop\\Deity\\SOC 2\\ClubConnect\\CC_ProdRefresh_March2024\\Mongo";

            string ProgramUsersFileName = "\\ProgramUsers.json";

            string DataImportItemsFileName = "\\DataImportItems.json";
            string EmailErrorsFileName = "\\EmailErrors.json";
            string EmailQueueFileName = "\\EmailQueue.json";
            string EmailsSentFileName = "\\EmailsSent.json";
            string InviteQueueFileName = "\\InviteQueue.json";
            string PdfRequestsFileName = "\\PdfRequests.json";
            string ProfilesFileName = "\\Profiles.json";
            string UserEnrollmentScormInfoFileName = "\\UserEnrollmentScormInfo.json";
            string WaitlistFileName = "\\Waitlist.json";

            //string CommerceCartsFileName = "\\CommerceCarts.json";
            //string CommerceOrdersFileName = "\\CommerceOrders.json";
            //string CommercePendingOrdersFileName = "\\CommercePendingOrders_20.json";
            //string EngageChatsFileName = "\\EngageChats.json";
            //string EngageChatApiCallsFileName = "\\EngageChatApiCalls.json";
            //string EngageUsersFileName = "\\EngageUsers.json";
            //string FitnessConnectFileName = "\\FitnessConnect_5000_a.json";
            //string LeadsFileName = "\\Leads.json";
            //string PdfEditsFileName = "\\PdfEdits.json";

            int nbrRowsChanged = 0;
            try {
                // 37,569 docs
                ProgramUsers programUsers = new ProgramUsers();
                nbrRowsChanged = programUsers.Deidentify_ProgramUsers(filePath, ProgramUsersFileName);
                Console.WriteLine($"Modified JSON file {ProgramUsersFileName} has been created successfully. {nbrRowsChanged} records.");

                // 98879 docs
                DataImportItems dataImportItems = new DataImportItems();
                nbrRowsChanged = dataImportItems.Deidentify_DataImportItems(filePath, DataImportItemsFileName);
                Console.WriteLine($"Modified JSON file {DataImportItemsFileName} has been created successfully. {nbrRowsChanged} records.");

                // 14  docs 
                EmailErrors emailErrors = new EmailErrors();
                nbrRowsChanged = emailErrors.Deidentify_EmailErrors(filePath, EmailErrorsFileName);
                Console.WriteLine($"Modified JSON file {EmailErrorsFileName} has been created successfully. {nbrRowsChanged} records.");

                // 1  docs 
                EmailQueue emailQueue = new EmailQueue();
                nbrRowsChanged = emailQueue.Deidentify_EmailQueue(filePath, EmailQueueFileName);
                Console.WriteLine($"Modified JSON file {EmailQueueFileName} has been created successfully. {nbrRowsChanged} records.");

                //  4,534,786 docs (resized by hand to 378,914 documents
                EmailsSent emailsSent = new EmailsSent();
                nbrRowsChanged = emailsSent.Deidentify_EmailsSent(filePath, EmailsSentFileName);
                Console.WriteLine($"Modified JSON file {EmailsSentFileName} has been created successfully. {nbrRowsChanged} records.");

                // 328 docs
                InviteQueue inviteQueue = new InviteQueue();
                nbrRowsChanged = inviteQueue.Deidentify_InviteQueue(filePath, InviteQueueFileName);
                Console.WriteLine($"Modified JSON file {InviteQueueFileName} has been created successfully. {nbrRowsChanged} records.");

                // 1601 docs  
                PdfRequests pdfRequests = new PdfRequests();
                nbrRowsChanged = pdfRequests.Deidentify_PdfRequests(filePath, PdfRequestsFileName);
                Console.WriteLine($"Modified JSON file {PdfRequestsFileName} has been created successfully. {nbrRowsChanged} records.");

                // 369774 docs
                Profiles profiles = new Profiles();
                nbrRowsChanged = profiles.Deidentify_Profiles(filePath, ProfilesFileName);
                Console.WriteLine($"Modified JSON file {ProfilesFileName} has been created successfully. {nbrRowsChanged} records.");

                // 115,535 docs
                UserEnrollmentScormInfo userEnrollmentScormInfo = new UserEnrollmentScormInfo();
                nbrRowsChanged = userEnrollmentScormInfo.Deidentify_UserEnrollmentScormInfo(filePath, UserEnrollmentScormInfoFileName);
                Console.WriteLine($"Modified JSON file {UserEnrollmentScormInfoFileName} has been created successfully. {nbrRowsChanged} records.");

                // 825 docs
                Waitlist waitlist = new Waitlist();
                nbrRowsChanged = waitlist.Deidentify_Waitlist(filePath, WaitlistFileName);
                Console.WriteLine($"Modified JSON file {WaitlistFileName} has been created successfully. {nbrRowsChanged} records.");



                // the follwoing were developed but deemed not needed due to dropping of the collections

                ////////// 3,583 docs
                ////////CommerceCarts commerceCarts = new CommerceCarts();
                ////////nbrRowsChanged = commerceCarts.Deidentify_CommerceCarts(filePath, CommerceCartsFileName);
                ////////Console.WriteLine($"Modified JSON file {CommerceCartsFileName} has been created successfully. {nbrRowsChanged} records.");

                ////////// 872  docs 
                ////////EngageChatApiCalls engageChatApiCalls = new EngageChatApiCalls();
                ////////nbrRowsChanged = engageChatApiCalls.Deidentify_EngageChatApiCalls(filePath, EngageChatApiCallsFileName);
                ////////Console.WriteLine($"Modified JSON file {EngageChatApiCallsFileName} has been created successfully. {nbrRowsChanged} records.");

                ////////// 160 docs
                ////////EngageUsers engageUsers = new EngageUsers();
                ////////nbrRowsChanged = engageUsers.Deidentify_EngageUsers(filePath, EngageUsersFileName);
                ////////Console.WriteLine($"Modified JSON file {EngageUsersFileName} has been created successfully. {nbrRowsChanged} records.");

                ////////// 4512 docs
                ////////Leads leads = new Leads();
                ////////nbrRowsChanged = leads.Deidentify_Leads(filePath, LeadsFileName);
                ////////Console.WriteLine($"Modified JSON file {LeadsFileName} has been created successfully. {nbrRowsChanged} records.");

                //////////////// 130,135  docs Error in processing out of memory
                //////////////EngageChats engageChats = new EngageChats();
                //////////////nbrRowsChanged = engageChats.Deidentify_EngageChats(filePath, EngageChatsFileName);
                //////////////Console.WriteLine($"Modified JSON file {EngageChatsFileName} has been created successfully. {nbrRowsChanged} records.");

                ////////// 45134 docs Errors bringing down no file no rows in the file
                ////////CommercePendingOrders commercePendingOrders = new CommercePendingOrders();
                ////////nbrRowsChanged = commercePendingOrders.Deidentify_CommercePendingOrders(filePath, CommercePendingOrdersFileName);
                ////////Console.WriteLine($"Modified JSON file {CommercePendingOrdersFileName} has been created successfully. {nbrRowsChanged} records.");

                //////// 2165103  docs  Path collision at userCertificationCategories.11709 remaining portion 11709
                //////FitnessConnect fitnessConnect = new FitnessConnect();
                //////nbrRowsChanged = fitnessConnect.Deidentify_FitnessConnect(filePath, FitnessConnectFileName);
                //////Console.WriteLine($"Modified JSON file {FitnessConnectFileName} has been created successfully. {nbrRowsChanged} records.");

                //////////// 31 docs Errors bringing down collection 
                //////////PdfEdits pdfEdits = new PdfEdits();
                //////////nbrRowsChanged = pdfEdits.Deidentify_PdfEdits(filePath, PdfEditsFileName);
                //////////Console.WriteLine($"Modified JSON file {PdfEditsFileName} has been created successfully. {nbrRowsChanged} records.");





            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                //return (-1);
            }

        }
    }
}
