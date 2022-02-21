using DocumentStore.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;
using QA.Core.Entities;
using QA.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Core.UnitTests{

    [TestFixture]
    public class QAQueryServiceTests
    {
        IQAQueryRepository _queryRepo;
        IRepository<Question> _questionsRepo;
        IRepository<QuestionAnswer> _questionAnswersRepo;
        

        [SetUp]
        public void SetupStuff(){
            _queryRepo = Substitute.For<IQAQueryRepository>();
            _questionsRepo = Substitute.For<IRepository<Question>>();
            _questionAnswersRepo = Substitute.For<IRepository<QuestionAnswer>>();
        }

        [Test]
        public void QAQueryService__GetQuestions__TestHappyCase()
        {
            // arrange
            var query = new GetQuestionsQuery();
            query.Keyword = "test";

            var response2 = new GetQuestionsResponse();
            _queryRepo.GetQuestions(query).Returns(response2);

            var service = new QAQueryService(_queryRepo,_questionsRepo, _questionAnswersRepo);

            // act
            var response = service.GetQuestions(query);

            // assert    
            Assert.NotNull(response);
        }

        [Test]
        public void QAQueryService__GetQuestionAndAnswer__TestHappyCase()
        {
            // arrange
            var service = new QAQueryService(_queryRepo, _questionsRepo);

            var query = new DocumentStore.Core.Requests.GetDocumentQuery
            {
                UserId = "mrosario",
                Id = "testId"
            };

            Question question = GetQuestion();

            // setup repos ...
            _questionsRepo.GetById(query.Id).Returns(Task.FromResult(question));
            _queryRepo.GetAnswersForQuestion(query.Id).Returns(new List<QuestionAnswer>());

            // act
            var response = service.GetQuestionAndAnswers(query);

            // assert    
            Assert.NotNull(response);
        }

        private static Question GetQuestion()
        {
            return new Question()
            {
                Name = "what is 1 + 1 ?",
                Content = "Need help with math",
                HtmlContent = "Need help with math",
                Tags = "tag1,tag2,tag3",
                PermaLink = "permaLink",
                CreatedAt = DateTime.Now,
                CreatedBy = "mrosario",
                Id = Guid.NewGuid().ToString(),
                Abstract = "abstract"
            };
        }
    }
}