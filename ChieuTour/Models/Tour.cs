namespace ChieuTour.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        [Required]
        [StringLength(500)]
        public string TenTour { get; set; }

        [Required]
        public string MoTa { get; set; }

        [Required]
        public string LichTrinh { get; set; }

        [Column(TypeName = "money")]
        public decimal GiaNL { get; set; }

        [Column(TypeName = "money")]
        public decimal GiaTE { get; set; }

        [Required]
        [StringLength(200)]
        public string PhuongTien { get; set; }

        [Required]
        [StringLength(500)]
        public string Anh { get; set; }

        public int MaDanhMuc { get; set; }

        public int GiamGia { get; set; }

        [Required]
        [StringLength(50)]
        public string HoatDong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
