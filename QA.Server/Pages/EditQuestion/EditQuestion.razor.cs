using DocumentStore.Core.Requests;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using QA.Core.Services;
using QA.Core.Validators;

namespace QA.Server
{
    public partial class EditQuestionComponentBase : ComponentBase
    {
        private string markdownHtml = "";

        [Inject] NavigationManager? NavigationManager { get; set; }
        [Inject] private IDocumentsService<Question>? DocumentsService { get; set; }
        [Inject] private IQAQueryService? queryService { get; set; }

        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        
        [ParameterAttribute]public string Id { get; set; } = "";
        
        protected QA.Server.Components.MarkDownEdit? markDownEdit;  

        public Question Record { get; set; } = new Question();


        private async Task OnSave()
        {
            if(Record == null){
                throw new ApplicationException("Record is null");
            }

            if(markDownEdit == null){
                throw new ApplicationException("markDownEdit is null");
            }

            ValidationFailures = new List<ValidationFailure>();

            Record.CreatedBy = "system";
            Record.PermaLink = Record.Name;
            string content = await markDownEdit.GetContent();
            string html = await markDownEdit.GetHtmlContent();
            Record.Abstract = content;
            Record.HtmlContent = html;
            Record.Content = content;
            
            var questionValidator = new QuestionValidator();
            var validationResults = questionValidator.Validate(this.Record);
            if(validationResults.Errors.Count() > 0)
            {
                ValidationFailures = validationResults.Errors;
                return;
            }

            var command = new StoreDocumentCommand<Question>
            {
                Document = Record,
                UserId = "system"
            };
            var response = await DocumentsService.StoreDocument(command);
            if (!response.Ok())
            {
                ValidationFailures = response.ValidationErrors;
            }
            else
            {
                NavigationManager.NavigateTo($"view-question/{Record.Id}", true);
            }
        }
                
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            if(firstRender)
            {
                await OnLoadQuestionAsync();
            }            
        }

        string getCurrentUser(){
            return "system";
        }
        
        private async Task OnLoadQuestionAsync()
        {            
            var query = new GetDocumentQuery();
            query.Id = Id;
            query.UserId = getCurrentUser();
            var queryResponse = await queryService.GetQuestionAndAnswers(query);            

            if(queryResponse.Ok())
            {                
                Record = queryResponse.Question;             

                if(markDownEdit == null){
                    throw new ApplicationException("mark down edit is null");
                }

                // As an Angular dev, why did I have to do this???? :(
                StateHasChanged();

                await markDownEdit.SetContent(Record.Content);   
            }else{
                throw new ApplicationException("OnLoadQuestionAsync / GetQuestionAndAnswers / " + queryResponse.Message);
            }          
        }

        protected async Task OnSaveQuestion()
        {
            await OnSave();
        }
    }
}
