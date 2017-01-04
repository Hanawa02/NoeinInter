using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Usuario
    {
        public int IdUsuario { get; private set; }
        public string NomeUsuario { get; private set; }

        public Usuario(int idUsuario, string nomeUsuario)
        {
            this.IdUsuario = idUsuario;
            this.NomeUsuario = nomeUsuario;
        }
    }
}
