using UnityEngine;

namespace ColorChanging
{
    public class ActAndWait : CustomYieldInstruction
    {
        private readonly float _waiting;
        private readonly float _time;
        public ActAndWait(System.Action action, float waiting)
        {
            _time = Time.time;
            _waiting = waiting;
            action?.Invoke();
        }

        public override bool keepWaiting => Time.time - _time < _waiting;
        
        
    }
}