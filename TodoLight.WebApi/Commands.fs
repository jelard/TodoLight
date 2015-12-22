namespace TodoLight.Commands

open System
open TodoLight.Dtos
open TodoLight.DomainModel
open TodoLight.DataAccess
open TodoLight.Rop


module Commands =
    let repository = TodoRepository()  
    let addTodoToDatabase =  bindR repository.Add
    let convertToTodoModel = mapR DtoConverter.dtoToTodo
    let validate (todoDto:TodoDto) =
        if String.IsNullOrEmpty(todoDto.Name) then
            fail "Name must not be blank"
        else
            succeed todoDto 

    let createTodo (dto:TodoDto) =
          dto
            |> validate
            |> convertToTodoModel
            |> addTodoToDatabase
            
            
        
    

