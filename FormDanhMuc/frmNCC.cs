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
    public partial class frmNCC : DevExpress.XtraEditors.XtraForm
    {
        DBquanly quanly;
        public frmNCC()
        {
            InitializeComponent();
        }
        int InorUp = 0;

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 1;

            nhaCungCapBindingSource.AddNew();

            txtTenNCC.Focus();
        }

        private void frmNCC_Load(object sender, EventArgs e)
        {
            txtMNCC.Enabled = false;
            quanly = new DBquanly();
            nhaCungCapBindingSource.DataSource = quanly.NhaCungCaps.ToList();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 0;

            txtTenNCC.Focus();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            quanly = new DBquanly();

            int maNCC = int.Parse(txtMNCC.Text);
            var findNCC = quanly.NhaCungCaps.FirstOrDefault(p => p.MaNCC == maNCC);

            if(txtTenNCC.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Nhà Cung Cấp !");
            }
            else if(txtSDT.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Số Điện Thoại !");

            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Địa Chỉ !");

            }
            else
            {
                if(InorUp == 1)
                {
                    NhaCungCap nhaCungCap = new NhaCungCap();
                    nhaCungCap.TenNhaCungCap = txtTenNCC.Text;
                    nhaCungCap.DiaChi = txtDiaChi.Text;
                    nhaCungCap.DienThoai = int.Parse(txtSDT.Text);

                    quanly.NhaCungCaps.Add(nhaCungCap);

                    quanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Thêm Thành Công  !");

                }
                else
                {
                    if(findNCC != null)
                    {
                        findNCC.TenNhaCungCap = txtTenNCC.Text;
                        findNCC.DiaChi = txtDiaChi.Text;
                        findNCC.DienThoai = int.Parse(txtSDT.Text);

                        quanly.SaveChanges();

                        MessageBox.Show("Bạn Đã Sửa Thành Công  !");
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại để sửa!");

                    }
                }
            }
            nhaCungCapBindingSource.DataSource = quanly.NhaCungCaps.ToList();


        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            quanly = new DBquanly();

            int maNCC = int.Parse(txtMNCC.Text);
            var findNCC = quanly.NhaCungCaps.FirstOrDefault(p => p.MaNCC == maNCC);
            if(findNCC != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    findNCC.TenNhaCungCap = txtTenNCC.Text;
                    findNCC.DiaChi = txtDiaChi.Text;
                    findNCC.DienThoai = int.Parse(txtSDT.Text);

                    quanly.NhaCungCaps.Remove(findNCC);

                    quanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Xóa Thành Công  !");
                }
            }
            else
            {
                MessageBox.Show("Không tồn tại !");

            }
            nhaCungCapBindingSource.DataSource = quanly.NhaCungCaps.ToList();

        }
    }
}