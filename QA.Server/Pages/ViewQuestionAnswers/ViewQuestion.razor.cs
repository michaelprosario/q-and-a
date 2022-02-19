using DocumentStore.Core.Requests;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using QA.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Server
{
    public partial class ViewQuestionComponentBase : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
        }

        protected void OnAskQuestion(){
            NavigationManager.NavigateTo($"add-question", true);
        }        
    }
}
