using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;
using static libraryLotto.Data.LottoDs;
using System.Data;
using static libraryLotto.dlm.queryDataLogicMapping;

namespace libraryLotto
{
   public class ApiInterface
    {
        public LottoDataTable GetLotto()
        {
            return Variabili._Lotto();
        }
        public List<Struct_Joing_Lotto_LottoPalle> GetLottoAndBall()
        {
            return Variabili._LottoAndPalle();
        }

        public List<Struct_Joing_Lotto_LottoPalle> GetLottoAndBallTestLambda()
        {
            return Variabili._LottoAndPalleTest();
        }
    }
}
