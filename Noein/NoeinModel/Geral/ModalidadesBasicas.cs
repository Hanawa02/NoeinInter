using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class ModalidadesBasicas : IComparable
    {
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public ModalidadesBasicas(int codigo, string descricao)
        {
            this.Codigo = codigo;
            this.Descricao = descricao;
        }
        public override bool Equals(object obj)
        {
            try
            {
                if (obj.GetType() != typeof(ModalidadesBasicas)) {
                    return false;
                }
                return this.Codigo == ((ModalidadesBasicas)obj).Codigo;
            }
            catch (Exception ex) {
                return false;
                throw ex;
            }
        }

        public override int GetHashCode()
        {
            return this.Codigo;
        }

        public int CompareTo(object obj)
        {
            try
            {
                if (this.Equals(obj)) {
                    return 0;
                }

                if (this.Codigo < ((ModalidadesBasicas)obj).Codigo) {
                    return -1;
                }
                return 1;
            } catch(Exception ex)
            {
                return 1;
                throw ex;
            }
        }
    }
}
