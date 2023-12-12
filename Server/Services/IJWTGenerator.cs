using codesome.Shared.Models;

namespace codesome.Server.Services
{
    public interface IJWTGenerator
    {
        string GetToken(CustomUser user);
    }
}
