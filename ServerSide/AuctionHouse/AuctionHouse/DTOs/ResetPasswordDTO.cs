using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.DTOs
{
    public class ResetPasswordDTO
    {
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public Guid Token { get; set; }
    }
}
