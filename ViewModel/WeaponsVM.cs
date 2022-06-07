using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using W6Assingment.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace W6Assingment.ViewModel
{
    public class WeaponsVM : ViewModelBase
    {
        private List<Weapon> AllWeapons;
        private MainWindow MainWindow;
        private int Index = 0;
        private Dictionary<string, int> weaponNameToID = new Dictionary<string, int>();

        public RelayCommand OnLeftButtonCommand { get; private set; }
        public RelayCommand OnRightBUttonCommand { get; private set; }
        public RelayCommand OnWeaponCommand { get; private set; }

        public WeaponsVM(MainWindow _window)
        {
            MainWindow = _window;
            OnLeftButtonCommand = new RelayCommand(OnLeftButton);
            OnRightBUttonCommand = new RelayCommand(OnRightButton);
            OnWeaponCommand = new RelayCommand(OnWeaponClicked);
            Init();
        }

        public async void Init()
        {
            const string Url = "https://mhw-db.com/weapons?p={\"name\": true, \"id\": true, \"assets.image\": true}";
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(Url);
                    if(!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(response.ReasonPhrase);
                    }
                    string json = await response.Content.ReadAsStringAsync();
                    //deserialise json
                    var jsonTask = Task.Run(() => { return JsonConvert.DeserializeObject<List<Weapon>>(json); });
                    jsonTask.ConfigureAwait(true).GetAwaiter().OnCompleted(() => { 
                        AllWeapons = jsonTask.Result;
                        UpdateLists();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void UpdateLists()
        {
            List<Weapon> toshow = AllWeapons.GetRange(Index, 9);
            foreach (Weapon weapon in toshow)
            {
                if (!weaponNameToID.ContainsKey(weapon.name))
                    weaponNameToID.Add(weapon.name,weapon.Id);

                weapon.assets.bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                weapon.assets.bitmapImage.BeginInit();
                weapon.assets.bitmapImage.UriSource = new Uri(weapon.assets.image, UriKind.Absolute);
                weapon.assets.bitmapImage.EndInit();
                weapon.OnWeaponCommand = new RelayCommand(OnWeaponClicked);
            }
            MainWindow.updateListBox(toshow);

            Index += 9;
            toshow = AllWeapons.GetRange(Index, 9);
            foreach (Weapon weapon in toshow)
            {
                if (!weaponNameToID.ContainsKey(weapon.name))
                    weaponNameToID.Add(weapon.name, weapon.Id);

                weapon.assets.bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                weapon.assets.bitmapImage.BeginInit();
                weapon.assets.bitmapImage.UriSource = new Uri(weapon.assets.image, UriKind.Absolute);
                weapon.assets.bitmapImage.EndInit();
                weapon.OnWeaponCommand = new RelayCommand(OnWeaponClicked);
            }
            MainWindow.updateListBox2(toshow);
            Index += 9;
        }

        public void OnLeftButton(object message)
        {
            Index -= 18;
            if (Index != 0) Index -= 18;
            UpdateLists();
        }

        public void OnRightButton(object message)
        {
            if (Index + 18 >= AllWeapons.Count)
                Index = AllWeapons.Count - 18;
            UpdateLists();
        }

        public void OnWeaponClicked(object message)
        {
            //get id by name
            InspectWeapon inspect = new InspectWeapon(weaponNameToID[message as string], MainWindow);
            inspect.Show();
            MainWindow.Hide();
            //open new page
            //get all data for that weapon
            //display all data
        }
    }
}