using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

using W6Assingment.Model;
using W6Assingment.ViewModel;

namespace W6Assingment
{
    /// <summary>
    /// Interaction logic for InspectWeapon.xaml
    /// </summary>
    public partial class InspectWeapon : Window
    {
        MainWindow main;
        public InspectWeapon(int id, MainWindow mainWindow)
        {
            InitializeComponent();
            main = mainWindow;
            DataContext = new InspectVM(id,mainWindow,this);
            Closing += OnClose;
        }

        public void InitWeapon(Weapon weapon)
        {
            //rarity
            List<int> images = new List<int>();
            for (int i = 0; i < weapon.rarity; i++)
            {
                images.Add(0);
            }
            Stars.ItemsSource = images;

            //image
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(weapon.assets.image,UriKind.Absolute);
            image.EndInit();
            WeaponImage.Source = image;

            //weaponName
            WeaponName.Text = weapon.name;

            //sharpness
            if(weapon.durability!= null)
            {
                List<SharpnessModel> sharpness = new List<SharpnessModel>(7);
                sharpness.Add(new SharpnessModel() { color = "Red", width = weapon.durability[0].red / 4 });
                sharpness.Add(new SharpnessModel() { color = "Orange", width = weapon.durability[0].orange / 4 });
                sharpness.Add(new SharpnessModel() { color = "Yellow", width = weapon.durability[0].yellow / 4 });
                sharpness.Add(new SharpnessModel() { color = "Green", width = weapon.durability[0].green / 4 });
                sharpness.Add(new SharpnessModel() { color = "Blue", width = weapon.durability[0].blue / 4 });
                sharpness.Add(new SharpnessModel() { color = "White", width = weapon.durability[0].white / 4 });
                sharpness.Add(new SharpnessModel() { color = "Purple", width = weapon.durability[0].purple / 4 });
                Sharpness.ItemsSource = sharpness;
            }

            //Rightgrid
            Damage.Text = weapon.attack.raw.ToString();
            DamageType.Text = weapon.damageType;
            ElderSeal.Text = weapon.elderseal == null ? "No Seal" : weapon.elderseal;

            if(weapon.Crafting.UpgradeMaterials != null)
            {
                List<UpgradeModel> upgradeModels = new List<UpgradeModel>();
                for (int i = 0; i < weapon.Crafting.UpgradeMaterials.Length; i++)
                {
                    upgradeModels.Add(new UpgradeModel()
                    {
                        Amount = weapon.Crafting.UpgradeMaterials[i].quantity,
                        Name = weapon.Crafting.UpgradeMaterials[i].item.name
                    });
                }
                UpgradeMaterials.ItemsSource = upgradeModels;
            }
        }

        public void OnClose(object sender, CancelEventArgs e)
        {
            main.Show();
        }
    }
}
