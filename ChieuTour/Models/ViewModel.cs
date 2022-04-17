using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChieuTour.Models
{
    public class ViewModel
    {
        public NguoiDung nguoidung { get; set; }
        public ChiTietDonHang ctdh { get; set; }
        public DonHang donhang { get; set; }
        public Tour tour { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##0}")]
        public double thanhTien { get; set; }
    }
}