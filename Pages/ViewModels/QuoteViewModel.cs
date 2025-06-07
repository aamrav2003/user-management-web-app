using System;
using System.ComponentModel.DataAnnotations;

namespace IAB_251_Assessment2.Pages.ViewModels
{
    public class QuoteViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter the client name")]
        [Display(Name = "Client Name")]
        public string clientName { get; set; }
        
        [Display(Name = "Date Issued")]
        public DateTime dateIssued { get; set; }

        [Display(Name = "Status")]
        public string? status { get; set; }

        //–– I4/I6 fields ––
        [Display(Name = "Rate")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Rate { get; set; }

        [Display(Name = "Discount")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public decimal Discount { get; set; }

        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Customer Decision")]
        public string? CustomerDecision { get; set; }
    }
}
