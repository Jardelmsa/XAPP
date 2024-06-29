using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUser
    {
        Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular);
        Task<bool> ExisteUsuario(string email, string senha);

    }
}
