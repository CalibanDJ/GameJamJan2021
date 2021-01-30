using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingLine : MonoBehaviour
{
    private IList<Client> clients;

    public float spaceBeetween = 10.0f;

    public void addClient(Client cl)
    {
        clients.Add(cl);
        cl.transform.SetParent(transform);
        cl.transform.position = new Vector3(transform.position.x + Random.Range(-10.0f, 10.0f), clients.Count > 0 ? clients[clients.Count - 1].transform.position.y + spaceBeetween : transform.position.y, 0.0f);
    }

    public void nextClient()
    {
        clients.RemoveAt(0);
        foreach (Client cl in clients)
        {
            Vector3 oldPos = cl.transform.position;
            cl.transform.position = oldPos + Vector3.down * spaceBeetween;
        }
    }
}
