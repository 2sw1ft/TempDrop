using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;

namespace TempDropTweaks
{
    public class TempDropOptions
    {
        public static float temp_drop_celsius_max = 1f;

    }

    class TempDropTweaks
    {
        private static readonly string mod_options_folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string options_file_name = Path.Combine(mod_options_folder, "temp-drop.json");


        public static void OnLoad()
        {
            Debug.Log("[TempDrop] Version " + Assembly.GetExecutingAssembly().GetName().Version);


        }

        internal class TempDropSettings : ModSettingsBase
        {
            [Section("Temp Drop Settings")]

            [Name("Rate of Temp Drop")]
            [Description("The lower the value, the warmer. Default settings: 0 - piligrim, 5 - voyageur, 10 - stalker, 20 - interloper")]
            [Slider(0f, 30f)]
            public float temp_drop_celsius_max = 18f;

            protected override void OnConfirm()
            {
                TempDropOptions.temp_drop_celsius_max = temp_drop_celsius_max;


                string json_opts = FastJson.Serialize(this);

                File.WriteAllText(Path.Combine(mod_options_folder, options_file_name), json_opts);
            }
        }

        internal static class TempDropLoad
        {
            private static TempDropSettings custom_settings = new TempDropSettings();

            public static void OnLoad()
            {
                if (File.Exists(Path.Combine(mod_options_folder, options_file_name)))
                {
                    string opts = File.ReadAllText(Path.Combine(mod_options_folder, options_file_name));
                    custom_settings = FastJson.Deserialize<TempDropSettings>(opts);

                    TempDropOptions.temp_drop_celsius_max = custom_settings.temp_drop_celsius_max;

                }

                custom_settings.AddToModSettings("Temperature Drop");
            }
        }
    }
}
