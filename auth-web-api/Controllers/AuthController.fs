namespace AuthWebApi.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

open AuthWebApi

[<ApiController>]
[<Route("[controller]")>]
type AuthController () =
    inherit ControllerBase()

    [<HttpGet>]
    member __.GetUser() : int =
        42
