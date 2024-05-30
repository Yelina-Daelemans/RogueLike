using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Messages : MonoBehaviour
{
    private Label[] labels = new Label[5];

    private VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        labels[0] = root.Q<Label>("Label1");
        labels[1] = root.Q<Label>("Label2");
        labels[2] = root.Q<Label>("Label3");
        labels[3] = root.Q<Label>("Label4");
        labels[4] = root.Q<Label>("Label5");

        Clear();
        AddMessage("Welcome to the dungeon, Adventurer!", Color.cyan);
    }
    public void Clear() 
    {
        for (int i = 1; i < labels.Length; i++)
        {
            labels[i].text = string.Empty;
        }
    }
    public void MoveUp() 
    {
        for(int i = 1; i < labels.Length -1; i++) 
        {
            labels[i + 1].text = labels[i].text;
            labels[i + 1].style.color = labels[i].style.color;
        }
    }
    public void AddMessage(string content, Color color) 
    {
        MoveUp();
        labels[1].text = content;
        labels[1].style.color = color;
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
