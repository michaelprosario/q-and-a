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
        IQAQueryRepository repo;
        IRepository<Question> questionRepo;

        [SetUp]
        public void SetupStuff(){
            repo = Substitute.For<IQAQueryRepository>();
            questionRepo = Substitute.For<IRepository<Question>>();
        }

        [Test]
        public void QAQueryService__GetQuestions__TestHappyCase()
        {
            // arrange
            var query = new QA.Core.Services.GetQuestionsQuery();
            query.Keyword = "test";

            var response2 = new GetQuestionsResponse();
            repo.GetQuestions(query).Returns(response2);

            var service = new QA.Core.Services.QAQueryService(repo,questionRepo);

            // act
            var response = service.GetQuestions(query);

            // assert    
            Assert.NotNull(response);
        }
    }
}