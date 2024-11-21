using UnityEngine;

namespace Utils.ScriptableCondition
{
    public class AudioCondition : ICondition
    {
        public AudioSource source;
        
        public bool Eval()
        {
            return source.isPlaying;
        }
    }
}