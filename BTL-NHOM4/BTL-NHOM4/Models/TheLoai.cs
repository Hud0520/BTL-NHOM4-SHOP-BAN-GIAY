namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoai")]
    public partial class TheLoai
    {
        [Key]
        public int MaDanhMuc { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgaySua { get; set; }

        [StringLength(50)]
        public string TenTheLoai { get; set; }
    }
}
