using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count-1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25))
            }; 

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25)),
                new WeekEnd(new DateTime(2017, 4, 29), new DateTime(2017, 4, 29))
            };
            
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }
        [TestMethod]
        public void Test_WhenNegativeDayCount_ThenThrowArgumentExceptio()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = -1;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25))
            };

            try
            {
                new WorkDayCalculator().Calculate(startDate, count, weekends);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "dayCount can not be negative");
                return;
            }
        }
        [TestMethod]
        public void Test_StartWeekendBeforeStartDateAndEndWeekendAfterStartDate()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2017, 4, 15), new DateTime(2017, 4, 16)),
                new WeekEnd(new DateTime(2017, 4, 19), new DateTime(2017, 4, 22))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual( new DateTime(2017, 4, 27),result);
        }
        [TestMethod]
        public void Test_StartWeekendIsEqualToStartDate()
        {
            DateTime startDate = new DateTime(2017, 4, 10);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2017, 4, 10), new DateTime(2017, 4, 15))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2017, 4, 20), result);
        }
        [TestMethod]
        public void Test_EndWeekendIsEqualToStartDate()
        {
            DateTime startDate = new DateTime(2017, 4, 10);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2017, 4, 3), new DateTime(2017, 4, 10))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2017, 4, 16), result);
        }
        [TestMethod]
        public void Test_StartDateBetweenStartWeekendAndEndWeekend()
        {
            DateTime startDate = new DateTime(2017, 4, 10);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2017, 4, 3), new DateTime(2017, 4, 15))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2017, 4, 20), result);
        }
    }
}
