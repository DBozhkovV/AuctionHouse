using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }        
        [Required] 
        public string Name { get; set; }        
        [Required] 
        public string Description { get; set; }
        [Required]
        public float BuyPrice { get; set; }       
        [Required]
        public float StartingPrice { get; set; }     
        public float Bid { get; set; } = 0;  
        [Required]
        public DateTime DateAdded { get; set; }       
        [Required]
        public DateTime StartingBidDate { get; set; }  
        [Required]
        public DateTime EndBidDate { get; set; }    
        public float BoughtFor { get; set; } = 0;   
        public bool IsAvailable { get; set; } = true;
        public bool IsAccepted { get; set; } = false;
        public string? MainImageName { get; set; } = null;
        public List<string>? ImagesNames { get; set; } = null; 
        public Guid AuthorUserId { get; set; }
        [ForeignKey("AuthorUserId")]
        public virtual User Author { get; set; }
        [InverseProperty("Item")]
        public virtual Order Order { get; set; }
        
        public Item()
        {
        }
    }
}