namespace ChieuTour.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        public int MaTinTuc { get; set; }

        [StringLength(200)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        [StringLength(200)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        public int? MaNguoiDung { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
