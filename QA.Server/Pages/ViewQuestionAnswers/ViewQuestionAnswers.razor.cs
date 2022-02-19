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
        protected QA.Server.Components.MarkDownEdit markDownEdit;
        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        public List<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();
        public Question Question { get; set; }
        public string MarkdownHtml { get; set; } = "";

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDocumentsService<EntityVote> votesDocumentsService { get; set; }
        [Inject] private IDocumentsService<QuestionAnswer> answerDocumentsService { get; set; }
        [Inject] private IQAQueryService queryService { get; set; }
        
        [ParameterAttribute]public string Id { get; set; } = "";
               
        protected override async Task OnInitializedAsync()
        {
            var query = new GetDocumentQuery();
            query.Id = Id;
            query.UserId = getCurrentUser();
            var queryResponse = await queryService.GetQuestionAndAnswers(query);

            if(queryResponse.Ok())
            {
                Question = queryResponse.Question;
                Answers = queryResponse.Answers;
            }
        }

        string getCurrentUser(){
            return "system";
        }

        protected async Task OnSaveAnswerAsync()
        {
            QuestionAnswer questionAnswer = await makeAnswer();
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
                UserId = getCurrentUser(),
                Document = questionAnswer
            };
            return await answerDocumentsService.StoreDocument(command);
        }

        private async Task<StoreDocumentResponse<EntityVote>> saveVote(EntityVote vote)
        {
            var command = new StoreDocumentCommand<EntityVote>
            {
                UserId = getCurrentUser(),
                Document = vote
            };
            return await votesDocumentsService.StoreDocument(command);
        }        

        private async Task<QuestionAnswer> makeAnswer()
        {
            string content = await markDownEdit.GetContent();
            string html = await markDownEdit.GetHtmlContent();

            var questionAnswer = new QuestionAnswer();
            questionAnswer.Name = "n/a";
            questionAnswer.Content = content;
            questionAnswer.HtmlContent = html;
            questionAnswer.Tags = "n/a";
            questionAnswer.PermaLink = "n/a";
            questionAnswer.CreatedBy = getCurrentUser();
            questionAnswer.Id = Guid.NewGuid().ToString();
            questionAnswer.Abstract = questionAnswer.HtmlContent;
            questionAnswer.QuestionId = Id;
            return questionAnswer;
        }

        protected async Task OnAnswerUpVote(QuestionAnswer answer) {
            bool voteExists = queryService.UserVotedForEntity(getCurrentUser(), "answer", answer.Id);
            if(voteExists)
                return;

            answer.Votes += 1;
            StoreDocumentResponse<QuestionAnswer> storeResponse = await saveAnswer(answer);

            if (!storeResponse.Ok())
            {
                throw new ApplicationException("Error while up vote of answer");
            }

            // Log that the current user has voted for this answer.
            var vote = new EntityVote();
            vote.CreatedBy = getCurrentUser();
            vote.Id = Guid.NewGuid().ToString();
            vote.ParentEntityType = "answer";
            vote.ParentEntityId = answer.Id;
            StoreDocumentResponse<EntityVote> saveVoteResponse = await saveVote(vote);
            if (!saveVoteResponse.Ok())
            {
                throw new ApplicationException("Error while save of vote");
            }

        }
    }
}
