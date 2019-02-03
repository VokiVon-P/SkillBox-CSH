using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class AirCargoV : AirV, ICarryCargo
    {
        void ICarryCargo.Unload() => Console.WriteLine(GetType() + "_ICarryCargo.Unload()");
        void ICarryCargo.Load() => Console.WriteLine(GetType() + "_ICarryCargo.Load()");

    }
}