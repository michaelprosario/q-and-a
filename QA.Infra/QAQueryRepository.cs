
using QA.Core.Entities;
using QA.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using DocumentStore.Core.Helpers;

namespace QA.Infra
{
    public class QAQueryRepository : IQAQueryRepository
    {
        private readonly QAContext dbContext;

        public QAQueryRepository(QAContext dbContext)
        {
            this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public List<QuestionAnswer> GetAnswersForQuestion(string questionId)
        {
            Require.NotNullOrEmpty(questionId, "query is required");
            var answers = dbContext.QuestionAnswers.Where(r => r.QuestionId == questionId).ToList();
            if (answers == null)
            {
                throw new ApplicationException("answers returned null");
            }

            return answers;
        }

        public GetQuestionsResponse GetQuestions(GetQuestionsQuery query)
        {
            Require.ObjectNotNull(query, "query is required");
            var questions = dbContext.Questions.Where(r => r.Name.Contains(query.Keyword) || r.Content.Contains(query.Keyword)).ToList();
            if(questions == null)
            {
                throw new ApplicationException("questions returned null");
            }
            var response = new GetQuestionsResponse
            {
                Questions = questions
            };
            return response;
        }
    }
}
