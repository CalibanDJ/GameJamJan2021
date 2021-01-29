using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBubble : MonoBehaviour
{
    public float time = 2.0f;
    private float timeLeft;
    private Client myClient;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = time;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void resetTimer()
    {
        timeLeft = time;
    }

    public void setClient(Client client)
    {
        myClient = client;
    }

    public void rejectClient()
    {
        myClient.reject();
    }
}
