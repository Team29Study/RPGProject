using UnityEngine;

public class MagicZone: MonoBehaviour, IProjectile
{
    public int damage { get; set; }

    public float delay;
    public float duration; 
    private float currDuration;
    private bool isExploded;
    
    private HitBox hitBox;
    public GameObject particle;
    
    private void Awake()
    {
        hitBox = GetComponentInChildren<HitBox>(true);
    }
    
    public void RegisterDamage(Collider other)
    {
        if (!other.TryGetComponent(out PlayerHealth damagable)) return;
            
        damagable.TakeDamage(damage, transform);
        hitBox.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        hitBox.onTrigger += RegisterDamage;

        currDuration = 0;
        isExploded = false;
        hitBox.gameObject.SetActive(false);
        particle.gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        hitBox.onTrigger -= RegisterDamage;
    }

    private void Update()
    {
        currDuration += Time.deltaTime;
        if (currDuration >= delay && !isExploded)
        {
            isExploded = true;
            hitBox.gameObject.SetActive(true);
            particle.gameObject.SetActive(true);
            currDuration = 0;
        }

        if (currDuration >= duration)
        {
            ProjectileManager.Instance.DestroyProjectile(gameObject);

        }
    }
}