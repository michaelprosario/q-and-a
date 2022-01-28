using DocumentStore.Core.Requests;
using FluentValidation;

namespace DocumentStore.Core.Validators
{
    public class GetDocumentsByCollectionQueryValidator : AbstractValidator<GetDocumentsByCollection>
    {
        public GetDocumentsByCollectionQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Collection).NotNull().NotEmpty();
        }
    }
}