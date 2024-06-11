using AspnetIdentityRoleBasedTutorial.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspnetIdentityRoleBasedTutorial.Controllers.AdminController
{
    public class AdminKamarController : Controller
    {

      
        private readonly ApplicationDbContext _appcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminKamarController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _appcontext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> GetAll()
        {
            var kamarList = await _appcontext.Kamar.Include(k => k.KategoriKamar).ToListAsync();
            return View(kamarList);
        }
        [HttpGet]

        public async Task<IActionResult> TambahKamar()
        {

            var kategoriList = _appcontext.KategoriKamar.ToList();
            ViewBag.KategoriList = new SelectList(kategoriList, "KategoriID", "NamaKategori");

            var viewModel = new KamarViewModel
            {
                KategoriKamar = new KategoriKamar(),
                Kamar = new Kamar()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> TambahKamar(KamarViewModel viewModel, IFormFile gambar, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (gambar != null && gambar.Length > 0)
                    {
                        // Simpan gambar ke dalam folder "upload" di wwwroot
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(gambar.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await gambar.CopyToAsync(stream);
                        }
                        // Update path gambar ke viewModel
                        viewModel.Kamar.GambarKamar = uniqueFileName;
                    }

                    // Set JenisKamarID berdasarkan KategoriID yang dipilih
                    viewModel.Kamar.JenisKamarID = viewModel.KategoriKamar.KategoriID;

                    // Simpan data kamar ke dalam database
                    _appcontext.Kamar.Add(viewModel.Kamar);
                    await _appcontext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home"); // Redirect ke halaman yang sesuai
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Terjadi kesalahan saat menyimpan data: " + ex.Message);
                }
            }

            // Jika model state tidak valid, kembali ke view dengan data yang dimasukkan sebelumnya
            var kategoriList = _appcontext.KategoriKamar.ToList();
            ViewBag.KategoriList = new SelectList(kategoriList, "KategoriID", "NamaKategori");
            return View(viewModel);
        }


    }
}
