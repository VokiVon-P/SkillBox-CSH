using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclesApp
{
    class TrainV : GroundV, ICarryCargo
    {
        public override string Move() => "Ту-туууууу";

        void ICarryCargo.Unload() => Console.WriteLine(GetType() + "_ICarryCargo.Unload()");
        void ICarryCargo.Load() => Console.WriteLine(GetType() + "_ICarryCargo.Load()");

    }
}
