using UnityEngine;

public class MagicZone: IProjectile
{
    public float delay = 1f;
    public float duration = 0.5f; 
    private float currDuration;
    private bool isExploded;
    
    private HitBox hitBox;
    
    private void Awake()
    {
        hitBox = GetComponentInChildren<HitBox>(true);
    }

    public void OnEnable()
    {
        currDuration = 0;
        isExploded = false;
        hitBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        currDuration += Time.deltaTime;
        if (currDuration >= delay && !isExploded)
        {
            isExploded = true;
            hitBox.gameObject.SetActive(true);
            currDuration = 0;
        }

        if (currDuration >= duration)
        {
            ProjectileManager.Instance.DestroyProjectile(this);
        }

    }

    private void OnTriggerEnter(Collider other) // 콜라이더를 hitBox만 가지도록 하여 처리
    {
        ProjectileManager.Instance.DestroyProjectile(this);
    }
}