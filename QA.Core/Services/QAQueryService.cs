using DocumentStore.Core.Responses;
using DocumentStore.Core.Requests;
using System.Runtime.Serialization;
using System.Collections.Generic;
using QA.Core.Entities;
using QA.Core.Helpers;

namespace QA.Core.Services
{

    [DataContract]
    public class GetQuestionsQuery : Request
    {
        [DataMember]
        public string Keyword { get; set; }
    }

    [DataContract]
    public class GetQuestionsResponse : AppResponse
    {
        public List<Question> Questions;

        public GetQuestionsResponse()
        {
            Questions = new List<Question>();
        }
    }

    public class QAQueryService {

        public QAQueryService()
        {

        }

        public GetQuestionsResponse GetQuestions(GetQuestionsQuery query)
        {
            Require.ObjectNotNull(query, "query is required");
            GetQuestionsResponse response = new GetQuestionsResponse();
            return response;
        }
    }
}