using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
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

        #region Relation
        public Product? Product { get; set; }
        public User? User { get; set; }
        #endregion
    }
}
