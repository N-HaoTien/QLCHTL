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
    public partial class frmLoaiMatHang : DevExpress.XtraEditors.XtraForm
    {
        DBquanly dBquanly;
        int InorUp = 0;
        public frmLoaiMatHang()
        {
            InitializeComponent();
        }


        private void frmLoaiMatHang_Load(object sender, EventArgs e)
        {
            txtLoaiMatHang.Enabled = false;

            dBquanly = new DBquanly();

            loaiHangHoaBindingSource.DataSource = dBquanly.LoaiHangHoas.ToList();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 1;

            loaiHangHoaBindingSource.AddNew();

            txtTenLoaiMH.Focus();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();
            int LoaiHHoa = int.Parse(txtLoaiMatHang.Text);

            var LoaiHH = dBquanly.LoaiHangHoas.FirstOrDefault(p => p.MaLoaiHH == LoaiHHoa);

            if(LoaiHH != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    LoaiHH.TenLoaiHH = txtTenLoaiMH.Text;

                    dBquanly.LoaiHangHoas.Remove(LoaiHH);

                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Xóa Thành Công !");
                }
            }
            else
            {
                MessageBox.Show("Không Tồn Tại Để Xóa !");
            }

            loaiHangHoaBindingSource.DataSource = dBquanly.LoaiHangHoas.ToList();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 0;


            txtTenLoaiMH.Focus();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int LoaiHHoa = int.Parse(txtLoaiMatHang.Text);
            var LoaiHH = dBquanly.LoaiHangHoas.FirstOrDefault(p => p.MaLoaiHH == LoaiHHoa);
            if (txtTenLoaiMH.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Loại Hàng !");

            }
            else
            {
                if (InorUp == 1)
                {
                    dBquanly = new DBquanly();

                    

                    if (LoaiHH == null)
                    {
                        LoaiHangHoa loaiHangHoa = new LoaiHangHoa();

                        loaiHangHoa.TenLoaiHH = txtTenLoaiMH.Text;

                        dBquanly.LoaiHangHoas.Add(loaiHangHoa);

                        dBquanly.SaveChanges();

                        MessageBox.Show("Bạn Đã Thêm Loại Hàng Hóa Thành Công !");
                    }
                    else
                    {
                        MessageBox.Show("Loại Mặt Hàng Đã Tồn Tại !");

                    }
                }
                else if(InorUp == 0)
                {
                    if(LoaiHH != null)
                    {
                        LoaiHH.TenLoaiHH = txtTenLoaiMH.Text;

                        dBquanly.SaveChanges();

                        MessageBox.Show("Bạn Đã Sửa Tên Loại Hàng Hóa Thành Công !");

                    }
                }
            }
            loaiHangHoaBindingSource.DataSource = dBquanly.LoaiHangHoas.ToList();
        }




    }
}
