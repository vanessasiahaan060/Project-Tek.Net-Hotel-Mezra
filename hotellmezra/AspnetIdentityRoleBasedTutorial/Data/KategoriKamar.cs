using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetIdentityRoleBasedTutorial.Data
{
    [Table("KategoriKamar")]
    public class KategoriKamar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KategoriID { get; set; }

        [Required(ErrorMessage = "Nama kategori harus diisi.")]
        public string NamaKategori { get; set; }

        // Ganti Kamars menjadi Kamars

    }
}
