using libraryLotto;
using libraryLotto.dlm;
using NUnit.Framework;
using System.Collections.Generic;
using static libraryLotto.dlm.KendoResultDtaLogicMapping;
using static libraryLotto.dlm.queryDataLogicMapping;

namespace LottoUnitTest
{
    public class Tests
    {
        readonly ApiInterface api = new ApiInterface();
        batchDownloadData lottoData = new batchDownloadData();
        [SetUp]
        public void Setup()
        {
            lottoData.downloadAllLotto();// carichiamo la base dati
        }

        [Test]
        public void ApiTest()
        {
            Assert.True(api.GetLottoAndBall().Count > 0);
            Assert.True(api.GetLottoKendoQuery("").results.Count == 10);
            Assert.True(api.GetLottoKendoQuery("").count >3000);
            KendoData a = api.GetLottoKendoQuery(@" { 'group':[],'skip':500,'sort':[{'dir':'asc','field':'anno'}],'take':10}");
            Assert.True(a.count > 3000);
            Assert.True(a.results.Count ==10);
            Assert.True(a.results[0].id == 20020079);
            Assert.True(a.results[9].id == 20020088);
        }
    }
}