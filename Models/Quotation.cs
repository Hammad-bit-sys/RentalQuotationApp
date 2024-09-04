using System.ComponentModel.DataAnnotations;

namespace RentalQuotationApp.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Quotation Number cannot be longer than 50 characters.")]
        public string QuotationNumber { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public string CustomerCategory { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public DateTime QuotationExpiry { get; set; }
     

        [Required]
        public DateTime RentalStartDate { get; set; }

        [Required]
        public DateTime RentalEndDate { get; set; }

        [Required]
        public int NumberOfVehicles { get; set; }

        [Required]
        public decimal TotalRentalSum { get; set; }

        [Required]
        public decimal TotalAdditionalCost { get; set; }

        [Required]
        public string ApprovalStatus { get; set; }


        public ICollection<CostComponent> CostComponents { get; set; } = new List<CostComponent>();
    }
}
