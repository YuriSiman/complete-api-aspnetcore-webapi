using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CompleteApi.Domain.Interfaces
{
    public interface IUser
    {
        string Nome { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
