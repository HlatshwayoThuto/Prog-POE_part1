using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace CyberSecurityChatbot
{
    internal class AudioPlayer
    {
        public static void PlayGreeting()
       {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "greeting.wav");

            if (File.Exists(filePath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Audio error: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Greeting audio not found at: " + filePath);
                Console.WriteLine("⚠️ Make sure your project references System.Windows.Extensions via NuGet if SoundPlayer is not working.");
            }
        }
    }
}