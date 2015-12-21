namespace TodoLight.Commands

open TodoLight.Dtos
open TodoLight.Dtos
open TodoLight.DomainModel
open TodoLight.DataAccess

module Commands =
    let repository = TodoRepository()  
    let addTodoToDatabase = repository.Add
    let convertToTodoModel = DtoConverter.dtoToTodo

    let createTodo (dto:TodoDto) =
          dto
            |> convertToTodoModel
            |> addTodoToDatabase
            
            
        
    

