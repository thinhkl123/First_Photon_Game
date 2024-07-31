using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button reStartBtn;
    [SerializeField] private Button homeBtn;
    [SerializeField] private GameObject waitingText;

    [SerializeField] private PhotonView view;


    private void Start()
    {
        resultText.text = "Score: " + GameManager.Ins.Score.ToString();

        /*
        reStartBtn.onClick.RemoveAllListeners();
        reStartBtn.onClick.AddListener(() =>
        {
            OnClickReStartBtn();
        });

        homeBtn.onClick.RemoveAllListeners();
        homeBtn.onClick.AddListener(() =>
        {
            OnClickHomeBtn();
        });
        */


        if (!PhotonNetwork.IsMasterClient)
        {
            reStartBtn.gameObject.SetActive(false);
            //reStartBtn.SetActive(false);
            homeBtn.gameObject.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    public void OnClickReStartBtn()
    {
        view.RPC(nameof(OnClickReStartBtnRPC), RpcTarget.All);
    }
    public void OnClickHomeBtn()
    {
        view.RPC(nameof(OnClickHomeBtnRPC), RpcTarget.All);
    }

    [PunRPC] void OnClickReStartBtnRPC()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    
    [PunRPC] void OnClickHomeBtnRPC()
    {
        PhotonNetwork.LeaveRoom();
    }
    

}
