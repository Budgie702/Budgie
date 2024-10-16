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
            //adding needs
            for (int i = 0; i < dataset.Tables[3].Rows.Count; i++)
            {
                if (Convert.ToInt64(dataset.Tables[1].Rows[placement][0]) == Convert.ToInt64(dataset.Tables[2].Rows[i][1]))
                {
                    needs.Add(new Needs(Convert.ToInt32(dataset.Tables[2].Rows[i][0]), (float)Convert.ToDouble(dataset.Tables[2].Rows[i][2])));
                }
            }
            //adding wants
            for (int i = 0; i < dataset.Tables[4].Rows.Count; i++)
            {
                if (Convert.ToInt64(dataset.Tables[1].Rows[placement][0]) == Convert.ToInt64(dataset.Tables[3].Rows[i][1]))
                {
                    wants.Add(new Wants(Convert.ToInt32(dataset.Tables[3].Rows[i][0]), (float)Convert.ToDouble(dataset.Tables[3].Rows[i][2])));
                }
            }
            //adding savings
            for(int i = 0; i < dataset.Tables[5].Rows.Count; i++)
            {
                if (Convert.ToInt64(dataset.Tables[1].Rows[placement][0]) == Convert.ToInt64(dataset.Tables[4].Rows[i][1]))
                {
                    savings.Add(new Savings(Convert.ToInt32(dataset.Tables[4].Rows[i][0]), (float)Convert.ToDouble(dataset.Tables[4].Rows[i][2])));
                }
            }
        }
    }
}
