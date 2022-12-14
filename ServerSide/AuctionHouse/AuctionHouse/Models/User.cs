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
        public bool IsAdmin { get; set; } = false;       
        public float Balance { get; set; } = 0;
        [InverseProperty("Author")]
        public virtual ICollection<Item> AuthoredItems { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
        }
    }
}