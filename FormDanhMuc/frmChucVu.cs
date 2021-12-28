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
    public partial class frmChucVu : DevExpress.XtraEditors.XtraForm
    {
        public frmChucVu()
        {
            InitializeComponent();
        }

        DBquanly dBquanly;

        int InorUp = 0;
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 1;
            chucVuBindingSource.AddNew();
            txtTenChucVu.Focus();
        }

        private void frmChucVu_Load(object sender, EventArgs e)
        {
            dBquanly = new DBquanly();

            txtMaChucVu.Enabled = false;
            chucVuBindingSource.DataSource = dBquanly.ChucVus.ToList();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 0;

            txtTenChucVu.Focus();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(txtTenChucVu.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Chức Vụ !");
            }
            else
            {
                dBquanly = new DBquanly();
                int maCV = int.Parse(txtMaChucVu.Text);
                var mCV = dBquanly.ChucVus.FirstOrDefault(p => p.MaCV == maCV);

                if(InorUp == 1)
                {
                    ChucVu chucVu = new ChucVu();

                    chucVu.TenCV = txtTenChucVu.Text;

                    dBquanly.ChucVus.Add(chucVu);

                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Thêm Chức Vụ Thành Công !");

                }
                else
                {
                    if(mCV != null)
                    {
                        mCV.TenCV = txtTenChucVu.Text;

                        dBquanly.SaveChanges();

                        MessageBox.Show("Bạn Đã Sửa Thành Công !");
                    }
                    else
                    {
                        MessageBox.Show("Chức Vụ Không Tồn Tại !");

                    }
                }
            }
            chucVuBindingSource.DataSource = dBquanly.ChucVus.ToList();

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();
            int maCV = int.Parse(txtMaChucVu.Text);
            var mCV = dBquanly.ChucVus.FirstOrDefault(p => p.MaCV == maCV);
            if (mCV != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    mCV.TenCV = txtTenChucVu.Text;

                    dBquanly.ChucVus.Remove(mCV);

                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Xóa Thành Công !");
                }
               
            }
            else
            {
                MessageBox.Show("Chức Vụ Không Tồn Tại !");

            }

        }
    }
}