namespace TodoLight.Dtos

open System
open TodoLight.DomainModel

[<CLIMutable>]
type TodoDto = {
    Id:Guid
    Name:string
    Description:string
    DueDate:string
    IsDone:bool
    DoneDate:string
}

module DtoConverter = 
    let convertToOptionDate (dte:string) =
         if String.IsNullOrWhiteSpace(dte) then
            None
         else
            Some (DateTime.Parse(dte))

    let convertOptionDateToString (dte:Option<DateTime>) =
        match dte with
          | Some d -> d.ToString()
          | None -> ""

    let dtoToTodo (dto:TodoDto) =
       {Todo.Id  = dto.Id; 
        Name = dto.Name; 
        Description = dto.Description; 
        DueDate =  (convertToOptionDate dto.DueDate); 
        DoneDate = (convertToOptionDate dto.DoneDate); 
        IsDone = dto.IsDone}
             
    let todoToDto (todo:Todo) =
       {TodoDto.Id  = todo.Id; 
        Name = todo.Name; 
        Description = todo.Description; 
        DueDate = (convertOptionDateToString todo.DueDate); 
        DoneDate = (convertOptionDateToString todo.DoneDate); 
        IsDone = todo.IsDone}

            