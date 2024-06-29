using Application.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationNoticies: IGenericsApplication<Noticia>
    {
        Task AdcionarNoticia(Noticia noticia);
        Task AtualizarNoticia(Noticia noticia);
        Task<List<Noticia>> ListarNoticiaAtivas();
    }
}
