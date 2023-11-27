using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;

namespace Pharmacy_Project.ViewModels
{
    public class OutgoingsViewModel
    {
        public IEnumerable<Outgoing> Outgoings { get; set; }

        //Свойство для навигации по страницам
        public PageViewModel Page { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

    }
}
