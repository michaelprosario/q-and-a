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

        [Parameter]
        public EventCallback<QuestionAnswer> OnUpVote { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected async Task OnUpVoteAnswerAsync()
        {
            await OnUpVote.InvokeAsync(Answer);
        }

        protected async Task OnEdit(){
            await Task.Run(() => NavigationManager.NavigateTo($"edit-answer/{Answer.Id}", true));
        }
    }
}
