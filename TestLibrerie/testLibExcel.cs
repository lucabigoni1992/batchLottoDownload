using libExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static libExcel.Variables;

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
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime TestDate { get; set; }
        }
        [TestMethod("TEST LIBRERIA EXCEL")]
        public void TestMethod1()
        {

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

            Variables p = new Variables();
            p.CreateExcelFile(tmList, "C:\\temp");
            //Variables E = new Variables();
            //List<Account> bankAccounts = new List<Account>
            //    {
            //        new Account
            //        {
            //            ID = 345,
            //            Balance = 541.27
            //        },
            //        new Account
            //        {
            //            ID = 123,
            //            Balance = -127.44
            //        }
            //    };
            //E.DisplayInExcel(bankAccounts, (account, cell) =>
            //                                                 {
            //                                                     cell.Value = account.ID;
            //                                                     cell.Offset[0, 1].Value = account.Balance;
            //                                                     if (account.Balance < 0)
            //                                                     {
            //                                                         cell.Interior.Color = 255;
            //                                                         cell.Offset[0, 1].Interior.Color = 255;
            //                                                     }
            //                                                 });
        }
    }
}
