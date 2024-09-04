using System.ComponentModel.DataAnnotations;

namespace RentalQuotationApp.Models
{
    public class CostComponent
    //{
    //    public int Id { get; set; }
    //    public int QuotationId { get; set; }
    //    public string Description { get; set; }
    //    public decimal Cost { get; set; }

    //    public Quotation Quotation { get; set; }
    //}
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int QuotationId { get; set; }

        public Quotation Quotation { get; set; }
    }
}
