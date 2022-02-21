using DocumentStore.Core.Requests;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using QA.Core.Entities;
using QA.Core.Services;
using QA.Core.Validators;

namespace QA.Server
{
    public partial class EditAnswerComponentBase : ComponentBase
    {
        
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDocumentsService<QuestionAnswer> DocumentsService { get; set; }
        [Inject] private IQAQueryService queryService { get; set; }

        public IList<ValidationFailure> ValidationFailures = new List<ValidationFailure>();
        
        [ParameterAttribute]public string Id { get; set; } = "";
        
        protected QA.Server.Components.MarkDownEdit markDownEdit;  

        public QuestionAnswer Record { get; set; } = new QuestionAnswer();


        protected async Task OnSaveRecord()
        {
            if(DocumentsService == null) throw new NullReferenceException("DocumentsService");
            if(NavigationManager == null) throw new NullReferenceException("NavigationManager");

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
            Record.Name = "n/a";
            Record.Abstract = content;
            Record.HtmlContent = html;
            Record.Content = content;
            
            var questionValidator = new QuestionAnswerValidator();
            var validationResults = questionValidator.Validate(this.Record);
            if(validationResults.Errors.Count() > 0)
            {
                ValidationFailures = validationResults.Errors;
                return;
            }

            var command = new StoreDocumentCommand<QuestionAnswer>
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
                NavigationManager.NavigateTo($"view-question/{Record.QuestionId}", true);
            }
        }
                
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            if(firstRender)
            {
                await OnLoadRecordAsync();
            }            
        }

        string getCurrentUser(){
            return "system";
        }
        
        private async Task OnLoadRecordAsync()
        {            
            if(queryService == null) throw new NullReferenceException("queryService");

            var query = new GetDocumentQuery();
            query.Id = Id;
            query.UserId = getCurrentUser();
            var queryResponse = await queryService.GetAnswer(query);            

            if(queryResponse.Ok())
            {                
                Record = queryResponse.Data;             

                if(markDownEdit == null){
                    throw new ApplicationException("mark down edit is null");
                }

                // As an Angular dev, why did I have to do this???? :(
                StateHasChanged();

                await markDownEdit.SetContent(Record.Content);   
            }else{
                throw new ApplicationException("OnLoadQuestionAsync / GetAnswer / " + queryResponse.Message);
            }          
        }
    }
}
