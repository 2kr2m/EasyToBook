using EasyToBook.Domain.Entities;

namespace EasyToBook.WebApp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Villa>? villaList { get; set; }
        public DateOnly checkInDate { get; set; }
        public DateOnly checkOutDate { get; set; }
        public int nmbrOfNights { get; set; }

    }
}
