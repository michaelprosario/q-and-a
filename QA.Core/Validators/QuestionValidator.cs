using FluentValidation;
using QA.Core.Entities;

namespace QA.Core.Validators {
    public class QuestionValidator : AbstractValidator<Question> {
        public QuestionValidator () {
            RuleFor (x => x.Name).NotEmpty ();
            RuleFor (x => x.Content).NotEmpty ();
            RuleFor (x => x.Tags).NotEmpty ();
            RuleFor (x => x.PermaLink).NotEmpty ();
            RuleFor (x => x.CreatedBy).NotEmpty ();
            RuleFor (x => x.Abstract).NotEmpty ();
            RuleFor (x => x.Id).NotEmpty ();
        }

    }
}