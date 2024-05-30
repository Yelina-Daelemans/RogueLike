using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
   [SerializeField] private ItemType type;
    public ItemType Type { get; private set; }
    public enum ItemType 
    {
        HealtPotion,

        Fireball,

        ScrollOfConfusion
    }
    private void Start()
    {

    }
}
