using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMOMANAGEMENT
{
   
    class DataAccessLayer  // Tạo lớp quản lý sinh viên
    {
        DataConnection dataC;         // lấy lại kết nối trong dataconnection
        SqlDataAdapter dataA;          // lấy tất cả các bảng trong csdl -> ***phuong thuc  : Fill -> Fill data vào class DataTable
        SqlCommand Cmd;

        public DataAccessLayer()
        {
            // Hàm tạo để lấy biến từ DataConnect
            dataC = new DataConnection();
        }

// PHƯƠNG THỨC :: Lấy danh sách SV tu SQL Server sang Winform 

        public DataTable getAllSV () 
        {
            //B1: Tạo câu lệnh bằng SQL để lấy toàn bộ ds sv
            string sql = "SELECT * FROM sinhvien";

            //B2: Tạo kết nối tới SQL Ser
            SqlConnection con = dataC.getConnect();             //Gọi hàm getConnect từ DataConnection


            //B3: Khởi tạo đối tượng của lớp SqlDataAdapter -> Khai bao SqlDataAdapter là một biến toàn cục (truyền câu lệnh SQL và 1 chuỗi kết nối)
            dataA = new SqlDataAdapter(sql, con);            


            //B4: Mở kết nối (mở path để truyền và lấy dữ liệu từ SQL ser
            con.Open();                                       


            //B5: Fill data tu SqlDataAdapter vao DataTable 
            DataTable dataT = new DataTable();                  
            dataA.Fill(dataT);                                  


            //B6: Đóng kết nối
            con.Close(); 
            return dataT;                                      //cần trả về datatable -> chứa các data (dataA.Fill) mà khi ta  SELECT * FROM SV
        }



// PHUONG THUC :: Them du lieu -> tra gia tri bool ( success: True / failed: False)
    
        public bool InsertSV (SINHVIEN sv)                      //chua toan bo info SV can them
        {
            string sql = "INSERT INTO sinhvien(HoTen,MaSo,Lop,NgaySinh,GioiTinh,MaVung,SoDienThoai,Mail) VALUES (@HoTen,@MaSo,@Lop,@NgaySinh,@GioiTinh,@MaVung,@SoDienThoai,@Mail)";  //khai bao du lieu kieu chuoi 
            
            SqlConnection con = dataC.getConnect(); // Tao ket noi den sql = ham getConnect cua lop DataConnection

            try
            {
                // de them sua xoa -> class SqlCommand
                Cmd = new SqlCommand(sql, con); 

                con.Open();
                // khi mo ket noi thi thu vien se co error -> them debug loi -> nhung cau lenh phat sinh loi se de vo ham try{} catch

                Cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = sv.HoTen;
                Cmd.Parameters.Add("@MaSo", SqlDbType.VarChar).Value = sv.MSSV;
                Cmd.Parameters.Add("@Lop", SqlDbType.VarChar).Value = sv.Lop;
                Cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = sv.NgaySinh;
                Cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = sv.GioiTinh;
                Cmd.Parameters.Add("@MaVung", SqlDbType.Char).Value = sv.MaVung;
                Cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar).Value = sv.SDT;
                Cmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = sv.Mail;
                Cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception e) // neu error -> k the add data tu database ->return false
            {
                return false;
            }
            return true;
        }




 // PHUONG THUC :: Cap nhat du lieu -> tra gia tri bool ( success: True / failed: False)

        public bool UpdateSV(SINHVIEN sv)                      //chua toan bo info SV can them
        {
            string sql = "UPDATE SINHVIEN SET HoTen= @hoTen,MSSV=@MSSV,Lop=@Lop ,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,SDT=@SDT,Mail=@Mail WHERE Id=@Id)";  //khai bao du lieu kieu chuoi 

            SqlConnection con = dataC.getConnect(); // Tao ket noi den sql = ham getConnect cua lop DataConnection

            try
            {
                // de them sua xoa -> class SqlCommand
                Cmd = new SqlCommand(sql, con);

                con.Open();
                // khi mo ket noi thi thu vien se co error -> them debug loi -> nhung cau lenh phat sinh loi se de vo ham try{} catch

                Cmd.Parameters.Add("@Id", SqlDbType.Char).Value = sv.Id;
                Cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = sv.HoTen;
                Cmd.Parameters.Add("@MSSV", SqlDbType.VarChar).Value = sv.MSSV;
                Cmd.Parameters.Add("@Lop", SqlDbType.VarChar).Value = sv.Lop;
                Cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = sv.NgaySinh;
                Cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = sv.GioiTinh;
                Cmd.Parameters.Add("@MaVung", SqlDbType.Char).Value = sv.MaVung;
                Cmd.Parameters.Add("@SDT", SqlDbType.VarChar).Value = sv.SDT;
                Cmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = sv.Mail;
                Cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception e) // neu error -> k the add data tu database ->return false
            {
                return false;
            }
            return true;
        }

        //ham nay co chuc nang refresh database o datagridview
      public DataTable DisplayData()
        {
            //B1: Tao cau lenh = ngon ngu SQL de lay toan bo data SV
            string sql = "SELECT * FROM sinhvien";               //khai bao du lieu kieu chuoi 

            //B2: Tao mot ket noi den sql 
            SqlConnection con = dataC.getConnect();             //Goi ham GetConnect trong lop DataConnection


            //B3: khOI tao doi tuong cua lop SqlDataAdapter -> Khai bao SqlDataAdapter la mot bien toan cuc
            dataA = new SqlDataAdapter(sql, con);               // trong SqlDataAdapt truyen cau lenh SQL va chuoi ket noi vao


            //B4: Mo ket noi
            con.Open();                                         // mo path de truyen va lay data tu SQL Ser ***


            //B5: Fill data tu SqlDataAdapter vao DataTable 
            DataTable dataT = new DataTable();                  //khai bao bien DataTable
            dataA.Fill(dataT);                                  // Dua all data tu bang CSDL vao DataTable


            //B6: Dong ket noi
            con.Close();
            return dataT;


        }

        //PHUONG THUC : Xoa du lieu SV
        public bool DeleteSV(SINHVIEN sv)                      //chua toan bo info SV can them
        {
            string sql = "DELETE SINHVIEN WHERE Id=@Id)"; 

            SqlConnection con = dataC.getConnect(); // Tao ket noi den sql = ham getConnect cua lop DataConnection

            try
            {
                // de them sua xoa -> class SqlCommand
                Cmd = new SqlCommand(sql, con);

                con.Open();
                // khi mo ket noi thi thu vien se co error -> them debug loi -> nhung cau lenh phat sinh loi se de vo ham try{} catch

                Cmd.Parameters.Add("@Id", SqlDbType.Char).Value = sv.Id;
                Cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception e) // neu error -> k the add data tu database ->return false
            {
                return false;
            }
            return true;
        }
    }
}
