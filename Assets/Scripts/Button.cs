using System.Collections;
using UnityEngine;

namespace BachelorProject {
    public class Button : MonoBehaviour
    {
        private bool _wasPushed;

        public void PushButton()
        {
            _wasPushed = true;
            StartCoroutine(nameof(Wait));
        }

        public bool WasPushed()
        {
            return _wasPushed;
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            _wasPushed = false;
        }
    }
}
