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

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDocumentsService<Question> DocumentsService { get; set; }
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        public Question Record{ get; set; }
        public string FormMessage { get; set; } = "";
        protected QA.Server.Components.MarkDownEdit markDownEdit;  
        
        private void OnNewRecord()
        {            
            Record = new Question
            {
                Id = Guid.NewGuid().ToString()
            };            
        }

        private async Task OnSave()
        {
            ValidationFailures = new List<ValidationFailure>();

            Record.CreatedBy = "system";
            Record.PermaLink = Record.Name;
            string content = await markDownEdit.GetContent();
            string html = await markDownEdit.GetHtmlContent();
            Record.Abstract = content;
            Record.HtmlContent = html;
            Record.Content = content;
            Console.WriteLine(Record.HtmlContent);

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
                NavigationManager.NavigateTo($"view-question/{Record.Id}", true);
            }
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
