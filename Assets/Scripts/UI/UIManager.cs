using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;
    public GameObject inventory;
    public InventoryUI Inventory { get; set; }
    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    [Header("Documents")]
    public HealthBar healthBarScript;
    public Messages messageScript;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = GetComponent<InventoryUI>();
    }
    public static UIManager Get
    {
        get => Instance;
    }
    /*
     * In deze functies roep je functies van respectievelijk HealthBar en Messages aan om de waarden door te geven. 
     * Let wel op: je werkt met variabelen van het type GameObject, dus je hebt het Script component nodig. 
     */
    public void UpdateHealth(int current, int max) 
    {
        healthBarScript.GetComponent<HealthBar>().SetValues(current, max);
    }

    public void AddMessage(string message, Color color) 
    {
        messageScript.GetComponent<Messages>().AddMessage(message, color);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
