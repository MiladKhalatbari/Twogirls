using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.DataLayer.Entities
{
    public class UserDiscountCodes
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }

        #region Relation
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("DiscountId")]
        public DiscountCode  DiscountCode { get; set; }
        #endregion

    }
}
