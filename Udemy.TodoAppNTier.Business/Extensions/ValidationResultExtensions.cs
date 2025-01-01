using FluentValidation.Results;
using System.Collections.Generic;
using Udemy.ToDoAppNTier.Common.ResponseObject;

namespace Udemy.TodoAppNTier.Business.Extensions
{
	public static class ValidationResultExtensions 
	{
		public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
		{
			List<CustomValidationError> errors = new();
			foreach (var error in validationResult.Errors)
			{
				errors.Add(new() { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage });
			}
			return errors;
		}

	}
}
