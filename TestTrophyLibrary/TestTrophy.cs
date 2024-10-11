using Trophy_library;

namespace TestTrophyLibrary
{
    [TestClass]
    public class TestTrophy
    {
        private readonly Trophy _trophy = new() { Id = 1, Competition = "World Cup", Year = 2022 };
        private readonly Trophy _trophy2 = new() { Competition = "DK Cup", Year = 2019 };
        private readonly Trophy _lowyear = new() { Id = 1, Competition = "World Cup", Year = 1970 };
        private readonly Trophy _highyear = new() { Id = 1, Competition = "World Cup", Year = 2024 };
        private readonly Trophy _shortcomp = new() { Id = 1, Competition = "WC", Year = 2022 };
        private readonly Trophy _nullcomp = new() { Id = 1, Competition = null, Year = 2023 };
        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("{Id=1, Competition=World Cup, Year=2022}", _trophy.ToString());
        }

        [TestMethod]
        public void TestValidateYear()
        {
            Assert.ThrowsException<Exception>(() => _lowyear.ValidateYear());
            Assert.ThrowsException<Exception>(() => _highyear.ValidateYear());
        }
        [TestMethod]
        public void TestValidateCompetition()
        {
            Assert.ThrowsException<Exception>(() => _nullcomp.ValidateCompetition());
            Assert.ThrowsException<Exception>(() => _shortcomp.ValidateCompetition());
        }
        [TestMethod]
        public void TestValidate()
        {
            _trophy.Validate();
            _trophy2.Validate();
            Assert.ThrowsException<Exception>(() => _lowyear.Validate());
            Assert.ThrowsException<Exception>(() => _highyear.Validate());
            Assert.ThrowsException<Exception>(() => _shortcomp.Validate());
        }
    }
}