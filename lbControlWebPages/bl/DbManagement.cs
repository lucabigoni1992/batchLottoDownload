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
    public static class DbManagement
    {
        internal static List<Registry> Scheduler = new List<Registry>();
        internal static string fileDsName = Path.Combine(GetApplicationRoot(), Resources.bkDbXml_folder, Resources.bkDbXml_SiteDb);
        private static readonly SiteData _DSSiteData = new SiteData();

        static DbManagement()
        {
            DsSiteLoad();
        }

        //struttura Dati 

        internal static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
        internal static void DsSiteSave()
        {
            string dir = (Path.GetDirectoryName(fileDsName));
            Console.WriteLine("file-> " + fileDsName);
            if (!Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            _DSSiteData.WriteXml(fileDsName);
            _DSSiteData.AcceptChanges();
        }
            
        public static void DsSiteLoad()
        {
            if (_DSSiteData.Site.Rows.Count > 0)
                _DSSiteData.Site.Rows.Clear();
            if (System.IO.File.Exists(fileDsName))
                _DSSiteData.ReadXml(fileDsName);
            Scheduler.Clear();
            foreach (SiteRow row in _DSSiteData.Site.Rows)
                AddSchedule(row);

            JobManager.StopAndBlock();
            JobManager.RemoveAllJobs();
            JobManager.Initialize(Scheduler.ToArray<Registry>());

        }


        //Site
        internal static SiteRow DsSiteData_newRow(SiteInputMapping elem)
        {
            SiteRow rows = _DSSiteData.Site.FindByUrl(elem.Url);
            if (rows != null)
                return rows;
             rows= DsSiteData_newRow();
           FromInputSiteToRow (elem, ref rows);
            return rows;
        }  //Site
        internal static SiteRow DsSiteData_newRow(string Url)
        {
            SiteRow rows = _DSSiteData.Site.FindByUrl(Url);
            if (rows != null)
                return rows;
            else
                return DsSiteData_newRow();
        }
        internal static SiteRow DsSiteData_newRow() { return _DSSiteData.Site.NewSiteRow(); }
        internal static void LottoDs_addRow(SiteRow row)
        {
            if (_DSSiteData.Site.FindByUrl(row.Url) == null)
            {
                _DSSiteData.Site.AddSiteRow(row);
                DsSiteSave();
                DsSiteLoad();
            }
        }
        internal static void DsSiteData_disableRow(string url)
        {
            if (_DSSiteData.Site.FindByUrl(url) != null)
            {
                var row = _DSSiteData.Site.FindByUrl(url);
                row.Active = (byte)(row.Active == 0 ? 1 : 0);
            }
        }

        internal static void DsSiteData_deleteRow(string url)
        {
            if (_DSSiteData.Site.FindByUrl(url) != null)
                _DSSiteData.Site.RemoveSiteRow(_DSSiteData.Site.FindByUrl(url));
            _DSSiteData.Site.AcceptChanges();
            DsSiteSave();
            DsSiteLoad();
        }
        internal static SiteDataTable Site() { return _DSSiteData.Site; }


        //gestione job
        private static void AddSchedule(SiteRow row)
        {
            var r = new Registry();
            _ = r.Schedule(async () =>
                              await InteractiveDB.AddUpdateSiteAsync(row)
                            ).WithName(row.Url)
                            .ToRunNow()
#if DEBUG
                            .AndEvery(5)
                            .Minutes();
#else
                            .AndEvery(row.Ore)    
                            .Hours();
#endif
           
            Scheduler.Add(r);
        }

        internal static IEnumerable<SiteMapping> SiteAllRow()
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
