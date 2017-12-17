using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HowIsTheWeather
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }
        private string _temperature;
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged("Temperature");
            }
        }
        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        private string _wind;
        public string Wind
        {
            get { return _wind; }
            set
            {
                _wind = value;
                OnPropertyChanged("Wind");
            }
        }
        private string _humidity;
        public string Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged("Humidity");
            }
        }
        private string _pressure;
        public string Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                OnPropertyChanged("Pressure");
            }
        }
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged("Search");
            }
        }
        public Command SearchCommand { get; set; } = new Command();
        public MainWindowViewModel()
        {
            SearchCommand.Func = DoSearch;
        }
        private void DoSearch(object parameter)
        {
            WeatherAPI api = new WeatherAPI();
            Weather w = api.GetWeather(Search);
            if (w == null) return;
            if (w.Query.Count == 0)
            {
                MessageBox.Show("Нет результата по запросу");
                return;
            }
            City = w.Query.Results.Channel.Location.City;
            Location = $"{w.Query.Results.Channel.Location.Region.Trim()},\n{w.Query.Results.Channel.Location.Country.Trim()}";
            Temperature = $"{w.Query.Results.Channel.Item.Condition.Temp} {w.Query.Results.Channel.Units.Temperature}";
            Status = w.Query.Results.Channel.Item.Condition.Text;
            Wind = $"{w.Query.Results.Channel.Wind.Speed} {w.Query.Results.Channel.Units.Speed}";
            Humidity = w.Query.Results.Channel.Atmosphere.Humidity;
            Pressure = $"{w.Query.Results.Channel.Atmosphere.Pressure} {w.Query.Results.Channel.Units.Pressure}";
            using (StreamWriter file = new StreamWriter("history.txt", true ))
                file.WriteLine(Search);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public Action<object> Func { get; set; }
        public void Execute(object parameter)
        {
            Func(parameter);
        }
    }
}