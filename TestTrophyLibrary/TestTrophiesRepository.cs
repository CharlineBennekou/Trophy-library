using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trophy_library;

namespace TestTrophyLibrary
{
    [TestClass]
    public class TestTrophiesRepository
    {
        private readonly TrophiesRepository _repo = new();

        private readonly Trophy _trophy1 = new() {Competition = "World Cup", Year = 2022 };
        private readonly Trophy _trophy2 = new() {Competition = "DK Cup", Year = 2019 };
        private readonly Trophy _trophy3 = new() { Competition = "Euro Cup", Year = 2018 };

        [TestInitialize]
        public void Initialize()
        {
            _repo.Add(_trophy1);
            _repo.Add(_trophy2);
            _repo.Add(_trophy3);
        }

        // Test Add method
        [TestMethod]
        public void AddTrophyMethod()
        {
            _repo.Add(new Trophy() { Competition = "School cup", Year = 1999 });
            Assert.AreEqual(4, _repo.Get().Count());

        }

        // Section testing Get method
        [TestMethod]
        public void GetMethodsWithoutParameters()
        {
            var result = _repo.Get();
            Assert.AreEqual(3, result.Count()); // Returns all trophies
        }

        [TestMethod]
        public void TestFilteringByYearAndCompetition()
        {
            // Test filtering by year
            var filteredByYear = _repo.Get(year: 2020);
            Assert.AreEqual(1, filteredByYear.Count()); //Only one trophy in 2020
            Assert.AreEqual("World Cup", filteredByYear.First().Competition);

            // Test filtering by competition
            var filteredByCompetition = _repo.Get(competitionincludes: "Cup");
            Assert.AreEqual(3, filteredByCompetition.Count()); // All trophies contain "Cup"

            // Test combined filtering by year and competition
            var combinedFilter = _repo.Get(year: 2019, competitionincludes: "Cup");
            Assert.AreEqual(2, combinedFilter.Count()); // DK Cup and World Cup
        }

        [TestMethod]
        public void TestSortingByYearAndCompetition()
        {
            // Test sorting by year ascending
            var sortedByYearAsc = _repo.Get(orderBy: "year");
            var yearAscList = sortedByYearAsc.ToList();
            Assert.AreEqual(2018, yearAscList[0].Year); // Euro Cup
            Assert.AreEqual(2019, yearAscList[1].Year); // DK Cup
            Assert.AreEqual(2022, yearAscList[2].Year); // World Cup

            // Test sorting by year descending
            var sortedByYearDesc = _repo.Get(orderBy: "year desc");
            var yearDescList = sortedByYearDesc.ToList();
            Assert.AreEqual(2022, yearDescList[0].Year); // World Cup
            Assert.AreEqual(2019, yearDescList[1].Year); // DK Cup
            Assert.AreEqual(2018, yearDescList[2].Year); // Euro Cup

            // Test sorting by competition ascending
            var sortedByCompetitionAsc = _repo.Get(orderBy: "competition asc");
            var competitionAscList = sortedByCompetitionAsc.ToList();
            Assert.AreEqual("DK Cup", competitionAscList[0].Competition);
            Assert.AreEqual("Euro Cup", competitionAscList[1].Competition);
            Assert.AreEqual("World Cup", competitionAscList[2].Competition);

            // Test sorting by competition descending
            var sortedByCompetitionDesc = _repo.Get(orderBy: "competition desc");
            var competitionDescList = sortedByCompetitionDesc.ToList();
            Assert.AreEqual("World Cup", competitionDescList[0].Competition);
            Assert.AreEqual("Euro Cup", competitionDescList[1].Competition);
            Assert.AreEqual("DK Cup", competitionDescList[2].Competition);
        }

        [TestMethod]
        public void TestInvalidOrderBy()
        {
            Assert.ThrowsException<ArgumentException>(() => _repo.Get(orderBy: "invalid")); // Throws argument exception for invalid orderBy parameter
        }

        [TestMethod]
        public void TestRemove_ValidAndInvalidIds()
        {
            // Test removing a valid trophy by ID
            // The remove method returns null if it doesn't find the trophy and returns the removed trophy if it removes a trophy
            var removedTrophy = _repo.Remove(_trophy1.Id);
            Assert.IsNotNull(removedTrophy); // We assert that the trophy is removed since the method returns a trophy

            var remainingTrophies = _repo.Get();
            Assert.AreEqual(2, remainingTrophies.Count()); // Should be 2 trophies left
            Assert.IsFalse(remainingTrophies.Any(t => t.Id == _trophy1.Id)); // Trophy1 should no longer exist

            // Test trying to remove an invalid/non-existent ID
            var invalidRemove = _repo.Remove(999); // ID 999 doesn't exist
            Assert.IsNull(invalidRemove); // Should return null since it didn't find a trophy to remove

            // Verify that the list of trophies is unchanged after invalid removal
            Assert.AreEqual(2, _repo.Get().Count()); // Should still be 2 trophies left
        }


    }
}
