namespace RequestApp.Hubs
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
