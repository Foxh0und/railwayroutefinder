using System;
using RailywayRouteFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RailwayRouteUnitTest
{
    [TestClass]
    public class TrackTests
    {
        [TestMethod]
        public void Destination()
        {
            Track lTestTrack = new Track('A', 5);

            Assert.AreEqual( 'A', lTestTrack.Destination );
            Assert.AreNotEqual( 'B', lTestTrack.Destination );
        }

        [TestMethod]
        public void Distance()
        {
            Track lTestTrack = new Track( 'A', 5 );

            Assert.AreEqual( (uint)5, lTestTrack.Distance );
            Assert.AreNotEqual( 3, lTestTrack.Distance );
        }
    }
}
