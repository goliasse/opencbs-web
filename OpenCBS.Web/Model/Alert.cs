namespace OpenCBS.Web.Model
{
    public class Alert
    {
        public string ContractCode { get; set; }
        public int LateDays { get; set; }
        public string ClientName { get; set; }
        public decimal Amount { get; set; }
        public string LoanOfficer { get; set; }
        public string BranchName { get; set; }
    }
}
