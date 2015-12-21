namespace TodoLight.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Net
open System.Net.Http
open System.Web.Http
open System.Web.Http.Results
open TodoLight.DataAccess
open TodoLight.Commands.Commands
open TodoLight.Dtos
open Chessie

[<RoutePrefix("api/todo")>]
type TodoController(repository:TodoRepository) as this =
    inherit ApiController()
 
    let returnResult content = 
        NegotiatedContentResult(HttpStatusCode.OK, content, this) :> IHttpActionResult
    
    new() = new TodoController(TodoRepository())  

    member this.Get(id:Guid) =
         id
            |> repository.Get
            |> DtoConverter.todoToDto

    member this.Post( [<FromBody>]dto:TodoDto) : IHttpActionResult =
        dto
            |> createTodo
            |> returnResult
   
         