using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using libraryLotto.Data;
using libraryLotto.Data.LottoDsTableAdapters;
using static libraryLotto.Data.LottoDs;

namespace libraryLotto
{
    internal static class Variabili
    {
        internal static string urlLotto = @"https://www.superenalotto.com/";
        internal static string urlLottoRisultati = @"risultati/";
        internal static string extractData = @"/risultati/estrazione-";
        internal static string fileDsName = Properties.Resources.bkDbXml.Replace("|DataDirectory|", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).Replace("file:\\", "");
        internal static int annoDiInizio = 1997;

        //   internal static int annoDiInizio = 1997;
        //      internal static int annoDiInizio = 2006;

        static Variabili()
        {
            _DsLottoLoad();
        }

        //struttura Dati 

        internal static LottoTableAdapter LottoTA = new LottoTableAdapter();
        internal static QuotazioniVinciteTableAdapter QuotazioniVinciteTA = new QuotazioniVinciteTableAdapter();
        internal static LottoPalleTableAdapter LottoPalleTA = new LottoPalleTableAdapter();
        internal static string connectionDb = Properties.Resources.MyConnectionString.Replace("|DataDirectory|", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).Replace("file:\\", "");

        internal static LottoDs _DsLotto = new LottoDs();

        private static void UpdateDb()
        {
            try
            {
                using (LottoTA.Connection = new SqlConnection(connectionDb))
                {
                    LottoTA.Connection.Open();
                    LottoTA.Update(_DsLotto);
                    LottoTA.Fill(_DsLotto.Lotto);
                    LottoTA.Connection.Close();
                }
                using (LottoPalleTA.Connection = new SqlConnection(connectionDb))
                {
                    LottoPalleTA.Connection.Open();
                    LottoPalleTA.Update(_DsLotto);
                    LottoPalleTA.Fill(_DsLotto.LottoPalle);
                    LottoPalleTA.Connection.Close();
                }
                using (QuotazioniVinciteTA.Connection = new SqlConnection(connectionDb))
                {
                    QuotazioniVinciteTA.Connection.Open();
                    QuotazioniVinciteTA.Update(_DsLotto);
                    QuotazioniVinciteTA.Fill(_DsLotto.QuotazioniVincite);
                    QuotazioniVinciteTA.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        internal static void _DsLottoSave()
        {
            UpdateDb();//carico il db
            _DsLotto.WriteXml(fileDsName);//scrivo il file
        }
        internal static void _DsLottoLoad()
        {
            UpdateDb();
            DateTime LastLottoLoad = _DsLottoGetLastDate();
            annoDiInizio = LastLottoLoad.Year < 1997 ? 1997 : LastLottoLoad.Year;
        }

        internal static DateTime _DsLottoGetLastDate()
        {
            if (_DsLotto.Lotto.Rows.Count == 0)
                return DateTime.MinValue;
            DateTime.TryParse(_DsLotto.Lotto.Compute("max([data])", string.Empty).ToString(), out DateTime dt);
            return dt;
        }

        //Lotto
        internal static LottoRow _LottoDs_newRow() { return _DsLotto.Lotto.NewLottoRow(); }
        internal static void _LottoDs_addRow(LottoRow row)
        {
            if (_DsLotto.Lotto.FindById(row.Id) == null)
                _DsLotto.Lotto.AddLottoRow(row);

        }
        internal static LottoDataTable _Lotto() { return _DsLotto.Lotto; }

        //LottoPalle
        internal static LottoPalleRow _LottoPalleDs_newRow() { return _DsLotto.LottoPalle.NewLottoPalleRow(); }
        internal static LottoPalleRow _LottoPalleDs_newRow(int id, int numeroPalla, string tipoPalla)
        {
            LottoPalleRow row = _DsLotto.LottoPalle.NewLottoPalleRow();
            row.Id = id;
            row.nPalla = numeroPalla;
            row.tipoPalla = tipoPalla;
            return row;
        }
        internal static void _LottoPalleDs_addRow(LottoPalleRow row)
        {
            if (_DsLotto.LottoPalle.FindByIdnPallatipoPalla(row.Id, row.nPalla, row.tipoPalla) == null)
                _DsLotto.LottoPalle.AddLottoPalleRow(row);

        }
        internal static LottoPalleDataTable _LottoPalle() { return _DsLotto.LottoPalle; }

        //QuatazioniVincite
        internal static QuotazioniVinciteRow _QuatazioniVinciteRow_newRow() { return _DsLotto.QuotazioniVincite.NewQuotazioniVinciteRow(); }
        internal static void _QuatazioniVincite_addRow(QuotazioniVinciteRow row)
        {

            if (_DsLotto.QuotazioniVincite.FindByIdenumTipoVincitapremio(row.Id, row.enumTipoVincita, row.premio) == null)
                _DsLotto.QuotazioniVincite.AddQuotazioniVinciteRow(row);
        }
        internal static QuotazioniVinciteDataTable _QuatazioniVincite() { return _DsLotto.QuotazioniVincite; }

    }
}
