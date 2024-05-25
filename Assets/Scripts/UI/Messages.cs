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
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = root.Q<Label>($"Label{i}");
        }
        Clear();
        AddMessage("Welcome to the dungeon, Adventurer!", Color.cyan);
    }
    public void Clear() 
    {
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i].text = string.Empty;
        }
    }
    public void MoveUp() 
    {
        for(int i = 0;i < labels.Length; i++) 
        {
            labels[i].text = labels[i + 1].text;
        }
    }
    public void AddMessage(string content, Color color) 
    {
        MoveUp();
        labels[0].text = content;
        labels[0].style.color = new StyleColor(color);
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
