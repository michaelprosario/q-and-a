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
    public partial class AddQuestionComponentBase : ComponentBase
    {
        private string markdownHtml = "";
        public Question Record;
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        [Inject] private IDocumentsService<Question> DocumentsService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        public string FormMessage { get; set; } = "";
        
        private void OnNewRecord()
        {
            // change goes here ...
            Record = new Question
            {
                Id = Guid.NewGuid().ToString()
            };
            FormMessage = "Create Question";
        }

        private async Task OnSave()
        {
            ValidationFailures = new List<ValidationFailure>();

            Record.CreatedBy = "system";
            Record.PermaLink = Record.Name;
            Record.Abstract = Record.Content;

            var questionValidator = new QuestionValidator();
            var validationResults = questionValidator.Validate(this.Record);
            if(validationResults.Errors.Count() > 0)
            {
                ValidationFailures = validationResults.Errors;
                return;
            }


            var command = new StoreDocumentCommand<Question>
            {
                Document = Record,
                UserId = "system"
            };
            var response = await this.DocumentsService.StoreDocument(command);
            if (!response.Ok())
            {
                ValidationFailures = response.ValidationErrors;
            }
            else
            {
                // todo - redirect to view question ...
                NavigationManager.NavigateTo($"view-question/{Record.Id}");
            }
        }

        protected Task OnMarkdownValueHTMLChanged(string value)
        {
            markdownHtml = value;
            return Task.CompletedTask;
        }
        
        private void OnSaveAndClose()
        {
            NavigationManager.NavigateTo($"view-question/{Record.Id}");
        }

        protected override async Task OnInitializedAsync()
        {
            OnNewRecord();
        }

        protected async Task OnSaveQuestion()
        {
            await OnSave();
        }

    }
}
