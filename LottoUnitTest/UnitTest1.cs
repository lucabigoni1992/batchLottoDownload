using libraryLotto;
using libraryLotto.dlm;
using NUnit.Framework;
using System.Collections.Generic;
using static libraryLotto.dlm.KendoResultDataLogicMapping;
using static libraryLotto.dlm.queryDataLogicMapping;
using static libraryLotto.dlm.queryDataStatisticsLogicMapping;

namespace LottoUnitTest
{
    public class Tests
    {
        readonly ApiInterface api = new ApiInterface();
        [SetUp]
        public void Setup()
        {
            batchDownloadData lottoData = new batchDownloadData();
            lottoData.downloadAllLotto();// carichiamo la base dati
        }

        [Test]
        public void ApiTest_LottoAndBall()
        {
            Assert.True(api.GetLottoAndBall().Count > 0);
        }

        [Test]
        public void ApiTest_LottoKendoQueryt()
        {
            Assert.True(api.GetLottoKendoQuery("").results.Count == 10);
            Assert.True(api.GetLottoKendoQuery("").count > 3000);
        }
        [Test]
        public void ApiTest_KendoData_LottoKendoQueryt()
        {
            KendoData a = api.GetLottoKendoQuery(@" { 'group':[],'skip':500,'sort':[{'dir':'asc','field':'anno'}],'take':10}");
            Assert.True(a.count >= 3151);
            Assert.True(a.results.Count == 10);
            Assert.True(a.results[0].id == 20020038);
            Assert.True(a.results[9].id == 20050113);
        }
        [Test]
        public void ApiTest_list_LottoDetailesFromId()
        {
            List<Struct_Joing_AllTable> a = api.GetLottoPallefromId(20020001);
            Assert.True(a.Count == 7);
            Assert.True(a[0].id == 20020001);
            Assert.True(a[0].tipoPalla == "jolly");
            Assert.True(a[0].nPalla == 45);
        }
        [Test]
        public void ApiTest_KendoData_GetLottoDetailesFromId()
        {
            KendoData a = api.GetLottoDetailesFromId(20200001);
            Assert.True(a.count == 13);
            Assert.True(a.results.Count == a.count);
            Assert.True(a.results[0].id == 20200001);
            Assert.True(a.results[0].premio == "Vincite immediate");
            Assert.True(a.results[0].valore == "25,00");
            Assert.True(a.results[0].enumTipoVincita == "Vincite immediate");
        }
    }
}