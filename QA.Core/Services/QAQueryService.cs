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

    public class QAQueryService {

        IQAQueryRepository _qaQueryRepository;
        IRepository<Question> _questionsRepo;
        public QAQueryService(IQAQueryRepository qaQueryRepository, IRepository<Question> questionsRepo)
        {
            Require.ObjectNotNull(qaQueryRepository, "qaQueryRepository is required");
            Require.ObjectNotNull(questionsRepo, "questionsRepo is required");
            _qaQueryRepository = qaQueryRepository;
            _questionsRepo = questionsRepo;
        }

        public GetQuestionsResponse GetQuestions(GetQuestionsQuery query)
        {
            Require.ObjectNotNull(query, "query is required");
            GetQuestionsResponse response = new GetQuestionsResponse();
            
            return _qaQueryRepository.GetQuestions(query);
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