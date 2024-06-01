using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private AdamMilVisibility algorithm;
    public List<Vector3Int> FieldOfView = new List<Vector3Int>();
    public int FieldOfViewRange = 8;
    private void Start()
    {
        
        algorithm = new AdamMilVisibility();
        UpdateFieldOfView();
        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateHealth(hitPoints, maxHitPoints);
        }

    }

    public void Move(Vector3 direction)
    {
        if (MapManager.Get.IsWalkable(transform.position + direction))
        {
            transform.position += direction;
        }
    }

    public void UpdateFieldOfView()
    {
        var pos = MapManager.Get.FloorMap.WorldToCell(transform.position);

        FieldOfView.Clear();
        algorithm.Compute(pos, FieldOfViewRange, FieldOfView);

        if (GetComponent<Player>())
        {
            MapManager.Get.UpdateFogMap(FieldOfView);
        }
    }
    [Header("Powers")]
    [SerializeField] private int maxHitPoints;
    [SerializeField] private int hitPoints;
    [SerializeField] private int defense;
    [SerializeField] private int power;
    public int MaxHitPoints { get; private set; }
    public int HitPoints { get; private set; }
    public int Defense { get; private set; }
    public int Power { get; private set; }
    private void Die() 
    {
        if (GetComponent<Player>())
        {
            UIManager.Get.AddMessage("You Died! Idiot!", Color.red);
            GameObject grave = GameManager.Get.GetGameObject("Dead", this.transform.position);
            grave.name = $"Remains of {this.name}";
        }
        else if (GetComponent<Enemy>())
        {
            UIManager.Get.AddMessage($"{this.name} is dead!", Color.green);
            GameManager.Get.RemoveEnemy(this);
        }
        GameObject.Destroy(this.gameObject);
    }
    public void DoDamage(int hp) 
    {
        hitPoints -= hp;
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            Die();
        }
        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateHealth(hitPoints, maxHitPoints);
        }
    }
    public void Heal(int hp)
    {
        hitPoints += hp;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
            
        UIManager.Get.UpdateHealth(hitPoints, maxHitPoints);
            
         if (GetComponent<Player>() && hitPoints + hp > maxHitPoints) {
                 int rest = maxHitPoints - hitPoints;
                UIManager.Get.AddMessage($"The player has been healed with a potion +{rest}", Color.yellow);
         }

        
        
    }
}
