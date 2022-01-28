using FluentValidation;
using QA.Core.Entities;

namespace QA.Core.Validators {
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswer> {
        public QuestionAnswerValidator () {
            RuleFor (x => x.Id).NotEmpty ();            
            RuleFor (x => x.QuestionId).NotEmpty (); 
            RuleFor (x => x.Content).NotEmpty ();                        
            RuleFor (x => x.CreatedBy).NotEmpty ();
            RuleFor (x => x.Abstract).NotEmpty ();
        }

    }
}