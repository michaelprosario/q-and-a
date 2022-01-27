using DocumentStore.Core.Requests;
using FluentValidation;

namespace DocumentStore.Core.Validators
{
    public class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
    {
        public DeleteDocumentCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}