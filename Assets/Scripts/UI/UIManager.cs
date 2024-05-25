using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private HealthBar healthBarScript;
    private Messages messageScript;
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        

    }
    // Start is called before the first frame update
    void Start()
    {
        healthBarScript = GetComponent<HealthBar>();
        messageScript = GetComponent<Messages>();
    }
    /*
     * In deze functies roep je functies van respectievelijk HealthBar en Messages aan om de waarden door te geven. 
     * Let wel op: je werkt met variabelen van het type GameObject, dus je hebt het Script component nodig. 
     */
    public void UpdateHealth(int current, int max) 
    {
        if (healthBarScript != null)
        {
            healthBarScript.SetValues(current, max);
        }
    }

    public void AddMessage(string message, Color color) 
    {
        if (messageScript != null)
        {
            messageScript.AddMessage(message, color);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
