using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclesApp
{
    /*
     * Параметризованный класс станции 
     */
    class Station<T>
    {
        public string Name { get; set; }
        public int NumberOfV => _OnStationV.Count;

        private List<T> _OnStationV { get; }

        public void ArriveAtStation(T v)
        {
        }

        T LeaveStation()
        {
            return default(T);
        }

        public Station()
        {
            _OnStationV = new List<T>();
        }
    }
}
