using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EndCanvasController : MonoBehaviour
    {
        bool _canInteract;
        // Update is called once per frame
        void Update()
        {
            if (_canInteract && Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            Invoke(nameof(SetInteractibility), 1f);
        }

        private void OnDisable()
        {
            _canInteract = false;
        }

        void SetInteractibility()
        {
            _canInteract = true;
        }
    }
}