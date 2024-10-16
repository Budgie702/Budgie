namespace BudgieBudgeting.DatabaseItems
{
    public class Needs
    {
        int NeedId;
        float Need;
        public Needs(int NeedId, float Need)
        {
            setNeed(Need);
            setNeedId(NeedId);
        }
        private void setNeedId(int NeedId)
        {
            this.NeedId = NeedId;
        }
        public void setNeed(float Need)
        {
            this.NeedId = NeedId;
        }
        public int getNeedId()
        {
            return this.NeedId;
        }
        public float getNeed()
        {
            return this.Need;
        }
    }
}
