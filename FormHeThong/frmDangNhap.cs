using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOAN_QLCHTL.Models;
namespace DOAN_QLCHTL.FormHeThong
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        DBquanly quanlys;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            quanlys = new DBquanly();


            var tkhoan = quanlys.TaiKhoans.FirstOrDefault(p => p.TaiKhoan1 == txtTenDangNhap.Text && p.matkhau == txtMatKhau.Text);

            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Tài Khoản", "Thông Báo");

            }
            else if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Mật Khẩu", "Thông Báo");

            }
            else
            {
                if (tkhoan == null)
                {
                    MessageBox.Show("Đăng Nhập Thất Bại !", "Thông Báo");
                }
                else
                {
                    MessageBox.Show("Bạn Đã Đăng Nhập Thành Công !", "Thông Báo");
                    frmMain.idnhanvien = tkhoan.MaNV;
                    frmMain.chucvuNV = tkhoan.NhanVien.ChucVu.TenCV;
                    frmMain.tenNV = tkhoan.NhanVien.TenNV;
                    frmMain frm = new frmMain();
                    frm.DangXuat += Frm_DangXuat;
                    frm.Show();
                    this.Hide();
                }
            }
        }

        private void Frm_DangXuat(object sender, EventArgs e)
        {
            (sender as frmMain).IsThoat = false;
            (sender as frmMain).Close();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}