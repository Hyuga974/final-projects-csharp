using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace gtksharp_proj_linux
{
	class MainWindow : Window
	{
		private string api_key = null;
		private string base_url = "http://api.openweathermap.org/data/2.5/";
		private string base_icon_url = "http://openweathermap.org/img/wn/";
		private string lang = "fr";
		private string unit = "metric";
		private HttpClient client = new HttpClient();
		[UI] private SearchEntry searchBarmeteo = null;
		[UI] private Button searchSendmeteo = null;
		[UI] private Label cityLabel = null;
		[UI] private Label temperatureLabel = null;
		[UI] private Label humidityLabel = null;
		[UI] private Label meteoDescription = null;
		[UI] private Label latLabel = null;
		[UI] private Label lonLabel = null;
		[UI] private Button buttonExit = null;
		[UI] private Image meteoImage = null;

		[UI] private Button saveSettings = null;

		[UI] private Entry defaultCity = null;
		[UI] private ComboBox langDropdown = null;
		[UI] private ListStore langListStore = null;

		private string[] langs = new string[]{
			"af","al","ar","az","bg","ca","cz","da","de","el","en","eu","fa","fi","fr","gl","he","hi","hr","hu","id","it","ja","kr","la","lt","mk","no","nl","pl","pt","pt_br","ro","ru","sv","sk","sl","sp","sr","th","tr","ua","vi","zh_cn","zh_tw","zu"
			};

		private void editMeteoValue(HttpResponseMessage content)
		{
			if ((int) content.StatusCode == 404) {
				cityLabel.Text = "Ville non trouvée.";
				temperatureLabel.Text = "";
				humidityLabel.Text = "";
				meteoDescription.Text = "";
				latLabel.Text = "";
				lonLabel.Text = "";
				DisplayImage(null);
			} else {
				JObject result = JsonConvert.DeserializeObject<JObject>(content.Content.ReadAsStringAsync().Result);
				cityLabel.Text = (string) result["name"];
				temperatureLabel.Text = (double) result["main"]["temp"] + "°C";
				humidityLabel.Text = "Humidité : " + (double) result["main"]["humidity"] + "%";
				meteoDescription.Text = (string) result["weather"][0]["description"];
				latLabel.Text = "Latitude : " + (double) result["coord"]["lat"];
				lonLabel.Text = "Longitude : " + (double) result["coord"]["lon"];
				DisplayImage(base_icon_url + result["weather"][0]["icon"] + ".png");
			}
		}
		public MainWindow() : this(new Builder("MainWindow.glade")) { }

		private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
		{
			builder.Autoconnect(this);

			foreach (string lang in langs)
			{
				langListStore.SetValue(langListStore.Append(), 0, lang);
			}

			client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

			if (!File.Exists("./key.json")) {
				JObject json_key = new JObject(
					new JProperty("api_key", "")	
				);
				File.WriteAllText("./key.json", json_key.ToString());
				using (StreamWriter file = File.CreateText("./key.json"))
				using (JsonTextWriter writer = new JsonTextWriter(file))
				{
					json_key.WriteTo(writer);
				}

				Console.WriteLine("key.json file not found. The file was created.");
				Console.WriteLine("Please update api_key property in the key.json file.");
				Application.Quit();
			} else {
				JObject o1 = JObject.Parse(File.ReadAllText("./key.json"));
				using (StreamReader file = File.OpenText("./key.json"))
				using (JsonTextReader reader = new JsonTextReader(file))
				{
					JObject o2 = (JObject)JToken.ReadFrom(reader);
					if ((string) o2["api_key"] == "")
					{
						Console.WriteLine("Please update api_key property in the key.json file.");
						Application.Quit();
					} else {
						api_key = (string) o2["api_key"];
					}
				}
			}

			if (!File.Exists("./options.json"))
			{
				JObject defaultOption = new JObject(
					new JProperty("lang", "fr"),
					new JProperty("default_city", "")
				);
				updateOptionJson(defaultOption);
			} else {
				JObject options = readOptionJson();
				lang = (string) options["lang"];
				if ((string) options["default_city"] != "")
				{
					string query = base_url + "weather?q=" + options["default_city"] + "&units=" + unit + "&lang=" + lang + "&appid=" + api_key;
					var content = client.GetAsync(query).Result;
					editMeteoValue(content);
				}
			}

			DeleteEvent += Window_DeleteEvent;
			saveSettings.Clicked += saveSettings_Clicked;
			searchSendmeteo.Clicked += searchSendmeteo_Clicked;
			buttonExit.Clicked += buttonExit_Clicked;
		}

		private void DisplayImage(string url)
		{
			if (url == null)
			{
				meteoImage.Pixbuf = null;
			} else {
				using (HttpClient httpClient = new HttpClient())
				{
					Task<byte[]> dataArr = httpClient.GetByteArrayAsync(url);
					dataArr.Wait();

					meteoImage.Pixbuf = new Gdk.Pixbuf (dataArr.Result);
				}
			}
		}

		private void searchSendmeteo_Clicked(object sender, EventArgs a)
		{
			string query = base_url + "weather?q=" + searchBarmeteo.Text + "&units=" + unit + "&lang=" + lang + "&appid=" + api_key;
			HttpResponseMessage content = client.GetAsync(query).Result;
			editMeteoValue(content);
		}

		private JObject readOptionJson() {
			JObject o1 = JObject.Parse(File.ReadAllText("./options.json"));

			// read JSON directly from a file
			using (StreamReader file = File.OpenText("./options.json"))
			using (JsonTextReader reader = new JsonTextReader(file))
			{
				JObject o2 = (JObject)JToken.ReadFrom(reader);
				return o2;
			}
		}

		private void updateOptionJson(JObject data) {
			File.WriteAllText("./options.json", data.ToString());

			// write JSON directly to a file
			using (StreamWriter file = File.CreateText("./options.json"))
			using (JsonTextWriter writer = new JsonTextWriter(file))
			{
				data.WriteTo(writer);
			}
		}

		private void saveSettings_Clicked(object sender, EventArgs a)
		{
			JObject newSettings = new JObject(
				new JProperty("lang", langs[langDropdown.Active]),
				new JProperty("default_city", defaultCity.Text)
			);
			updateOptionJson(newSettings);
			lang = langs[langDropdown.Active];
		}

		private void Window_DeleteEvent(object sender, DeleteEventArgs a)
		{
			Application.Quit();
		}

		private void buttonExit_Clicked(object sender, EventArgs a)
		{
			Application.Quit();
		}

	}
}