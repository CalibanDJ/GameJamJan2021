using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drawer : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public GameObject drawerOpen;
    
    private int width;
    private int height;
    private int numberOfItems;
    private DragDrop[,] items;
    private GameObject GODrawerOpen;


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GODrawerOpen.SetActive(true);
    }

    void Start()
    {
        GODrawerOpen = Instantiate(drawerOpen, new Vector3(0, 0, 0), Quaternion.identity);
        GODrawerOpen.transform.SetParent(transform);
        GODrawerOpen.transform.position = transform.position;
        GODrawerOpen.SetActive(false);
    }

    public void SetSize(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.numberOfItems = 0;

        this.items = new DragDrop[width , height];

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            InsertItem(eventData.pointerDrag.GetComponent<DragDrop>());
        }
    }

    private void InsertItem(DragDrop item)
    {
        if(numberOfItems < width * height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (items[i, j] == null)
                    {
                        Debug.Log("Item save");
                        item.GetComponent<Image>().enabled = false;
                        items[i, j] = item;
                        numberOfItems++;
                        return;
                    }
                }
            }
        }
    }

    
}
