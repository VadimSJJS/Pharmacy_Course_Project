using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;

namespace Pharmacy_Project.ViewModels
{
    public class IncomingsViewModel
    {
        public IEnumerable<Incoming> Incomings { get; set; }

        //Свойство для навигации по страницам
        public PageViewModel Page { get; set; }

    }
}
