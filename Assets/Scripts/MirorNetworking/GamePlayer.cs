using Assets.Scripts;
using Assets.Scripts.Interface;
using Mirror;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.Gameplay
{
    public class GamePlayer : NetworkBehaviour, IDamageable
    {
        [SerializeField] TextMeshProUGUI _healthBar;
        [SerializeField][SyncVar] int _health;

        public int Health
        {
            get => _health;
            set
            {
                // TODO clamp to hp max, check for overhealth
                _healthBar.text = "hp: " + value.ToString();
                _health = value;
                if (value <= 0)
                {
                    Debug.LogWarning("Player death not implented");
                    //TODO death
                }
            }
        }
        public override void OnStartClient()
        {
            base.OnStartClient();
            _healthBar.text = "hp: " + Health.ToString();

            FindObjectsOfType<GameplayPlayerCounter>()[0].UpdatePlayerCount();
        }

        [ServerCallback]
        public void Damage(int amount)
        {
            _health -= amount;
        }

    }
}
