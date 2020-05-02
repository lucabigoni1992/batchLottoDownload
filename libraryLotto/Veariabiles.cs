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
using static libraryLotto.dlm.KendoResultDataLogicMapping;
using System.IO;
using static libraryLotto.dlm.queryDataStatisticsLogicMapping;
using static libraryLotto.dlm.KendoResultDataStatisticsLogicMapping;

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
            string dir = (Path.GetDirectoryName(fileDsName));
            if (!Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            _DsLotto.WriteXml(fileDsName);//scrivo il file
            _DsLotto.AcceptChanges();
        }
        internal static void _DsLottoLoad()
        {
            if (System.IO.File.Exists(fileDsName))
                _DsLotto.ReadXml(fileDsName);//scrivo il file
            DateTime LastLottoLoad = _DsLottoGetLastDate();
            annoDiInizio = LastLottoLoad.Year < 1997 ? new DateTime(2020, 1, 1) : LastLottoLoad;

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
            if (_DsLotto.Lotto.FindByid(row.id) == null)
                _DsLotto.Lotto.AddLottoRow(row);

        }
        internal static LottoDataTable _Lotto() { return _DsLotto.Lotto; }

        //LottoPalle
        internal static LottoPalleRow _LottoPalleDs_newRow() { return _DsLotto.LottoPalle.NewLottoPalleRow(); }
        internal static LottoPalleRow _LottoPalleDs_newRow(int id, int numeroPalla, string tipoPalla)
        {
            LottoPalleRow row = _DsLotto.LottoPalle.NewLottoPalleRow();
            row.id = id;
            row.nPalla = numeroPalla;
            row.tipoPalla = tipoPalla;
            return row;
        }
        internal static void _LottoPalleDs_addRow(LottoPalleRow row)
        {
            if (_DsLotto.LottoPalle.FindByidnPallatipoPalla(row.id, row.nPalla, row.tipoPalla) == null)
                _DsLotto.LottoPalle.AddLottoPalleRow(row);

        }
        internal static LottoPalleDataTable _LottoPalle() { return _DsLotto.LottoPalle; }

        //QuatazioniVincite
        internal static QuotazioniVinciteRow _QuatazioniVinciteRow_newRow() { return _DsLotto.QuotazioniVincite.NewQuotazioniVinciteRow(); }
        internal static void _QuatazioniVincite_addRow(QuotazioniVinciteRow row)
        {

            if (_DsLotto.QuotazioniVincite.FindByidenumTipoVincitapremio(row.id, row.enumTipoVincita, row.premio) == null)
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
            IEnumerable<Struct_Joing_AllTable> _Lotto_Tabpalle = (
                    from Tablotto in _DsLotto.Lotto
                    join Tabpalle in _DsLotto.LottoPalle on Tablotto.id equals Tabpalle.id
                    select new Struct_Joing_AllTable(Tablotto, Tabpalle)
                    );
            return _Lotto_Tabpalle;
        }
        private static IEnumerable<Struct_Joing_AllTable> _enumTablotto_TabQuVin()
        {
            IEnumerable<Struct_Joing_AllTable> _TablottoTabQuVin = (
                    from Tablotto in _DsLotto.Lotto
                    join TabQuVin in _DsLotto.QuotazioniVincite on Tablotto.id equals TabQuVin.id
                    select new Struct_Joing_AllTable(Tablotto, TabQuVin)
                    );
            return _TablottoTabQuVin;
        }

        internal static List<Struct_Joing_AllTable> _listLottoAndPalle()
        {
            return _enumTabLotto_Tabpalle().ToList();
        }

        internal static KendoData _LottoFromKendo(string KendoQuery)
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
                    return GettableByKendofilter(enumerable, KendoQuery.Replace(@"""", "'"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        internal static List<Struct_Joing_AllTable> _LottoPallefromId(int id)
        {
            if (id <= 0)
                throw new Exception("errore id negativo");
            List<Struct_Joing_AllTable> ris = new List<Struct_Joing_AllTable>();
            foreach (LottoPalleRow row in _DsLotto.Lotto.FindByid(id).GetLottoPalleRows())
                ris.Add(new Struct_Joing_AllTable(row));
            return ris;
        }
        internal static KendoData _LottoDetailesFromId(int id)
        {
            if (id <= 0)
                throw new Exception("errore id negativo");
            var enumerable = _enumTablotto_TabQuVin().Where(r => r.id == id);
            return new KendoData(
            enumerable.Count(),
            enumerable.ToList()
                    );
        }
        internal static KendoStatisticsData _LottoStatisticsBalls()
        {
            var enumerable = _enumTabLotto_Tabpalle()
                .GroupBy(r => r.nPalla)
                .Select(group => new Struct_lotto_Statistics(
                   group.Key.ToString(),
                  group.Count()
                ));
            return new KendoStatisticsData(enumerable,true);// provalista();
        }
        internal static KendoStatisticsData _LottoStatisticsQuote()
        {
            var enumerable = _enumTablotto_TabQuVin()
                    .GroupBy(r => r.anno)
                    .Select(group => new Struct_lotto_Statistics(
                       group.Key.ToString(),
                      group.GroupBy(r => r.enumTipoVincita).ToList()
                    ));
            return new KendoStatisticsData(enumerable);
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

        private static List<Struct_lotto_Statistics> provalista()
        {
            List<Struct_lotto_Statistics> list = new List<Struct_lotto_Statistics>();
            list.Add(new Struct_lotto_Statistics());
            list.Add(new Struct_lotto_Statistics("JP Morgan", 116));
            list.Add(new Struct_lotto_Statistics("HSBC", 165));
            list.Add(new Struct_lotto_Statistics("Credit Suisse", 12));
            list.Add(new Struct_lotto_Statistics("Goldman Sachs", 22));
            list.Add(new Struct_lotto_Statistics("Morgan Stanley", 47));
            list.Add(new Struct_lotto_Statistics("Societe Generale", 263));
            list.Add(new Struct_lotto_Statistics("UBS", 80));
            list.Add(new Struct_lotto_Statistics("BNP Paribas", 15));
            list.Add(new Struct_lotto_Statistics("Unicredit", 86778));
            list.Add(new Struct_lotto_Statistics("Credit Agricole", 56756));
            list.Add(new Struct_lotto_Statistics("Deutsche Bank", 77));
            list.Add(new Struct_lotto_Statistics("Barclays", 75));
            list.Add(new Struct_lotto_Statistics("Citigroup", 75));
            list.Add(new Struct_lotto_Statistics("RBS", 255));
            return list;
        }

    }

}
