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
        if (GameManager.Get.player == this) { UIManager.Get.UpdateHealth(hitPoints, maxHitPoints); }
        
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
        if(GameManager.Get.player == this && GameManager.Get.player == null) { UIManager.Get.AddMessage("You died", Color.red); }
        foreach(var enemy in GameManager.Get.Enemies) 
        { if (enemy == this && enemy == null) 
            { UIManager.Get.AddMessage($"{gameObject.name} is dead!", Color.green); 
                GameManager.Get.RemoveEnemy(enemy);
                Destroy(gameObject);
            } 
        }
        Vector3 currentPosition = transform.position;
        GameObject Grave = GameManager.Get.GetGameObject("Dead", new Vector2(currentPosition.x, currentPosition.y));
        Grave.name = $"Remains of {gameObject.name}";
        Destroy(gameObject);
    }
    public void DoDamage(int hp) 
    {
        hp -= hitPoints;
        if(hp <= 0) { hp = 0; Die(); }
        if (GetComponent<Player>()) 
        {
            UIManager.Get.UpdateHealth(hitPoints, maxHitPoints);
        }
    }

}
