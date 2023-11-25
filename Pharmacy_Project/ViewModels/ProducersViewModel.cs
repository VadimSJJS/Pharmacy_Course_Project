using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;

namespace Pharmacy_Project.ViewModels
{
    public class ProducersViewModel
    {
        public IEnumerable<Producer> Producers { get; set; }

        //Свойство для навигации по страницам
        public PageViewModel Page { get; set; }
    }
}
