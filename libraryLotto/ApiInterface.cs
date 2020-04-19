using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;
using System.Data;
using libraryLotto.dlm;
using static libraryLotto.dlm.KendoResultDtaLogicMapping;
using static libraryLotto.Data.LottoDs;
using static libraryLotto.dlm.queryDataLogicMapping;
using static libraryLotto.dlm.queryDataStatisticsLogicMapping;

namespace libraryLotto
{
   public class ApiInterface
    {
   
        public List<Struct_Joing_AllTable> GetLottoAndBall()
        {
            return Variabili._listLottoAndPalle();
        }

        public KendoData GetLottoKendoQuery(string KendoQuery)
        {
            return Variabili._LottoFromKendo(KendoQuery);
        }
        public List<Struct_Joing_AllTable> GetLottoPallefromId(int id)
        {
            return Variabili._LottoPallefromId(id);
        }
        public KendoData GetLottoDetailesFromId(int id)
        {
            return Variabili._LottoDetailesFromId(id);
        }
        public List<Struct_lotto_Statistics> GetLottoStatisticsBalls()
        {
            return Variabili._LottoStatisticsBalls();
        }
        public List<Struct_lotto_Statistics> GetLottoStatisticsQuote()
        {
            return Variabili._LottoStatisticsQuote();
        }
    }
}
