using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;
using System.Data;

namespace libExcel
{
    public class ReadEcxel
    {

        public void ReadEcxelFile(out DataTable ris, string FileDirectory, string fileName)
        {
            ris = new DataTable();
            if (!Directory.Exists(FileDirectory))
                throw new Exception("Non esiste il esiste il Path del file");
            using (FileStream fs = new FileStream(Path.Combine(FileDirectory, fileName + ".xlsx"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
            {
                WorkbookPart wbPart = doc.WorkbookPart;
                //statement to get the sheet object  
                Sheet mysheet = (Sheet)wbPart.Workbook.Sheets.ChildElements.GetItem(0);
                //statement to get the worksheet object by using the sheet id  
                Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                IEnumerable<Cell> cells = Worksheet.GetFirstChild<SheetData>().Descendants<Cell>();
                IEnumerable<Row> rows = Worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                bool isHeader = true;
                foreach (Row row in rows)
                {
                    if (isHeader)//salto la prima ci sono gli header
                    {
                        isHeader = false;
                        foreach (Cell c in row.Elements<Cell>())
                            ris.Columns.Add(c.CellValue.InnerText);
                        continue;
                    }
                    int j = 0;
                    DataRow addRow = ris.NewRow();
                    foreach (Cell c in row.Elements<Cell>())
                    {
                        string str = row.ChildElements[j].InnerText;
                        string clm = rows.ElementAt<Row>(0).ChildElements[j].InnerText;
                        addRow[clm] =String.IsNullOrEmpty( str)?"":str;
                        j++;
                    }
                    ris.Rows.Add(addRow);
                }
            }

        }
    }
}
