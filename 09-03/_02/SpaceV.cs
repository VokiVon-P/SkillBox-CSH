using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class SpaceV : BaseV
    {
        public SpaceV() : base()
        {
            Environment = "Space";
        }

        public override string Move() => "Поехали! К звездам!";
    }
}