using System.Data;
using System.Reflection.Metadata.Ecma335;

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
        public Budget(DataSet dataset, int placement)
        {
            for (int i = 0; i < dataset.Tables[3].Rows.Count; i++)
            {
                /*if (Convert.ToInt64(dataset.Tables[].Rows[]) == Convert.ToInt64())
                {

                }*/
            }
            for (int i = 0; i < dataset.Tables[4].Rows.Count; i++)
            {

            }
            for(int i = 0; i < dataset.Tables[5].Rows.Count; i++)
            {

            }
        }
    }
}
