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
        public CodeDiscount()
        {
            this.Customers = new HashSet<Customer>();
            this.Orders = new HashSet<Order>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string code { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int isActive { get; set; }

        public float? Discount { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}