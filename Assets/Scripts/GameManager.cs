using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public List<Actor> Enemies = new List<Actor>();
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
        foreach (var enemy in Enemies)
        {
            return enemy;
        }
        if (player.transform.position == location) { return player; }
        else {
            foreach (var enemy in Enemies)
            {
                if (enemy.transform.position == location) { return enemy; }
            }
            return null;
        }
    }
    public GameObject CreateActor(string name, Vector2 position)
    {
        GameObject actor = Instantiate(Resources.Load<GameObject>($"Prefabs/{name}"), new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity);
        actor.name = name;
        return actor;
    }
    public void AddEnemy(Actor enemy) { Enemies.Add(enemy); }
}
