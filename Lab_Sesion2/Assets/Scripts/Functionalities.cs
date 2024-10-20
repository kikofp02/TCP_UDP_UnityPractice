using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Functionalities : MonoBehaviour
{
    public Toggle tcp;
    public Toggle udp;

    public GameObject MainMenu;
    public GameObject Connection;
    public GameObject Input;
    public GameObject LobyView;

    public InputField inputIP;
    public InputField messageInputField;

    public GameObject ConectionUDP;
    public GameObject ConectionTCP;

    public GameObject ClientUDP;
    public GameObject ClientTCP;

    private void Start()
    {
        
    }

    public void ButtonHost()
    {
        if (tcp.isOn)
            ConectionTCP.GetComponent<ServerTCP>().startServer();
        else
            ConectionUDP.GetComponent<ServerUDP>().startServer();

        MainMenu.SetActive(false);
        LobyView.SetActive(true);
    }

    public void ButtonJoin()
    {
        Connection.SetActive(false);
        Input.SetActive(true);
    }

    public void JoinIP()
    {
        if (tcp.isOn)
            ClientTCP.GetComponent<ClientTCP>().serverIp = inputIP.text;
        else
            ClientUDP.GetComponent<ClientUDP>().serverIp = inputIP.text;

        MainMenu.SetActive(false);
        LobyView.SetActive(true);
        Connection.SetActive(true);
        Input.SetActive(false);
    }

    public void ExitLoby()
    {
        if (tcp.isOn)
            ClientTCP.GetComponent<ClientTCP>().Disconnect();
        else
            ClientUDP.GetComponent<ClientUDP>().Disconnect();

        MainMenu.SetActive(true);
        LobyView.SetActive(false);
    }
}
