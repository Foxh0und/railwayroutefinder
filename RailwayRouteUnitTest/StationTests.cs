using System;
using RailywayRouteFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RailwayRouteUnitTest
{
    [TestClass]
    public class StationTester
    {
        [TestMethod]
        public void getTrackExistence()
        {
            Station lTestStation = new Station( 'A' );
            lTestStation.addNewTrack( 'B', 5 );

            Assert.IsTrue( lTestStation.getTrackExistence( 'B' ) );
            Assert.IsFalse( lTestStation.getTrackExistence( 'C' ) );
        }

        [TestMethod]
        public void addNewTrack()
        {
            Station lTestStation = new Station( 'A' );
            Assert.IsFalse( lTestStation.getTrackExistence( 'B' ) ); //test that track doesn't exist.

            lTestStation.addNewTrack( 'B', 5 ); // add the track
            Assert.IsTrue( lTestStation.getTrackExistence( 'B' ) ); //track should now exist.
        }

        [TestMethod]
        public void Label()
        {
            Station lTestStation = new Station( 'A' );

            Assert.AreEqual( 'A', lTestStation.Label );
            Assert.AreNotEqual( 'B', lTestStation.Label );
        }

        [TestMethod]
        public void getTrack()
        {
            Station lTestStation = new Station( 'A' );
            lTestStation.addNewTrack( 'B', 5 );

            Track lTestTrack = lTestStation.getTrack( 'B' );

            Assert.AreEqual( 'B', lTestTrack.Destination );
            Assert.AreEqual( (uint)5 , lTestTrack.Distance );
        }

        [TestMethod]
        [ExpectedException( typeof( Exception ))]
        public void getTrackException()
        {
            Station lTestStation = new Station( 'A' );
            Track lTestTrack = lTestStation.getTrack( 'B' );

        }
    }
}
