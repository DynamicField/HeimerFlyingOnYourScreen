using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HeimerFlyingOnYourScreen
{
    public static class ResourceData
    {
        public static string[] ImplementedSkins { get; } = new string[]
            {
                "Classic",
                "AlienInvader",
                "ExplodingHeim",
                "PiltoverHeim",
                "Snowmerdinger"
            };
        public static void UpdateImageSource(Image img, bool isTurret = false)
        {
            var skin = Properties.Settings.Default.Skin;
            img.Source = new BitmapImage(new Uri($"pack://application:,,,/HeimerFlyingOnYourScreen;component/HeimerImages/{skin}/{skin}{(isTurret ? "Turret" : "")}.png"));
        }

        public static void UpdateImageSource(Image img, string skinName, bool replaceSettings = true)
        {
            if (!ImplementedSkins.Contains(skinName)) throw new InvalidOperationException("Skin name isn't right :/");
            if (replaceSettings)
            {
                Properties.Settings.Default.Skin = skinName;
                Properties.Settings.Default.Save();
            }
            img.Source = new BitmapImage(new Uri($"pack://application:,,,/HeimerFlyingOnYourScreen;component/HeimerImages/{skinName}/{skinName}.png"));
        }
    }
}
