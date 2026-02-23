namespace RaceIntel.Api.Admin;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[ServiceFilter(typeof(AdminKeyAuthFilter))]
public abstract class AdminControllerBase : ControllerBase
{
    //base controller for admin endpoints, protected by AdminKeyAuthFilter
}