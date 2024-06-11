using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] public bool Up;
    // Start is called before the first frame update
    void Start()
    {
       Up =  GameManager.Get.GetLadderAtLocation(Vector2.up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
