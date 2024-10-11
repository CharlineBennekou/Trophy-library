namespace Trophy_library
{
    public class Trophy
    {

        public int Id { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }


        public Trophy() //empty constructor
        {
        }

        public Trophy(string competition, int year) //constructor
        {
            Competition = competition;
            Year = year;
        }


        public override string ToString() //overriding ToString method
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Competition)}={Competition}, {nameof(Year)}={Year.ToString()}}}";
        }

        public void ValidateYear() //Validates if year is null, less than 1970 or greater than 2024
        { 

            if (Year <= 1970)
            {
                throw new Exception("Year must be greater than 1970");
            }
            else if (Year >=2024)
            {
                throw new Exception("Year must be less than 2024");
            }
        }

        public void ValidateCompetition() //Validates if competition name is less than 3 characters long
        {
            if (string.IsNullOrEmpty(Competition)) {
                throw new Exception("Competition name must not be null");
            }

            if (Competition.Length < 3)
            {
                throw new Exception("Competition name must be at least 3 characters long");
            }
        }
        public void Validate() //Validates year and competition name
        {
            ValidateYear();
            ValidateCompetition();
        }
    }
}
