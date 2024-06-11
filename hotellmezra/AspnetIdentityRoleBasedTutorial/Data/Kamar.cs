using ModelClasses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetIdentityRoleBasedTutorial.Data
{
    [Table("Kamar")]
    public class Kamar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KamarID { get; set; }

        [Required(ErrorMessage = "Nama kamar harus diisi.")]
        public string NomorKamar { get; set; }

        // Properti tambahan sesuai kebutuhan (misalnya: JenisKamarID, Harga, Ketersediaan, dll.)
        public int JenisKamarID { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Range 1 to 999.99 only")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Please insert two digits after decimal. Example: 0.00")]
        public double Price { get; set; }



        [Required]
        [MaxLength(2000, ErrorMessage = "Length can not exist more than 30 characters")]
        public string Description { get; set; }


        public bool Ketersediaa { get; set; }
        public ICollection<PImages>? ImgUrls { get; set; }

        public string? HomeImgUrl { get; set; }

        // Hubungan dengan tabel KategoriKamar
        [ForeignKey("JenisKamarID")]
        public KategoriKamar KategoriKamar { get; set; } // Ubah tipe data jika diperlukan
    }
}

