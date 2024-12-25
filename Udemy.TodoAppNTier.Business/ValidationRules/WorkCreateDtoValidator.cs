using FluentValidation;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTier.Business.ValidationRules
{
	public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
	{
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
        }
    }
}
