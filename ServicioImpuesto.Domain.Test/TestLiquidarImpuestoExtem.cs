using System;
using ServicioImpuesto.Domain;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;


namespace ServicioImpuesto.Domain.Test
{
    [TestFixture()]
    class TestClass
    {

        /**
        * ------------------------------------------------------------------------------
        * | Escenario:  Declaracion de industria y comercio extemporanea correcta sin emplazamiento
        * ------------------------------------------------------------------------------
        * | HU No.: 1
        * -------------------------------------------------------------------------
        * | Criterios de aceptación:  Dado que el Valor a Declarar es mayor a Cero y sin emplazamiento
        * | Cuando Liquide la sanción
        * | Entonces la Sanción por extemporaneidad es igual al Valor Declarado* Cantidad de meses de
        * | Extemporaneidad* 5%
        * ------------------------------------------------------------------------------
        * | Dado: El usuario al momento de declarar el usuario ha pasado los primeros 5 dias para la su
        * | declaracion y su declaracion es de $ 781242 con 6 dias de retraso y sin emplazamiento
        * | Entonces: El sistema debera mostrar cual es la cantidad a declarar del usuario
        * ------------------------------------------------------------------------------
        * | Cuando: Se va a declarar un total de $ 781242 el 18/Mayo/2019
        * | sin emplazamiento
        * ------------------------------------------------------------------------------
        * | Entonces: El sistema debera mostrar cual es la cantidad a declarar del usuario
        * | "El impuesto es extemporaneo sin emplamzamiento y Su cantidad a declarar es de $781242 más una sacion de $39062,12
        * |  Total a pagar es de $820304,1"
        * ------------------------------------------------------------------------------
        */

        /* 781242 + (781242 * 1 * 0,05) =  $820304.1*/

        [Test()]
        public void TestDeclaracionExtemporaneaSinEmplazamiento()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            float prueba = extem.Liquidar(781242f, new DateTime(2019, 5, 12), false);
            Assert.AreEqual(820304.1f, prueba);
            Console.WriteLine("El impuesto es extemporaneo sin emplamzamiento y Su cantidad a declarar" +
                " es de $781242 más una sacion de $"+(prueba - 781242)+" Total a pagar es de $" + prueba);
        }


        /**
        * ------------------------------------------------------------------------------
        * | Escenario:  Declaracion de industria y comercio extemporanea Incorrecta
        * ------------------------------------------------------------------------------
        * | HU No.: 1
        * -------------------------------------------------------------------------
        * | Criterios de aceptación:  Dado que el Valor a Declarar es mayor a Cero y sin emplazamiento
        * | Cuando Liquide la sanción
        * | Entonces la Sanción por extemporaneidad es igual al Valor Declarado* Cantidad de meses de
        * | Extemporaneidad* 5%
        * ------------------------------------------------------------------------------
        * | Dado: El usuario al momento de declarar el usuario ha pasado los primeros 5 dias para la su
        * | declaracion y su declaracion es de $ -781242 con 6 dias de retraso y sin emplazamiento
        * | Entonces: El sistema debera mostrar cual es la cantidad a declarar del usuario
        * ------------------------------------------------------------------------------
        * | Cuando: Se va a declarar un total de $ -781242 el 18/Mayo/2019
        * | sin emplazamiento
        * ------------------------------------------------------------------------------
        * | Entonces:  El sistema debe lanzar la siguiente excepción
        * |"Error #1 pago menor a $0 pesos, codigo '#0001'"
        * ------------------------------------------------------------------------------
        */

        [Test()]
        public void TestIncorrectaValorDeclaradoNegativo()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            var exception = Assert.Catch<Exception>(() => {
                extem.Liquidar(-781242f, new DateTime(2019, 5, 12), false);
            });
            Assert.AreEqual(exception.Message, "Error #1 pago menor a $0 pesos, codigo '#0001'");
            Console.WriteLine(exception.Message);
        }

        /**
        * ------------------------------------------------------------------------------
        * | Escenario:  Declaracion de industria y comercio extemporanea Incorrecta
        * ------------------------------------------------------------------------------
        * | HU No.: 1
        * -------------------------------------------------------------------------
        * | Criterios de aceptación: 
        * ------------------------------------------------------------------------------
        * | Dado:  Dado: dado que el usuario quiere declarar a tiempo $781242 cop 
        * | 
        * ------------------------------------------------------------------------------
        * | Entonces: El sistema debe lanzar la siguiente excepción ya que no se le aplicara una sancion.
        * | "Warnig #2 no contienen una sancion Extemporanea '#0002'"
        * ------------------------------------------------------------------------------
        */


  

        [Test()]
        public void TestDeclaracionSinExtemporanidad()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            var exception = Assert.Catch<Exception>(() => {
                extem.Liquidar(781242f, new DateTime(2019, 05, 18), false);
            });
            Assert.AreEqual(exception.Message,"Warnig #2 no contienen una sancion Extemporanea '#0002'");
            Console.WriteLine(exception.Message);
        }



        /**
         * ------------------------------------------------------------------------------
         * | Escenario: Declaracion de industria y comercio extemporanea Correcta sin emplazamiento
         * | teniendo 1 año de atrazo en su pago
         * ------------------------------------------------------------------------------
         * | HU No.: 1
         * ------------------------------------------------------------------------------
         * | Citerios de aceptación: Dado el Valor es mayor a Cero y con emplazamiento 
         * | cuando Liquide la sanción La Sanción por extemporaneidad es igual Valor 
         * | Declarado* Cantidad de meses de Extemporaneidad* 10%
         * ------------------------------------------------------------------------------
         * | Dado: la fecha máxima para realizar el pago es el día 5 del mes actual del año
         * | actual (Para este mes: 18/Mayo/2019)
         * ------------------------------------------------------------------------------
         * | Cuando: Se va a declarar un total de $1000000 el 18 de mayo del 2018  
         * | con emplazamiento
         * ------------------------------------------------------------------------------
         * | Entonces: se debe mostrar un mensaje indicando que,
         * | "tiene una sancion de $1200000 y su total a pagar es de $2200000"
         * ------------------------------------------------------------------------------
         */

         /*1000000 + 1000000 * 12 * 0,10 ) = 2200000*/

        [Test()]
        public void TestDeclaracionExtemporaneaConEmplazamiento()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            float prueba = extem.Liquidar(1000000f, new DateTime(2018, 05, 18), true);
            Assert.AreEqual(2200000, prueba);
            Console.WriteLine("tiene una sancion de $" + (prueba - 1000000) + " y su total a pagar es de $" + prueba);
        }

        /**
         * ------------------------------------------------------------------------------
         * | Escenario: Declaracion de industria y comercio extemporanea Correcta con emplazamiento
         * | teniendo 1 año de atrazo en su pago
         * ------------------------------------------------------------------------------
         * | HU No.: 1
         * ------------------------------------------------------------------------------
         * | Criterios de aceptación:  Dado que el Valor a Declarar es mayor a Cero y sin emplazamiento
         * | Cuando Liquide la sanción
         * | Entonces la Sanción por extemporaneidad es igual al Valor Declarado* Cantidad de meses de
         * | Extemporaneidad* 5%
         * ------------------------------------------------------------------------------
         * | Dado: la fecha máxima para realizar el pago es el día 5 del mes actual del año
         * | actual (Para este mes: 18/Mayo/2019)
         * ------------------------------------------------------------------------------
         * | Cuando: Se va a declarar un total de $1000000 el 18 de mayo del 2018  
         * | con emplazamiento
         * ------------------------------------------------------------------------------
         * | Entonces: se debe mostrar un mensaje indicando que, 
         * | "tiene una sancion de $500000 y su total a pagar es de $1600000"
         * ------------------------------------------------------------------------------
         */

        /*1000000 + 1000000 * 12 * 0,5 ) = $1600000*/

        [Test()]
        public void TestDeclaracionExtemporaneaSinEmplazamiento2()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            float prueba = extem.Liquidar(1000000f, new DateTime(2018, 05, 18), false);
            Assert.AreEqual(1600000f, prueba);
            Console.WriteLine("tiene una sancion de $"+(prueba-1000000)+" y su total a pagar es de $"+prueba);
        }

        /**
         * ------------------------------------------------------------------------------
         * | Escenario: Declaracion de industria y comercio extemporanea Correcta sin emplazamiento
         * ------------------------------------------------------------------------------
         * | HU No.: 1
         * ------------------------------------------------------------------------------
         * | Citerios de aceptación: Dado que el Valor a Declarar es 0 y sin emplazamiento
         * | Cuando Liquide la sanción
         * | La Sanción por extemporaneidad sin emplazamiento es igual Cantidad de días de Extemporaneidad* 1SMLDV
         * ------------------------------------------------------------------------------
         * | Dado: la fecha máxima para realizar el pago es el día 5 del mes actual del año
         * | actual (Para este mes: 18/Mayo/2019)
         * ------------------------------------------------------------------------------
         * | Cuando: Se va a declarar un total de $0 el 12 de mayo del 2019  
         * | con emplazamiento
         * ------------------------------------------------------------------------------
         * | Entonces: se debe mostrar un mensaje indicando que,
         * | "el salario minimo esta en $828116, tiene una sancion de $4140580 y su total a pagar es de $4968696"
         * ------------------------------------------------------------------------------
         */

        /*  828116 * 6 = 4140580 */

        [Test()]
        public void TestDeclaracionExtemporaneaSinEmplazamientoSmldv()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            float prueba = extem.Liquidar(0f, new DateTime(2019, 05, 12), false);
            Assert.AreEqual(4968696f, prueba);
            Console.WriteLine("el salario minimo esta en $"+extem.smldv+", tiene una sancion de $" + (prueba- extem.smldv) + " y su total a pagar es de $" + prueba);
        }



        /**
         * ------------------------------------------------------------------------------
         * | Escenario: Declaracion de industria y comercio extemporanea Correcta con emplazamiento
         * ------------------------------------------------------------------------------
         * | HU No.: 1
         * ------------------------------------------------------------------------------
         * | Citerios de aceptación: Dado que el Valor a Declarar es 0 y Con emplazamiento
         * | Cuando Liquide la sanción
         * | La Sanción por extemporaneidad sin emplazamiento es igual Cantidad de días de Extemporaneidad* 2SMLDV
         * ------------------------------------------------------------------------------
         * | Dado: la fecha máxima para realizar el pago es el día 5 del mes actual del año
         * | actual (Para este mes: 18/Mayo/2019)
         * ------------------------------------------------------------------------------
         * | Cuando: Se va a declarar un total de $0 el 12 de mayo del 2019  
         * | con emplazamiento
         * ------------------------------------------------------------------------------
         * | Entonces: se debe mostrar un mensaje indicando que,
         * | "el salario minimo esta en $828116 y tiene una sancion de $9109276 y su total a pagar es de $9937392"
         * ------------------------------------------------------------------------------
         */

        /* (828116*2) * 6 = 9937392 */

        [Test()]
        public void TestDeclaracionExtemporaneaConEmplazamientoSmldv()
        {
            LiquidarImpuestoExtem extem = new LiquidarImpuestoExtem();
            float prueba = extem.Liquidar(0f, new DateTime(2019, 05, 12), true);
            Assert.AreEqual(9937392f, prueba);
            Console.WriteLine("el salario minimo esta en $" + extem.smldv + " y tiene una sancion de $" + (prueba - extem.smldv) + " y su total a pagar es de $" + prueba);
        }


    }

}
