using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp1
{
    internal class CityViewModel: INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanged;
        public City city;
        public ICommand LoadDataCommand { protected set; get; }
        public CityViewModel()
    }
}
