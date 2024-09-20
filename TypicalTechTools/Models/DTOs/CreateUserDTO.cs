using System.ComponentModel.DataAnnotations;

namespace TypicalTechTools.Models.DTOs
{
    public class CreateUserDTO
    {
        //Username Field
        [Required]
        [StringLength(10, ErrorMessage = "Username must be less than 10 characters!")]
        public string UserName { get; set; } = string.Empty;

        //Password Field
        [Required]
        //Uses a regex pattern to enforce password complexity rules, including at least one digit, one uppercase letter,
        //one lowercase letter, and one special character. Also restricts the password length to between 8 and 12 characters.
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&,?_-])[A-Za-z0-9*.!@$%^&,?_-].{8,12}$"
            , ErrorMessage = "Password Must contain: at least 1 number, 1 uppercase letter " +
            "and 1 lowercase letter and one special character!")]
        public string Password { get; set; } = string.Empty;

        //Password Confirmation (must match with password) this field doesnt require regex
        [Required]
        public string PasswordConfirmation { get; set; } = string.Empty;

        //Email field
        [Required]
        //Validates the email address format using a complex regex pattern to ensure it conforms to standard email formatting rules.
        [RegularExpression("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])"
            , ErrorMessage = "Email Adress is not in a valid format!")]
        public string Email { get; set; } = string.Empty;

    }
}
