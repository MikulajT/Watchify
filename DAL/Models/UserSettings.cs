namespace DAL.Models
{
    public class UserSettings
    {
        public IEnumerable<int> GenreIds { get; set; }
        public int ShowsCount { get; set; }
    }
}
