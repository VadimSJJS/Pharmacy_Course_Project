using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;

namespace Pharmacy_Project.ViewModels
{
    public class MedicinesViewModel
    {
        public IEnumerable<Medicine> Medicines { get; set; }
     
        //Свойство для навигации по страницам
        public PageViewModel Page { get; set; }

        public DateTime? date { get; set; }
        public int Count { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
