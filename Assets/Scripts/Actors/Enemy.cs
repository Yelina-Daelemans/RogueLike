using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(Actor), typeof(AStar))]
public class Enemy : MonoBehaviour
{
    public Actor target;
    public bool isFighting = false;
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
        Action.MoveOrHit(GetComponent<Actor>(), direction);
    }
    public void RunAI()
    {
        // TODO: If target is null, set target to player (from gameManager) 
        if (target == null) { target = GameManager.Get.player; }
        // TODO: convert the position of the target to a gridPosition 
        var gridPosition = MapManager.Get.FloorMap.WorldToCell(target.transform.position);
        // First check if already fighting, because the FieldOfView check costs more cpu 
        if (isFighting || GetComponent<Actor>().FieldOfView.Contains(gridPosition))
        {
            // TODO: If the enemy was not fighting, is should be fighting now 
            if (!isFighting) { isFighting = true; }
            // TODO: call MoveAlongPath with the gridPosition 
            
        }


        if (Vector3.Distance(target.transform.position, this.transform.position) < 1.5)
        {
            Action.Hit(target, this.GetComponent<Actor>());
        }
        else 
        {
            MoveAlongPath(gridPosition);
        }
        
        
    }
}

//^(?([^\r\n])\s)*\r?$\r?\n
