using Acs.TodoList.Domain.Dtos.ItemEntity.common;
using FluentValidation;

namespace Acs.TodoList.Domain.Validators
{
    public class PaginationRequestModelValidator : AbstractValidator<PaginationRequestModel>
    {
        public PaginationRequestModelValidator()
        {
            RuleFor(x => x.Limit).GreaterThan(0).WithMessage("Limit must be greater than 0.");
            RuleFor(x => x.Offset).Must(BeAValidCustomValidate).WithMessage("Please specify a valid");
        }

        private bool BeAValidCustomValidate(int offset)
        {
            // custom validating logic goes here
            return true;
        }
    }
}
