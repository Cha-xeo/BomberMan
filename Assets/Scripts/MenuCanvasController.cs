using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MenuCanvasController : MonoBehaviour
    {
        [SerializeField] GameObject MenuRootPanel;
        [SerializeField] GameObject LobbtButton;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuRootPanel.SetActive(!MenuRootPanel.activeSelf);
                var manager = NetworkManager.singleton;
                if (manager && !manager.isNetworkActive)
                {
                    LobbtButton.SetActive(false);
                }
                else
                {
                    LobbtButton.SetActive(true);
                }
            }
        }

        public void ReturnToLobby()
        {
            var manager = NetworkManager.singleton;
            if (AplicationController.AplicationController.Instance.isServer)
            {
                manager.StopHost();
            }
            else
            {
                manager.StopClient();
            }
            MenuRootPanel.SetActive(false);
        }

        public void QuitGame()
        {
            AplicationController.AplicationController.Instance.quit();
        }

    }
}