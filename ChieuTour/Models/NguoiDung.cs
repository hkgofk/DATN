namespace ChieuTour.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DonHangs = new HashSet<DonHang>();
            TinTucs = new HashSet<TinTuc>();
        }

        [Key]
        public int MaNguoiDung { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tài khoản")]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(100)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        public int? IdQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
