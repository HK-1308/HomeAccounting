namespace WinFormsApp1.Models
{
    public class SummerizedExpensesByCategory
    {
        public int ExpenceCategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public decimal ExpenceSum { get; set; }
        
        public decimal ExpencePersent { get; set; }

        public SummerizedExpensesByCategory()
        {
            
        }

        public SummerizedExpensesByCategory(int ExpenceCategoryId,string CategoryName,decimal ExpenceSum)
        {
            this.ExpenceCategoryId = ExpenceCategoryId;
            this.CategoryName = CategoryName;
            this.ExpenceSum = ExpenceSum;
        }
    }
}