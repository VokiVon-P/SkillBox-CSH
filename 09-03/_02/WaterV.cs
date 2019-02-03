using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class WaterV : BaseV
    {

        public WaterV() : base()
        {
            Environment = "Water";
        }

        public override string Move()
        {
            return "Движемся по воде";
        }
    }
}