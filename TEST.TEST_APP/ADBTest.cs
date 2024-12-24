using ADBSevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.TEST_APP
{
    [TestClass]
    public class ADBTest
    {
        [TestMethod]
        public async void TestInstallFastBoot()
        {
            ADBService adbService = new ADBService();
            bool result = await adbService.FlashBootImageAsync("D:\\code\\rom\\boot.img", "96NAY0YGW1");

            Console.WriteLine(result);
        }
    }
}
    
   
        

    
