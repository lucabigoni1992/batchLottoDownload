using libExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static libExcel.Variables;

namespace TestLibrerie
{

    [TestClass]
    public class testLibExcel
    {
        [TestMethod("TEST LIBRERIA EXCEL")]
        public void TestMethod1()
        {

            TestModelList tmList = new TestModelList();
            tmList.testData = new List<TestModel>();
            TestModel tm = new TestModel();
            tm.TestId = 1;
            tm.TestName = "Test1";
            tm.TestDesc = "Tested 1 time";
            tm.TestDate = DateTime.Now.Date;
            tmList.testData.Add(tm);

            TestModel tm1 = new TestModel();
            tm1.TestId = 2;
            tm1.TestName = "Test2";
            tm1.TestDesc = "Tested 2 times";
            tm1.TestDate = DateTime.Now.AddDays(-1);
            tmList.testData.Add(tm1);

            TestModel tm2 = new TestModel();
            tm2.TestId = 3;
            tm2.TestName = "Test3";
            tm2.TestDesc = "Tested 3 times";
            tm2.TestDate = DateTime.Now.AddDays(-2);
            tmList.testData.Add(tm2);

            TestModel tm3 = new TestModel();
            tm3.TestId = 4;
            tm3.TestName = "Test4";
            tm3.TestDesc = "Tested 4 times";
            tm3.TestDate = DateTime.Now.AddDays(-3);
            tmList.testData.Add(tm);
            
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
