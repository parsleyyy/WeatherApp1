using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
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
        {
            city= new City();
            this.LoadDataCommand = new Command(LoadData);
        }
        public string Title
        {
            get { return city.Title; }
            set
            { 
                city.Title = value;
                OnPropertyChanged("Title");
            }
        }
        public double Latitude
        { 
            get { return city.Latitude; }
            set 
            { 
                city.Latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        public double Longitude
        { 
            get { return city.Longitude; }
            set 
            { 
                city.Longitude = value;
                OnPropertyChanged("Longitude");
            }
        }
        public string Time
        { 
            get { return city.Time; }
            set
            { 
                city.Time = value;
                OnPropertyChanged("Time");
            }
        }
        public double Temperature
        { 
            get { return city.Temperature; }
            set
            {
                city.Temperature = value;
                OnPropertyChanged("Temperature");
            }
        }
        public double Windspeed
        { 
            get { return city.Windspeed; }
            set
            {
                city.Windspeed = value;
                OnPropertyChanged("Windpeed");
            }
        }
        private async void LoadData()
        {
            string lat = this.Latitude.ToString().Replace(",", ".");
            string lon = this.Longitude.ToString().Replace(",", ".");
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true;";
            try
            {
                HttpClient client= new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();//выброс исключения, если произошла ошибка
                
                // десерилзация ответа в формате json
                var content = await response.Content.ReadAsStringAsync();
                JsonObject o = JsonObject.Parse(content);
                var str = o.SelectToken(@"$.current_weather");
                var current_watherInfo = JsonConvert.DeseriaLizeObject<WeatherInfo>(str.ToString());
                
                this.Time= current_watherInfo.Time;
                this.Temperature= current_watherInfo.Temperature;
                this.Windspeed= current_watherInfo.Windspeed;
            }
            catch (Exception ex)
            { }
        }
        public void OnPropertyChanged(string prop="")
        {
            if (PropertyChanged != null) 
            { 
                PropertyChanged(this, new PropertyChangingEventArgs(prop));
            }
        }
    }
}
