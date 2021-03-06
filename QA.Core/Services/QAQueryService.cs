using DocumentStore.Core.Enums;
using DocumentStore.Core.Interfaces;
using DocumentStore.Core.Requests;
using DocumentStore.Core.Responses;
using QA.Core.Entities;
using QA.Core.Helpers;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace QA.Core.Services
{

    public interface IQAQueryRepository
    {
        GetQuestionsResponse GetQuestions(GetQuestionsQuery query);
        List<QuestionAnswer> GetAnswersForQuestion(string questionId);
        bool UserVotedForEntity(string userId, string entityType, string entityId);
    }

    [DataContract]
    public class GetQuestionsQuery : Request
    {
        [DataMember]
        public string Keyword { get; set; }
    }

    [DataContract]
    public class GetQuestionsResponse : AppResponse
    {
        [DataMember]
        public List<Question> Questions { get; set; }     

        public GetQuestionsResponse()
        {
            Questions = new List<Question>();
        }
    }

    [DataContract]
    public class GetQuestionAndAnswersResponse : AppResponse
    {
        [DataMember]
        public List<QuestionAnswer> Answers { get; set; }
        [DataMember]
        public Question Question{ get; set; }

        public GetQuestionAndAnswersResponse()
        {
            Answers = new List<QuestionAnswer>();
        }
    }    

    public interface IQAQueryService {
        GetQuestionsResponse GetQuestions(GetQuestionsQuery query);
        Task<GetQuestionAndAnswersResponse> GetQuestionAndAnswers(GetDocumentQuery query);
        bool UserVotedForEntity(string userId, string entityType, string entityId);
        Task<DataResponse<QuestionAnswer>> GetAnswer(GetDocumentQuery query);
    }

    public class QAQueryService : IQAQueryService {

        IQAQueryRepository _qaQueryRepository;
        IRepository<Question> _questionsRepo;
        IRepository<QuestionAnswer> _answersRepo;
        public QAQueryService(IQAQueryRepository qaQueryRepository, 
                              IRepository<Question> questionsRepo,
                              IRepository<QuestionAnswer> answersRepo
                              )
        {
            Require.ObjectNotNull(qaQueryRepository, "qaQueryRepository is required");
            Require.ObjectNotNull(questionsRepo, "questionsRepo is required");
            Require.ObjectNotNull(answersRepo, "answersRepo is required");
            
            _qaQueryRepository = qaQueryRepository;
            _questionsRepo = questionsRepo;
            _answersRepo = answersRepo;
        }

        public GetQuestionsResponse GetQuestions(GetQuestionsQuery query)
        {
            Require.ObjectNotNull(query, "query is required");
            GetQuestionsResponse response = new GetQuestionsResponse();
            
            return _qaQueryRepository.GetQuestions(query);
        }

        public bool UserVotedForEntity(string userId, string entityType, string entityId)
        {
            Require.NotNullOrEmpty(userId, "userId is required");
            Require.NotNullOrEmpty(entityType, "entityType is required");
            Require.NotNullOrEmpty(entityId, "entityId is required");

            return _qaQueryRepository.UserVotedForEntity(userId, entityType, entityId);
        }

        public async Task<DataResponse<QuestionAnswer>> GetAnswer(GetDocumentQuery query)
        {
            Require.ObjectNotNull(query, "query is required");

            DataResponse<QuestionAnswer> response = new DataResponse<QuestionAnswer>();
            var getRecordResponse = await _answersRepo.GetById(query.Id);
            if( getRecordResponse == null)
            {
                response.Code = ResponseCode.NotFound;
                response.Message = "record not found";
                return response;
            }

            response.Data = getRecordResponse;
            return response;
        }        
 
        public async Task<GetQuestionAndAnswersResponse> GetQuestionAndAnswers(GetDocumentQuery query)
        {
            Require.ObjectNotNull(query, "query is required");
            GetQuestionAndAnswersResponse response = new GetQuestionAndAnswersResponse();
            
            var question = await _questionsRepo.GetById(query.Id);
            if(question == null)
            {
                response.Code = ResponseCode.NotFound;
                response.Message = "_questionsRepo - question not found";
                return response;
            }

            List<QuestionAnswer> answers = _qaQueryRepository.GetAnswersForQuestion(query.Id);
            if(answers == null)
            {
                response.Message = "_qaQueryRepository.GetAnswersForQuestion returned null";
                return response;
            }
            
            response.Question = question;
            response.Answers = answers;
            
            return response;           
        }        
    }
}