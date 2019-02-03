using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class BoatV : WaterV
    {
        // водоизмещение корабля
        public int Displacement { get; set; }

        public override string Move() => "Полный вперед!";
    }
}