using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehiclesApp
{
    public class BalloonV : AirV, ITouristHelper
    {
        public BalloonV() : base() { Fuel = FuelV.H2; }

        public void MakeMeWonder()
        {
            Console.WriteLine("Вот это да, какие горы!");
        }

        public override string Move() => "Медленно летим над Швейцарией";

        public void SightSeeing()
        {
            Console.WriteLine("Смотрим на Женевское озеро с высоты птичьего полета");
        }
    }
}