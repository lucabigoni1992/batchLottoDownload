using libraryLotto;
using NUnit.Framework;

namespace LottoUnitTest
{
    public class Tests
    {
        ApiInterface api = new ApiInterface();
            batchDownloadData lottoData = new batchDownloadData();
        [SetUp]
        public void Setup()
        {
            lottoData.downloadAllLotto();// carichiamo la base dati
        }

        [Test]
        public void ApiTest()
        {
            Assert.True(api.GetLotto().Rows.Count > 3080);
            Assert.True(api.GetLottoAndBall().Count > 0);
            Assert.True(api.GetLottoAndBallTestLambda().Count > 0);
        }
    }
}