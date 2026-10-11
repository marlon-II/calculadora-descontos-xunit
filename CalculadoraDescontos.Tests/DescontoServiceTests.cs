using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculadoraDescontos.App;

namespace CalculadoraDescontos.Tests
{
    public class DescontoServiceTests
    {
        [Theory]
        [InlineData(2, "BRONZE")]
        [InlineData(7,"PRATA")]
        [InlineData(15,"OURO")]
        public void ObterCategoriaClienteTest(int totalCompras, string esperado)
        {
            var servico = new DescontoService();

            string resultado = servico.ObterCategoriaCliente(totalCompras);

            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(100,10,90)]
        [InlineData(200,20,160)]
        [InlineData(50,0,50)]
        public void CalcularDescontoPorPercentualTest(int valorOriginal, int percentualDesconto, int esperado)
        {
            var servico = new DescontoService();

            int resultado = servico.CalcularDescontoPorPercentual(valorOriginal, percentualDesconto);

            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(20,false,true)]
        [InlineData(16,true,true)]
        [InlineData(17,false,false)]
        public void EValidoParaCupomTest(int idade, bool primeiraCompra, bool esperado)
        {
            var servico = new DescontoService();

            bool resultado = servico.EValidoParaCupom(idade, primeiraCompra);

            Assert.Equal(esperado, resultado);
        }
    }
}