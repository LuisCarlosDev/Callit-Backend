using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Callit.Communication.Enums;

namespace Callit.Communication.Responses;

public class ResponseSignInJson
{
    public UserLogged User { get; set; }
    public string Token { get; set; } = string.Empty;
}

public class UserLogged
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User;
}
