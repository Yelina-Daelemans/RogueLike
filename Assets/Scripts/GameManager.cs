using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public List<Actor> Enemies = new List<Actor>();
    public List<Consumable> Items = new List<Consumable>();
    public Actor player;
    public List<Ladder> Ladder = new List<Ladder>();
    public List<Tombstone> Tombstones = new List<Tombstone>();
    private static string SavePath;
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
        SavePath = Application.persistentDataPath + "/savefilegame.json";
        LoadGame();
    }
    public static void SaveGameData(Actor actor) 
    {
        string json = JsonUtility.ToJson(actor);
        File.WriteAllText(SavePath, json);
    }
    public static void LoadGameData(Actor act) 
    {
        if (File.Exists(SavePath)) 
        {
            string json = File.ReadAllText(SavePath);
            JsonUtility.FromJsonOverwrite(json, act);
        }
    }
    public void SaveGame()
    {
        if (player != null)
        {
            SaveGameData(player);
        }
    }
    public void LoadGame()
    {
        if (player != null)
        {
            LoadGameData(player);
        }
    }
    public void DeleteSaveGame()
    {
        
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void AddTombStone(Tombstone stone) 
    {
        Tombstones.Add(stone);
    }
    public void ClearFloor() 
    {     
        GameObject.Destroy(gameObject);
        Enemies.Clear();
        Items.Clear();
        Ladder.Clear();
        Tombstones.Clear();
    }
    public void AddLadder(Ladder ladder) 
    {
        Ladder.Add(ladder);
    }

    public Ladder GetLadderAtLocation(Vector3 location) 
    {
        foreach (Ladder ladder in Ladder)
        {
            if (ladder.transform.position == location) 
            {
                return ladder;
            }
        }       
        return null;
    }
    public List<Actor> GetNearbyEnemies(Vector3 location)
    {
        var result = new List<Actor>();
        foreach (Actor enemy in Enemies)
        {
            if (Vector3.Distance(enemy.transform.position, location) < 5)
            {
                result.Add(enemy);
            }
        }
        return result;
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

    public void AddEnemy(Actor enemy) 
    {

       UIManager.Get.UpdateFloorInfoEnemies(Enemies.Count);
        Enemies.Add(enemy); 
    }
    public void RemoveEnemy(Actor enemy) 
    {
        UIManager.Get.UpdateFloorInfoEnemies(Enemies.Count);
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
