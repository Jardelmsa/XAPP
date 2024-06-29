using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class ApplicationUsers : IApplicationUser

    {

        IUser _IUser;

        public ApplicationUsers(IUser IUser)
        {
            _IUser = IUser;
        }

        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            return await _IUser.AdicionarUsuario(email, senha, idade, celular);
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            return await _IUser.ExisteUsuario(email, senha); 
        }
    }
}
