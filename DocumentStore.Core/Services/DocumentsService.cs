using System;
using System.Threading.Tasks;
using DocumentStore.Core.Enums;
using DocumentStore.Core.Helpers;
using DocumentStore.Core.Interfaces;
using DocumentStore.Core.Requests;
using DocumentStore.Core.Responses;
using DocumentStore.Core.Validators;

namespace DocumentStore.Core.Services
{
    namespace DocumentStore.Core.Services
    {
        public interface IDocumentsService<T> where T : IEntity
        {
            Task<NewRecordResponse> AddDocument(AddDocumentCommand<T> command);
            Task<AppResponse> DeleteDocument(DeleteDocumentCommand command);
            Task<GetDocumentResponse<T>> GetDocument(GetDocumentQuery query);
            Task<AppResponse> UpdateDocument(UpdateDocumentCommand<T> command);
            Task<StoreDocumentResponse<T>> StoreDocument(StoreDocumentCommand<T> command);
        }

        public class DocumentsService<T> : IDocumentsService<T> where T : IEntity
        {
            private readonly IRepository<T> _repository;

            public DocumentsService(
                IRepository<T> repository
            )
            {
                Require.ObjectNotNull(repository, "repository should be defined");

                _repository = repository;
            }

            public async Task<NewRecordResponse> AddDocument(AddDocumentCommand<T> command)
            {
                Require.ObjectNotNull(command, "command is required");
                var response = new NewRecordResponse
                {
                    Code = ResponseCode.Success
                };

                var validationResult = new AddDocumentCommandValidator<T>().Validate(command);
                if (!validationResult.IsValid)
                {
                    response.ValidationErrors = validationResult.Errors;
                    response.Code = ResponseCode.BadRequest;
                    return response;
                }

                PopulateNewDocumentFields(command.Document, command.UserId);

                var doc = await _repository.Add(command.Document);
                response.RecordId = doc.Id;

                return response;
            }

            public async Task<StoreDocumentResponse<T>> StoreDocument(StoreDocumentCommand<T> command)
            {
                Require.ObjectNotNull(command, "command is defined");
                var response = new StoreDocumentResponse<T>();

                var validationResult = new StoreDocumentCommandValidator<T>().Validate(command);
                if (!validationResult.IsValid)
                {
                    response.ValidationErrors = validationResult.Errors;
                    response.Code = ResponseCode.BadRequest;
                    return response;
                }

                var recordExists = await _repository.RecordExists(command.Document.Id);
                string currentRecordId;
                if (recordExists)
                {
                    currentRecordId = command.Document.Id;
                    PopulateDocumentForUpdate(command.Document, command.UserId);
                    await _repository.Update(command.Document);
                }
                else
                {
                    PopulateNewDocumentFields(command.Document, command.UserId);
                    var newDocument = await _repository.Add(command.Document);
                    currentRecordId = newDocument.Id;
                }

                var documentToReturn = await _repository.GetById(currentRecordId);
                response.Document = documentToReturn;
                return response;
            }

            public async Task<AppResponse> UpdateDocument(UpdateDocumentCommand<T> command)
            {
                Require.ObjectNotNull(command, "Command is required");

                var response = new AppResponse
                {
                    Code = ResponseCode.Success
                };

                var validationResult = new UpdateDocumentCommandValidator<T>().Validate(command);
                if (!validationResult.IsValid)
                {
                    response.Code = ResponseCode.BadRequest;
                    response.ValidationErrors = validationResult.Errors;
                    return response;
                }

                PopulateDocumentForUpdate(command.Document, command.UserId);
                await _repository.Update(command.Document);

                return response;
            }

            public async Task<GetDocumentResponse<T>> GetDocument(GetDocumentQuery query)
            {
                Require.ObjectNotNull(query, "query is required");
                var validationResult = new GetDocumentQueryValidator().Validate(query);
                if (!validationResult.IsValid)
                {
                    var response = new GetDocumentResponse<T>
                    {
                        Code = ResponseCode.BadRequest,
                        ValidationErrors = validationResult.Errors
                    };
                    return response;
                }

                var doc = await _repository.GetById(query.Id);
                return new GetDocumentResponse<T>
                {
                    Document = doc
                };
            }

            public async Task<AppResponse> DeleteDocument(DeleteDocumentCommand command)
            {
                Require.ObjectNotNull(command, "Command is required");
                var response = new AppResponse
                {
                    Code = ResponseCode.Success
                };

                var validationResult = new DeleteDocumentCommandValidator().Validate(command);
                if (!validationResult.IsValid)
                {
                    response.Code = ResponseCode.BadRequest;
                    response.ValidationErrors = validationResult.Errors;
                    return response;
                }

                var record = await _repository.GetById(command.Id);
                if (record == null)
                {
                    response.Code = ResponseCode.NotFound;
                    return response;
                }

                record.DeletedAt = DateTime.Now;
                record.DeletedBy = command.UserId;
                record.IsDeleted = true;
                await _repository.Update(record);

                return response;
            }

            public IEntity PopulateNewDocumentFields(IEntity document, string userId)
            {
                Require.ObjectNotNull(document, "document is required");
                document.CreatedAt = DateTime.Now;
                document.CreatedBy = userId;
                if (string.IsNullOrEmpty(document.Id)) document.Id = Guid.NewGuid().ToString();

                return document;
            }

            private void PopulateDocumentForUpdate(IEntity document, string userId)
            {
                document.UpdatedAt = DateTime.Now;
                document.UpdatedBy = userId;
            }

            public async Task<GetDocumentsResponse<T>> GetAllDocuments()
            {
                var response = new GetDocumentsResponse<T>
                {
                    Documents = await _repository.List()
                };
                return response;
            }
        }
    }
}