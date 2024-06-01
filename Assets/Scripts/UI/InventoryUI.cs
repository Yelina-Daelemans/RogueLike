using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    public Label[] labels = new Label[8];
    public int selected;
    public int numItems;
    private VisualElement root;

    public int Selected { get; private set; }
    public int NumItems { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = root.Q<Label>($"item{i}");
        }
        Clear();
        root.style.display = DisplayStyle.None;
    }
    public void Clear()
    {
        for (int i = 1; i < labels.Length; i++)
        {
            labels[i].text = "";
        }
    }
    private void UpdateSelected() 
    {

        for (int i = 1; i <= labels.Length; i++) 
        {
            if ( i == selected) 
            {               
                labels[i].style.backgroundColor = Color.green;
            }
            else { labels[i].style.backgroundColor = Color.clear; }
            
        }
        
    }
    public void SelectNextItem() 
    {
        if (selected > numItems) 
        {
            selected++;
            UpdateSelected();
        }
        
    }

    public void SelectPreviousItem() 
    {
        if (selected < numItems)
        {
            selected--;
            UpdateSelected();
        }
    }

    public void Show(List<Consumable> list) 
    {
        selected = 0;
        numItems = list.Count;
        Clear();
        //Je stelt de naam van de labels gelijk aan de name property van elk element in de lijst 
        for (int i = 0; i < list.Count && i < labels.Length; i++)
        {
            labels[i].text = list[i].name;
        }
        UpdateSelected();
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide() 
    {
        root.style.display = DisplayStyle.None;
    }
}
