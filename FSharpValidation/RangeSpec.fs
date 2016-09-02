module RangeSpec

open System.ComponentModel.DataAnnotations
open NUnit.Framework
open FluentAssertions

type Data() =
      [<Required>]
      [<Range(10, 20)>]
      member val Value = 0 with set,get

      [<MinLength(10)>]
      member val Text = "" with set, get

[<Test>]
let shouldValidateRange() = 
      let data = Data()
      let context = ValidationContext(data)
      Validator.ValidateObject(data, context)

      context.MemberName <- "Value"
      //let err = Assert.Throws<ValidationException>(fun x -> Validator.ValidateProperty(data.Value, context))
      //err.ValidationAttribute.Should().Be(typeof<RangeAttribute>, "") |> ignore

      context.MemberName <- "Text"
      let err = Assert.Throws<ValidationException>(fun x -> Validator.ValidateProperty(data.Text, context))
      err.ValidationAttribute.GetType().Should().Be(typeof<MinLengthAttribute>, "") |> ignore

