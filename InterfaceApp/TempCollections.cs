using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApp
{
    public class SideBarHashTags : ObservableCollection<string>
    {
        public SideBarHashTags()
        {
            Add("#Цой Жив");
            Add("#Ямакаси");
            Add("Квантовый скачек");
            Add("#кинопоиск");
            Add("#рубль");
            Add("#селфи");
            Add("#ужин");
            Add("#игры");
        }
    }
}
