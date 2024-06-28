using Domain.Interfaces;
using Domain.Interfaces.InterfacesServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceNoticies : IServicesNoticies
    {
        private readonly INoticies _INoticies;

        public ServiceNoticies(INoticies INoticies)
        {
            _INoticies = INoticies;
        }
        public async  Task AdcionarNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidaPropriedadeString(noticia.Titulo, "Titulo");
            var validarInformacoes = noticia.ValidaPropriedadeString(noticia.Informacao, "Informacao");

            if(validarTitulo && validarInformacoes)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;

               await  _INoticies.Adicionar(noticia);
            }
        }

        public  async Task AtualizarNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidaPropriedadeString(noticia.Titulo, "Titulo");
            var validarInformacoes = noticia.ValidaPropriedadeString(noticia.Informacao, "Informacao");

            if (validarTitulo && validarInformacoes)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.Ativo = true;

                await _INoticies.Atualizar(noticia);
            }
        }

        public async Task<List<Noticia>> ListarNoticiaAtivas()
        {
            return await _INoticies.ListarNoticias(n => n.Ativo);
        }
    }
}
