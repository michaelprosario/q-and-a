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
    public partial class ViewAnswerComponentBase : ComponentBase
    {
        [Parameter]
        public QuestionAnswer Answer { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
        }
    }
}
