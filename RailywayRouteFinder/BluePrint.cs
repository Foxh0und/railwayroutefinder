using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    public class BluePrint
    {
        private List<String> fSpecifications;
        private Dictionary<String,QuestionType> fQuestions; 

        public BluePrint( string aFileName )
        {
            String[] lInput = System.IO.File.ReadAllLines( aFileName );
            fQuestions = new Dictionary<string, QuestionType>();

            foreach(String line in lInput)
            {
                if ( line.StartsWith( "Graph: " ) )
                    stationParser( line );
                else if ( Char.IsDigit( line[0] ) )
                    questionParser( line );
            }
        }

        public List<String> Specifications
        {
            get
            {
                return fSpecifications;
            }
        }

        public Dictionary<String, QuestionType> Questions
        {
            get
            {
                return fQuestions;
            }
        }


        private void stationParser( String aSpecification )
        {
            fSpecifications = aSpecification.Replace( "Graph:", "" ).Replace( " ", "" ).Split( ',' ).Distinct().ToList();
        }

        private void questionParser( String aQuestion )
        {
            String lQuestion = new Regex( @"\.(.*?)\." ).Match( aQuestion ).ToString(); // get first sentence
            lQuestion = lQuestion.Replace( ".", "" );
            lQuestion = lQuestion.Trim();

            if ( lQuestion.Contains( "The distance of the route" ) )
            {
                lQuestion = lQuestion.Replace( "The distance of the route ", "" );
                lQuestion = lQuestion.Replace( "-", "");
                fQuestions.Add( lQuestion, QuestionType.DirectPath );
            }
            else if( lQuestion.Contains("The number of trips starting at ") )
            {
                if ( lQuestion.Contains( "maximum" ) )
                {
                    String lStations = "";
                    foreach ( Match station in new Regex( @"\W([A-Z])(\W|$)" ).Matches( lQuestion ) )
                        lStations += station;

                    lStations = lStations.Replace( " ", "" );        
                    String lNumber = Regex.Replace( lQuestion, @"[^\d]", "" );
                    
                    fQuestions.Add( lStations + lNumber, QuestionType.MaximumStops );
                }
                else if( lQuestion.Contains( "exactly" ) )
                {
                    String lStations = "";
                    foreach ( Match station in new Regex( @"\W([A-Z])(\W|$)" ).Matches( lQuestion ) )
                        lStations += station;

                    lStations = lStations.Replace( " ", "" );
                    String lNumber = Regex.Replace( lQuestion, @"[^\d]", "" );

                    fQuestions.Add( lStations + lNumber, QuestionType.ExactStops );
                }

            }
            else if( lQuestion.Contains( "The length of the shortest route" ) )
            {
                String lStations = "";
                foreach ( Match station in new Regex( @"\W([A-Z])(\W|$)" ).Matches( lQuestion ) )
                    lStations += station;

                lStations = lStations.Replace( " ", "" );
                String lNumber = Regex.Replace( lQuestion, @"[^\d]", "" );

                fQuestions.Add( lStations + lNumber, QuestionType.ShortestPath );
            }

            else if( lQuestion.Contains( "The number of different routes" ) )
            {
                    String lStations = "";
                    foreach ( Match station in new Regex( @"\W([A-Z])(\W|$)" ).Matches( lQuestion ) )
                        lStations += station;

                    lStations = lStations.Replace( " ", "" );        
                    String lNumber = Regex.Replace( lQuestion, @"[^\d]", "" );
                    
                    fQuestions.Add( lStations + lNumber, QuestionType.MaximumDistance );
            }
        }
    }
}