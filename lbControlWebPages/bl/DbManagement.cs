using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using lbControlWebPages;
using lbControlWebPages.Properties;
using lbControlWebPages.webPagesData;
using static lbControlWebPages.webPagesData.SiteData;
using FluentScheduler;
using libraryLotto.dlm;
using static libraryLotto.dlm.SiteMapping;

namespace libraryLotto
{
    internal static class DbManagement
    {
        internal static List<Registry> Scheduler = new List<Registry>();
        internal static string fileDsName = Path.Combine(GetApplicationRoot(), Resources.bkDbXml_folder, Resources.bkDbXml_SiteDb);
        private static SiteData _DSSiteData = new SiteData();

        static DbManagement()
        {
            _DsSiteLoad();
        }

        //struttura Dati 

        internal static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
        internal static void _DsSiteSave()
        {
            string dir = (Path.GetDirectoryName(fileDsName));
            Console.WriteLine("file-> " + fileDsName);
            if (!Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            _DSSiteData.WriteXml(fileDsName);
            _DSSiteData.AcceptChanges();
        }
        internal static void _DsSiteLoad()
        {
            if (System.IO.File.Exists(fileDsName))
                _DSSiteData.ReadXml(fileDsName);
            foreach (SiteRow row in _DSSiteData.Site.Rows)
                addSchedule(row);

        }


        //Site
        internal static SiteRow _dsSiteData_newRow(string Url)
        {
            SiteRow rows = _DSSiteData.Site.FindBySite(Url);
            if (rows != null)
                return rows;
            else
                return _dsSiteData_newRow();
        }
        internal static SiteRow _dsSiteData_newRow() { return _DSSiteData.Site.NewSiteRow(); }
        internal static void _LottoDs_addRow(SiteRow row)
        {
            if (_DSSiteData.Site.FindBySite(row.Site) == null)
            {
                _DSSiteData.Site.AddSiteRow(row);
                _DsSiteSave();
                addSchedule(row);
            }
        }
        internal static SiteDataTable _Site() { return _DSSiteData.Site; }


        //gestione job
        private static void addSchedule(SiteRow row)
        {
            var r = new Registry();
            _ = r.Schedule(async () =>
                              await InteractiveDB.AddUpdateSiteAsync(row.Site, row.CadAggiornamento)
                            ).ToRunNow()
                            .AndEvery(row.CadAggiornamento)
#if DEBUG
                            .Minutes();
# else
                            .Hours();
#endif
            Scheduler.Add(r);
            JobManager.Initialize(Scheduler.ToArray<Registry>());
        }

        internal static IEnumerable<SiteMapping> _SiteAllRow()
        {
            try
            {
                IEnumerable<SiteMapping> _SiteData = (
                     from Tablotto in _DSSiteData.Site
                     select new SiteMapping(Tablotto)
                     );
                return _SiteData;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }


    }
}
