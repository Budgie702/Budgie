using System.Data;

namespace BudgieBudgeting.DatabaseItems
{
    public class Customer
    {
        int CustomerId;
        string Name;
        string Email;
        string Password;
        float Income;
        Budget budget;
        public Customer(DataSet dataset,int placement)
        {
            for (int i = 0; dataset.Tables[1].Rows.Count > 0; i++)
            {
                if (Convert.ToInt64(dataset.Tables[0].Rows[0][placement]) == Convert.ToInt64(dataset.Tables[0].Rows[i][1]))
                {
                    this.budget = new Budget(dataset,i);
                    break;
                }
            }
        }
    }
}
