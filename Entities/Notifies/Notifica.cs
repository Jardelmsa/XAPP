using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notifies
{
    public class Notifica
    {

        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifica> Notificacoes { get; set; }


        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {

            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidaPropriedadeDecimal(decimal valor, string nomePropriedade)
        {

            if (valor <1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "O valor tem que ser maior que 0.",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
    }
}
