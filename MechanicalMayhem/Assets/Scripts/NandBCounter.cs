using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NandBCounter : MonoBehaviour
{
    public static NandBCounter Instance;

    public TMP_Text NandBText;
    public int currentNandBs = 0;

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        NandBText.text = "NandBS: " + currentNandBs.ToString();
    }

        public void IncreaseNandBS(int v)
    {
        currentNandBs += v;
        NandBText.text = "NandBS: " + currentNandBs.ToString();
    }
}
