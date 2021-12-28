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
namespace DOAN_QLCHTL.FormChucNang
{
    public partial class frmNhapHang : DevExpress.XtraEditors.XtraForm
    {
        DBquanly dBquanly;
        public int idNV = frmMain.idnhanvien;
        public string tenNV = frmMain.tenNV;
        public frmNhapHang()
        {
            InitializeComponent();
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            dBquanly = new DBquanly();
            txtTNV.Text = tenNV;

            AutoCompleteStringCollection auto1 = new AutoCompleteStringCollection();

            List<NhaCungCap> nhaCungCaps = dBquanly.NhaCungCaps.ToList();

            cbNCC.DataSource = nhaCungCaps;
            cbNCC.ValueMember = "MaNCC";
            cbNCC.DisplayMember = "TenNhaCungCap";
            foreach (NhaCungCap item in nhaCungCaps)
            {
                auto1.Add(item.TenNhaCungCap);
            }
            cbNCC.AutoCompleteCustomSource = auto1;
            List<HangHoa> hangHoas = dBquanly.HangHoas.ToList();

            cbTenMatHang.DataSource = hangHoas;
            cbTenMatHang.ValueMember = "MaHH";
            cbTenMatHang.DisplayMember = "TenHH";

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();

            foreach (HangHoa item in hangHoas)
            {
                auto.Add(item.TenHH);
            }
            cbTenMatHang.AutoCompleteCustomSource = auto;


        }



        private void btnInorUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();

            if (string.IsNullOrEmpty(txtMPN.Text))// Nếu text Null thì thêm !
            {
                PhieuNhap pNhap = new PhieuNhap();

                pNhap.MaNV = idNV;

                pNhap.MaNCC = int.Parse(cbNCC.SelectedValue.ToString());

                pNhap.ThoiDiemLap = DateTime.Now;

                dBquanly.PhieuNhaps.Add(pNhap);

                dBquanly.SaveChanges();

                txtMPN.Text = pNhap.MaPN.ToString();
            }
            int maPN = int.Parse(txtMPN.Text);
            int MaMatHang = int.Parse(cbTenMatHang.SelectedValue.ToString());
            var MatHang = dBquanly.HangHoas.FirstOrDefault(mh => mh.MaHH == MaMatHang);
            var ct = dBquanly.ChiTietPhieuNhaps.FirstOrDefault(p => p.MaPN == maPN && p.MaHH == MaMatHang);
            if (ct == null)
            {
                if (int.Parse(nbSoLuong.Value.ToString()) <= 0)
                {
                    MessageBox.Show("Mời Bạn Nhập Số Lượng > 0 ");
                }
                else
                {
                    ChiTietPhieuNhap chiTietPhieuNhap = new ChiTietPhieuNhap();

                    chiTietPhieuNhap.MaPN = maPN;
                    chiTietPhieuNhap.MaHH = MaMatHang;
                    chiTietPhieuNhap.SL = int.Parse(nbSoLuong.Value.ToString());

                    dBquanly.ChiTietPhieuNhaps.Add(chiTietPhieuNhap);

                    MatHang.SLTon += chiTietPhieuNhap.SL;
                    dBquanly.SaveChanges();

                    MessageBox.Show("Bạn Đã Thêm Thành Công !");
                }
            }
            else
            {
                if (int.Parse(nbSoLuong.Value.ToString()) < 0)
                {
                    if (ct.SL < -int.Parse(nbSoLuong.Value.ToString()))
                    {
                        MatHang.SLTon = MatHang.SLTon - ct.SL;
                        ct.SL = 0;
                        dBquanly.ChiTietPhieuNhaps.Remove(ct);
                    }
                    else
                    {
                        ct.SL += int.Parse(nbSoLuong.Value.ToString());
                        MatHang.SLTon = MatHang.SLTon + int.Parse(nbSoLuong.Value.ToString());
                        MessageBox.Show("Bạn Đã Thay Đổi SL Thành Công !");
                    }
                }
                else
                {
                    ct.SL += int.Parse(nbSoLuong.Value.ToString());
                    MatHang.SLTon = MatHang.SLTon + int.Parse(nbSoLuong.Value.ToString());
                    MessageBox.Show("Bạn Đã Thay Đổi SL Thành Công !");
                }

                dBquanly.SaveChanges();
                

            }
            chiTietPhieuNhapBindingSource.DataSource = dBquanly.ChiTietPhieuNhaps.Where(p => p.MaPN == maPN).ToList();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dBquanly = new DBquanly();
            int maPN = int.Parse(txtMPN.Text);
            int MaMatHang = int.Parse(cbTenMatHang.SelectedValue.ToString());
            var MatHang = dBquanly.HangHoas.FirstOrDefault(mh => mh.MaHH == MaMatHang);
            var ct = dBquanly.ChiTietPhieuNhaps.FirstOrDefault(p => p.MaPN == maPN && p.MaHH == MaMatHang);
            if (ct != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không", "Xóa dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dBquanly.ChiTietPhieuNhaps.Remove(ct);

                    MatHang.SLTon = MatHang.SLTon - ct.SL;

                    dBquanly.SaveChanges();
                    MessageBox.Show("Bạn Đã Xóa Thành Công");
                }
            }
            else
            {
                MessageBox.Show("Không Tồn Tại để xóa");

            }
            chiTietPhieuNhapBindingSource.DataSource = dBquanly.ChiTietPhieuNhaps.Where(p => p.MaPN == maPN).ToList();
        }
    }
}