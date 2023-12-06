using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isReceivedCash = false;
    public bool isReceivedFish = false;
    
    public bool isHasCashandFish = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isReceivedCash && isReceivedFish)
        {
            isHasCashandFish = true;
        }
        else
        {
            isHasCashandFish = false;
        }
    }
}
