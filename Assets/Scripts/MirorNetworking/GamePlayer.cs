using Assets.Scripts.Interface;
using Mirror;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GamePlayer : NetworkBehaviour, IDamageable
    {
        [SerializeField] TextMeshProUGUI _healthBar;
        [SerializeField][SyncVar(hook = nameof(UpdateHealth))] int _health;
        public int Health
        {
            get => _health;
            set
            {
                // TODO clamp to hp max, check for overhealth
                _health = value;
                /*if (value <= 0)
                {
                    CmdPlayerDeath();
                }*/
            }
        }

        public void UpdateHealth(int oldHealth, int newHealth)
        {
            _healthBar.text = "hp: " + newHealth.ToString();
            if (!isLocalPlayer) return;
            if (newHealth <= 0)
            {
                CmdPlayerDeath();
            }

        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            _healthBar.text = "hp: " + Health.ToString();
        }



        [ServerCallback]
        public void Damage(int amount)
        {
            Health -= amount;
        }

        [ClientRpc]
        public void RpcShowWinner(NetworkIdentity loser)
        {
            if (loser.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                AplicationController.AplicationController.Instance.gameCondition = AplicationController.GameCondition.Lost;

            }
            else
            {
                AplicationController.AplicationController.Instance.gameCondition = AplicationController.GameCondition.Won;
            }
            if (isServer)
            {
                NetworkManager.singleton.StopHost();
            }
            else
            {
                NetworkManager.singleton.StopClient();
            }
        }

        [Command]
        public void CmdPlayerDeath()
        {
            RpcShowWinner(netIdentity);
        }
    }
}