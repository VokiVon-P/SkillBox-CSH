using System.Text;

namespace VehiclesApp
{
    public enum FuelV
    {
        Gas, Gasoline, Disel, Electro, H2, C2H5OH
    }

    abstract public class BaseV
    {
        public string Engine { get; set; }
        public int MaxSpeed { get; set; }
        public string Environment { get; set; }
        public FuelV  Fuel { get; set; }

        public abstract string Move();


        public override string ToString()
        {
            var txt = new StringBuilder();
            txt.Append("============\n");
            txt.Append($"Тип: {base.ToString()}");
            txt.Append($"\nEnvironment = {Environment}");
            txt.Append($"\nEngine = {Engine}");
            txt.Append($"\nFUEL = {Fuel.ToString()}");
            txt.Append($"\nMaxSpeed = {MaxSpeed}");
            txt.Append("\n============");
            return txt.ToString();
        }
        
    }
}
