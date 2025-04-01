using Contract.Authentication;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Authentication.Commands.Logins.ByPassword;
using Sale.Application.Authentication.Commands.Logins.SSOVietmap;
using Sale.Application.Authentication.Queries;
using Sale.Contract.Users;

namespace Sale.ApiService.Controllers;

public class AuthenticationController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Authenticates a user using their username and password.
    /// </summary>
    /// <param name="request">The login request containing the user's credentials.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation, containing the action result.
    /// Returns a 200 OK with a TokenResponse if authentication is successful,
    /// or a 400 Bad Request with an ApiErrorResponse if authentication fails.
    /// </returns>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginByPassword([FromBody] LoginByPasswordRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new LoginByPasswordCommand(rq.Username, rq.Password))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Authenticates a user using Vietmap SSO (Single Sign-On).
    /// </summary>
    /// <param name="request">The login request containing the client ID and access token for Vietmap SSO.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation, containing the action result.
    /// Returns a 200 OK with a TokenResponse if authentication is successful,
    /// or a 400 Bad Request with an ApiErrorResponse if authentication fails.
    /// </returns>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Authentication.LoginSSOVietmap)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginSsoVietmap([FromBody] LoginVietmapSsoRequest request)
    {
        return await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new LoginVietmapSsoCommand(rq.ClientId, rq.AccessToken))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    /// Retrieves the profile information of the currently authenticated user.
    /// </summary>
    /// <returns>
    /// A Task that represents the asynchronous operation, containing the action result.
    /// Returns a 200 OK with a UserResponse containing the user's profile information if successful,
    /// or a 400 Bad Request with an ApiErrorResponse if the retrieval fails.
    /// </returns>
    [HttpGet(ApiRoutes.Authentication.Profile)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Profile() =>
        await Maybe<ProfileQuery>.From(new ProfileQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);
}