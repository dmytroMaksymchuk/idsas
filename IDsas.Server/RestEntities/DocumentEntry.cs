namespace IDsas.Server.RestEntities;

public class DocumentEntry
{
    public string DocumentToken { get; set; }
    public string Title { get; set; }
    public byte[] Content { get; set; }
}