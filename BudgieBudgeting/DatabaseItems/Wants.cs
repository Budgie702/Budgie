namespace BudgieBudgeting.DatabaseItems
{
    public class Wants
    {
        int WantId;
        float Want;
        public Wants(int WantId, float Want)
        {
            setWant(Want);
            setWantId(WantId);
        }
        private void setWantId(int NeedId)
        {
            this.WantId = NeedId;
        }
        public void setWant(float Want)
        {
            this.Want = Want;
        }
        public int getWantId()
        {
            return this.WantId;
        }
        public float getWant()
        {
            return this.Want;
        }
    }
}
