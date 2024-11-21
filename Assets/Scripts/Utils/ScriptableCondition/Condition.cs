namespace Utils.ScriptableCondition
{
    public class Condition : ICondition
    {
        private ConditionData data;

        public Condition(ConditionData data)
        {
            this.data = data;
        }

        public bool Eval()
        {
            return false;
        }

    }
}