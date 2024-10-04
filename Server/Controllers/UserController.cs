using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]

public class UserController : ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;

    public UserController(IManagementApiClient managementApiClient)
    {
        _managementApiClient = managementApiClient;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
        return users.Select(x => new UserDto
        {
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            IsBlocked = x.Blocked ?? false,
        });
    }
}