using System;

namespace IAB_251_Assessment2.Pages.ViewModels
{
    public class QuoteViewModel
    {
        public int Id { get; set; }
        public string clientName { get; set; }
        public DateTime dateIssued { get; set; }
        public string status { get; set; }

        //–– I4/I6 fields ––
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerDecision { get; set; }
    }
}
