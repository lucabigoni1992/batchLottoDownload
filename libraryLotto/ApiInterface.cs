using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;
using System.Data;
using static libraryLotto.dlm.queryDataLogicMapping;
using libraryLotto.dlm;
using static libraryLotto.dlm.KendoResultDtaLogicMapping;

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
            return  Variabili._LottoFromKendo(KendoQuery);
        }
    }
}
