/*
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core;
using QA.Core.Entities;
using QA.Core.Requests;
using QA.Core.Services;

namespace QA.Blazor.Pages.EditContentSource
{
    public partial class EditContentSource : ComponentBase
    {
        public ContentSource Record;

        public GetContentSourceAndPostsResponse Response;
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();

        [Inject] private IContentServices ContentServices { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter] public string Id { get; set; }

        public string FormMessage { get; set; } = "";
        
        private void OnNewRecord()
        {
            Record = new ContentSource
            {
                Id = Guid.NewGuid().ToString()
            };

            Id = Record.Id;

            FormMessage = "Create New Content Source";
        }

        private async Task OnSave()
        {
            ValidationFailures = new List<ValidationFailure>();
            var command = new StoreContentSourceCommand
            {
                ContentSource = Record,
                UserId = "system"
            };
            var response = await ContentServices.StoreContentSource(command);
            if (!response.Ok())
            {
                ValidationFailures = response.ValidationErrors;
            }
            else
            {
                await LoadRecord();    
            }
            
        }

        private void OnSaveAndClose()
        {
            NavigationManager.NavigateTo($"view-content-source/{Record.Id}");
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
                var query = new GetContentSourceAndPostsQuery(Id)
                {
                    UserId = "system"
                };

                Response = await ContentServices.GetContentSourceAndPosts(query);

                if (Response == null || !Response.Ok()) throw new ApplicationException("response is null");

                Record = Response.ContentSource;
                FormMessage = Record.UpdatedAt == null ? $"Created at {Record.CreatedAt} " : $"Last updated {Record.UpdatedAt} ";
                
            }
        }
    }
}
*/