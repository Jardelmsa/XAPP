using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.InterfacesServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class ApplicationNoticies : IApplicationNoticies
    {
        INoticies _INoticies;
        IServicesNoticies _IServicesNoticies; 

        public ApplicationNoticies(INoticies INoticies, IServicesNoticies IServicesNoticies)
        {
            _INoticies = INoticies;
            _IServicesNoticies = IServicesNoticies;
        }

        public async Task AdcionarNoticia(Noticia noticia)
        {
            await _IServicesNoticies.AdcionarNoticia(noticia);
        }

        public async Task AtualizarNoticia(Noticia noticia)
        {
            await _IServicesNoticies.AtualizarNoticia(noticia);
        }

        public async Task<List<Noticia>> ListarNoticiaAtivas()
        {
           return  await _IServicesNoticies.ListarNoticiaAtivas();
        }




        public async Task Adicionar(Noticia Objeto)
        {
            await _INoticies.Adicionar(Objeto);
        }

        public async Task Atualizar(Noticia Objeto)
        {
            await _INoticies.Atualizar(Objeto);
        }
        
        public async Task<Noticia> BuscarPorId(int Id)
        {
            return await _INoticies.BuscarPorId(Id);
        }

        public async Task Excluir(Noticia Objeto)
        {
            await _INoticies.Excluir(Objeto);
        }

        public async Task<List<Noticia>> Listar()
        {
            return await _INoticies.Listar();
        }

       
    }
}
