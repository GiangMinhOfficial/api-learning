using WebApiApp.Data;
using WebApiApp.Models;

namespace WebApiApp.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext context;

        public LoaiRepository(MyDbContext context)
        {
            this.context = context;
        }

        public LoaiVM Add(LoaiModel loai)
        {
            var _loai = new Loai
            {
                TenLoai = loai.TenLoai
            };
            context.Add(loai);
            context.SaveChanges();
            return new LoaiVM
            {
                MaLoai = _loai.MaLoai,
                TenLoai = _loai.TenLoai
            };
        }

        public void Delete(int id)
        {
            var loai = context.Loais.FirstOrDefault(x => x.MaLoai == id);

            if (loai != null)
            {
                context.Remove(loai);
                context.SaveChanges();
            }
        }

        public List<LoaiVM> GetAll()
        {
            var loais = context.Loais.Select(x => new LoaiVM
            {
                MaLoai = x.MaLoai,
                TenLoai = x.TenLoai
            }).ToList();

            return loais;
        }

        public LoaiVM? GetById(int id)
        {
            var loai = context.Loais.FirstOrDefault(x => x.MaLoai == id);

            if (loai == null)
            {
                return null;
            }

            return new LoaiVM
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai
            };
        }

        public void Update(LoaiVM loai)
        {
            var _loai = context.Loais.FirstOrDefault(x => x.MaLoai == loai.MaLoai);
            if (_loai != null)
            {
                _loai.TenLoai = loai.TenLoai;
                context.SaveChanges();
            }
        }
    }
}
