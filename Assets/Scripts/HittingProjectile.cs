using UnityEngine;

public class HittingProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Material _material;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            var hittingPosition= other.ClosestPoint(other.transform.position);

            Collider[] enemys = Physics.OverlapSphere(hittingPosition, gameObject.transform.localScale.x, _layerMask);
            foreach(var enemy in enemys)
            {
                if(enemy.gameObject.TryGetComponent<EnemyDamageHandler>(out var damageHandler))
                {
                    damageHandler.TakeGamage(_material);
                }
            }
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        FireHandler.Instance.OnProjectileDestroy();
    }
}
