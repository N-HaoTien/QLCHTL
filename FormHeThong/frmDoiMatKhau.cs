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
namespace DOAN_QLCHTL
{
    public partial class frmDoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        DBquanly dBquanly;
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Đăng Nhập !", "Thông Báo");
            }
            else if (txtMatKhauCu.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Mật Khẩu Cũ !", "Thông Báo");
            }
            else if (txtMatKhauMoi.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Mật Khẩu Mới !", "Thông Báo");
            }
            else if (txtMatKhauXacNhan.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Mật Khẩu Xác Nhận !", "Thông Báo");
            }
            else
            {
                dBquanly = new DBquanly();

                var taikhoan = dBquanly.TaiKhoans.FirstOrDefault(p => p.TaiKhoan1 == txtTenDangNhap.Text || p.matkhau == txtMatKhauCu.Text);

                if (taikhoan == null)
                {
                    MessageBox.Show("Tên Đăng Nhập Hoặc Mật Khẩu Cũ của bạn sai !", "Thông Báo");

                }
                else
                {
                    if (txtMatKhauMoi.Text != txtMatKhauXacNhan.Text)
                    {
                        MessageBox.Show("Mật Khẩu Xác Nhận không trùng với mật khẩu mới của bạn !");
                    }
                    else
                    {
                        taikhoan.matkhau = txtMatKhauMoi.Text;
                        dBquanly.SaveChanges();
                        MessageBox.Show("Bạn Đã Thay Đổi Mật Khẩu Thành Công !");

                        txtTenDangNhap.Text = "";
                        txtMatKhauCu.Text = "";
                        txtMatKhauMoi.Text = "";
                        txtMatKhauXacNhan.Text = "";
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "";
            txtMatKhauCu.Text = "";
            txtMatKhauMoi.Text = "";
            txtMatKhauXacNhan.Text = "";
        }
    }
}