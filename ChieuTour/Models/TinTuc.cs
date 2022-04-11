namespace ChieuTour.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        public int MaTinTuc { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [DisplayName("Tiêu đề")]
        [StringLength(200)]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        [DisplayName("Nội dung")]
        [AllowHtml]
        public string NoiDung { get; set; }

        [StringLength(200)]
        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày đăng")]
        public DateTime? NgayDang { get; set; }

        public int? MaNguoiDung { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
