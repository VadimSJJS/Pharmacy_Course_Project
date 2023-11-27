using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;

namespace Pharmacy_Project.ViewModels
{
    public class DiseasesViewModel
    {
        public IEnumerable<Disease> Diseases{ get; set; }

        //Свойство для навигации по страницам
        public PageViewModel Page { get; set; }

        public string DiseaseName { get; set; }
    }
}
