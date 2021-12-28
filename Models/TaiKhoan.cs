namespace DOAN_QLCHTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [Column("TaiKhoan")]
        [StringLength(20)]
        public string TaiKhoan1 { get; set; }

        [Required]
        [StringLength(20)]
        public string matkhau { get; set; }

        public int MaNV { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
