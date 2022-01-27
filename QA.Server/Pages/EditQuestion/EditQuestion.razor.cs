using DocumentStore.Core.Requests;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QA.Server
{
    public partial class EditQuestionComponentBase : ComponentBase
    {
        public Question Record;

        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();

        [Inject] private IDocumentsService<Question> DocumentsService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter] public string Id { get; set; }

        public string FormMessage { get; set; } = "";
        
        private void OnNewRecord()
        {
            // change goes here ...
            Record = new Question
            {
                Id = Guid.NewGuid().ToString()
            };

            Id = Record.Id;

            FormMessage = "Create Question";
        }

        private async Task OnSave()
        {
            ValidationFailures = new List<ValidationFailure>();
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

        private void OnSaveAndClose()
        {
            NavigationManager.NavigateTo($"view-question/{Record.Id}");
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadRecord();
        }

        private async Task LoadRecord()
        {
            if (Id == "new")
            {
                OnNewRecord();
            }
            else
            {
                var query = new GetDocumentQuery
                {
                    Id = Id,
                    UserId = "system"
                };

                var response = await DocumentsService.GetDocument(query);

                if (response == null || !response.Ok()) throw new ApplicationException("response is null");

                Record = response.Document;
                FormMessage = Record.UpdatedAt == null ? $"Created at {Record.CreatedAt} " : $"Last updated {Record.UpdatedAt} ";                
            }
        }        
    }
}
