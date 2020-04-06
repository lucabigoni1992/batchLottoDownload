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
            public int id { get; set; }
            public string enumTipoVincita { get; set; }
            public long valore { get; set; }
            public int vincitori { get; set; }
            public string premio { get; set; }
            public string valuta { get; set; }
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

        public class Struct_Joing_AllTable
        {

            public int id { get; set; }//id che le tre tabelle hanno in comune
            public int anno { get; set; }
            public DateTime data { get; set; }
            public string hrfQuotazioni { get; set; }
            public int nPalla { get; set; }
            public String tipoPalla { get; set; }
            public string enumTipoVincita { get; set; }
            public long valore { get; set; }
            public int vincitori { get; set; }
            public string premio { get; set; }
            public string valuta { get; set; }

            public Struct_Joing_AllTable(int id, int anno, DateTime data, string hrfQuotazioni, int nPalla, string tipoPalla, string enumTipoVincita, long valore, int vincitori, string premio, string valuta)
            {
                this.id = id;
                this.anno = anno;
                this.data = data;
                this.hrfQuotazioni = hrfQuotazioni;
                this.nPalla = nPalla;
                this.tipoPalla = tipoPalla;
                this.enumTipoVincita = enumTipoVincita;
                this.valore = valore;
                this.vincitori = vincitori;
                this.premio = premio;
                this.valuta = valuta;
            }

            public Struct_Joing_AllTable(LottoDs.LottoRow tablotto, LottoDs.LottoPalleRow tabpalle, LottoDs.QuotazioniVinciteRow tabQuotazioniVincitein)
            {
                this.id = tablotto.Id;
                this.anno = tablotto.anno;
                this.data = tablotto.data;
                this.hrfQuotazioni = tablotto.hrfQuotazioni;
                this.nPalla = tabpalle.nPalla;
                this.tipoPalla = tabpalle.tipoPalla;
                this.enumTipoVincita = tabQuotazioniVincitein.enumTipoVincita;
                this.valore = tabQuotazioniVincitein.valore;
                this.vincitori = tabQuotazioniVincitein.vincitori;
                this.premio = tabQuotazioniVincitein.premio;
                this.valuta = tabQuotazioniVincitein.IsvalutaNull() ? "" : valuta;
            }
            public Struct_Joing_AllTable(LottoDs.LottoRow tablotto,  LottoDs.QuotazioniVinciteRow tabQuotazioniVincitein)
            {
                this.id = tablotto.Id;
                this.anno = tablotto.anno;
                this.data = tablotto.data;
                this.hrfQuotazioni = tablotto.hrfQuotazioni;
                this.enumTipoVincita = tabQuotazioniVincitein.enumTipoVincita;
                this.valore = tabQuotazioniVincitein.valore;
                this.vincitori = tabQuotazioniVincitein.vincitori;
                this.premio = tabQuotazioniVincitein.premio;
                this.valuta = tabQuotazioniVincitein.IsvalutaNull() ? "" : valuta;
            }
            public Struct_Joing_AllTable(LottoDs.LottoRow tablotto, LottoDs.LottoPalleRow tabpalle)
            {
                this.id = tablotto.Id;
                this.anno = tablotto.anno;
                this.data = tablotto.data;
                this.hrfQuotazioni = tablotto.hrfQuotazioni;
                this.nPalla = tabpalle.nPalla;
                this.tipoPalla = tabpalle.tipoPalla;
            }
            public Struct_Joing_AllTable(LottoDs.LottoRow tablotto)
            {
                this.id = tablotto.Id;
                this.anno = tablotto.anno;
                this.data = tablotto.data;
                this.hrfQuotazioni = tablotto.hrfQuotazioni;
            }
        }

    }
}
