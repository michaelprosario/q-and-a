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
    public partial class ViewQuestionAnswersComponentBase : ComponentBase
    {
        public Question Question { get; set; }
        public List<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        [Inject] private IQAQueryService queryService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDocumentsService<QuestionAnswer> answerDocumentsService { get; set; }
        
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

        protected async Task OnSaveAnswerAsync()
        {
            QuestionAnswer questionAnswer = makeAnswer();
            StoreDocumentResponse<QuestionAnswer> storeResponse = await saveAnswer(questionAnswer);

            if (!storeResponse.Ok())
            {
                ValidationFailures = storeResponse.ValidationErrors;
            }
            else
            {
                NavigationManager.NavigateTo($"view-question/{Id}", true);
            }
        }

        private async Task<StoreDocumentResponse<QuestionAnswer>> saveAnswer(QuestionAnswer questionAnswer)
        {
            var command = new StoreDocumentCommand<QuestionAnswer>
            {
                UserId = "system",
                Document = questionAnswer
            };
            var storeResponse = await answerDocumentsService.StoreDocument(command);
            return storeResponse;
        }

        private QuestionAnswer makeAnswer()
        {
            var questionAnswer = new QuestionAnswer();
            questionAnswer.Name = "n/a";
            questionAnswer.Content = AnswerContent;
            questionAnswer.HtmlContent = MarkdownHtml;
            questionAnswer.Tags = "n/a";
            questionAnswer.PermaLink = "n/a";
            questionAnswer.CreatedBy = "system";
            questionAnswer.Id = Guid.NewGuid().ToString();
            questionAnswer.Abstract = questionAnswer.HtmlContent;
            questionAnswer.QuestionId = Id;
            return questionAnswer;
        }

        protected async Task OnAnswerUpVote(QuestionAnswer answer) {
            answer.Votes += 1;
            StoreDocumentResponse<QuestionAnswer> storeResponse = await saveAnswer(answer);

            if (!storeResponse.Ok())
            {
                throw new ApplicationException("Error while up vote of answer");
            }
        }
    }
}
