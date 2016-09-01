using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using System;
using FluentAssertions;
using System.Collections.Generic;

namespace CSharpValidation {

	class Data {
		[StringLength(2), MaxLength(2)]
		public string A { set; get; } = "AAA";

		[MaxLength(2), StringLength(2)]
		public string B { set; get; } = "BBB";

		[Required]
		public string C { set; get; }
	}

	public class ValicationPriority {

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

		[Test]
		public void ShouldGetValidationError() {
			var data = new Data();
			var context = new ValidationContext(data);
			var errors = new List<ValidationResult>();
			Validator.TryValidateObject(data, context, errors);
			errors.Count.Should().Be(1);
		}
	}
}
