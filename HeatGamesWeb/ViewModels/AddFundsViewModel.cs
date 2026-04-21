using System.ComponentModel.DataAnnotations;

namespace HeatGamesWeb.ViewModels
{
    public class AddFundsViewModel
    {
        [Required(ErrorMessage = "The amount is required.")]
        [Range(5, 1000, ErrorMessage = "You can add between 5 and 1000 units.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter the cardholder name.")]
        [Display(Name = "Name on Card")]
        public string CardHolderName { get; set; } = null!;

        [Required(ErrorMessage = "The card number is required.")]
        [CreditCard(ErrorMessage = "Invalid credit card format.")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the expiration date.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Format must be MM/YY")]
        [Display(Name = "Expiration Date (MM/YY)")]
        public string ExpiryDate { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the CVV code.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV code must be 3 digits.")]
        [Display(Name = "CVV")]
        public string Cvv { get; set; } = null!;
    }
}