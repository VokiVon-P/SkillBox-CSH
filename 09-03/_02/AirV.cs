using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class AirV : BaseV
    {
        public AirV() : base()
        {
            Environment = "Air";
        }

        public override string Move() => "Полетели в Тай!!!";
    }
}