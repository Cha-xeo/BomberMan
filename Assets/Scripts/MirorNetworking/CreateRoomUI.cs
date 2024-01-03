using System.Collections;
using UnityEngine;
using Mirror;
using TMPro;

namespace Assets.Scripts.Room
{
    public class CreateRoomUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField _IPAdresse;
        public void JoinRoom()
        {
            var manager = RoomManager.singleton;
            if (_IPAdresse.text.Length != 0)
            {
                manager.networkAddress = _IPAdresse.text;
            }
            Debug.Log("networkAddress: " + manager.networkAddress);
            manager.StartClient();
        }
        public void CreateRoom()
        {
            var manager = RoomManager.singleton;
            AplicationController.AplicationController.Instance.isServer = true;
            manager.StartHost();
        }


    }
}