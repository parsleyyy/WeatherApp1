using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherApp1;

public partial class MainPage : ContentPage
{
	City MyCity;
	List<City> data;

	CityViewModel viewModel;
	public MainPage()
	{
		InitializeComponent();
		LoadCitis();
	}

	public async void LoadCities()
	{
		// добавление городов
		string json = await SecureStorage.GetAsync("City");
		if (!string.IsNullOrEmpty(json)) 
		{
			data = JsonConvert.DeserializeObject<CityViewModel>(json);
			foreach (var track in data)
			{
				languagePicker.Items.Add(track.Title);
			}
		}
	}
	private void PickerSelectedIndexChanged(object sender, EventArgs e)
	{
		string name = languagePicker.SelectedItem.ToString();
		foreach (var track in data)
		{
			if (name == track.Title)
			{
				DisplayAlert("Уведомление", track.Title + "lat:" +track.Latitude.ToString()
					+ "lon:" + track.Longitude.ToString() "OK");
				// установка контекста данных
			}
		}
	}
}

