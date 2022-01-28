using FluentValidation;
using QA.Core.Entities;

namespace QA.Core.Validators {
    public class CommentValidator : AbstractValidator<Comment> {
        public CommentValidator () {
            
            RuleFor (x => x.Content).NotEmpty ();                    
            RuleFor (x => x.CreatedBy).NotEmpty ();            
            RuleFor (x => x.Id).NotEmpty ();
            RuleFor (x => x.ParentEntityType).NotEmpty ();
            RuleFor (x => x.ParentEntityId).NotEmpty ();
        }
    }
}