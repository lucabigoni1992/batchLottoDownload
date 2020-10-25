using HtmlAgilityPack;
using libraryLotto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static lbControlWebPages.webPagesData.SiteData;
using System.Threading;
using libraryLotto.dlm;
using static libraryLotto.dlm.KendoResultDataSiteInputMapping;
using System.Linq;
using System.Net.Mail;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace lbControlWebPages
{
    public static class CompareStringDiffPlex
    {
        public static string Compare (string oldText, string newText)
        {
            StringBuilder sb = new StringBuilder();

            var d = new Differ();
            var builder = new InlineDiffBuilder(d);
            var result = builder.BuildDiffModel(oldText, newText);

            foreach (var line in result.Lines)
            {
                if (line.Type == ChangeType.Inserted)
                {
                    sb.Append("+ ");
                }
                else if (line.Type == ChangeType.Deleted)
                {
                    sb.Append("- ");
                }
                else if (line.Type == ChangeType.Modified)
                {
                    sb.Append("* ");
                }
                else if (line.Type == ChangeType.Imaginary)
                {
                    sb.Append("? ");
                }
                //else if (line.Type == ChangeType.Unchanged)
                //{
                //    sb.Append("  ");
                //}

                sb.Append(line.Text + "<br/>");
            }
            return sb.ToString();

        }
    }
}
