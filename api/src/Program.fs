module Api.App

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Api.HttpHandlers

let authorize : HttpHandler =
    requiresAuthentication (challenge JwtBearerDefaults.AuthenticationScheme)

// ---------------------------------
// Web app
// ---------------------------------

let webApp =
    choose [
        subRoute "/api"
            (choose [
                GET >=> choose [
                    route "/hello" >=> handleGetHello
                 
                ]
                authorize >=>
                    GET >=> choose [
                        route "/user" >=> handleGetUser
                    ]
            ])
        setStatusCode 404 >=> text "Not Found" ]

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureServices (services: IServiceCollection) =
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (fun config ->
            config.Authority <- "https://localhost:6001"
            config.Audience <- "api")
        ) |> ignore
    services.AddGiraffe() |> ignore

let configureApp (app: IApplicationBuilder) =
    // app.UseAuthentication () |> ignore
    // app.UseAuthorization() |> ignore
    app.UseAuthentication()
        .UseGiraffe webApp

let configureCors (builder : CorsPolicyBuilder) =
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyMethod()
           .AllowAnyHeader()
           |> ignore

let configureLogging (builder : ILoggingBuilder) =
    builder.AddFilter(fun l -> l.Equals LogLevel.Error)
           .AddConsole()
           .AddDebug() |> ignore

[<EntryPoint>]
let main _ =

    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder -> 
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)|> ignore)
        .Build()
        .Run()
    0