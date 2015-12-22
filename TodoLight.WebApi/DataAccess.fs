module TodoLight.DataAccess

open System
open Microsoft.FSharp.Data.TypeProviders
open TodoLight.DomainModel
open TodoLight.Rop

type internal DbConnection = SqlEntityConnection<ConnectionString = @"Data Source=localhost;Initial Catalog=TodoLight;Integrated Security=True">

type TodoRepository() =
    member this.GetAll() =
        use context = DbConnection.GetDataContext()

        query {
            for t in context.Todo do
            select ({Id = t.Id; Name = t.Name; Description = t.Description; DueDate =  (nullableToOption t.DueDate); DoneDate = (nullableToOption t.DoneDate); IsDone = t.IsDone})
        }|> Seq.toList

     member this.Get(id) =
        use context = DbConnection.GetDataContext()

        let todo = 
            query {
                for t in context.Todo do
                where (t.Id = id)
                select (t)
               }|> Seq.head

        {Todo.Id = todo.Id; Name = todo.Name; Description = todo.Description; DueDate =  (nullableToOption todo.DueDate); DoneDate = (nullableToOption todo.DoneDate); IsDone = todo.IsDone}
        

    member this.Add(todo:Todo) =

        try
            use context = DbConnection.GetDataContext()
            let newTodo = new  DbConnection.ServiceTypes.Todo(Id = todo.Id,
                                                                Name = todo.Name,
                                                                Description = todo.Description,
                                                                DueDate = (optionToNullable todo.DueDate),
                                                                DoneDate = (optionToNullable todo.DoneDate),
                                                                IsDone = todo.IsDone)
            context.Todo.AddObject(newTodo)
            context.DataContext.SaveChanges() |> ignore
            
            succeed()
        with
            | ex -> fail ex.Message
