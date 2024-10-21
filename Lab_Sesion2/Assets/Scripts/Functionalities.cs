using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Functionalities : MonoBehaviour
{
    public Toggle tcp;
    public Toggle udp;

    public GameObject MainMenuSec;
    public GameObject LoginSec;
    public GameObject ConnectionSec;
    public GameObject InputSec;
    public GameObject LobyViewSec;

    public TMP_InputField inputUsername;
    public TMP_InputField inputIP;
    public TMP_InputField inputChatTxt;

    public GameObject Conections;
    public GameObject MessagePrefab;
    public GameObject ViewScrollContent;

    public string userName;
    bool isHost = false;
    bool isChatting = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isChatting)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string message = inputChatTxt.text;
                inputChatTxt.text = "";

                OnSendMessage(message);
            }
        }
    }

    public void SetUserName()
    {
        userName = inputUsername.text;
        inputUsername.text = "";
        LoginSec.SetActive(false);
        ConnectionSec.SetActive(true);
    }

    public void ButtonHost()
    {
        if (tcp.isOn)
            Conections.GetComponent<ServerTCP>().startServer();
        //else
            //Conections.GetComponent<ServerUDP>().startServer();

        MainMenuSec.SetActive(false);
        LobyViewSec.SetActive(true);
        isHost = true;
        isChatting = true;
    }

    public void ButtonJoin()
    {
        ConnectionSec.SetActive(false);
        InputSec.SetActive(true);
    }

    public void JoinIP()
    {
        if (tcp.isOn)
            Conections.GetComponent<ClientTCP>().serverIp = inputIP.text;
        //else
            //Conections.GetComponent<ClientUDP>().serverIp = inputIP.text;

        MainMenuSec.SetActive(false);
        LobyViewSec.SetActive(true);
        ConnectionSec.SetActive(true);
        InputSec.SetActive(false);
        isChatting = true;
    }

    public void ExitLoby()
    {
        if (tcp.isOn)
            Conections.GetComponent<ClientTCP>().Disconnect();
        //else
            //Conections.GetComponent<ClientUDP>().Disconnect();

        MainMenuSec.SetActive(true);
        LobyViewSec.SetActive(false);
        isHost = false;
        isChatting = false;
    }

    public void OnSendMessage(string message)
    {
        if (isHost)
        {
            if (tcp.isOn)
                Conections.GetComponent<ServerTCP>().BroadcastMessageServer(message, null);
            //else
            //    Conections.GetComponent<ServerUDP>().BroadcastMessageServer(message, null);
        }
        else
        {
            if (tcp.isOn)
                Conections.GetComponent<ClientTCP>().Send(message);
            //else
            //    Conections.GetComponent<ClientUDP>().Send(message);
        }
    }

    public void InstanciateMessage(string message)
    {

    }
}
