using System.Collections.Generic;

namespace VehiclesApp
{
    /*
     * Параметризованный класс станции
     * с минимальным функционалом
     */
    internal class Station<T>
    {
        private List<T> _OnStationV { get; }


        public string Name { get; set; }

        // кол-во транспорта на станции
        public int NumberOfV => _OnStationV.Count;
        
        public void ArriveAtStation(T v) => _OnStationV.Add(v);
        public bool LeaveStation(T v) => _OnStationV.Remove(v);
        public bool IsOnStation(T v) => _OnStationV.Contains(v);
        
        public Station(string name)
        {
            _OnStationV = new List<T>();
            Name = name;
        }
        
    }
}
