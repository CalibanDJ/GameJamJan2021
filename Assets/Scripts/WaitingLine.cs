using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingLine : MonoBehaviour
{
    private IList<Client> clients = new List<Client>();

    public int maxClients = 5;
    public float spaceBeetween = 1.0f;
    public float xRand = 1.0f;

    public void addClient(Client cl)
    {
        cl.transform.SetParent(transform);
        bool first = clients.Count == 0;
        cl.transform.position = new Vector3(transform.position.x + Random.Range(-xRand, xRand), first ? transform.position.y : clients[clients.Count - 1].transform.position.y + spaceBeetween, first ? transform.position.z : clients[clients.Count - 1].transform.position.z + 0.1f);

        cl.setActive(first);
        clients.Add(cl);
    }

    public int getClientCount()
    {
        return clients.Count;
    }

    public bool isFull()
    {
        return clients.Count >= maxClients;
    }

    public void nextClient()
    {
        clients.RemoveAt(0);
        foreach (Client cl in clients)
        {
            Vector3 oldPos = cl.transform.position;
            cl.transform.position = oldPos + Vector3.down * spaceBeetween;
        }

        if (clients.Count > 0)
        {
            clients[0].setActive(true);
        }
    }
}
