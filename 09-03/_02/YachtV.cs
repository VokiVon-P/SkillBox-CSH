using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class YachtV : BoatV
    {
        public bool HasSail { get; set; }

        public override string Move() => "Поднять паруса!!!";
    }
}