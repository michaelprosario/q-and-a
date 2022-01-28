using DocumentStore.Core.Interfaces;
using DocumentStore.Core.Requests;
using FluentValidation;

namespace DocumentStore.Core.Validators
{
    public class StoreDocumentCommandValidator<T> : AbstractValidator<StoreDocumentCommand<T>> where T : IEntity
    {
        public StoreDocumentCommandValidator()
        {
            RuleFor(x => x.Document).NotNull();
            RuleFor(x => x.Document).NotNull().NotEmpty();
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}