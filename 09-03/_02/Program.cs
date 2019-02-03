using System;

namespace VehiclesApp
{
    class Program
    {
        static void GetInfo(BaseV bv)
        {
            Console.WriteLine(bv);

            if (bv is ICarryCargo) (bv as ICarryCargo).Load();
            // if (bv.GetType().GetInterface("ICarryCargo") != null) (bv as ICarryCargo).Load(); 
            Console.WriteLine(bv.Move());
            if (bv is ICarryCargo) (bv as ICarryCargo).Unload();

            if (bv is ITouristHelper)
            {
                (bv as ITouristHelper).MakeMeWonder();
                (bv as ITouristHelper).SightSeeing();
            }
                

            Console.WriteLine();

        }

        static void Main(string[] args)
        {

            //GetInfo(new BaseV());
            GetInfo(new BoatV());
            GetInfo(new YachtV());
            GetInfo(new SubmarineV());
            GetInfo(new SpaceV());
            GetInfo(new SpaceV
            {
                Fuel = FuelV.C2H5OH,
                MaxSpeed = 300000,
                Engine = "Душа",
                Environment = "Космос"
            } );
            GetInfo(new TrainV());
            GetInfo(new CarV());
            GetInfo(new AirCargoV());
            GetInfo(new BalloonV());


            /////////////////////////////////////////////
            // Keep the console window open in debug mode.
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
