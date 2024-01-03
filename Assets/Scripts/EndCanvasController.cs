using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EndCanvasController : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }
    }
}