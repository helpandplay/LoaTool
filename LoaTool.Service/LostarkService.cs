using LoaTool.WebService;

namespace LoaTool.Service;
public class LostarkService
{
    private readonly LostArkWebClient lostArkWebClient;

    public LostarkService()
    {
        this.lostArkWebClient = new LostArkWebClient();
    }

    public void getData()
    {
        lostArkWebClient.getData();
    }
}
