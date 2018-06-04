using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WasteBatchConsole;
using WasterDAL.Model;

namespace WasteBatchTests
{
    [TestClass]
    public class StatsCalculatorTest
    {
        [TestMethod]
        public void DayStats_ProvidedTodaysRecordsWithTotal100Secs_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,1,0,0),
                EndDate = new DateTime(2014,8,19,1,0,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,2,0,40),
                EndDate = new DateTime(2014,8,19,2,1,40)
          
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 19), new DateTime(2014,8,20));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void DayStats_ProvidedTodaysRecordsWithTotal100SecsForTodayAndFirstStartsPreviousDay_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,18,23,54,00),
                EndDate = new DateTime(2014,8,19,0,0,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,2,0,40),
                EndDate = new DateTime(2014,8,19,2,1,40)
          
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 19), new DateTime(2014, 8, 20));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void DayStats_ProvidedTodaysRecordsWithTotal100SecsForTodayAndLastEndsNextDay_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,18,23,54,00),
                EndDate = new DateTime(2014,8,19,0,0,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,23,59,0),
                EndDate = new DateTime(2014,8,20,2,1,40)
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 19), new DateTime(2014, 8, 20));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WeekStats_ProvidedTodaysRecordsWithTotal100Secs_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,1,0,0),
                EndDate = new DateTime(2014,8,19,1,0,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,2,0,40),
                EndDate = new DateTime(2014,8,19,2,1,40)
          
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 18), new DateTime(2014, 8, 25));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WeekStats_ProvidedRecordsDifferentDaysWithTotal100Secs_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,18,1,0,0),
                EndDate = new DateTime(2014,8,18,1,0,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,2,0,40),
                EndDate = new DateTime(2014,8,19,2,1,40)
          
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 18), new DateTime(2014, 8, 25));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WeekStats_ProvidedRecordsEachWeekDayWithTotal400Secs_Returns400SecsAsField()
        {
            //arrange
            var input = new[] {
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,18,1,0,0),
                    EndDate = new DateTime(2014,8,18,1,0,40)
          
                },
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,19,2,0,40),
                    EndDate = new DateTime(2014,8,19,2,1,40)
          
                },
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,20,2,0,40),
                    EndDate = new DateTime(2014,8,20,2,1,40)
          
                },
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,21,2,0,40),
                    EndDate = new DateTime(2014,8,21,2,1,40)
          
                },
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,22,2,0,40),
                    EndDate = new DateTime(2014,8,22,2,1,40)
          
                },
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,23,2,0,40),
                    EndDate = new DateTime(2014,8,23,2,1,40)
          
                },
                new TrackRecord
                {
                    StartDate = new DateTime(2014,8,24,2,0,40),
                    EndDate = new DateTime(2014,8,24,2,1,40)
          
                }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 18), new DateTime(2014, 8, 25));
            //assert
            Assert.AreEqual(400, result);
        }

        [TestMethod]
        public void WeekStats_ProvidedRecordsDifferentDaysFirstStartsPreviousWeekWithTotalForWeek100Secs_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,17,23,0,0),
                EndDate = new DateTime(2014,8,18,0,0,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,19,2,0,40),
                EndDate = new DateTime(2014,8,19,2,1,40)
          
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 18), new DateTime(2014, 8, 25));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WeekStats_ProvidedRecordsDifferentDaysLastEndNextWeekWithTotalForWeek100Secs_Returns100SecsAsField()
        {
            //arrange
            var input = new[] {new TrackRecord
            {
                StartDate = new DateTime(2014,8,18,0,1,0),
                EndDate = new DateTime(2014,8,18,0,1,40)
          
            },
            new TrackRecord
            {
                StartDate = new DateTime(2014,8,24,23,59,00),
                EndDate = new DateTime(2014,8,25,2,1,40)
          
            }};

            //act

            var result = StatsCalculator.CalculateStats(input, new DateTime(2014, 8, 18), new DateTime(2014, 8, 25));
            //assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void MonthStats_ProvidedRecordsEachMonthDayWithTotal1860Secs_Returns1860SecsAsField()
        {
            var inputList = new List<TrackRecord>();

            for(int i = 0; i < 31;i++)
            {
                inputList.Add(new TrackRecord
                    {
                        StartDate = new DateTime(2014, 8, i+1, 2, 0, 40),
                        EndDate = new DateTime(2014, 8, i+1, 2, 1, 40)
                    });
            }
            //act

            var result = StatsCalculator.CalculateStats(inputList, new DateTime(2014, 8, 1), new DateTime(2014, 9, 1));
            //assert
            Assert.AreEqual(1860, result);
        }


        [TestMethod]
        public void MonthStats_ProvidedRecordsEachMonthDayWithAndFirstThatStartsPreviousMonthTotal1900Secs_Returns1900SecsAsField()
        {
            var inputList = new List<TrackRecord>();

            inputList.Add(new TrackRecord
                          {
                                StartDate = new DateTime(2014, 7, 31, 2, 0, 40),
                    EndDate = new DateTime(2014, 8, 1, 0, 0, 40) 
                          });

            for (int i = 0; i < 31; i++)
            {
                inputList.Add(new TrackRecord
                {
                    StartDate = new DateTime(2014, 8, i + 1, 2, 0, 40),
                    EndDate = new DateTime(2014, 8, i + 1, 2, 1, 40)
                });
            }
            //act

            var result = StatsCalculator.CalculateStats(inputList, new DateTime(2014, 8, 1), new DateTime(2014, 9, 1));
            //assert
            Assert.AreEqual(1900, result);
        }

        [TestMethod]
        public void MonthStats_ProvidedRecordsEachMonthDayWithAndLastThatEndsNextMonthTotal1920Secs_Returns1920SecsAsField()
        {
            var inputList = new List<TrackRecord>();


            for (int i = 0; i < 31; i++)
            {
                inputList.Add(new TrackRecord
                {
                    StartDate = new DateTime(2014, 8, i + 1, 2, 0, 40),
                    EndDate = new DateTime(2014, 8, i + 1, 2, 1, 40)
                });
            }

            inputList.Add(new TrackRecord
            {
                StartDate = new DateTime(2014, 8, 31, 23, 59, 0),
                EndDate = new DateTime(2014, 9, 1, 0, 0, 40)
            });
            //act

            var result = StatsCalculator.CalculateStats(inputList, new DateTime(2014, 8, 1), new DateTime(2014, 9, 1));
            //assert
            Assert.AreEqual(1920, result);
        }
    }
}
