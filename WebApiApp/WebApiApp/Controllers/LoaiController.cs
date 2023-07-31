using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiApp.Data;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext context;
        public LoaiController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var loais = context.Loais.ToList();
            return Ok(loais);
        }

        [HttpGet("{id}")]
        public IActionResult GetLoaiById(int id)
        {
            var loai = context.Loais.FirstOrDefault(x => x.MaLoai == id);

            if(loai == null)
            {
                return NotFound();
            }

            return Ok(loai);
        }

        [HttpPost]
        public IActionResult Create(LoaiModel loaiModel)
        {
            try
            {
                if (loaiModel == null)
                {
                    return BadRequest();
                }

                Loai loai = new Loai
                {
                    TenLoai = loaiModel.TenLoai
                };

                context.Loais.Add(loai);
                context.SaveChanges();
                return Ok(loai);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateLoaiById(int id, LoaiModel updatedLoaiModel)
        {
            try
            {
                var loai = context.Loais.FirstOrDefault(x => x.MaLoai == id);

                if (loai == null)
                {
                    return NotFound();
                }

                loai.TenLoai = updatedLoaiModel.TenLoai;
                context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
