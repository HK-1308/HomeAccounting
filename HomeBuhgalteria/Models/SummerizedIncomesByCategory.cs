namespace WinFormsApp1.Models
{
    public class SummerizedIncomesByCategory
    {
        public int IncomeCategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public decimal IncomeSum { get; set; }
        
        public decimal IncomePersent { get; set; }

        public SummerizedIncomesByCategory()
        {
            
        }

        public SummerizedIncomesByCategory(int IncomeCategoryId,string CategoryName,decimal IncomeSum)
        {
            this.IncomeCategoryId = IncomeCategoryId;
            this.CategoryName = CategoryName;
            this.IncomeSum = IncomeSum;
        }
    }
}