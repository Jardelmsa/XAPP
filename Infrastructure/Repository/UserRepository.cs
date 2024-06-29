using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configs;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUser
    {
        private readonly DbContextOptions<Context> _optionsBuilder;

        public UserRepository()
        {
            _optionsBuilder = new DbContextOptions<Context>();
        }
        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using (var data = new Context(_optionsBuilder))
                {
                  await data.ApplicationUsers.AddAsync(
                        new ApplicationUser
                        {
                            Email = email,
                            PasswordHash = senha,
                            Idade = idade,
                            Celular = celular
                        });

                    await data.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
                
            }
            return true;
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            try
            {
                using (var data = new Context(_optionsBuilder))
                {
                    return await data.ApplicationUsers
                        .Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(senha))
                        .AsNoTracking()
                        .AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }
    }
}
