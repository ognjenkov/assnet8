using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace assnet8.Services.SignalR;

public class InvitesTeamHub : Hub
{
    public async Task JoinGroup(string groupId)
    {
        try
        {
            var roles = Context.User?.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            var userTeamId = Context.User?.FindFirst("TeamId")?.Value;

            if (string.IsNullOrEmpty(userTeamId) || userTeamId != groupId)
            {
                await Clients.Caller.SendAsync("Unauthorized", "1");
                return;
            }

            if (roles == null || !roles.Any())
            {
                await Clients.Caller.SendAsync("Unauthorized", "2");
                return;
            }

            if (!roles.Contains("TeamLeader") && !roles.Contains("Creator"))
            {
                await Clients.Caller.SendAsync("Unauthorized", "3");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        catch (System.Exception)
        {
            await Clients.Caller.SendAsync("Unauthorized", "4");
        }
    }

    public async Task LeaveGroup(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
    }
}