using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Configuration;
using KdTree;
using KdTree.Math;

namespace Earthquakes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Earthquake> earthquakes = new ObservableCollection<Earthquake>();
        HashSet<string> codes = new HashSet<string>();
        // KD-Tree is using to find the nearest cities by latitude and longitude
        KdTree<float, string> kdTree = new KdTree<float, string>(2, new FloatMath()); 

        readonly string uri = ConfigurationManager.AppSettings["AllHourGeojsonUrl"];
        readonly int nearestCityCount = int.Parse(ConfigurationManager.AppSettings["NearestCityCount"]);
        readonly int updatedInterval = int.Parse(ConfigurationManager.AppSettings["SourceUpdatIntervalMin"]);
        readonly int retryCount = 3;
        int intervalSec = 0;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //load cities from the resource file worldcities.csv into KD-Tree
                LoadCities();
                //Load the earchquakes into ObservableCollection for the past one hour. Url for GeoJSON is getting from App.config
                LoadEarthquakes();
                //bind the data to list view
                LvQuakes.ItemsSource = earthquakes;
                LastUpdated.Text = "Updated " + DateTime.Now.ToString("hh:mm:ss tt");
                //open a back ground thread to upda the UI automatically
                Thread thread = new Thread(new ThreadStart(UpdateEarthquakeList));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception e)
            {
                TxtMessage.Visibility = Visibility.Visible;
                TxtMessage.Text = "Error: Please try again later";
            }
        }
        /// <summary>
        /// Load the cities from the resource file worldcities.csv into KD-Tree.
        /// KD-Tree is using to find the nearest cities by latitude and longitude
        /// </summary>
        private void LoadCities()
        {
            var fileContent = Properties.Resources.worldcities.Split('\n');
            int headerCount = 1;
            var csv = fileContent.Select(l => l.Split(',')).ToArray();
            foreach (var rw in csv.Skip(headerCount).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
            {
                float latitude, longitude;
                if (float.TryParse(rw[7], out latitude) && float.TryParse(rw[8], out longitude))
                {
                    kdTree.Add(new[] { longitude, latitude }, rw[6].Trim('"'));
                }
            }
        }

        private void UpdateEarthquakeList()
        {
            while (true)
            {
                //Delay the specified interval to get GeoJSON. 
                //GeoJSON is updating in every 5 minutes. This interval is stored in App.config
                Thread.Sleep(intervalSec * 1000);
                Application.Current.Dispatcher
                    .BeginInvoke(new Action(() => LoadEarthquakes()));
                //Refresh updated time on UI
                LastUpdated.Dispatcher.Invoke(new Action(() =>
                    LastUpdated.Text = "Updated " + DateTime.Now.ToString("hh:mm:ss tt")));
            }
        }

        private void LoadEarthquakes()
        {
            var result = Service.GetJsonData(uri, retryCount);

            //Calculate the seconds for the next execution to invoke the GeoJSON. 
            //GeoJSON is updating in every 5 minutes. This interval is stored in App.config
            var generatedTimeDiff = DateTime.UtcNow.Subtract(
                FromUnixTime(long.Parse(result["metadata"]["generated"].ToString()))).Seconds;
            intervalSec = updatedInterval * 60 - generatedTimeDiff;

            foreach (var feature in result["features"].Children())
            {
                var f = feature["properties"];
                if (codes.Contains(f["code"].ToString())) continue;
                codes.Add(f["code"].ToString());

                var longitude = decimal.Parse(feature["geometry"]["coordinates"][0].ToString());
                var latitude = decimal.Parse(feature["geometry"]["coordinates"][1].ToString());

                var nearestCities = string.Join(",", kdTree.GetNearestNeighbours(
                    new[] { (float)longitude, (float)latitude },
                    nearestCityCount).Select(c => c.Value));

                //This will automatically update the UI
                earthquakes.Insert(0, new Earthquake
                {
                    Place = f["place"].ToString(),
                    DateTime = FromUnixTime(long.Parse(f["time"].ToString())),
                    Magnitude = decimal.Parse(f["mag"].ToString()),
                    NearestCities = nearestCities,
                    Latitude = latitude,
                    Longitude = longitude,
                    Depth = decimal.Parse(feature["geometry"]["coordinates"][2].ToString())
                });
            }
        }
        /// <summary>
        /// Convert milliseconds since the epoch to date/time
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        private DateTime FromUnixTime(long unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(unixTime / 1000.0);
        }
    }

}
