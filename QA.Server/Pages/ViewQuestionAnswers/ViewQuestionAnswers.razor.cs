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
    public partial class ViewQuestionAnswersComponentBase : ComponentBase
    {
        public Question Question { get; set; }
        public List<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        [Inject] private IQAQueryService queryService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        
        public string MarkdownHtml { get; set; } = "";
        public string AnswerContent { get; set; } = "";
        [ParameterAttribute]public string Id { get; set; } = "";
        
        protected override async Task OnInitializedAsync()
        {
            GetDocumentQuery query = new GetDocumentQuery();
            query.Id = Id;
            query.UserId = "system";
            var queryResponse = await queryService.GetQuestionAndAnswers(query);

            if(queryResponse.Ok())
            {
                Question = queryResponse.Question;
                Answers = queryResponse.Answers;
            }
        }

        protected Task OnMarkdownValueHTMLChanged(string value)
        {
            MarkdownHtml = value;
            return Task.CompletedTask;
        }   

        protected void OnSaveAnswer() {
            // populate answer ...

            // setup storage service ...

            // store answer 

            // check if response is ok


            // Ok? Refresh page

            // Not ok? Display answer validation errors

        }     
    }
}
