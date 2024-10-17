using System.Data;

namespace BudgieBudgeting.DatabaseItems
{
    public class Customer
    {
        private int CustomerId;
        private string Name;
        private string Email;
        private string Password;
        private float Income;
        private Budget budget;
        public Customer(DataSet dataset,int Customerplacement)
        {
            InitializeBudget(dataset, Customerplacement);

        }
        //initializing budget
        private void InitializeBudget(DataSet dataset, int Customerplacement)
        {
            for (int i = 0; dataset.Tables[1].Rows.Count > 0; i++)
            {
                if (Convert.ToInt64(dataset.Tables[0].Rows[0][Customerplacement]) == Convert.ToInt64(dataset.Tables[0].Rows[i][1]))
                {
                    this.budget = new Budget(dataset, i);
                    break;
                }
            }
        }
        //setters
        private void SetCustomerId(int CustomerId)
        {
            this.CustomerId = CustomerId;
        }
        public void SetName(String Name)
        {
            this.Name = Name;
        }
        public void SetEmail(String Email)
        {
            this.Email = Email;
        }
        public void SetPassword(String Password)
        {
            this.Password = Password;
        }
        public void SetIncome(float Income)
        {
            this.Income = Income;
        }
        //Getters
        public int GetCustomerId()
        {
            return this.CustomerId;
        }
        public String GetName()
        {
            return this.Name;
        }
        public String GetEmail()
        {
            return this.Email;
        }
        public String GetPassword(){ 
            return this.Password; 
        }
        public float GetIncome()
        {
            return this.Income;
        }
        public Budget GetBudget()
        {
            return this.budget;
        }
        //Sql Stuff
    }
}
