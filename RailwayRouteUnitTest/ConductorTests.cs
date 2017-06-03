using System;
using RailywayRouteFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RailwayRouteUnitTest
{
    [TestClass]
    public class ConductorTests
    {
        [TestMethod]
        public void QuestionTester()
        {
            BluePrint lTestBluePrint = new BluePrint( "testinput.txt" );
            Conductor lTestConductor = new Conductor( lTestBluePrint );
        }
    }
}
