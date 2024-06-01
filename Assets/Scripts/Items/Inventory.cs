using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Consumable>Items = new List<Consumable>();
    public int MaxItems;

    //De functie AddItem voegt het item toe aan de lijst,
    //maar enkel als de lijst minder dan het maximum aantal items bevat.
    //Afhankelijk daarvan geeft de functie true of false als resultaat. 
    public void AddItem(Consumable item) 
    {
        /*if (Items.Count < MaxItems)
        {
            Items.Add(item);
            return true;
        }
        else 
        {
            return false;
        }*/
        Items.Add(item);
    }
    public void DropItem(Consumable item) 
    {
        Items.Remove(item);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
