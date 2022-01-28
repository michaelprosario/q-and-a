using DocumentStore.Core.Requests;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using QA.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QA.Server
{
    public partial class ViewQuestionAnswersComponentBase : ComponentBase
    {
        public Question Question;
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        [Inject] private IDocumentsService<Question> DocumentsService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        public string FormMessage { get; set; } = "";
        [ParameterAttribute]public string Id { get; set; } = "";
        
        protected override async Task OnInitializedAsync()
        {
        }
    }
}
