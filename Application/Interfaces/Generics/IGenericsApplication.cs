using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Generics
{
    public interface IGenericsApplication<T> where T :class
    {
        Task Adicionar(T Objeto);
        Task Atualizar(T Objeto);
        Task Excluir(T Objeto);
        Task<T> BuscarPorId(int Id);
        Task<List<T>> Listar();
    }
}
