using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour
{
    [Header("Variables")]
    GameObject PEDObject;
    PulseEngineDriver PEDScript;
    public TextAsset BradyFile; // declare a TextAsset variable to hold the state file data
    public TextAsset NormalFile;
    public TextAsset TachyFile;
    public TextAsset ArrhythmiaFile;

    // Start is called before the first frame update
    void Start()
    {
        PEDObject = GameObject.Find("Pulse Engine Driver");
        PEDScript = PEDObject.GetComponent<PulseEngineDriver>();
        
        BradyFile = Resources.Load<TextAsset>("states/Bradycardic@0s");
        NormalFile = Resources.Load<TextAsset>("states/DefaultMale@0s");
        TachyFile = Resources.Load<TextAsset>("states/Tachycardic@0s");
        ArrhythmiaFile = Resources.Load<TextAsset>("states/DefaultMale@0s");
    }

    public void BradyCardiac()
    {
        PEDScript.initialStateFile = BradyFile;
    }

    public void NormalCardia()
    {
        PEDScript.initialStateFile = NormalFile;
    }

    public void TachyCardiac()
    {
        PEDScript.initialStateFile = TachyFile;
    }

    public void ArrhythmiaCardia()
    {
        PEDScript.initialStateFile = ArrhythmiaFile;
    }
}
