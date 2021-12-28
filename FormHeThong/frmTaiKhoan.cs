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
using System.Data.Entity;

namespace DOAN_QLCHTL
{
    public partial class frmTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public frmTaiKhoan()
        {
            InitializeComponent();

        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            DBquanly dBquanly = new DBquanly();
            taiKhoanBindingSource.DataSource = dBquanly.TaiKhoans.ToList();


            List<NhanVien> nhanviens = dBquanly.NhanViens.ToList();
            List<TaiKhoan> taikhoans = dBquanly.TaiKhoans.ToList();
            nhanviens.Insert(0, new NhanVien() { TenNV = "" });
            cbNhanVien.Properties.DataSource = nhanviens;
            cbNhanVien.Properties.ValueMember = "MaNV";
            cbNhanVien.Properties.DisplayMember = "TenNV";
            foreach (TaiKhoan item in taikhoans)
            {
                cbNhanVien.EditValue = item.NhanVien.TenNV;
            }
            //nhanVienBindingSource.DataSource = dBquanly.NhanViens.ToList();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            taiKhoanBindingSource.AddNew();

            txtTenDangNhap.Focus();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tài Khoản !");

            }
            else if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Mật Khẩu !");

            }
            else
            {
                try
                {
                    DBquanly dBquanly = new DBquanly();
                    var taiKhoan = dBquanly.TaiKhoans.FirstOrDefault(p => p.TaiKhoan1 == txtTenDangNhap.Text);

                    if (taiKhoan == null)
                    {
                        TaiKhoan taikhoans = new TaiKhoan();
                        taikhoans.TaiKhoan1 = txtTenDangNhap.Text;
                        taikhoans.matkhau = txtMatKhau.Text;
                        taikhoans.MaNV = int.Parse(cbNhanVien.ItemIndex.ToString());

                        dBquanly.TaiKhoans.Add(taikhoans);
                        dBquanly.SaveChanges();
                        MessageBox.Show("Bạn Đã Thêm Tài Khoản Thành Công !");
                    }
                    else
                    {
                        MessageBox.Show("Tên Đăng Nhập Này Đã Tồn Tại");
                    }
                    taiKhoanBindingSource.DataSource = dBquanly.TaiKhoans.ToList();
                }
                catch
                {
                    MessageBox.Show("Mời Bạn Chọn Tên Nhân Viên");
                }
            }

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DBquanly dBquanly = new DBquanly();
            var taiKhoan = dBquanly.TaiKhoans.FirstOrDefault(p => p.TaiKhoan1 == txtTenDangNhap.Text);
            if (taiKhoan != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    taiKhoan.TaiKhoan1 = txtTenDangNhap.Text;
                    taiKhoan.matkhau = txtMatKhau.Text;
                    taiKhoan.MaNV = int.Parse(cbNhanVien.ItemIndex.ToString());

                    dBquanly.TaiKhoans.Remove(taiKhoan);
                    dBquanly.SaveChanges();
                    MessageBox.Show("Bạn Đã Xóa Thành Công !");

                }
            }
            else
            {
                MessageBox.Show("Không Tồn Tại Tài Khoản Để Xóa");

            }
            taiKhoanBindingSource.DataSource = dBquanly.TaiKhoans.ToList();
        }
    }
}