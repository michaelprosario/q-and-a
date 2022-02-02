
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
            var answers = dbContext.QuestionAnswers.Where(r => r.QuestionId == questionId).OrderByDescending(r => r.Votes).ToList();
            if (answers == null)
            {
                throw new ApplicationException("answers returned null");
            }

            return answers;
        }

        public bool UserVotedForEntity(string userId, string entityType, string entityId){
            Require.NotNullOrEmpty(userId, "userId is required");
            Require.NotNullOrEmpty(entityType, "entityType is required");
            Require.NotNullOrEmpty(entityId, "entityId is required");

            return dbContext.EntityVotes.Any(
                r => r.CreatedBy == userId && 
                r.ParentEntityType == entityType && 
                r.ParentEntityId == entityId 
                );      
        }

        public GetQuestionsResponse GetQuestions(GetQuestionsQuery query)
        {
            Require.ObjectNotNull(query, "query is required");
            var recordSet = dbContext.Questions.AsQueryable();
            
            if(query.Keyword != null && query.Keyword.Length > 0)
            {
                recordSet = recordSet.Where(r => r.Name.Contains(query.Keyword) || r.Content.Contains(query.Keyword));
            }
            var questions = recordSet.ToList();
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
