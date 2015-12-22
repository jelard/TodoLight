#load "Rop.fs"

open System
open TodoLight.Rop

type PersonDto = {
        Id:int
        Name:string
    }


let personDto = {Id = 1;Name = "Jelard"}

let saveToDatabase (dto:PersonDto) =
    try
        printfn "Person Saved. %A" dto |> ignore
        succeed()
    with 
       | ex -> fail "Failed to update"

let saveToDatabaseR = bindR saveToDatabase

let validate (dto:PersonDto) = 
    if String.IsNullOrEmpty(dto.Name) then
        fail "Name must not be blank"
    else
       succeed dto
let validateR = bindR validate


personDto
    |> validate
    |> saveToDatabaseR



