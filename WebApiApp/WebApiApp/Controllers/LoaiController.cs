﻿using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var loais = context.Loais.ToList();
                return Ok(loais);
            }
            catch
            {
                return BadRequest();
            }
           
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
        [Authorize]
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
                return StatusCode(StatusCodes.Status201Created, loaiModel);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteLoaiById(int id)
        {
            var loai = context.Loais.FirstOrDefault(x => x.MaLoai == id);

            if (loai == null)
            {
                return NotFound();
            }

            context.Loais.Remove(loai);
            context.SaveChanges();

            return Ok(loai);
        }
    }
}
