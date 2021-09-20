using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WebApiFundamentos.Validators;

namespace WebApiFundamentos.Test
{
    [TestClass]
    public class PrimeraLetraMayusculaTest
    {
        [TestMethod]
        public void VerificarError()
        {
            var decorador = new PrimerLetraMayuscula();
            string nombre = "angel";
            var context = new ValidationContext(new { Nombre = nombre });

            var resultado = decorador.GetValidationResult(nombre, context);

            Assert.AreEqual("La primer letra del nombre debe ser mayúscula", resultado.ErrorMessage);
        }

        [TestMethod]
        public void VerificarValorNulo()
        {
            var decorador = new PrimerLetraMayuscula();
            string nombre = null;
            var context = new ValidationContext(new { Nombre = nombre });

            var resultado = decorador.GetValidationResult(nombre, context);

            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void VerificarExisteLetraMayuscula()
        {

            var decorador = new PrimerLetraMayuscula();
            string nombre = "Angel";
            var context = new ValidationContext(new { Nombre = nombre });

            var resultado = decorador.GetValidationResult(nombre, context);

            Assert.IsNull(resultado);
        }
    }
}
