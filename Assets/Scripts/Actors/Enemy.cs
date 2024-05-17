using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Actor), typeof(AStar))]
public class Enemy : MonoBehaviour
{
    public Actor target;
    public bool isFighting;
    public AStar algorithm;
    //algorithm gelijkstelt aan het AStar component van dit script. 
    private void Start()
    {
        GameManager.Get.AddEnemy(GetComponent<Actor>());
        algorithm = GetComponent<AStar>();
    }
    public void MoveAlongPath(Vector3Int targetPosition)
    {
        Vector3Int gridPosition = MapManager.Get.FloorMap.WorldToCell(transform.position);
        Vector2 direction = algorithm.Compute((Vector2Int)gridPosition, (Vector2Int)targetPosition);
        Action.Move(GetComponent<Actor>(), direction);
    }
    public void RunAI()
    {
        Vector3Int gridPosition = MapManager.Get.FloorMap.WorldToCell(transform.position);
        // TODO: If target is null, set target to player (from gameManager) 
        if (target == null) { target = GameManager.Get.player; }
        // TODO: convert the position of the target to a gridPosition 
        target.transform.position = gridPosition;
        // First check if already fighting, because the FieldOfView check costs more cpu 
        if (isFighting || GetComponent<Actor>().FieldOfView.Contains(gridPosition))
        {
            // TODO: If the enemy was not fighting, is should be fighting now 
            foreach(var enemy in GameManager.Get.Enemies)
            {
                if (enemy != isFighting) 
                {
                     isFighting = true;
                }
            }
            // TODO: call MoveAlongPath with the gridPosition 
            MoveAlongPath(gridPosition);
        }
    }
}
//^(?([^\r\n])\s)*\r?$\r?\n
