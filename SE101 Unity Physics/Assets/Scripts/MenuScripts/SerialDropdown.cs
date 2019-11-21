using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class SerialDropdown : MonoBehaviour {

    // Filter out
    // /dev/tty
    // COM3
    // COM4

    public Dropdown dropdown;

    void Start () {
        PopulateList();	
	}
	
	void PopulateList() {
        List<string> ports = new List<string>(SerialPort.GetPortNames());
        dropdown.AddOptions(ports);
    }
}
