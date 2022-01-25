//This project uses Duende Software and parts of their templates (no affiliation with Wrapt or Craftsman).
//Please see the Duende Licensing information at https://duendesoftware.com/
// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace Reza.AuthServer.Extensions;

using Microsoft.AspNetCore.Mvc;
using Duende.IdentityServer.Models;
using Reza.AuthServer.ViewModels;

public static class Extensions
{
    /// <summary>
    /// Checks if the redirect URI is for a native client.
    /// </summary>
    /// <returns></returns>
    public static bool IsNativeClient(this AuthorizationRequest context)
    {
        return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
           && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
    }

    public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
    {
        controller.HttpContext.Response.StatusCode = 200;
        controller.HttpContext.Response.Headers["Location"] = "";
        
        return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
    }
}