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

        static void TestBoatStation()
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.WriteLine("===  Корабли  ===");
            Console.WriteLine();

            //////////////////////
            // Тест для кораблей
            Station<BoatV> macaoStation = new Station<BoatV>("Macao");
            Station<BoatV> nyStation = new Station<BoatV>("New York");
            Station<BoatV> hkStation = new Station<BoatV>("Hong Kong");

            BoatV boat01 = new BoatV { Name = "Михаил Светлов" };
            YachtV yacht01 = new YachtV { Name = "White Hawk"};

            Console.ForegroundColor = ConsoleColor.Cyan;

            // корабль в Нью-Йорк
            Console.WriteLine($"{boat01.Name} => {boat01.Move()} в {nyStation.Name}");
            nyStation.ArriveAtStation(boat01);
            Console.WriteLine($"В порт {nyStation.Name} прибыл корабль {boat01.Name}");
            Console.WriteLine($"В порту {nyStation.Name} находиться {nyStation.NumberOfV} кораблей");
            Console.WriteLine();

            // яхта в Гон-Конг
            Console.WriteLine($"{yacht01.Name} => {yacht01.Move()} в {hkStation.Name}");
            hkStation.ArriveAtStation(yacht01);
            Console.WriteLine($"В порт {hkStation.Name} прибыл корабль {yacht01.Name}");
            Console.WriteLine($"В порту {hkStation.Name} находиться {hkStation.NumberOfV} кораблей");
            Console.WriteLine();

            // корабль из Нью-Йорка в Макао
            Console.WriteLine($"{boat01.Name} => {boat01.Move()} в {macaoStation.Name}");
            nyStation.LeaveStation(boat01);
            Console.WriteLine($"В порту {nyStation.Name} находиться {nyStation.NumberOfV} кораблей");
            Console.WriteLine();

            // яхта из Гон-Конга в Макао
            Console.WriteLine($"{yacht01.Name} => {yacht01.Move()} в {macaoStation.Name}");
            hkStation.LeaveStation(yacht01);
            Console.WriteLine($"В порту {hkStation.Name} находиться {hkStation.NumberOfV} кораблей");
            Console.WriteLine();

            // яхта и корабль в Макао
            Console.WriteLine($"В порт {macaoStation.Name} прибыл корабль {boat01.Name}");
            macaoStation.ArriveAtStation(boat01);
            //Console.WriteLine($"В порт {macaoStation.Name} прибыл корабль {yacht01.Name}");
            macaoStation.ArriveAtStation(yacht01);
            for (int i = 0; i < 7; i++)
            {
                macaoStation.ArriveAtStation(new BoatV{Name = $"boat_{i}"});
            }
            Console.WriteLine($"В порту {macaoStation.Name} находиться {macaoStation.NumberOfV} кораблей");
            Console.WriteLine($"Пришла ли в порт {macaoStation.Name} яхта {yacht01.Name} ?");
            
            string ans = macaoStation.IsOnStation(yacht01) ? $"Да! Яхта {yacht01.Name} в порту {macaoStation.Name}" : $"Нет! Яхты {yacht01.Name} нет в порту {macaoStation.Name}";
            Console.WriteLine(ans);
            Console.WriteLine();
            
            Console.ForegroundColor = oldColor;
        }

        static void TestTrainStation()
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.WriteLine("===  Поезда  ===");
            Console.WriteLine();

            //////////////////////
            // Тест для поездов
            Station<TrainV> train_st_London = new Station<TrainV>("Ватерлоо \t: Лондон");
            Station<TrainV> train_st_Paris  = new Station<TrainV>("Аустерлиц \t: Париж");


            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"На вокзале {train_st_London.Name} \t находиться {train_st_London.NumberOfV} поездов");
            Console.WriteLine($"На вокзале {train_st_Paris.Name} \t находиться {train_st_Paris.NumberOfV} поездов");
            Console.WriteLine();
            Console.WriteLine("================================================");
            Random rnd = new Random();

            int max = rnd.Next(25, 50);
            for (int i = 0; i < max; i++)
            {
                TrainV tr_ = new TrainV { Name = $" Поезд_{i}" };
                Station<TrainV> st_ = (i % 2 == 0 ? train_st_London : train_st_Paris);
                st_.ArriveAtStation(tr_);
                Console.WriteLine($"Вокзал \t{st_.Name} \t - прибывает\t{tr_.Name}");
                // отправление поездов
                if (rnd.Next(100) % 2 == 0) Console.WriteLine($"Вокзал\t{st_.Name} \t - отправляется\t{st_.LeaveStationFirst().Name}");
                    
            }
            Console.WriteLine("================================================");
            Console.WriteLine();
            Console.WriteLine($"На вокзале {train_st_London.Name} \t находиться {train_st_London.NumberOfV} поездов");
            Console.WriteLine($"На вокзале {train_st_Paris.Name} \t находиться {train_st_Paris.NumberOfV} поездов");

            Console.WriteLine();

            Console.ForegroundColor = oldColor;
        }

        static void Main(string[] args)
        {

            //TestInfo();
            TestBoatStation();
            TestTrainStation();
            ConsoleHelper.KeepConsole();

        }
    }
}
