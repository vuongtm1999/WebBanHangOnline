using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_CodeDiscount")]
    public class CodeDiscount : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string code { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int isUsed { get; set; }

        public int Discount { get; set; }

        public virtual Customer Customer { get; set; }

    }
}