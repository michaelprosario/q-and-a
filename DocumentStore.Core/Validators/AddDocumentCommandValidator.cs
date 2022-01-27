using DocumentStore.Core.Interfaces;
using DocumentStore.Core.Requests;
using FluentValidation;

namespace DocumentStore.Core.Validators
{
    public class AddDocumentCommandValidator<T> : AbstractValidator<AddDocumentCommand<T>> where T : IEntity
    {
        public AddDocumentCommandValidator()
        {
            RuleFor(x => x.Document).NotNull();
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}