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
    public partial class QuestionItemComponentBase : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
        }
    }
}
