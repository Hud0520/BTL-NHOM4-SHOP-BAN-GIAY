namespace BTL_ASP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        [DisplayName("Mã tin tức")]
        public int MaTinTuc { get; set; }

        [StringLength(250)]
        [DisplayName("Tiêu đề")]
        public string TieuDe { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Ngày sửa")]
        public DateTime? NgaySua { get; set; }

        [StringLength(250)]
        [DisplayName("Đường dẫn")]
        public string link { get; set; }

        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [StringLength(50)]
        [DisplayName("Ảnh tiêu đề")]
        public string AnhTieuDe { get; set; }

        [StringLength(50)]
        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }
    }
}
