using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }      
        [Required]
        public string FirstName { get; set; }       
        [Required]
        public string LastName { get; set; }        
        [Required]
        public string Username { get; set; }        
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required] 
        public string PhoneNumber { get; set; }
        public float Balance { get; set; } = 0;
        public Role Role { get; set; } = Role.User;
        public Guid VerificationToken { get; set; } = Guid.NewGuid();
        public bool IsVerified { get; set; } = false;
        public DateTime? VerifiedAt { get; set; } = null;
        public Guid? PasswordResetToken { get; set; } = null;
        public DateTime? PasswordResetTokenExpires { get; set; } = null;

        [InverseProperty("Author")]
        public virtual ICollection<Item> AuthoredItems { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
        }
    }
}