using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libraryLotto.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace libraryLotto.dlm
{

    public class queryDataLogicMapping
    {
        public class DateFormatConverter : IsoDateTimeConverter
        {
            public DateFormatConverter(string format)
            {
                DateTimeFormat = format;
            }
        }
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
            public int nEstrazione { get; set; }//id che le tre tabelle hanno in comune
            public int anno { get; set; }
            [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
            public DateTime data { get; set; }
            public string hrfQuotazioni { get; set; }
            public int nPalla { get; set; }
            public String tipoPalla { get; set; }
            public string enumTipoVincita { get; set; }
            public string valore { get; set; }
            public int vincitori { get; set; }
            public string premio { get; set; }
            public string valuta { get; set; }

            //statistiche
            public int nVincitori{ get; set; }
            public string premio6Punti { get; set; }

            public Struct_Joing_AllTable(int id, int nEstrazione, int anno, DateTime data, string hrfQuotazioni, int nPalla, string tipoPalla, string enumTipoVincita, string valore, int vincitori, string premio, string valuta)
            {
                this.id = id;
                this.nEstrazione = nEstrazione;
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
                this.id = tablotto.id;
                this.nEstrazione = tablotto.nEstrazione;
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
            public Struct_Joing_AllTable(LottoDs.LottoRow tablotto, LottoDs.QuotazioniVinciteRow tabQuotazioniVincitein)
            {
                this.id = tablotto.id;
                this.nEstrazione = tablotto.nEstrazione;
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
                this.id = tablotto.id;
                this.nEstrazione = tablotto.nEstrazione;
                this.anno = tablotto.anno;
                this.data = tablotto.data;
                this.hrfQuotazioni = tablotto.hrfQuotazioni;
                this.nPalla = tabpalle.nPalla;
                this.tipoPalla = tabpalle.tipoPalla;
            }
            public Struct_Joing_AllTable(LottoDs.LottoRow tablotto)
            {
                this.id = tablotto.id;
                this.nEstrazione = tablotto.nEstrazione;
                this.anno = tablotto.anno;
                this.data = tablotto.data;
                this.hrfQuotazioni = tablotto.hrfQuotazioni;
                SetStatistics(tablotto);
            }
            public Struct_Joing_AllTable( LottoDs.LottoPalleRow tabpalle)
            {
                this.id = tabpalle.id;
                this.nPalla = tabpalle.nPalla;
                this.tipoPalla = tabpalle.tipoPalla;
            }

            private void SetStatistics(LottoDs.LottoRow tablotto)
            {
                nVincitori = tablotto.GetQuotazioniVinciteRows()
                    .Where(r => r.premio == "6 punti")
                    .Select(r => r.vincitori)
                    .First();
                premio6Punti = tablotto.GetQuotazioniVinciteRows()
                    .Where(r => r.premio == "6 punti")
                    .Select(r => r.valore)
                    .First();
            }
        }

    }
}
