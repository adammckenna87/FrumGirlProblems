using Braintree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheChesedProject.Models
{
    public class CheckOutViewModel
    {

        public Cart Cart { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string ShippingAddressLine1 { get; set; }

        [Required]
        [Display(Name = "Address Line 2")]
        public string ShippingAddressLine2 { get; set; }

       
        [Required]
        [Display(Name = "Address Line 1")]
        public string BillingAddressLine1 { get; set; }

        [Required]
        [Display(Name = "Address Line 2")]
        public string BillingAddressLine2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        public string ShippingZipcode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }

        
        [Display(Name = "Locale")]
        public string ShippingLocale { get; set; }

        
        [Display(Name = "Region")]
        public string ShippingRegion { get; set; }

        [Required]
        [Display(Name = "Name On Card")]
        public string NameOnCard { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiry Month")]
        public string BillingCardExpirationMonth { get; set; }

        [Required]
        [Display(Name = "Expiry Year")]
        public string BillingCardExpirationYear { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public string CVV { get; set; }

        [Display(Name = "Save Credit Card")]
        public bool SaveBillingCard { get; set; }

        [Display(Name = "Save Shipping Address")]
        public bool SaveShippingAddress { get; set; }
        public CreditCard[] CreditCards { get; set; }
        public Address[] Addresses { get; set; }

        public string SavedAddressId { get; set; }


    }
}
