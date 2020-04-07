using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using libraryLotto.Data;
using static libraryLotto.Data.LottoDs;
using static libraryLotto.dlm.queryDataLogicMapping;
using static libraryLotto.bl.BuisnessLogicUtilities;
using Newtonsoft.Json;
using static libraryLotto.dlm.KendoDataLogicMapping;
using libraryLotto.dlm;
using static libraryLotto.dlm.KendoResultDtaLogicMapping;

namespace libraryLotto
{
    internal static class Variabili
    {
        internal static string urlLotto = @"https://www.superenalotto.com/";
        internal static string urlLottoRisultati = @"risultati/";
        internal static string extractData = @"/risultati/estrazione-";
        internal static string fileDsName = Properties.Resources.bkDbXml.Replace("|DataDirectory|", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).Replace("file:\\", "");
        internal static DateTime annoDiInizio = new DateTime(1997, 1, 1);

        //   internal static int annoDiInizio = 1997;
        //      internal static int annoDiInizio = 2006;

        static Variabili()
        {
            _DsLottoLoad();
        }

        //struttura Dati 


        internal static LottoDs _DsLotto = new LottoDs();


        internal static void _DsLottoSave()
        {
            _DsLotto.WriteXml(fileDsName);//scrivo il file
            _DsLotto.AcceptChanges();
        }
        internal static void _DsLottoLoad()
        {
            if (System.IO.File.Exists(fileDsName))
                _DsLotto.ReadXml(fileDsName);//scrivo il file
            DateTime LastLottoLoad = _DsLottoGetLastDate();
            annoDiInizio = LastLottoLoad.Year < 1997 ? new DateTime(1997, 1, 1) : LastLottoLoad;

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
            if (row.Id == 0)
            {
                string stop = "" + "dd";
            }
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


        //ITERAZIONI TRA DATATABLE
        private static IEnumerable<Struct_Joing_AllTable> _enumTablotto()
        {
            IEnumerable<Struct_Joing_AllTable> _Tablotto = (
                    from Tablotto in _DsLotto.Lotto
                    select new Struct_Joing_AllTable(Tablotto)
                    );
            return _Tablotto;
        }
        private static IEnumerable<Struct_Joing_AllTable> _enumTabLotto_Tabpalle()
        {
            IEnumerable<Struct_Joing_AllTable> _Lotto_Tabpalle  = (
                    from Tablotto in _DsLotto.Lotto
                    join Tabpalle in _DsLotto.LottoPalle on Tablotto.Id equals Tabpalle.Id
                    select new Struct_Joing_AllTable(Tablotto, Tabpalle)
                    );
            return _Lotto_Tabpalle;
        }
        private static IEnumerable<Struct_Joing_AllTable> _enumTablotto_TabQuVin()
        {
            IEnumerable<Struct_Joing_AllTable> _TablottoTabQuVin = (
                    from Tablotto in _DsLotto.Lotto
                    join TabQuVin in _DsLotto.QuotazioniVincite on Tablotto.Id equals TabQuVin.Id
                    select new Struct_Joing_AllTable(Tablotto, TabQuVin)
                    );
            return _TablottoTabQuVin;
        }

        internal static List<Struct_Joing_AllTable> _listLottoAndPalle()
        {
            return _enumTabLotto_Tabpalle().ToList();
        }

        public static KendoData _LottoFromKendo(string KendoQuery)
        {
            try
            {
                IEnumerable<Struct_Joing_AllTable> enumerable = _enumTablotto();
                if (String.IsNullOrEmpty(KendoQuery))
                {
                    enumerable = enumerable.OrderByDescending(estrazione => estrazione.id)
                        .ToList();
                    return new KendoData(
                          enumerable.Count(),
                          enumerable
                                .ToList()
                                .GetRange(0, 10)
                                  );
                        }
                else
                    return GettableByKendofilter(enumerable, KendoQuery.Replace(@"""","'"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        private static KendoData GettableByKendofilter(IEnumerable<Struct_Joing_AllTable> enumerable, string kendoQuery)
        {
            //applichiamo where
            QueryDescriptor Qd = JsonConvert.DeserializeObject<QueryDescriptor>(kendoQuery);
            enumerable = enumerable.Where(Qd)
                            .OrderBy(Qd);
            return new KendoData(
                  enumerable.Count(),
                  enumerable
                          .Range(Qd)
                          .ToList()
                          );
        }
    }

}
