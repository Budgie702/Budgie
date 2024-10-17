using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BudgieBudgeting.DatabaseItems
{
    public class Budget
    {
        private int BudgetId;
        private float NeedsBudget;
        private float WantsBudget;
        private float SavingsBudget;
        private List<Needs> needs;
        private List<Wants> wants;
        private List<Savings> savings;
        public Budget(DataSet dataset, int BudgetPlacement)
        {
            SetBudgetId(Convert.ToInt32(dataset.Tables[1].Rows[BudgetPlacement][0]));
            SetNeedsBudget((float)Convert.ToDecimal(dataset.Tables[1].Rows[BudgetPlacement][2]));
            SetWantsBudget((float)Convert.ToDecimal(dataset.Tables[1].Rows[BudgetPlacement][3]));
            SetSavingsBudget((float)Convert.ToDecimal(dataset.Tables[1].Rows[BudgetPlacement][4]));
            InitializeOtherTables(dataset, BudgetPlacement);
        }
        //setters
        private void SetBudgetId(int BudgetId)
        {
            this.BudgetId = BudgetId;
        }
        public void SetNeedsBudget(float NeedsBudget)
        {
            this.NeedsBudget = NeedsBudget;
        }
        public void SetWantsBudget(float WantsBudget)
        {
            this.WantsBudget = WantsBudget;
        }
        public void SetSavingsBudget(float SavingsBudget)
        {
            this.SavingsBudget = SavingsBudget;
        }
        //Getters
        public int GetBudgetId() { 
            return this.BudgetId;
        }
        public float GetNeedsBudget()
        {
            return this.NeedsBudget;
        }
        public float GetWantsBudget()
        {
            return this.WantsBudget;
        }
        public float GetSavingsBudget()
        {
            return this.SavingsBudget;
        }
        public List<Needs> GetNeeds()
        {
            return needs;
        }
        public List<Wants> GetWants()
        {
            return wants;
        }
        public List<Savings> GetSavings()
        {
            return savings;
        }
        //creating other tables
        private void InitializeOtherTables(DataSet dataset, int BudgetPlacement)
        {
            //adding needs
            for (int i = 0; i < dataset.Tables[2].Rows.Count; i++)
            {
                if (Convert.ToInt64(dataset.Tables[1].Rows[BudgetPlacement][0]) == Convert.ToInt64(dataset.Tables[2].Rows[i][1]))
                {
                    needs.Add(new Needs(Convert.ToInt32(dataset.Tables[2].Rows[i][0]), (float)Convert.ToDouble(dataset.Tables[2].Rows[i][2])));
                }
            }
            //adding wants
            for (int i = 0; i < dataset.Tables[3].Rows.Count; i++)
            {
                if (Convert.ToInt64(dataset.Tables[1].Rows[BudgetPlacement][0]) == Convert.ToInt64(dataset.Tables[3].Rows[i][1]))
                {
                    wants.Add(new Wants(Convert.ToInt32(dataset.Tables[3].Rows[i][0]), (float)Convert.ToDouble(dataset.Tables[3].Rows[i][2])));
                }
            }
            //adding savings
            for (int i = 0; i < dataset.Tables[4].Rows.Count; i++)
            {
                if (Convert.ToInt64(dataset.Tables[1].Rows[BudgetPlacement][0]) == Convert.ToInt64(dataset.Tables[4].Rows[i][1]))
                {
                    savings.Add(new Savings(Convert.ToInt32(dataset.Tables[4].Rows[i][0]), (float)Convert.ToDouble(dataset.Tables[4].Rows[i][2])));
                }
            }
        }
        //Sql Stuff
    }
}
