
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Data;
using libExcel;

namespace TestLibrerie
{

    [TestClass]
    public class testLibExcel
    {
        public class provaIniezioneClasse
        {
            public int prova1 { get; set; }
            public string prova2 { get; set; }
            public string prova3 { get; set; }
            public DateTime TestDate { get; set; }
        }
        [TestMethod("TEST LIBRERIA EXCEL")]
        public void CreazioneLtturaFile()
        {
            //in primo luogo mi creo un ambiente pulito
            string p = @"C:\\temp\test";
            string fileName = @"OutputTest";
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            foreach (FileInfo file in new DirectoryInfo(p).GetFiles())
                if (file.Name.Contains(fileName))
                    file.Delete();
            //parto
            List<provaIniezioneClasse> tmList = new List<provaIniezioneClasse>();
            provaIniezioneClasse tm = new provaIniezioneClasse();
            tm.prova1 = 1;
            tm.prova2 = "prova2";
            tm.prova3 = "prova3 1 time";
            tm.TestDate = DateTime.Now.Date;
            tmList.Add(tm);
            provaIniezioneClasse tm2 = new provaIniezioneClasse();
            tm2.prova1 = 123;
            tm2.prova2 = "prova2";
            tm2.prova3 = "prova3 1 time";
            tm2.TestDate = DateTime.UtcNow;
            tmList.Add(tm2);

            provaIniezioneClasse tm3 = new provaIniezioneClasse();
            tm3.prova2 = "prova2";
            tmList.Add(tm3);

            WriteExcel wr = new WriteExcel();
            wr.WriteExcelFile(tmList, p, fileName);
            Assert.IsTrue(File.Exists(Path.Combine(p, fileName + ".xlsx")));//l'ho creato è già cosa buona e giusta

            ReadEcxel rd = new ReadEcxel();
            DataTable read = new DataTable();
            rd.ReadEcxelFile(out read, p, fileName);
            Assert.IsTrue(tmList.Count == read.Rows.Count);//
            int i = 0;
            foreach (DataRow rows in read.Rows)
            {
                provaIniezioneClasse tmp = tmList[i++];
                Assert.IsTrue(rows[0].ToString() == tmp.prova1.ToString());
                Assert.IsTrue((rows[1].ToString() == "" && string.IsNullOrEmpty(tmp.prova2)) || (rows[1].ToString() == tmp.prova2.ToString()));
                Assert.IsTrue((rows[2].ToString() == "" && string.IsNullOrEmpty(tmp.prova3)) || (rows[2].ToString() == tmp.prova3.ToString()));
                Assert.IsTrue((tmp.TestDate != DateTime.MinValue && rows[3].ToString() == tmp.TestDate.ToString()) 
                    || (tmp.TestDate == DateTime.MinValue && rows[3].ToString() == ""));
            }
            // adesso lo rileggo e confronto il contenuto

        }
    }
}
