using BLL.Models.BLLModels;
using Common;

namespace Watchify.ViewModels
{
    public class ShowModel
    {
        public IEnumerable<BGenre> Genres { get; set; }
        public BUserSettings UserSettings { get; set; }
        public ShowType ShowType { get; set; }
    }
}
