using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    private VisualElement root;

    private VisualElement healthBar;

    private Label healthLabel;
    // Start is called before the first frame update
    void Start()
    {
        root= GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<VisualElement>("HealthBar");
        healthLabel = root.Q<Label>("Label");
    }
    public void SetValues(int currentHitPoints, int maxHitPoints) 
    {
        float percent = (float)currentHitPoints / maxHitPoints * 100;

        healthBar.style.width = Length.Percent(percent);
        healthLabel.text = $"{currentHitPoints}/{maxHitPoints} HP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}