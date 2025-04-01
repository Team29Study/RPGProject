using UnityEngine;

public class EnemyResourceHandler: MonoBehaviour
{
    public int health;
    public int attack;

    public float DetectedDistance;       

    public void ModifyHealth(int amount)
    {
        health += amount;
    }
}