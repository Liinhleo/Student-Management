using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMOMANAGEMENT
{
    class SINHVIEN // dat ten class theo table SINHVIEN de anh xa lai toan bo thuoc tinh trong SQL Ser 
    {
        // tao properties cho ca thuoc tinh nay
        public string Id { set;  get; }
        public string HoTen { set; get; }
        public string MSSV { set; get; }
        public string Lop { set; get; }
        public string NgaySinh { set; get; }
        public string GioiTinh { set; get; }
        public string SDT { set; get; }
        public string Mail { set; get; }
        public int MaVung { set; get; }
    }
}
