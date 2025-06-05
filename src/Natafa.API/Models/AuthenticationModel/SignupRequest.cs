
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Natafa.Api.Models.AuthenticationModel
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Birthday is required")]
        //[BirthdayValidation(18, 100, ErrorMessage = "Date of birth out of range")]   
        public DateOnly? Birthday { get; set; }

        //[Required(ErrorMessage = "Phone number is required")]
        [PhoneValidation(ErrorMessage = "Phone Number is invalid")]        
        public string? PhoneNumber { get; set; }               
    }

    public class BirthdayValidation : ValidationAttribute
    {
        private int _minAge;
        private int _maxAge;
        public BirthdayValidation(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }
        public override bool IsValid(object? value)
        {
            if (value is DateOnly)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                int age = today.Year - ((DateOnly)value).Year;

                // Adjust the age if the birthday hasn't occurred yet this year.
                if ((DateOnly)value > today.AddYears(-age))
                {
                    age--;
                }
                if (age >= _minAge && age <= _maxAge)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class PhoneValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true; // Không bắt buộc, nếu không có giá trị thì coi là hợp lệ
            string phoneNumber = value.ToString();
            string pattern = @"^\+?[0-9\s\-\(\)]{7,15}$"; // Số điện thoại hợp lệ (7-15 ký tự)
            return Regex.IsMatch(phoneNumber, pattern);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string phoneNumber = value.ToString();
            string pattern = @"^\+?[0-9\s\-\(\)]{7,15}$"; // Số điện thoại hợp lệ (7-15 ký tự)

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                return new ValidationResult("Số điện thoại không hợp lệ.");
            }

            return ValidationResult.Success;
        }
    }
}
