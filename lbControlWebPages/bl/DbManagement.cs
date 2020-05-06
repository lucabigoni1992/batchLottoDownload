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

namespace libraryLotto
{
    internal static class DbManagement
    {
        internal static string fileDsName = Path.Combine(GetApplicationRoot(), Resources.bkDbXml_folder, Resources.bkDbXml_SiteDb);

        static DbManagement()
        {
            _DsSiteLoad();
        }

        //struttura Dati 
        internal static SiteData _DSSiteData = new SiteData();

        internal static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
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
        }

        //Site
        internal static SiteRow _dsSiteData_newRow() { return _DSSiteData.Site.NewSiteRow(); }
        internal static void _LottoDs_addRow(SiteRow row)
        {
            if (_DSSiteData.Site.FindByid(row.id) == null)
            {
                _DSSiteData.Site.AddSiteRow(row);
                _DsSiteSave();
            }
        }
        internal static SiteDataTable _Site() { return _DSSiteData.Site; }
    }
}
