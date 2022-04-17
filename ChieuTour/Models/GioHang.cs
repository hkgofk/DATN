using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChieuTour.Models
{
    public class GioHang
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        public int maTour { get; set; }
        public string tenTour { get; set; }
        public string anh { get; set; }
        public double giaTE { get; set; }
        public double giaNL { get; set; }
        public int soLuongTE { get; set; }
        public int soLuongNL { get; set; }
        public double thanhTien { get { return soLuongNL * giaNL + soLuongTE * giaTE; }}
        public GioHang(int maTour)
        {
            this.maTour = maTour;
            Tour tour = db.Tours.Single(t => t.MaTour == this.maTour);
            anh = tour.Anh;
            if(tour.GiamGia != 0)
            {
                giaNL = (float)tour.GiaNL * (1 - (float)tour.GiamGia/100);
                giaTE = (float)tour.GiaTE * (1 - (float)tour.GiamGia/100);
            }
            else
            {
                giaNL = double.Parse(tour.GiaNL.ToString());
                giaTE = double.Parse(tour.GiaTE.ToString());
            }
            
            tenTour = tour.TenTour;
            soLuongNL = 1;
            soLuongTE = 0;
        }

    }
}