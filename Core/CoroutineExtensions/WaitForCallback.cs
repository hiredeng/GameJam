
using UnityEngine;

namespace ProjectName.Core.CoroutineExtensions
{
    public class WaitForCallback : CustomYieldInstruction
    {
        private bool _released = false;
        public override bool keepWaiting { get => _released; }

        public override void Reset()
        {
            _released = false;
        }

        public void Release()
        {
            _released = true;
        }
    }
}