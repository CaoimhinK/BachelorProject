using UnityEngine;

namespace Noidel {
    public class Button : MonoBehaviour
    {
        private bool _wasPushed;

        public void PushButton()
        {
            _wasPushed = true;
        }

        public bool WasPushed()
        {
            return _wasPushed;
        }
    }
}
