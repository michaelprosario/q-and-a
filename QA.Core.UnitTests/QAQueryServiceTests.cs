using DocumentStore.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;
using QA.Core.Entities;
using QA.Core.Services;
using System;
using System.Threading.Tasks;

namespace QA.Core.UnitTests{

    [TestFixture]
    public class QAQueryServiceTests
    {
        [Test]
        public void QAQueryService__GetQuestions__TestHappyCase()
        {
            // arrange
            var repo = Substitute.For<IQAQueryRepository>();
            var questionRepo = Substitute.For<IRepository<Question>>();
            
            var service = new QA.Core.Services.QAQueryService(repo,questionRepo);
            var query = new QA.Core.Services.GetQuestionsQuery();
            var response2 = new GetQuestionsResponse();
            repo.GetQuestions(query).Returns(response2);
            query.Keyword = "test";

            // act
            var response = service.GetQuestions(query);

            // assert    
            Assert.NotNull(response);
        }
    }
}