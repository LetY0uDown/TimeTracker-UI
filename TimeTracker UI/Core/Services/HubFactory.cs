using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace TimeTracker.UI.Core.Services;

internal sealed class HubFactory (IConfiguration config) : IHubFactory
{
    private readonly IConfiguration _config = config;

    public HubConnection CreateHub ()
    {
        var connection = new HubConnectionBuilder()
                            .WithUrl(_config["HostURL"] + "/MainHub")
                            .WithAutomaticReconnect()
                            .Build();

        return connection;
    }
}