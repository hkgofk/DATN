namespace ChieuTour.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Tour")]
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        public int MaTour { get; set; }

        [Required(ErrorMessage = "Tên tour không được để trống!")]
        [DisplayName("Tên tour")]
        [StringLength(500)]
        public string TenTour { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống!")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Lịch trình không được để trống!")]
        [DisplayName("Lịch trình")]
        public string LichTrinh { get; set; }

        [Required(ErrorMessage = "Giá người lớn không được để trống!")]
        [DisplayName("Giá người lớn")]
        [Column(TypeName = "money")]
        [Range(0, 1000000000, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal GiaNL { get; set; }

        [Required(ErrorMessage = "Giá trẻ em không được để trống!")]
        [DisplayName("Giá trẻ em")]
        [Column(TypeName = "money")]
        [Range(0, 1000000000, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal GiaTE { get; set; }

        [Required(ErrorMessage = "Phương tiện không được để trống!")]
        [DisplayName("Phương tiện")]
        [StringLength(200)]
        public string PhuongTien { get; set; }

        [DisplayName("Ảnh")]
        [StringLength(500)]
        public string Anh { get; set; }

        [DisplayName("Danh mục tour")]
        public int MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Giảm giá không được để trống!")]
        [DisplayName("Giảm giá")]
        [Range(0, 100, ErrorMessage = "Giảm giá 0 - 100")]

        public int GiamGia { get; set; }

        [Required(ErrorMessage = "Hoạt động không được để trống!")]
        [DisplayName("Hoạt động")]
        [StringLength(50)]
        public string HoatDong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
