module App

open Browser.Dom
open Fable.React
open Fable.React.Props

let init(): int = 0

let update (msg: bool) (model: int) =
    match msg with
    | true -> model + 1
    | false -> model - 1

let view model dispatch =
    div [] [
        div [] [
            label [] [ str "login" ]
            input []
        ]
        div [] [
            label [] [ str "password" ]
            input []
        ]
        div [] [
            button [ OnClick (fun _ -> dispatch true)] [ str "login" ]
        ]
    ]