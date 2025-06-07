﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IAB_251_Assessment2.BusinessLogic.Entities
{
    public class Quotation
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Client Name is required")]
        public string clientName { get; set; }
        
        public DateTime dateIssued { get; set; }
        
        public string? status { get; set; }

        //–– I4/I6 fields ––
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? CustomerDecision { get; set; }
        
        // Enhanced field for employee notes
        public string? Comments { get; set; }
        
        // Track who prepared the quotation
        public string? PreparedBy { get; set; }
        
        // Track when customer responded
        public DateTime? CustomerResponseDate { get; set; }
    }
}
