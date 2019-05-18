using System;
using System.Collections.Generic;
using System.Text;

namespace ServicioImpuesto.Domain
{

    //Clase de liquidacion de impuesto Extemporaneo
    public class LiquidarImpuestoExtem
    {
        public float valorADeclarar { private set; get; }
        public float smldv { private set; get; }
 
        private DateTime? maxFecha =  null;

        private float impuestoSinEmplazamiento = 0.05f;
        private float impuestoConEmplazamiento = 0.10f;

        public LiquidarImpuestoExtem()
        {
            this.valorADeclarar = 0;
            this.smldv = 828116;
        }


        public DateTime? getMaxFecha() {
            return this.maxFecha;
        }

        public void setMaxFecha(DateTime fechaPago) {
            int  dia = fechaPago.Day, mes = fechaPago.Month, año = fechaPago.Year;


            int diasDelSiguienteMes = DateTime.DaysInMonth(año, (mes >= 12) ? 1 : mes + 1);
            int diasDelMes = DateTime.DaysInMonth(año, mes);
            if ((dia +5) > diasDelSiguienteMes)
            {
                dia = (dia + 5) - diasDelMes;
                mes = mes+1;
            }
            this.maxFecha = new DateTime(mes > 12 ? 1 : año, mes, dia);
        }

        public float Liquidar(
            float valorDeclarado /*Valor a declarar*/,
            DateTime fechaPago /*Fecha estipulada para el pago*/,
            bool emplazamiento = false /*Solicitud o carta de aviso de pago*/){
         
            /*El pago de una declaracion de renta, debe ser de $0 pesos y/o mayor*/
            if (valorDeclarado < 0) { throw new Exception("Error #1 pago menor a $0 pesos, codigo '#0001'"); }

            if (fechaPago.ToShortDateString() == DateTime.Now.ToShortDateString()) { throw new Exception("Warnig #2 no contienen una sancion Extemporanea '#0002'"); }

            this.valorADeclarar = valorDeclarado;
            this.setMaxFecha(fechaPago);
            DateTime? fechaMax = this.getMaxFecha();

            if(DateTime.Now >= fechaMax.Value)
            {

                int mesesAcobrar = DateTime.Now.Month - fechaMax.Value.Month,
                    diasAcobrar = DateTime.Now.Day - fechaMax.Value.Day,
                    añosACobrar = DateTime.Now.Year - fechaMax.Value.Year;

                if (diasAcobrar > 0) mesesAcobrar++;
                if (mesesAcobrar < 0) mesesAcobrar += 12;

                if(añosACobrar > 0) {
                    if(fechaMax.Value.Month <= DateTime.Now.Month) { mesesAcobrar += añosACobrar * 12; }
                }

                //Cuando el Valor a Declarar es mayor a Cero 
                if (valorDeclarado > 0)
                {
                    this.valorADeclarar += (!emplazamiento) ?
                        //sin emplazamiento
                        //Sanción por extemporaneidad: Valor Declarado* Cantidad de días de Extemporaneidad* 5%
                        valorDeclarado * mesesAcobrar * this.impuestoSinEmplazamiento :
                        //con emplazamiento
                        //Sanción por extemporaneidad: Valor Declarado* Cantidad de días de Extemporaneidad* 10%
                        valorDeclarado * mesesAcobrar * this.impuestoConEmplazamiento;
                }
                // Cuando el Valor a Declarar es igual a Cero
                else if(valorDeclarado == 0)
                {
                    var diasFecha = DateTime.Now - fechaMax.Value;
                    int dias = diasFecha.Days;
                    this.valorADeclarar += (!emplazamiento) ?
                       //sin emplazamiento
                       //Sanción por extemporaneidad: Cantidad de dias de Extemporaneidad* 1SMLDV
                       dias * this.smldv :
                       //con emplazamiento
                       //Sanción por extemporaneidad: Cantidad de dias de Extemporaneidad* 2SMLDV
                       dias * (this.smldv*2);
                }
            }
            //retorno el valor de la sanciones
            return this.valorADeclarar;
        }
    }
}
