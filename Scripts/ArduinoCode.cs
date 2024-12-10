using System.IO.Ports;
using UnityEngine;


public class ArduinoCode : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM3", 19200);
    public string recivedstring;

    public string[] datas;

    void Start()
    {
        data_stream.Open();
    }

    void Update()
    {
        recivedstring = data_stream.ReadLine();
        string[] datas = recivedstring.Split(',');
        Debug.Log(datas[0]);
    }
}
