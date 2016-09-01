## `C#` Validation


## Priority?

```csharp
class Data {
	[StringLength(2), MaxLength(2)]
	public string A { set; get; } = "AAA";

	[MaxLength(2), StringLength(2)]
	public string B { set; get; } = "BBB";
}

public class ValidationSpec {
	[Test]
	public void ShouldValidateData() {
		var context = new ValidationContext(new Data());
		Func<string, object, Type> check = (name, value) => {
			try {
				context.MemberName = name;
				Validator.ValidateProperty(value, context);
				return typeof(object);
			} catch (ValidationException ex) {
				return ex.ValidationAttribute.GetType();
			}
		};

		var data = new Data();
		check("A", data.A).Should().Be(typeof(StringLengthAttribute));
		check("B", data.B).Should().Be(typeof(StringLengthAttribute));
	}
}
```
