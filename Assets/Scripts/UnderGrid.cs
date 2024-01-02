using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnderGrid : MonoBehaviour
    {
        [SerializeField] GameObject _animPrefab;
        GameObject _anim;
       
        public void PlayExplosion()
        {
            _anim.SetActive(true);
            Invoke(nameof(EndExplosion), 1.2f);
        }
        public void EndExplosion()
        {
            _anim.SetActive(false);
        }

        private void Start()
        {
            _anim = Instantiate(_animPrefab, transform);
        }
    }
}