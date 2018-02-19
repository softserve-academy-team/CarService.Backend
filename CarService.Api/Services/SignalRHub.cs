using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Api.Services
{
    public class SignalRHub: Hub
    {
        public async Task ConfirmEmail(string message)
        {
            await Clients.All.InvokeAsync("ConfirmEmail", message);
        }
    }
}
