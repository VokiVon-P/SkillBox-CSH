using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class GroundV : BaseV
    {
        public GroundV() : base()
        {
            Environment = "Ground";
        }

        public override string Move()
        {
            return "Движемся по земле";
        }
    }
}