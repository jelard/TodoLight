namespace TodoLight.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Net
open System.Net.Http
open System.Web.Http
open System.Web.Http.Results
open TodoLight.Dtos
open TodoLight.DomainModel
open TodoLight.DataAccess
open Chessie

[<RoutePrefix("api/todo")>]
type TodoController(repository:TodoRepository) as this =
    inherit ApiController()
   
    let addTodoToDatabase = repository.Add
    let convertToTodoModel = DtoConverter.dtoToTodo
    let returnResult content = 
        NegotiatedContentResult(HttpStatusCode.OK, content, this) :> IHttpActionResult

    new() = new TodoController(TodoRepository())
  
    member this.Get() =
        "Hello World"    

    member this.Post( [<FromBody>]dto:TodoDto) : IHttpActionResult =
        dto
            |> convertToTodoModel
            |> addTodoToDatabase
            |> returnResult
   
              