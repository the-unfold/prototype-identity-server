namespace Api

module HttpHandlers =

    open Microsoft.AspNetCore.Http
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open Giraffe
    open Api.Models

    let handleGetHello =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = {
                    Text = "Hello world, from Giraffe!!!"
                }
                return! json response next ctx
            }

    let handleGetUser =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let response = {
                    Text = "Returning user"
                }

                return! json response next ctx
            }