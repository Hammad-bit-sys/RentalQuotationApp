namespace RentalQuotationApp.ViewModels
{
    public class QuotationViewModel
    {
        // Properties for the quotation details
        public string Company { get; set; }
        public string Branch { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public DateTime QuotationExpiry { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public string QuotationNumber { get; set; }
        public int NumberOfVehicles { get; set; }
        public decimal TotalRentalSum { get; set; }
        public decimal TotalAdditionalCost { get; set; }
        public string ApprovalStatus { get; set; }

        // List of cost components related to the quotation
        public List<CostComponentViewModel> CostComponents { get; set; } = new List<CostComponentViewModel>();
    }

    public class CostComponentViewModel
    {
        // Properties for each cost component
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
