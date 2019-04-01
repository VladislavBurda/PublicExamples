using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace StudentWeb.Hubs
{
    public class TimeHub : Hub
    {
        private static bool work = false;
        protected IHubContext<TimeHub> _context;

        public TimeHub(IHubContext<TimeHub> context)
        {
            _context = context;
            StartAsync();
        }

        private async Task StartAsync()
        {
            if (!work)
            {
                work = true;
                while (work)
                {
                    string date = DateTime.Now.ToString();
                    if (_context != null && _context.Clients != null)
                        await _context.Clients.All.SendAsync("Send", date);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
