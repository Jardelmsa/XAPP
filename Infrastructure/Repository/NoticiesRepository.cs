using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configs;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class NoticiesRepository : GenericRepository<Noticia>, INoticies
    {
        private readonly DbContextOptions<Context> _optionsBuilder;
        public NoticiesRepository()
        {
            _optionsBuilder = new DbContextOptions<Context>();
        }
        public async Task<List<Noticia>> ListarNoticias(Expression<Func<Noticia, bool>> exNoticia)
        {
           using(var banco = new Context(_optionsBuilder))
            {
                return await banco.Noticia.Where(exNoticia).AsNoTracking().ToListAsync();
            }
        }
    }
}
