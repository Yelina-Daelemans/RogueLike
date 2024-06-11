using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            UIManager.Get.UpdateXp(Xp);

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
    [SerializeField] private int level;
    [SerializeField] private int Xp;
    [SerializeField] private int xpToNextLevel;

    public int MaxHitPoints { get; private set; }
    public int HitPoints { get; private set; }
    public int Defense { get; private set; }
    public int Power { get; private set; }
    public int Level { get; private set; }
    public int XP { get; private set; }
    public int XPToNextLevel { get; private set; }

    public void AddXp(int xp) 
    {
        if (xp > xpToNextLevel) 
        {
            Xp = xp;
            level++;
           xpToNextLevel = (int) math.round(xpToNextLevel * 1.7f);
            maxHitPoints += 6;
            defense += 3;
            power += 4;
            UIManager.Get.UpdateXp(xp);
            UIManager.Get.AddMessage("Congrats you are level up", Color.green);
        }
    }
    private void Die() 
    {
        if (GetComponent<Player>())
        {
            UIManager.Get.AddMessage("You died!", Color.red); //Red
        }
        else
        {
            AddXp(Xp);
            UIManager.Get.AddMessage($"{name} is dead!", Color.green); //Light Orange
        }
        GameManager.Get.GetGameObject("Dead", transform.position).name = $"Remains of {name}";
        GameManager.Get.RemoveEnemy(this);
        Destroy(gameObject);
        GameManager.Get.DeleteSaveGame();
    }

    public void DoDamage(int hp, Actor attacker) 
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
        //Wanneer de actor sterft, kijk je of de attacker de player is.
        if (GetComponent<Actor>() == null)
        {
            if (attacker.GetComponent<Player>()) 
            {
                AddXp(Xp);                              
            }
            
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
