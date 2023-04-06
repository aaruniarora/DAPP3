using UnityEngine; using UnityEngine.UI;
using System.Collections; using System;
using System.Text;
using ArduinoBluetoothAPI;

public class BlinkArduino : MonoBehaviour 
{  
    [Header("CanvasPanels")]
    public GameObject BPanel;
    public GameObject NPanel;
    public GameObject TPanel;
    public GameObject APanel;  

    [Header("CanvasButtons")]
    public Button btn50BPM; // Bradycardiac
    public Button btn75BPM; // Normal
    public Button btn115BPM; // TachyCardiac
    public Button btn200BPM; // Arrhythmia
    public Button ResetButton;

    [Header("CanvasTextbox")]
    public Text statusText;
    public Text iffunction;

    [Header("BluetoothAsset")]
    private BluetoothHelper bluetoothHelper;
    private StringBuilder stringBuilder = new StringBuilder();

    void Awake()
    {
        BPanel.SetActive(false);
        NPanel.SetActive(false);
        TPanel.SetActive(false);
        APanel.SetActive(false);
    }
    
    void Start () 
    {
        statusText.text = "Hi";

		try
        {
			bluetoothHelper = BluetoothHelper.GetInstance("HC-05");
            if(bluetoothHelper.isDevicePaired()) // if we have already paired with the device
            {bluetoothHelper.StartListening();} // we start listening for incoming messages
        }
        catch (BluetoothHelper.BlueToothNotEnabledException ex)
        {Debug.LogError("Bluetooth is not enabled");}
        catch(BluetoothHelper.BlueToothNotReadyException ex)
        {Debug.LogError("BlueTooth is not ready");}
        catch(BluetoothHelper.BlueToothNotSupportedException ex)
        {Debug.LogError("Cannot connect to bluetooth, attempting failed");}

        Debug.Log("Start Initialisation");
        bluetoothHelper.OnConnected += HandleOnConnected;
        Debug.Log("Bluetooth Connection done");
        bluetoothHelper.OnConnectionFailed += HandleOnConnectionFailed;
        Debug.Log("Bluetooth Connection failed");
        bluetoothHelper.OnDataReceived += HandleOnDataReceived;
        Debug.Log("Bluetooth data received");
        Debug.Log("Start Ended");

        statusText.text = "Initialised";
    }

    public void TaskOnClick50()
    {
        statusText.text = "LED at 50BPM";
        StartCoroutine(BlinkCoroutine("1"));
        statusText.text = "LED and variables at 50BPM";

        BPanel.SetActive(true);
        NPanel.SetActive(false);
        TPanel.SetActive(false);
        APanel.SetActive(false);
    }

    public void TaskOnClick75()
    {
        statusText.text = "LED at 75BPM";
        StartCoroutine(BlinkCoroutine("2"));
        statusText.text = "LED and variables at 75BPM";

        BPanel.SetActive(false);
        NPanel.SetActive(true);
        TPanel.SetActive(false);
        APanel.SetActive(false);
    }

    public void TaskOnClick115()
    {
        statusText.text = "LED at 115BPM";
        StartCoroutine(BlinkCoroutine("3"));
        statusText.text = "LED and variables at 115BPM";

        BPanel.SetActive(false);
        NPanel.SetActive(false);
        TPanel.SetActive(true);
        APanel.SetActive(false);
    }

    public void TaskOnClick200()
    {
        statusText.text = "LED at 200BPM";
        StartCoroutine(BlinkCoroutine("4"));
        statusText.text = "LED and variables at 200BPM";

        BPanel.SetActive(false);
        NPanel.SetActive(false);
        TPanel.SetActive(false);
        APanel.SetActive(true);
    }

    public void Reset() 
    {
        BPanel.SetActive(false);
        NPanel.SetActive(false);
        TPanel.SetActive(false);
        APanel.SetActive(false);
    }

    private IEnumerator BlinkCoroutine(string message)
    {
        iffunction.text = "Loop entered";

        while (true)
        {
            iffunction.text = "Entering if";

            if (Input.GetMouseButtonDown(0))
            {
                iffunction.text = "Loop broken";
                break;
            }
        
            iffunction.text = "Out of if";

            bluetoothHelper.SendData(Encoding.ASCII.GetBytes(message));

            iffunction.text = "Before Waiting";

            yield return new WaitForSeconds(0);

            iffunction.text = "After Waiting";            
        }

        iffunction.text = "Out of while";
    }

    void OnApplicationQuit() 
    {
        bluetoothHelper.Disconnect();
    }          

    void OnDestroy()
	{
        btn50BPM.onClick.RemoveListener (TaskOnClick50);
        btn75BPM.onClick.RemoveListener (TaskOnClick75);
        btn115BPM.onClick.RemoveListener (TaskOnClick115);
        btn200BPM.onClick.RemoveListener (TaskOnClick200);

		if(bluetoothHelper!=null) {bluetoothHelper.Disconnect();}
	}

    private void HandleOnConnected(BluetoothHelper helper)
    {statusText.text = "Connected to Arduino";}

    private void HandleOnConnectionFailed(BluetoothHelper helper)
    {statusText.text = "Connection failed";}

    private void HandleOnDataReceived(BluetoothHelper helper)
    {
        statusText.text = "Handle on Data Recieved Function entered";

        byte[] data = Encoding.ASCII.GetBytes(helper.Read());
        stringBuilder.Append(Encoding.ASCII.GetString(data));
        if (stringBuilder.ToString().EndsWith("\n"))
        {
            statusText.text = stringBuilder.ToString().Trim();
            stringBuilder.Length = 0;
        }

        statusText.text = "Handle on Data Recieved Function exit";
    }
}