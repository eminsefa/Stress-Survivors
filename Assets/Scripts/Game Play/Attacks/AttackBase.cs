using UnityEngine;

namespace StressSurvivors
{
    public class AttackBase : MonoBehaviour
    {
        [SerializeField] protected float      Cooldown;
        [SerializeField] protected float      FireSpeed;
        [SerializeField] protected bool       IsActive;
        [SerializeField] protected GameObject Body;

        public void Attack()
        {
            Body.SetActive(true);
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            Body.SetActive(false);
        }
    }
}