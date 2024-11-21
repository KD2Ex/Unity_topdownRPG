using UnityEngine;

namespace Utils.ScriptableCondition
{
    [CreateAssetMenu(fileName = "Scriptable Condition", menuName = "SO/ScriptableCondition")]
    public class ScriptableCondition : ScriptableObject
    {
        public ConditionData data;

        public Condition Init()
        {
            return new Condition(data);
        }
    }
}