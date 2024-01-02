using Assets.Scripts.Interface;
using Mirror;
using TMPro;
using UnityEngine;
public class GamePlayer : NetworkBehaviour, IDamageable
{
    [SerializeField] TextMeshProUGUI _healthBar;
    [SerializeField] [SyncVar] int _health;
    public int Health
    {
        get => _health;
        set
        {
            Debug.Log("Player hp set to: " + value);
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
    }

    [ServerCallback]
    public void Damage(int amount)
    {
        Health -= amount;
    }

}
