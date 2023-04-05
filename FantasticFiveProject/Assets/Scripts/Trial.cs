using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour
{
    [Header("Variables")]
    GameObject PEDObject;
    PulseEngineDriver PEDScript;
    // Start is called before the first frame update
    void Start()
    {
        PEDObject = GameObject.Find("Pulse Engine Driver");
        PEDScript = PEDObject.GetComponent<PulseEngineDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeData()
    {
        PEDScript.initialStateFile = "C:\Users\aarun\OneDrive - Imperial College London\BIOE60005 Bioengineering Group Project\Unity\BPM\Assets\PulsePhysiologyEngine\Data\states\Bradycardic@0s.json";
    }

}
