using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RPS_Game
{
    class PlayGame
    {

        //install the logger for a console app.
        private readonly ILogger _logger;
        public PlayGame(ILogger<PlayGame> logger)
        {
            _logger = logger;
        }

        public void startGame()
        {
            Console.WriteLine("Enter Player1 Name: "); //prompts user to input player 1 name
            String player1 = Console.ReadLine(); //takes input from user and stores it as player 1 name
            Console.WriteLine("Enter Player2 Name: "); //prompts user to input player 2 name
            String player2 = Console.ReadLine(); //takes input from user and stores it as player 2 name

            Player p1 = new Player();
            p1.Name = "Mark";

            _logger.LogInformation("LogInformation = Hello. My name is Log LogInformation");
            _logger.LogWarning("LogWarning = At {time} Now I'm Loggy McLoggerton", DateTime.Now);
            _logger.LogCritical("LogCritical = As of now, I'm Scrog McLog");
            _logger.LogDebug("Log Debug");//not printed to console
            _logger.LogError("LogError");
            _logger.LogTrace("Log Trace = Tracing my way back home.");//not printed to console


            Console.WriteLine($"My name is {p1.Name}.");
        }
    }
}
