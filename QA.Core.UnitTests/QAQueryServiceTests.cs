using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace QA.Core.UnitTests{

    [TestFixture]
    public class QAQueryServiceTests
    {
        [Test]
        public void QAQueryService__GetQuestions__TestHappyCase()
        {
            // arrange
            var service = new QA.Core.Services.QAQueryService();
            var query = new QA.Core.Services.GetQuestionsQuery();
            query.Keyword = "test";

            // act
            var getQuestionsResponse = service.GetQuestions(query);

            // assert            

        }
    }
}