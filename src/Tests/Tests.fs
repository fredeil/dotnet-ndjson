module Test

open System
open Xunit
open Parser


[<Fact>]
let ``Parses empty json returns Null`` () =
    let expected = Null
    let actual = JSON.parse("")
    Assert.Equal(expected, actual)

[<Fact>]
let ``Parses primitive type bool false`` () =
    let expected = Boolean false
    let actual = JSON.parse("false")
    Assert.Equal(expected, actual)

[<Fact>]
let ``Parses primitive type bool true`` () =
    let expected = Boolean true
    let actual = JSON.parse("true")
    Assert.Equal(expected, actual)

[<Fact>]
let ``Parses primitive type string text`` () =
    let expected = String "a"
    let actual = JSON.parse("\"a\"")
    Assert.Equal(expected, actual)

[<Fact>]
let ``Parses primitive type number with whitespace`` () =
    let expected = Number 1.0
    let actual = JSON.parse(" 1")

    Assert.Equal(expected, actual)

[<Fact>]
let ``Parses array with nested array`` () =
    let actual = JSON.parse """ [1, [-1,2.3], "abc"]"""
    let expected = Array [Number 1.0; Array [Number -1.0; Number 2.3]; String "abc"]

    Assert.Equal(expected, actual)