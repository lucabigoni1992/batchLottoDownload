﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using libraryLotto.Data;
using static libraryLotto.Data.LottoDs;
using static libraryLotto.dlm.queryDataLogicMapping;

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

        internal static List<Struct_Joing_Lotto_LottoPalle> _LottoAndPalle()
        {
            return (
                    from Tablotto in _DsLotto.Lotto
                    join Tabpalle in _DsLotto.LottoPalle on Tablotto.Id equals Tabpalle.Id
                    select new Struct_Joing_Lotto_LottoPalle(Tablotto, Tabpalle)
                    ).ToList();
        }


        internal static List<Struct_Joing_Lotto_LottoPalle> _LottoAndPalleTest()
        {
            var param = Expression.Parameter(typeof(Struct_Joing_Lotto_LottoPalle), "p");
            var exeWhere = Expression.Lambda<Func<Struct_Joing_Lotto_LottoPalle, bool>>(
                Expression.Equal(
                    Expression.Property(param, "aTest"),
                    Expression.Constant(1992)
                ),
                param
            );

            return (
                    from Tablotto in _DsLotto.Lotto
                    join Tabpalle in _DsLotto.LottoPalle on Tablotto.Id equals Tabpalle.Id
                    select new Struct_Joing_Lotto_LottoPalle(Tablotto, Tabpalle)
                    ).Where(exeWhere.Compile()).Where(p => p.lotto.anno == 1992).ToList(); ;
        }
    }

}
