using System;
using System.Collections.Generic;
using System.Text;
using libraryLotto.Data;
using static libraryLotto.Data.DsLotto;

namespace libraryLotto
{
    internal static class Variabili
    {
        internal static string urlLotto = @"https://www.superenalotto.com/";
        internal static string urlLottoRisultati = @"risultati/";
        internal static string extractData = @"/risultati/estrazione-";
        internal static string fileDsName = "Lotto.xml";
        internal static int annoDiInizio = 1997 ;
        //   internal static int annoDiInizio = 1997;
        //      internal static int annoDiInizio = 2006;

        static Variabili()
        {

            _DsLottoLoad();
        }

        //struttura Dati 
        internal static DsLotto _DsLotto = new DsLotto();
        internal static void _DsLottoSave() { _DsLotto.WriteXml(fileDsName); /*_DsLotto.WriteXml("schemaLotto.xml");*/ }
        internal static void _DsLottoLoad()
        {
            if (System.IO.File.Exists(fileDsName))
                _DsLotto.ReadXml(fileDsName);
            DateTime LastLottoLoad = _DsLottoGetLastDate();
            annoDiInizio = LastLottoLoad.Year < 1997 ? 1997 : LastLottoLoad.Year;
            /*_DsLotto.WriteXml("schemaLotto.xml");*/
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

        //QuatazioniVincite
        internal static QuatazioniVinciteRow _QuatazioniVinciteRow_newRow() { return _DsLotto.QuatazioniVincite.NewQuatazioniVinciteRow(); }
        internal static void _QuatazioniVincite_addRow(QuatazioniVinciteRow row)
        {

            if (_DsLotto.QuatazioniVincite.Select("id='" + row.id.ToString() + "' and " +
                "enumTipoVincita='" + row.enumTipoVincita.ToString() + "' and " +
                "valore='" + row.valore.ToString() + "' and " +
                "vincitori='" + row.vincitori.ToString() + "' and " +
                "premio='" + row.premio.ToString() + "'")
                .Length == 0)
                _DsLotto.QuatazioniVincite.AddQuatazioniVinciteRow(row);
        }
        internal static QuatazioniVinciteDataTable _QuatazioniVincite() { return _DsLotto.QuatazioniVincite; }

    }
}
