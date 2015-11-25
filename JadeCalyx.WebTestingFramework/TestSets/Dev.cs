﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using jcWebGuiTools;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using System.Configuration;
using TestSets.Utilities;

namespace TestSets
{
    /// <summary>
    /// The Dev class is used for the development of tests. It is a type of sandbox.
    /// 
    /// This uses NUnit as the test runner. It should be installed as a NuGet package
    /// (look in packages.config, you should see both the NUnit and Nunit3TestAdapter
    /// packages). To run, build the solution, then click menu item
    /// Test-->Windows--Test Explorer. The test explorer pane should open and have a list
    /// of tests you can run. Right click the HelloWorldTest and run it as
    /// an example of how to run a test and view its output.
    /// </summary>
    [TestFixture]
    public class Dev
    {
        jcBrowserFactory _browserFactory;
        AppFile _appFile;
        jcBrowser _br;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _appFile = new AppFile();
            _browserFactory = new jcBrowserFactory("Wikipedia", _appFile.WebPrefix);
        }
        [OneTimeTearDown]
        public void ClassTeardown()
        {

        }
        [SetUp]
        public void TestSetup()
        {

        }
        [TearDown]
        public void TestTeardown()
        {
            try
            {
                Console.WriteLine("Closing Browserr");
                _br.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error closing browser");
            }
            
        }

        [Test]
        public void HelloWorldTest()
        {
            Console.WriteLine("hello world");
            Assert.That(true, "test should have passed");
        }

        [Test]
        public void OpenBrowser()
        {
            var p = ConfigurationManager.AppSettings["WebPrefix"];
            _br = _browserFactory.GetBrowser("firefox");
            _br.GotoPage("main-page");
            _br.GetPage().SetText("search-box", "archery");
            _br.GetPage().Click("search-button");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.That(_br.GetPage().IsCurrentHandle("archery-page"), "not archery");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            _br.Close();

        }

    }
}
