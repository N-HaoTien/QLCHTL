using DOAN_QLCHTL.FormChucNang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOAN_QLCHTL
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        public static int idnhanvien;
        public static string chucvuNV;
        public static string tenNV;
        public event EventHandler DangXuat;
        public bool IsThoat = true;
        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MainViewModel>();
        }

        public void OpenForm(Type typeForm)
        {
            foreach (Form item in MdiChildren)//loop form con
            {
                if(item.GetType() ==  typeForm)
                {
                    item.Activate();
                    return;
                }    
            }//ktra nếu nó mở r k lấy ra nữa

            Form frm = (Form)Activator.CreateInstance(typeForm);
            frm.MdiParent = this;
            frm.Show();
            //show lên nếu nó chưa hiện document
        }
        private void btnQLTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmTaiKhoan));
        }

        private void btnDoiMatKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmDoiMatKhau));

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if(chucvuNV == "Cửa Hàng Trưởng")
            {
                //MessageBox.Show("Xin Chào Cửa Hàng Trưởng !", "Thông Báo");
            }
            else
            {
                //MessageBox.Show("Xin Chào Nhân Viên !", "Thông Báo");
                btnQLTK.Enabled = false;
                ribbonPage6.Visible = false;
                ribbonPage3.Visible = false;
            }
        }

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DangXuat(this, new EventArgs());
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsThoat)
            {
                Application.Exit();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsThoat)
            {
                Application.Exit();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsThoat)
            {
                if (MessageBox.Show("Bạn Có Muốn Thoát Chương Trình Không ?", "Thông Báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmNhanVien));
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmChucVu));

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmMatHang));

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmNCC));

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmKhachHang));

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmDonViTinh));

        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem14_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmLoaiMatHang));
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmBanHang));

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmNhapHang));
        }
    }
}
