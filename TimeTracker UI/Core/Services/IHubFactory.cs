using Microsoft.AspNetCore.SignalR.Client;

namespace TimeTracker.UI.Core.Services;

public interface IHubFactory
{
    HubConnection CreateHub ();
}