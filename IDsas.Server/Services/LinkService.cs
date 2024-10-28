using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public class LinkService(DatabaseContext databaseContext) : ILinkService
{
    public bool OwnsLink(Guid linkToken, Guid userToken)
    {
        Document document = databaseContext.DocumentLinks.First(d => d.Id == linkToken).Document;

        return document.AuthorToken == userToken;
    }

    public void SetAccessAllowed(Guid linkToken, bool allowed)
    {
        if (allowed)
        {
            DocumentLink d = databaseContext.DocumentLinks.First(d => d.Id == linkToken);
            d.IsAssociatedUserConfirmed = true;
            databaseContext.DocumentLinks.Update(d);
            databaseContext.SaveChanges();
        }
        else
        {
            DocumentLink d = databaseContext.DocumentLinks.First(d => d.Id == linkToken);
            d.AssociatedUserToken = null;
            databaseContext.DocumentLinks.Update(d);
            databaseContext.SaveChanges();
        }
    }
}