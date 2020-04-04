using System;
using System.Collections.Generic;
using System.Text;
using libraryLotto.Data;

namespace libraryLotto.dlm
{

    public class queryDataLogicMapping
    {
        public class Struct_lotto
        {
            public int id { get; set; }
            public int anno { get; set; }
            public DateTime data { get; set; }
            public string hrfQuotazioni { get; set; }

            public Struct_lotto()
            {
            }

            public Struct_lotto(int id, int anno, DateTime data, string hrfQuotazioni)
            {
                this.id = id;
                this.anno = anno;
                this.data = data;
                this.hrfQuotazioni = hrfQuotazioni;
            }
        }
        public class Struct_lottoPalle
        {
            public int id { get; set; }
            public int nPalla { get; set; }
            public String tipoPalla { get; set; }

            public Struct_lottoPalle()
            {
            }

            public Struct_lottoPalle(int id, int nPalla, string datatipoPalla)
            {
                this.id = id;
                this.nPalla = nPalla;
                this.tipoPalla = datatipoPalla;
            }
        }
        public class Struct_QuotazioniVincite
        {
            public int id;
            public string enumTipoVincita;
            public long valore;
            public int vincitori;
            public string premio;
            public string valuta;
            public Struct_QuotazioniVincite(int id, string enumTipoVincita, long valore, int vincitori, string premio, string valuta)
            {
                this.id = id;
                this.enumTipoVincita = enumTipoVincita;
                this.valore = valore;
                this.vincitori = vincitori;
                this.premio = premio;
                this.valuta = valuta;
            }
        }

        public class Struct_Joing_Lotto_LottoPalle
        {
            public Struct_Joing_Lotto_LottoPalle(LottoDs.LottoRow LottoRow, LottoDs.LottoPalleRow LottoPalleRow)
            {
                this. aTest = LottoRow.anno;
                this.lotto.id = LottoRow.Id;
                this.lotto.anno = LottoRow.anno;
                this.lotto.data = LottoRow.data;
                this.lotto.hrfQuotazioni = LottoRow.hrfQuotazioni;
                this.lottopalle.id = LottoPalleRow.Id;
                this.lottopalle.nPalla = LottoPalleRow.nPalla;
                this.lottopalle.tipoPalla = LottoPalleRow.tipoPalla;
            }
            public int aTest { get; set; }
            public Struct_lotto lotto=new Struct_lotto();
            public Struct_lottoPalle lottopalle=new Struct_lottoPalle();
        }

    }
}
