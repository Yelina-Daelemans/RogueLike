using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public List<Actor> Enemies = new List<Actor>();
    public List<Consumable> Items = new List<Consumable>();
    public Actor player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetGameObject(string name, Vector2 position)
    {
        GameObject actor = Instantiate(Resources.Load<GameObject>($"Prefabs/{name}"), new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity);
        actor.name = name;
        return actor;
    }

    public static GameManager Get { get => instance; }

    public void StartEnemyTurn() 
    {
        foreach(var enemy in Enemies) 
        {
            enemy.GetComponent<Enemy>().RunAI();
        }
    }
    public Actor GetActorAtLocation(Vector3 location)
    {
        if (player.transform.position == location) { return player; }
        else {
            foreach (var enemy in Enemies)
            {
                if (enemy.transform.position == location) { return enemy; }
            }
            return null;
        }
    }

    public void AddEnemy(Actor enemy) { Enemies.Add(enemy); }
    public void RemoveEnemy(Actor enemy) 
    {
        if (Enemies.Contains(enemy))
        {
            Enemies.Remove(enemy);
            Destroy(enemy.gameObject); 
        }
        
    }
    public void AddItem(Consumable consumable) 
    {
        Items.Add(consumable);
    }
    public void RemoveItem(Consumable consume) 
    {
        Items.Remove(consume);
    }
    public Consumable GetItemAtLocation(Vector3 location) 
    {
        foreach (Consumable item in Items) { if (item.transform.position == location) { return item; } }
        return null;
       
    }
}
