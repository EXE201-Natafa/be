using Natafa.Api.Models.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Models.ShippingAddressModel
{
    public class ShippingAddressRequest
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [PhoneValidation(ErrorMessage = "Phone Number is invalid")]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public bool IsDefault { get; set; }
    }
}
