namespace Pharmacy_Project.ViewModels
{
    public enum SortState
    {
        No, // не сортировать
        CountAsc,
        CountDesc


    }
    public class SortViewModel
    {
        public SortState DateSort { get; set; } // значение для сортировки по топливу

        public SortState CurrentState { get; set; }     // текущее значение сортировки

        public SortState MedicineSort { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            // DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            MedicineSort = sortOrder == SortState.CountAsc ? SortState.CountDesc : SortState.CountAsc;

            CurrentState = sortOrder;
        }
    }
}
