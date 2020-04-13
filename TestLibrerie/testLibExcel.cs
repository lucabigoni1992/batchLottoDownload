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
        public  class provaIniezioneClasse
        {
       //     public  int prova1 { get; set; }
            [StringLength(100)]
            public  string prova2 { get; set; }
            [StringLength(100)]
            public  string prova3 { get; set; }
        //    public  DateTime TestDate { get; set; }
        }
        [TestMethod("TEST LIBRERIA EXCEL")]
        public void TestMethod1()
        {

            List<provaIniezioneClasse> tmList = new List<provaIniezioneClasse>();

            provaIniezioneClasse tm = new provaIniezioneClasse();
      //      tm.prova1 = 1;
            tm.prova2 = "prova2";
            tm.prova3 = "prova3 1 time";
      //      tm.TestDate = DateTime.Now.Date;
            tmList.Add(tm);

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
