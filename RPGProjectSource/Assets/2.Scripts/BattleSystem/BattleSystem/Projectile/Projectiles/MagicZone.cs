using UnityEngine;

public class MagicZone: IProjectile
{
    public float duration = 1; 
    private float currDuration;
    
    public Transform target;
    private HitBox hitBox;
    
    private void Awake()
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;
        hitBox = GetComponentInChildren<HitBox>(true);
    }

    public void OnEnable()
    {
        currDuration = 0;
        hitBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        currDuration += Time.deltaTime;
        if (currDuration >= duration)
        {
            Debug.Log("폭팔");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileManager.Instance.DestroyProjectile(this);
    }
}