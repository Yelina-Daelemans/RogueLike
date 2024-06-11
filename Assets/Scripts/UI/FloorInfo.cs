using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorInfo : MonoBehaviour
{
    private VisualElement root;
    private Label floorinfo;
    private Label enemyinfo;
    
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        floorinfo = root.Q<Label>("Floor"); 
        enemyinfo = root.Q<Label>("Enemies");
    }
    public void SetFloorLabel(int floor) 
    {
        floorinfo.text = floor.ToString();
    }
    public void SetEnemyLabel(int enemy) 
    {
        enemyinfo.text = enemy.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
