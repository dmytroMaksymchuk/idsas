using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public interface ILinkService
{
    bool OwnsLink(Guid linkToken, Guid userToken);

    void SetAccessAllowed(Guid linkToken, bool allowed);
}