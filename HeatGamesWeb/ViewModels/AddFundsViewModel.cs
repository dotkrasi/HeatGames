using System.ComponentModel.DataAnnotations;

namespace HeatGamesWeb.ViewModels
{
    public class AddFundsViewModel
    {
        [Required(ErrorMessage = "Сумата е задължителна.")]
        [Range(5, 1000, ErrorMessage = "Може да добавите между 5 и 1000 лв.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Въведете име на картодържател.")]
        [Display(Name = "Име върху картата")]
        public string CardHolderName { get; set; } = null!;

        [Required(ErrorMessage = "Номерът на картата е задължителен.")]
        [CreditCard(ErrorMessage = "Невалиден формат на кредитна карта.")]
        [Display(Name = "Номер на карта")]
        public string CardNumber { get; set; } = null!;

        [Required(ErrorMessage = "Въведете валидност.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Форматът трябва да е ММ/ГГ")]
        [Display(Name = "Валидност (ММ/ГГ)")]
        public string ExpiryDate { get; set; } = null!;

        [Required(ErrorMessage = "Въведете CVV код.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV кодът трябва да е 3 цифри.")]
        [Display(Name = "CVV")]
        public string Cvv { get; set; } = null!;
    }
}