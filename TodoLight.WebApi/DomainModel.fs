module TodoLight.DomainModel

open System

let nullableToOption (n : System.Nullable<_>) = 
   if n.HasValue 
   then Some n.Value 
   else None

let optionToNullable (o : Option<_> ) =
   match o with 
    | Some x -> System.Nullable<_>(x) 
    | None -> System.Nullable<_>()

type Todo = {
    Id:Guid
    Name:string
    Description:string
    DueDate:Option<DateTime>
    IsDone:bool
    DoneDate:Option<DateTime>
}

