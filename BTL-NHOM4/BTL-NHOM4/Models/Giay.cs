namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Giay")]
    public partial class Giay
    {
        [Key]
        public int MaGiay { get; set; }

        public int MaDanhMuc { get; set; }

        [StringLength(200)]
        public string TenGiay { get; set; }

        [StringLength(20)]
        public string Mau { get; set; }

        [StringLength(20)]
        public string KichThuoc { get; set; }

        public float? Gia { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(100)]
        public string Anh { get; set; }

        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }
    }
}
