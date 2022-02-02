using DocumentStore.Core.Requests;
using DocumentStore.Core.Responses;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using QA.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Server
{
    public partial class SearchQuestionsComponentBase : ComponentBase
    {
        public List<Question> Questions { get; set; } = new List<Question>();
        public string SearchTerms { get; set; } = "";

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IQAQueryService queryService { get; set; }
                       
        protected override async Task OnInitializedAsync()
        {
            var query = new GetQuestionsQuery();
            query.Keyword = SearchTerms;
            query.UserId = getCurrentUser();

            var queryResponse = queryService.GetQuestions(query);

            if(queryResponse.Ok())
            {
                Questions = queryResponse.Questions;
            }
        }

        string getCurrentUser(){
            return "system";
        }    

        protected void OnAskQuestion(){
            NavigationManager.NavigateTo($"add-question", true);
        }
    }
}
