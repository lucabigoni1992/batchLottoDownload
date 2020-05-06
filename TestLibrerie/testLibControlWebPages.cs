
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Data;
using lbControlWebPages;

namespace TestLibrerie
{

    [TestClass]
    public class testLibControlWebPages
    {
        [TestMethod("TEST LIBRERIA CONTROL WEB PAGES-addRecord")]
        public void addRecord()
        {
            InteractiveDB.addUpdateSiteAsync(@"https://acpol2.army.mil/vacancy/vacancy_list.asp", 12);



        }
    }
}
