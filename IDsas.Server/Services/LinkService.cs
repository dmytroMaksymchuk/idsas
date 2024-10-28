using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public class LinkService(DatabaseContext databaseContext) : ILinkService
{
    public bool OwnsLink(Guid linkToken, Guid userToken)
    {
        Document document = databaseContext.DocumentLinks.First(d => d.Document.Id == linkToken).Document;

        return document.AuthorToken == userToken;
    }

    public void SetAccessAllowed(Guid linkToken, bool allowed)
    {
        DocumentLink d = databaseContext.DocumentLinks.First(d => d.Document.Id == linkToken);

        if (allowed)
        {
            d.IsAssociatedUserConfirmed = true;
        }
        else
        {
            d.AssociatedUserToken = null;
        }
        databaseContext.DocumentLinks.Update(d);
        databaseContext.SaveChanges();
    }
}