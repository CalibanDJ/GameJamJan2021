using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInfo : MonoBehaviour
{

    public GameObject panel;

    public void OpenInfo()
    {
        if(panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
                StartCoroutine(wait(45f));
                if (!isOpen)
                {  
                    panel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("Des voyageurs arrivant à la porte 404 ont perdus leurs objets, quel patatra !Donnez leurs les objets trouvés correspondant à leur besoin, mais attention à ne pas vous tromper !");
                } else
                {
                    panel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("Infos");
                }
            }
        }
    }

    IEnumerator wait(float f)
    {
        yield return new WaitForSeconds(f);
    }
}
