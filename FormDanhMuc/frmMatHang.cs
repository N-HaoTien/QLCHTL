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
    public partial class frmMatHang : DevExpress.XtraEditors.XtraForm
    {
        DBquanly dBquanly;

        private int InorUp = 0;
        public frmMatHang()
        {
            InitializeComponent();
        }

        private void frmMatHang_Load(object sender, EventArgs e)
        {
            dBquanly = new DBquanly();

            hangHoaBindingSource.DataSource = dBquanly.HangHoas.ToList();


            List<DonViTinh> donViTinhs = dBquanly.DonViTinhs.ToList();

            donViTinhs.Insert(0, new DonViTinh() { TenDVT = "" });

            CbTenDonVi.Properties.DataSource = donViTinhs;
            CbTenDonVi.Properties.ValueMember = "MaDVT";
            CbTenDonVi.Properties.DisplayMember = "TenDVT";

            List<LoaiHangHoa> hangHoas = dBquanly.LoaiHangHoas.ToList();
            cbTenLoaiHang.DataSource = hangHoas;
            cbTenLoaiHang.ValueMember = "MaLoaiHH";
            cbTenLoaiHang.DisplayMember = "TenLoaiHH";

            

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 1;

            hangHoaBindingSource.AddNew();

            txtTenHH.Focus();


        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InorUp = 0;

            txtTenHH.Focus();

        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();

            int mHH = int.Parse(txtMaHH.Text);

            var findmHH = dBquanly.HangHoas.FirstOrDefault(p => p.MaHH == mHH);

            if (txtTenHH.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Hàng Hóa !");
            }
            else if(txtGiaBan.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Giá Sản Phẩm !");

            }
            else if (cbTenLoaiHang.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Loại Hàng !");

            }
            else if (CbTenDonVi.Text == "")
            {
                MessageBox.Show("Mời Bạn Nhập Tên Đơn Vị !");

            }
            else
            {
                if (InorUp == 1)
                {
                    HangHoa hangHoa = new HangHoa();

                    hangHoa.TenHH = txtTenHH.Text;

                    hangHoa.GiaBan = int.Parse(txtGiaBan.Text);

                    hangHoa.MaDVT = int.Parse(CbTenDonVi.ItemIndex.ToString());

                    hangHoa.MaLoaiHH = int.Parse(cbTenLoaiHang.SelectedValue.ToString());

                    hangHoa.SLTon = int.Parse(txtSLTon.Text);

                    dBquanly.HangHoas.Add(hangHoa);

                    dBquanly.SaveChanges();

                    MessageBox.Show("Mời Đã Thêm Thành Công !");

                }
                else
                {
                    if (findmHH != null)
                    {

                            findmHH.TenHH = txtTenHH.Text;

                            findmHH.GiaBan = int.Parse(txtGiaBan.Text);

                            findmHH.MaDVT = int.Parse(CbTenDonVi.ItemIndex.ToString());

                            findmHH.MaLoaiHH = int.Parse(cbTenLoaiHang.SelectedValue.ToString());

                            findmHH.SLTon = int.Parse(txtSLTon.Text);

                            dBquanly.SaveChanges();

                            MessageBox.Show("Bạn Đã Sửa Thành Công !");
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Tên Đơn Vị Để sửa !");

                    }

                }
            }
            hangHoaBindingSource.DataSource = dBquanly.HangHoas.ToList();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();

            int mHH = int.Parse(txtMaHH.Text);

            var findmHH = dBquanly.HangHoas.FirstOrDefault(p => p.MaHH == mHH);

            if(findmHH != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    findmHH.TenHH = txtTenHH.Text;

                    findmHH.GiaBan = int.Parse(txtGiaBan.Text);

                    findmHH.MaDVT = int.Parse(CbTenDonVi.ItemIndex.ToString());

                    findmHH.MaLoaiHH = int.Parse(cbTenLoaiHang.SelectedValue.ToString());

                    findmHH.SLTon = int.Parse(txtSLTon.Text);

                    dBquanly.HangHoas.Remove(findmHH);
                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Xóa Thành Công !");
                }
                
            }
            else
            {
                MessageBox.Show("Không Tồn Tại Để Xóa !");

            }
            hangHoaBindingSource.DataSource = dBquanly.HangHoas.ToList();

        }
    }
}