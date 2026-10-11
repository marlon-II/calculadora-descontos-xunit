using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CalculadoraDescontos.App
{
    public class DescontoService
    {
        public string ObterCategoriaCliente(int totalCompras)
        {
           if(totalCompras < 5)
            {
                return "BRONZE";    
            }
            else if(totalCompras >= 5 && totalCompras <= 10)
            {
                return "PRATA";
            }
            else
            {
                return "OURO";
            }
        }

        public int CalcularDescontoPorPercentual(int valorOriginal, int percentualDesconto)
        {
           decimal valorFinal = valorOriginal * (1 - percentualDesconto / 100m);
            return (int)valorFinal;
        }

        public bool EValidoParaCupom(int idade, bool priemiraCompra)
        {
            if(idade >= 18 || priemiraCompra == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}