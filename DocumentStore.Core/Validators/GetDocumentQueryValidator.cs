using DocumentStore.Core.Requests;
using FluentValidation;

namespace DocumentStore.Core.Validators
{
    public class GetDocumentQueryValidator : AbstractValidator<GetDocumentQuery>
    {
        public GetDocumentQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}