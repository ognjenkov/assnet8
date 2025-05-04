using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace assnet8.Services.SignalR;

public class InvitesUserHub : Hub
{
    public async Task JoinGroup(string groupId)
    {
        try
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId) || userId != groupId)
            {
                await Clients.Caller.SendAsync("Unauthorized", "1");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        catch (System.Exception)
        {
            await Clients.Caller.SendAsync("Unauthorized", "2");
        }
    }

    public async Task LeaveGroup(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
    }
}