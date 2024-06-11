using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    private VisualElement root;

    private VisualElement healthBar;

    private Label healthLabel;

    private VisualElement xpBar;
    private Label level;

    // Start is called before the first frame update
    void Start()
    {
        root= GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<VisualElement>("HealthBar");
        healthLabel = root.Q<Label>("Label");
        xpBar = root.Q<VisualElement>("XP-Bar");
        level = root.Q<Label>("level");
    }
    public void SetValues(int currentHitPoints, int maxHitPoints) 
    {
        float percent = (float)currentHitPoints / maxHitPoints * 100;
        healthBar.style.width = Length.Percent(percent);
        healthLabel.text = $"{currentHitPoints} / {maxHitPoints} HP";
       
    }

    public void SetLevel(int level) 
    {
        this.level.text = level.ToString();
    }

    public void SetXP(int xp) 
    {
         float XP = (float) xp * 100;
        xpBar.style.width = Length.Percent(XP);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
