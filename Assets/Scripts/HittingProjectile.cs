using UnityEngine;

public class HittingProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Material _material;
    [SerializeField] private ByPathMovement _byPathMovement;
    [SerializeField] private GameObject _particles;
    private void OnEnable()
    {
        _byPathMovement.OnReachPoint += () => Destroy(gameObject);
    }
    private void OnDisable()
    {
        _byPathMovement.OnReachPoint -= () => Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Enemy"))
            return;

        var hittingPosition= other.ClosestPoint(other.transform.position);

        Instantiate(_particles, hittingPosition, Quaternion.identity);

        Collider[] enemys = Physics.OverlapSphere(hittingPosition, gameObject.transform.localScale.x, _layerMask);
        foreach (var enemy in enemys)
        {
            if (enemy.gameObject.TryGetComponent<EnemyDamageHandler>(out var damageHandler))
            {
                damageHandler.TakeGamage(_material);
            }
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        PlayerHandler.Instance.OnProjectileDestroy();
    }
}
