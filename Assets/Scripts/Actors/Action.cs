using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Action : MonoBehaviour
{
    static public void MoveOrHit(Actor actor, Vector2 direction)
    {
        // see if someone is at the target position 
        Actor target = GameManager.Get.GetActorAtLocation(actor.transform.position + (Vector3)direction);
        // if not, we can move 
        if (target == null)
        {
            Move(actor, direction);
        }
        else{ Hit(actor, target); }
        // end turn in case this is the player 
        EndTurn(actor);

    }
    static public void Hit(Actor actor, Actor target) 
    {
        //Bereken de damage. Die is gelijk aan de power van de actor, verminderd met de defense van het target. 
        int damage = actor.Power - target.Defense;
        
        //Als de damage groter dan 0 is, verminder je de hitpoints van het target. 
        //Voeg via UIManager een message toe, afhankelijk van de uitkomst. Je kan de name property van Actor gebruiken.  
        //Je message vermeld de namen van de actor en het target. Als er damage is, dan toont je message ook hoeveel.
        Color c = Color.red;
        string message = $" Weak {actor.name} attacked strong {target.name}, but did no damage";
        if (actor.GetComponent<Player>())
        {
            c = Color.white;
        }
        if (damage > 0)
        {
            target.DoDamage(damage);
            message = $"Strong {actor.name} did {damage} damage to weak {target.name}";
        }
        UIManager.Instance.AddMessage(message, c);
    }



    static public void Move(Actor actor, Vector2 direction)
    {
        actor.Move(direction);
        actor.UpdateFieldOfView();
    }
    static private void EndTurn(Actor actor) 
    {
        if(actor.GetComponent<Player>()) 
        {
            GameManager.Get.StartEnemyTurn();
        }
    }
}
//^(?([^\r\n])\s)*\r?$\r?\n
