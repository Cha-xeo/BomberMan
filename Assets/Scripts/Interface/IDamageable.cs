using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public interface IDamageable
    {
        public int Health { get; set; }
        public void Damage(int amount);
    }
}