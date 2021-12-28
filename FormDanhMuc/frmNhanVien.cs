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
    public partial class frmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        DBquanly dBquanly;
        public frmNhanVien()
        {
            InitializeComponent();
        }
        int InorUp = 2;
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dBquanly = new DBquanly();
            nhanVienBindingSource.DataSource = dBquanly.NhanViens.ToList();
            List<ChucVu> chucVus = dBquanly.ChucVus.ToList();
            chucVus.Insert(0, new ChucVu() { TenCV = "" });
            cbTenChucVu.Properties.DataSource = chucVus;
            cbTenChucVu.Properties.ValueMember = "MaCV";
            cbTenChucVu.Properties.DisplayMember = "TenCV";

            txtMNV.Enabled = false;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMNV.Text = "";
            InorUp = 1;
            nhanVienBindingSource.AddNew();

            txtTenNhanVien.Focus();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTenNhanVien.Text == "")
            {
                
                MessageBox.Show("Mời Bạn Nhập Tên Nhân Viên !");
            }
            else if (txtCMND.Text == "")
            {
                
                MessageBox.Show("Mời Bạn Nhập CMND !");
            }
            else if (txtDiaChi.Text == "")
            {
                
                MessageBox.Show("Mời Bạn Nhập Địa Chỉ !");
            }
            else if (txtSDT.Text == "")
            {
                
                MessageBox.Show("Mời Bạn Nhập Số Điện Thoại !");
            }
            else if (cbTenChucVu.Text == "")
            {
                
                MessageBox.Show("Mời Bạn Chọn Tên Chức Vụ !");
            }
            else
            {
                if(InorUp == 1)
                {
                        int manv = int.Parse(txtMNV.Text);
                        int cmnd = int.Parse(txtCMND.Text);

                        dBquanly = new DBquanly();
                        var nhanviens = dBquanly.NhanViens.FirstOrDefault(p => p.MaNV == manv || p.CMND == cmnd);
                        if (nhanviens == null)
                        {
                            NhanVien nhanVien = new NhanVien();

                            nhanVien.TenNV = txtTenNhanVien.Text;
                            nhanVien.SDT = int.Parse(txtSDT.Text);
                            nhanVien.CMND = int.Parse(txtCMND.Text);
                            nhanVien.DiaChi = txtDiaChi.Text;
                            nhanVien.MaCV = int.Parse(cbTenChucVu.ItemIndex.ToString());
                            
                            dBquanly.NhanViens.Add(nhanVien);
                            dBquanly.SaveChanges();
                            MessageBox.Show("Bạn Đã Thêm Nhân Viên Thành Công !");

                        }
                        else
                        {
                            MessageBox.Show("Đã Trùng Số CMND !");
                        }
                }
                else if (InorUp == 2)
                {
                        int manv = int.Parse(txtMNV.Text);
                        var nhanviens = dBquanly.NhanViens.FirstOrDefault(p => p.MaNV == manv);
                        if (nhanviens != null)
                        {
                            nhanviens.TenNV = txtTenNhanVien.Text;
                            nhanviens.SDT = int.Parse(txtSDT.Text);
                            nhanviens.CMND = int.Parse(txtCMND.Text);
                            nhanviens.DiaChi = txtDiaChi.Text;
                            nhanviens.MaCV = int.Parse(cbTenChucVu.ItemIndex.ToString());

                            dBquanly.SaveChanges();
                            MessageBox.Show("Bạn Đã Sửa Thông Tin Nhân Viên Thành Công !");
                        }
                        else
                        {
                            MessageBox.Show("Bạn Phải Chỉ đến nhân viên Cần Sửa");
                        }

                }

            }
            nhanVienBindingSource.DataSource = dBquanly.NhanViens.ToList();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMNV.Text = "";
            InorUp = 2;
            txtTenNhanVien.Focus();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();
            int manv = int.Parse(txtMNV.Text);

            var nhanviens = dBquanly.NhanViens.FirstOrDefault(p => p.MaNV == manv);

            if(nhanviens != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    nhanviens.TenNV = txtTenNhanVien.Text;
                    nhanviens.SDT = int.Parse(txtSDT.Text);
                    nhanviens.CMND = int.Parse(txtCMND.Text);
                    nhanviens.DiaChi = txtDiaChi.Text;
                    nhanviens.MaCV = int.Parse(cbTenChucVu.ItemIndex.ToString());

                    dBquanly.NhanViens.Remove(nhanviens);
                    dBquanly.SaveChanges();
                    MessageBox.Show("Bạn Đã Xóa Nhân Viên Thành Công !");
                }
            }
            else
            {
                MessageBox.Show("Không Tồn Tại Nhân Viên để Xóa !");
            }
            nhanVienBindingSource.DataSource = dBquanly.NhanViens.ToList();

        }
    }
}