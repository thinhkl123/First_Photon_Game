using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : UICanvas
{
    [Header("Health")]
    [SerializeField] private Slider heathBar;

    [Header("Goal")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void DecreaseHealth()
    {
        Debug.Log("Sender " + view.ViewID);
        view.RPC(nameof(DecreaseHealthRPC), RpcTarget.All);
    }

    public void AddScore()
    {
        view.RPC(nameof(AddScoreRPC), RpcTarget.All);
    }

    [PunRPC] void DecreaseHealthRPC()
    {
        //heathBar.value -= 0.1f;
        //Debug.Log("Reciever: " + view.ViewID + " " + heathBar.value.ToString());
        GameManager.Ins.UpdateHealth(-0.1f);
        heathBar.value = GameManager.Ins.Health;
        if (GameManager.Ins.Health <= 0f)
        {
            UIManager.Ins.OpenUI<GameOverUI>();
        }

    }

    [PunRPC] void AddScoreRPC()
    {
        GameManager.Ins.UpdateScore(1);
        scoreText.text = GameManager.Ins.Score.ToString();
    }
}
