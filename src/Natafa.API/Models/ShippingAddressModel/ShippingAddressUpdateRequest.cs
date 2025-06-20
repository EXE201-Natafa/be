using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Models.ShippingAddressModel
{
    public class ShippingAddressUpdateRequest
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public bool IsDefault { get; set; }
    }
}
