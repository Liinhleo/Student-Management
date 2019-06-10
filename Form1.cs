using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DEMOMANAGEMENT
{
    public partial class Form1 : Form
    {

        // Truy cập vào lớp Bussiness Layer
        BussinessLayer BL;
 

        public Form1()
        {
            InitializeComponent();
            BL = new BussinessLayer();
        }


        // PHương thức lấy dữ liệu từ Database vào FORM 1 

        public void ShowAllSV()
        {
           DataTable dataT = BL.getAllSV();
           DataGridview1.DataSource = dataT;
        }

        private void form1_Load(object sender, EventArgs e)
        {
            ShowAllSV();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        //Đem dữ liệu từ Datagridview lên phần THÔNG TIN SINH VIÊN
        private void DataGridview1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnMiss.Enabled = false;

            if (e.RowIndex >= 0)
            {

                //Lấy tất cả các thông tin từ 1 hàng trong bảng datagridview
                DataGridViewRow row = this.DataGridview1.Rows[e.RowIndex];
                //điền data vào tọa độ của cột và hàng.

                int ID = Convert.ToInt32(row.Cells[8].Value.ToString());

                txtHoTen.Text = row.Cells[0].Value.ToString();
                txtMSSV.Text = row.Cells[1].Value.ToString();
                txtLop.Text = row.Cells[2].Value.ToString();
                txtSDT.Text = row.Cells[5].Value.ToString();
                txtMail.Text = row.Cells[6].Value.ToString();
                cbbMaVung.Text = row.Cells[7].Value.ToString();

                if (row.Cells[4].Value.ToString() == "Nam")
                {
                    radioButton1.Checked = true;
                }
                if (row.Cells[4].Value.ToString() == "Nữ")
                {
                    radioButton1.Checked = true;
                }

                DateTime NgaySinh = DateTime.Parse(row.Cells[3].Value.ToString());
                dtpNgaySinh.Format = DateTimePickerFormat.Custom; // Chỉnh sửa format của Hiển thị ngày sinh
                dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            }
        }


        // Kiểm tra dữ liệu còn trống hay k 
        public bool CheckData()
        {
            string s = "";
            if (string.IsNullOrEmpty(txtHoTen.Text)|| string.IsNullOrEmpty(txtMSSV.Text)|| 
                string.IsNullOrEmpty(txtLop.Text)|| string.IsNullOrEmpty(dtpNgaySinh.Text) || 
                string.IsNullOrEmpty(cbbMaVung.Text) || string.IsNullOrEmpty(txtSDT.Text) || 
                string.IsNullOrEmpty(txtMail.Text) || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                if (string.IsNullOrEmpty(txtHoTen.Text))
                {
                    s = s + "Tên, ";
                }
                if (string.IsNullOrEmpty(txtMSSV.Text))
                {
                    s = s + "MSSV, ";
                }
                if (string.IsNullOrEmpty(txtLop.Text))
                {
                    s = s + "Lớp, ";
                }
                if (string.IsNullOrEmpty(dtpNgaySinh.Text))
                {
                    s = s + "Ngày Sinh, ";
                }

               /* if (string.IsNullOrEmpty(......Text))
                {
                    s = s + "Giới Tính, ";
                } */

                if (string.IsNullOrEmpty(cbbMaVung.Text))
                {
                    s = s + "Mã Vùng, ";
                }
                if (string.IsNullOrEmpty(txtSDT.Text))
                {
                    s = s + "Số điện thoại, ";
                }
                if (string.IsNullOrEmpty(txtMail.Text))
                {
                    s = s + "Email, ";
                }

                MessageBox.Show("Nhập lại  "+s, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            return true;
        }


// ------------------- XỬ LÝ CÁC RÀNG BUỘC

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SINHVIEN sv = new SINHVIEN();
            sv.GioiTinh = "Nam";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SINHVIEN sv = new SINHVIEN();
            sv.GioiTinh = "Nữ";
        }

        private void txtMail_Leave(object sender, EventArgs e) // ràng buộc mail
        {
            string mailformat = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            if (Regex.IsMatch(txtMail.Text, mailformat))
            {

                errorMail.Clear();
            }
            else
            {
                errorMail.SetError(this.txtMail, "Nhập lại địa chỉ mail đúng!");
                return;
            }

        }

        
        private void txtSDT_Leave_1(object sender, EventArgs e) //ràng buộc SĐT
        {
            string SDTformat = @"\[1-9]{2}\s+[0-9]{3}\s+[0-9]{4}";

            //THIẾU :: Loại số 0 ở đầu và chuỗi dài 9 chữ số

            if (Regex.IsMatch(txtSDT.Text, SDTformat))
            {
                errorPhone.Clear();
            }
            else
            {
                errorPhone.SetError(this.txtSDT, "Nhập lại số điện thoại đúng!");
                return;
            }
        }

       // THIẾU :: VIẾT HOA CHỮ CÁI ĐẦU





 // ------------------------------------------------------------
        // TẠO EVENT CHO NÚT THÊM DỮ LIỆU
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnMiss.Enabled = false;
            btnSave.Enabled = true;

            cleardata();
            txtHoTen.Enabled = true;
            txtHoTen.Focus();
        }
        
        private void RefreshData()  // Hàm load lại Database bên SQL Server V
        {
            DataTable Dt = BL.Refresh();
            DataGridview1.DataSource = Dt;
        }

        private void cleardata() // Xóa dữ liệu 
        {
            txtHoTen.Text ="";
            txtMSSV.Text = "";
            txtLop.Text = "";
            dtpNgaySinh.Text = "";
            cbbMaVung.Text = "";
            txtSDT.Text = "";
            txtMail.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        // NÚT LƯU DỮ LIỆU
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == false)
            {
                return;
            }
            SINHVIEN sv = new SINHVIEN();
            sv.HoTen = txtHoTen.Text;
            sv.MSSV = txtMSSV.Text;
            sv.Lop = txtLop.Text;
            sv.SDT = txtSDT.Text.ToString();
            sv.Mail = txtMail.Text;

            sv.NgaySinh = dtpNgaySinh.Value.ToString();

            sv.MaVung = int.Parse(cbbMaVung.Text.ToString());

            if (radioButton1.Checked == true)
                sv.GioiTinh = "Nam";
            else if (radioButton2.Checked == true)
                sv.GioiTinh = "Nữ";


            if (BL.InsertSV(sv))
            {
                MessageBox.Show("Thêm thành công");
                RefreshData();
                cleardata();
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }


        // TẠO EVENT CHO NÚT SỬA DỮ LIỆU
        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnMiss.Enabled = true;

            //txtHoTen.Enabled = true;
            //txtMSSV.Enabled = true;
            //txtLop.Enabled = true;
            //dtpNgaySinh.Enabled = true;
            //radioButton1.Enabled = true;
            //radioButton2.Enabled = true;
            //cbbMaVung.Enabled = true;
            //txtSDT.Enabled = true;
            //txtMail.Enabled = true;


            SINHVIEN sv = new SINHVIEN();
            sv.HoTen = txtHoTen.Text;
            sv.MSSV = txtMSSV.Text;
            sv.Lop = txtLop.Text;
            sv.SDT = txtSDT.Text.ToString();
            sv.Mail = txtMail.Text;

            sv.NgaySinh = dtpNgaySinh.Value.ToString();

            sv.MaVung = int.Parse(cbbMaVung.Text.ToString());

            if (radioButton1.Checked == true)
                sv.GioiTinh = "Nam";
            else if (radioButton2.Checked == true)
                sv.GioiTinh = "Nữ";


            if (BL.UpdateSV(sv))
            {
                MessageBox.Show("Sửa Thành Công!");
                RefreshData();
                cleardata();
            }
            else
            {
                MessageBox.Show("ERROR!");
            }
        }

        // NÚT XÓA DỮ LIỆU
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (DataGridview1.Rows.Count!=0)
            {
                if (MessageBox.Show("Bạn có muốn xoá dòng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SINHVIEN sv = new SINHVIEN();
                    sv.MSSV = txtMSSV.ToString();
                    if (BL.DeleteSV(sv))
                    {
                        RefreshData();
                        cleardata();
                        DataTable dataT = BL.getAllSV();
                        DataGridview1.DataSource = dataT;
                    }
                    else
                        MessageBox.Show("Đã có lỗi xảy ra", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            } 
        }

        // NÚT BỎ QUA
        private void btnMiss_Click(object sender, EventArgs e)
        {
            cleardata();
            btnMiss.Enabled = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            txtHoTen.Enabled = false;
        }

       
    }

}


/*
  
 * NÚT THÊM : EM ĐÃ SỬA LẠI RỒI ĐÓ 
 + Khi nhấn Thêm thì clear hết các trường dữ liệu hiện có, cho phép nhập liệu, con trỏ chuột focus đến ô nhập liệu của trường Họ tên, 
 + nút Thêm chuyển thành Lưu (hoặc ẩn nút Thêm đi hiện nút Lưu lên), Ẩn/ Tắt/ Khóa sự kiện của Nút Sửa và Xóa

 *NÚT SỬA 
+ Khi nhấn Sửa thì cho mở để thao tác thay đổi nội dung các ô nhập liệu, Nút Sửa chuyển thành Lưu (hoặc ẩn nút Sửa đi hiện nút Lưu lên), 
+ Ẩn/ Tắt/ Khóa sự kiện của nút Thêm và Xóa

  *NÚT XÓA
+ Khi nhấn Xóa thì hiện hộp thoại hỏi xem bạn có chắc chắn muốn xóa hay không? Chọn Yes để xóa, No không xóa. 
+ Khi xóa thì cập nhật dữ liệu vào bảng Danh sách sinh viên đồng thời lưu dữ liệu xuống file

  *NÚT LƯU: lưu dữ liệu xuống file
+ Khi nhấn Lưu: kiểm tra các trường dữ liệu nhập có null không? Nếu có thì báo lỗi và focus vào trường lỗi đầu tiên phát hiện (thứ tự từ trên xuống dưới từ trái qua phải). 
+ Nếu không có lỗi thì lưu dữ liệu và hiển thị trên bảng Danh sách sinh viên

 
*/
