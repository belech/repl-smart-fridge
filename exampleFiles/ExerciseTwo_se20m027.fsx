// Exercise 1 filtering

let input = [1;2;3;4] // [2;3;4]

let rec filter (f : 'a -> bool) (xs : list<'a>) =
    match xs with
    | [] -> []
    | x::xs -> 
        if f x then 
            x :: filter f xs 
        else 
            filter f xs

let res = filter (fun a -> a > 2) input

// Exercise 2

let rec aggregate (f : 'a -> 's -> 's) (s : 's) (xs : list<'a>) : 's =
    match xs with
    | [] -> s
    | x::xs -> f x (aggregate f s xs)

let tryMax (xs: list<int>) : Option<int> =
  match xs with
  | [] -> None
  | x :: xs -> Some(aggregate(max) x xs)

let resultTryMax = tryMax input

//Exercise 3

open System
open System.IO

let searchFiles = ["1.txt"; "2.txt"; "3.txt"]
let searchWords = ["Hello";"World"]

let rec printStringList (xs: list<string>) (name: string) =
  match xs with
  | [] -> ()
  | x :: xs -> 
    printfn "%s: %s" name x
    printStringList xs name


let rec filterStringList (f : 'a -> bool) (xs:list<'a>) =
  match xs with
      | [] -> []
      | x::xs -> 
          if f x then 
              x :: filterStringList f xs 
          else 
              filterStringList f xs

let rec containMultiple (line: string) (searchWords: list<string>): bool=
    match searchWords with
      | [] -> false
      | x::xs -> 
        if line.Contains(x) then true
        else containMultiple line xs

let rec simpleGrep (searchWords: list<string>) (fileList: list<string>) = 
  match fileList with
  | [] -> ()
  | file::fileList ->
    let lines = File.ReadLines(file)
    lines 
    |> Seq.toList
    |> filterStringList (fun a -> containMultiple a searchWords )
    |> (fun x -> printStringList x file)
    simpleGrep searchWords fileList


simpleGrep searchWords searchFiles