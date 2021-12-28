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
    public partial class frmDonViTinh : DevExpress.XtraEditors.XtraForm
    {
        DBquanly dBquanly;
        int Inorup = 0;
        public frmDonViTinh()
        {
            InitializeComponent();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Inorup = 1;

            donViTinhBindingSource.AddNew();

            txtTenDVT.Focus();
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            txtMaDVT.Enabled = false;
            dBquanly = new DBquanly();
            donViTinhBindingSource.DataSource = dBquanly.DonViTinhs.ToList();

        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Inorup = 0;

            txtTenDVT.Focus();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();

            int mDVT = int.Parse(txtMaDVT.Text);

            var findDVT = dBquanly.DonViTinhs.FirstOrDefault(p => p.MaDVT == mDVT);

            if(txtTenDVT.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Đơn Vị !");
            }
            else
            {
                if(Inorup == 1)
                {
                    DonViTinh donViTinh = new DonViTinh();

                    donViTinh.TenDVT = txtTenDVT.Text;

                    dBquanly.DonViTinhs.Add(donViTinh);

                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Thêm Thành Công !");

                }
                else
                {
                    if(findDVT != null)
                    {
                        findDVT.TenDVT = txtTenDVT.Text;

                        dBquanly.SaveChanges();

                        MessageBox.Show("Bạn Đã Sửa Thành Công !");
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Tên Đơn Vị Để sửa !");

                    }

                }
            }
            donViTinhBindingSource.DataSource = dBquanly.DonViTinhs.ToList();

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();

            int mDVT = int.Parse(txtMaDVT.Text);

            var findDVT = dBquanly.DonViTinhs.FirstOrDefault(p => p.MaDVT == mDVT);

            if (findDVT != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    findDVT.TenDVT = txtTenDVT.Text;

                    dBquanly.DonViTinhs.Remove(findDVT);

                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Xóa Thành Công !");
                }    
                
            }
            else
            {
                MessageBox.Show("Không Tồn Tại Tên Đơn Vị Để Xóa !");

            }
        }
    }
}