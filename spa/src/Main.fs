module Main

open Elmish
open Elmish.React
open App

Program.mkSimple init update view 
|> Program.withReactBatched "elmish-app"
|> Program.withConsoleTrace
|> Program.run