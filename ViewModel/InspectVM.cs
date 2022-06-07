using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace W6Assingment.ViewModel
{
    public class InspectVM
    {
        MainWindow MainWindow;
        InspectWeapon InspectWeapon;
        public InspectVM(int weapon, MainWindow mainWindow, InspectWeapon inspectwindow)
        {
            MainWindow = mainWindow;
            InspectWeapon = inspectwindow;
            Init(weapon);
        }

        private async void Init(int weaponId)
        {
            string Url = "https://mhw-db.com/weapons/"+weaponId;
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(Url);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(response.ReasonPhrase);
                    }
                    string json = await response.Content.ReadAsStringAsync();
                    //deserialise json
                    var jsonTask = Task.Run(() => { return JsonConvert.DeserializeObject<Weapon>(json); });
                    jsonTask.ConfigureAwait(true).GetAwaiter().OnCompleted(() => {
                        InspectWeapon.InitWeapon(jsonTask.Result);
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
