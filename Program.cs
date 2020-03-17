using DiscordBotHavi.Classes;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DiscordBotHavi
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordBot bot = new DiscordBot();

          
            // Runs bot indefinitely
            bot.RunAsync().GetAwaiter().GetResult();

            
            
        }
    }
}
