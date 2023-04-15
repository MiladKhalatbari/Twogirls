using Microsoft.Build.Framework;

namespace twoGirlsOnlineShop.Models
{
    public class ImagePath
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
