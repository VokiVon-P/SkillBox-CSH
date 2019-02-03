using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class SubmarineV : WaterV
    {
        public int DivingDepth { get; set; }

        public override string Move() => "По местам! Погружение!";
    }
}