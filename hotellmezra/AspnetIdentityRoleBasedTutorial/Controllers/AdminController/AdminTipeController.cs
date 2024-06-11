using AspnetIdentityRoleBasedTutorial.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspnetIdentityRoleBasedTutorial.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class AdminTipeController : Controller
    {
        private readonly ApplicationDbContext _appcontext;

        public AdminTipeController(ApplicationDbContext context)
        {
            _appcontext = context;
        }

        public async Task<IActionResult> GetAll()
        { 
          
            return View(await _appcontext.KategoriKamar.ToListAsync()); // Kirim data ke view GetAll.cshtml (harus dibuat terlebih dahulu)
        }
        // Action method untuk menampilkan form tambah tipe
        public IActionResult TambahTipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TambahTipe(KategoriKamar kategori)
        {
            if (ModelState.IsValid)
            {
                // Cek apakah nama kategori sudah ada dalam database
                bool namaSudahTersedia = await _appcontext.KategoriKamar.AnyAsync(k => k.NamaKategori == kategori.NamaKategori);
                if (namaSudahTersedia)
                {
                    ModelState.AddModelError("NamaKategori", "Nama kategori sudah tersedia.");
                    return View(kategori); // Kembali ke view dengan pesan error
                }

                // Jika nama belum tersedia, lanjutkan proses penambahan kategori
                _appcontext.Add(kategori);
                await _appcontext.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }
            // Handle invalid model state
            return View(kategori);
        }   



        // Action method untuk menampilkan form edit tipe
        public async Task<IActionResult> EditTipe(int id)
        {
            // Cari kategori berdasarkan ID yang diberikan
            var kategori = await _appcontext.KategoriKamar.FindAsync(id);
            if (kategori == null)
            {
                return NotFound(); // Kategori tidak ditemukan, tampilkan halaman 404
            }

            return View(kategori); // Kirim data kategori ke view EditTipe.cshtml (harus dibuat terlebih dahulu)
        }

        // Action method untuk menangani submit form edit tipe
        [HttpPost]
        public async Task<IActionResult> EditTipe(KategoriKamar kategori)
        {
            if (ModelState.IsValid)
            {
                // Cek apakah nama kategori sudah ada dalam database, kecuali untuk kategori yang sedang diedit
                bool namaSudahTersedia = await _appcontext.KategoriKamar.AnyAsync(k => k.NamaKategori == kategori.NamaKategori && k.KategoriID != kategori.KategoriID);
                if (namaSudahTersedia)
                {
                    ModelState.AddModelError("NamaKategori", "Nama kategori sudah tersedia.");
                    return View(kategori); // Kembali ke view dengan pesan error
                }

                // Jika nama belum tersedia, lanjutkan proses edit kategori
                _appcontext.Update(kategori);
                await _appcontext.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }
            // Handle invalid model state
            return View(kategori);
        }


        //hapus
        // Action method untuk menampilkan konfirmasi hapus kategori
        public async Task<IActionResult> HapusTipe(int id)
        {
            var kategori = await _appcontext.KategoriKamar.FindAsync(id);
            if (kategori == null)
            {
                return NotFound(); // Kategori tidak ditemukan, tampilkan halaman 404
            }

            return View(kategori); // Kirim data kategori ke view HapusTipe.cshtml (harus dibuat terlebih dahulu)
        }

        // Action method untuk menangani proses penghapusan kategori
        [HttpPost, ActionName("HapusTipe")]
        public async Task<IActionResult> HapusTipeConfirmed(int id)
        {
            var kategori = await _appcontext.KategoriKamar.FindAsync(id);
            if (kategori == null)
            {
                return NotFound(); // Kategori tidak ditemukan, tampilkan halaman 404
            }

            _appcontext.KategoriKamar.Remove(kategori);
            await _appcontext.SaveChangesAsync();
            return RedirectToAction(nameof(GetAll)); // Redirect ke halaman daftar kategori setelah hapus berhasil
        }
    }
}
