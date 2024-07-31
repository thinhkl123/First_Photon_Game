using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private PhotonView view;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 70f;
    [SerializeField] private float dashTime = 0.1f;

    [Header("Wrap")]
    [SerializeField] private float minX, maxX, minY, maxY;

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI nickName;

    private LineRenderer ray;
    private float curSpeed;

    private void Start()
    {
        curSpeed = speed;
        ray = FindObjectOfType<LineRenderer>();

        if (view.IsMine)
        {
            nickName.text = PhotonNetwork.NickName;
        }
        else
        {
            nickName.text = view.Owner.NickName;
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(x, y);
            transform.position += (Vector3) move * curSpeed * Time.deltaTime;

            Wrap();

            if (Input.GetKeyDown(KeyCode.Space) && move != Vector2.zero)
            {
                StartCoroutine(Dash());
            }

            animator.SetFloat(Constance.ANIMATOR_SPEED, move.magnitude);

            ray.SetPosition(0, transform.position);
        }
        else
        {
            ray.SetPosition(1, transform.position);
        }
    }

    IEnumerator Dash()
    {
        curSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        curSpeed = speed;
    }

    private void Wrap()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x > maxX) 
        {
            transform.position = new Vector2(minX, transform.position.y);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.CompareTag(Constance.TAG_ENEMY))
            {
                Debug.Log("Enemy");
                UIManager.Ins.GetUI<PlayUI>().DecreaseHealth();
                //PlayUI playUI = FindObjectOfType<PlayUI>();
                //playUI.DecreaseHealth();
            }
        }
    }
}
