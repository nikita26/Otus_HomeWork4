using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus_HomeWork4
{
    public class App : BackgroundService
    {
        private IGame _game;
        public App(IGame game)
        {
            _game = game;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() => { _game.Start(); });
        }
    }
}
