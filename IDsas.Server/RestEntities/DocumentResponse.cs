namespace IDsas.Server.RestEntities;

public class DocumentResponse
{
    public string DocumentToken { get; set; }
    public string Title { get; set; }
    public byte[] Content { get; set; }
    public ShareState shareState { get; set; }
}