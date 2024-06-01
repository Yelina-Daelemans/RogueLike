using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;
using static UnityEditor.Progress;

[RequireComponent(typeof(Actor))]
public class Player : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;
    public Inventory inventory;

    [Header("Inventory")]
    private bool inventoryIsOpen = false;
    private bool droppingItem = false;
    private bool usingItem = false;
    private void Awake()
    {
        controls = new Controls();
    }

    private void Start()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        GameManager.Get.player = GetComponent<Actor>();
    }

    private void OnEnable()
    {
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.SetCallbacks(null);
        controls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = controls.Player.Movement.ReadValue<Vector2>();
            if ( direction.y > null)
            {
                UIManager.Get.Inventory.SelectNextItem();
            }
            else if(direction.y < 0)
            {
                UIManager.Get.Inventory.SelectPreviousItem();
            }
            Move();
        }
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            /*
             * Nu maak je de OnExit functie af, die gekoppeld is aan de escape toets. 
             * Daarin kijk je eerst of de inventory open is. Als dat zo is, dan sluit je de inventory via de UIManager en zet je de waarde van inventoryIsOpen, 
             * droppingItem en usingItem op false. 
             */
            if (inventoryIsOpen == true) 
            {
                UIManager.Get.Inventory.Hide();
                inventoryIsOpen = false;
                droppingItem = false;
                usingItem = false;
            }
        }
    }

    private void Move()
    {
        Vector2 direction = controls.Player.Movement.ReadValue<Vector2>();
        Vector2 roundedDirection = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
        Debug.Log("roundedDirection");
        Action.MoveOrHit(GetComponent<Actor>(), roundedDirection);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            Consumable item = GameManager.Get.GetItemAtLocation(gameObject.transform.position);

            if (item == null)
            {
                UIManager.Get.AddMessage("Fuck wait a minute there is nothing here", Color.clear);
            }
            //Er was wel een item, maar je inventory is vol 
            else if (inventory.Items.Count < inventory.MaxItems) 
            {
                UIManager.Get.AddMessage("YOU DON'T HAVE MAGIC POCKETS DUMPY!", Color.blue);
            }
            else  
            {
                inventory.AddItem(item);
                gameObject.SetActive(false);
                GameManager.Get.Items.Remove(item); UIManager.Get.AddMessage("*Clap* *clap* *clap* Wouw you finally did it Congrats but not really.", Color.magenta);
            }
            

        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if (inventoryIsOpen != true) 
            {
                UIManager.Get.Inventory.Show(inventory.Items);
                inventoryIsOpen = true;
                droppingItem = true;
            }
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventoryIsOpen)
            {
                Consumable selectedItem = inventory.Items[UIManager.Get.Inventory.Selected];
                inventory.DropItem(selectedItem);
                if (Input.GetKeyDown(KeyCode.D))
                {
                    
                    selectedItem.transform.position = transform.position;
                    GameManager.Get.Items.Add(selectedItem);
                    selectedItem.gameObject.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.U))
                {
                    UseItem(selectedItem);
                    Destroy(selectedItem.gameObject);
                }

                UIManager.Get.Inventory.Hide();
            }
        }
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if (inventoryIsOpen != true)
            {
                UIManager.Get.Inventory.Show(inventory.Items);
                inventoryIsOpen = true;
                usingItem = true;
            }
        }
    }
    private void UseItem(Consumable item)
    {
        if (item.name == "HealthPotion")
        {
            gameObject.GetComponent<Actor>().Heal(1);
        }
        if (item.name == "Fireball") 
        {
            List<Actor> enemies = GameManager.Get.GetNearbyEnemies(transform.position);
            foreach (Actor enem in enemies)
            {
                enem.DoDamage(5);
            }
            UIManager.Get.AddMessage("You did some real emotional damage there", Color.blue);
        }
        if (item.name == "ScrollOfConfusion") 
        {
            List<Actor> enemies = GameManager.Get.GetNearbyEnemies(transform.position);

            foreach (Actor enem in enemies) 
            {
                enem.GetComponent<Enemy>().Confuse();
            }

            UIManager.Get.AddMessage("they are confused now hmmm what to do", Color.gray);
        }
    }
}
