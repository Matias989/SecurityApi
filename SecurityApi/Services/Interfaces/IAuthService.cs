using Microsoft.AspNetCore.Mvc;
using SecurityApi.Models;

namespace SecurityApi.Services.Interfaces
{
    public interface IAuthService
    {
        string CreateToken(string emailUser);
    }
}
