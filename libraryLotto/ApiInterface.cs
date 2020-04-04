using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;
using static libraryLotto.Data.LottoDs;

namespace libraryLotto
{
   public class ApiInterface
    {
        public LottoDataTable GetLotto()
        {
            return Variabili._Lotto();
        }
        public LottoDataTable GetLottoAndBall()
        {
            return Variabili._Lotto();
        }
    }
}
