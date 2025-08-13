using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Models.ViewModels
{
    public class RoomsSearchViewModel
    {
        // Query inputs
        public string? CheckInDate { get; set; }
        public string? CheckOutDate { get; set; }
        public string? ReservationTime { get; set; }
        public int GuestCount { get; set; } = 2;
        public string? Q { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Star { get; set; }
        public string? Sort { get; set; } // price_asc, price_desc, star_desc

        // Results
        public IEnumerable<Room> Rooms { get; set; } = Enumerable.Empty<Room>();

        // Helpers
        public int Nights
        {
            get
            {
                if (DateTime.TryParse(CheckInDate, out var ci) && DateTime.TryParse(CheckOutDate, out var co))
                {
                    var n = (co.Date - ci.Date).Days;
                    return n > 0 ? n : 1;
                }
                return 1;
            }
        }
    }
}
