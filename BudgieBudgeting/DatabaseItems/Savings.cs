namespace BudgieBudgeting.DatabaseItems
{
    public class Savings
    {
        private int SavingId;
        private float Saving;
        public Savings(int SavingId, float Saving)
        {
            setNeed(Saving);
            setNeedId(SavingId);
        }
        private void setNeedId(int NeedId)
        {
            this.SavingId = NeedId;
        }
        public void setNeed(float Need)
        {
            this.SavingId = SavingId;
        }
        public int getNeedId()
        {
            return this.SavingId;
        }
        public float getNeed()
        {
            return this.Saving;
        }
    }
}
