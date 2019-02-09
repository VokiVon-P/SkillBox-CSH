using System;
using CHelpers;


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

        static void TestInfo()
        {
            //GetInfo(new BaseV());
            GetInfo(new BoatV());
            GetInfo(new YachtV());
            GetInfo(new SubmarineV());
            GetInfo(new SpaceV());
            GetInfo(new SpaceV
            {
                Name = "Space Cowboy",
                Fuel = FuelV.C2H5OH,
                MaxSpeed = 300000,
                Engine = "Душа",
                Environment = "Космос"
            });
            GetInfo(new TrainV());
            GetInfo(new CarV());
            GetInfo(new AirCargoV());
            GetInfo(new BalloonV());
        }
        
        static void Main(string[] args)
        {

            TestInfo();

            ConsoleHelper.KeepConsole();

        }
    }
}
