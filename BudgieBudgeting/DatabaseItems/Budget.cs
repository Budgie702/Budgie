namespace BudgieBudgeting.DatabaseItems
{
    public class Budget
    {
        int BudgetId;
        float NeedsBudget;
        float WantsBudget;
        float SavingsBudget;
        List<Needs> needs;
        List<Wants> wants;
        List<Savings> savings;
        public Budget()
        {

        }
        public void initialiseList()
        {

        }
    }
}
