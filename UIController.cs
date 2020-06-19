using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public bool UIactive = false;
    public bool UIbuilt = false;
    public bool UIconfirm = false;
    List<UIclass> uic = new List<UIclass>();
    public bool selections = false;
    public GameObject currentselection;
    public List<GameObject> selectables;

    public UIclass GetItem(int thisid)
    {
        return uic.Find(x => x.buttonid == thisid);
    }

    public void uiHide(int id, bool hide)
    {
        UIclass indexer = GetItem(id);
        if (hide)
        {
            indexer.buttonobj.transform.position = indexer.buttonobjoriginal; 
        }
        else
        {
            indexer.buttonobj.transform.position = indexer.buttonobjhidden; 
        }
    }

    public void buttonclicked(UIclass button)
    {
        if (button.buttonid == 1)
        {
            currentselection.GetComponent<Interactable>().destroychild();
            currentselection.GetComponent<Interactable>().applychild(3);
        }
        if (button.buttonid == 2)
        {
            currentselection.GetComponent<Interactable>().destroychild();
            currentselection.GetComponent<Interactable>().applychild(2);
        }
        if (button.buttonid == 3)
        {
            currentselection.GetComponent<Interactable>().destroychild();
            currentselection.GetComponent<Interactable>().applychild(8);
        }
        if (button.buttonid == 4)
        {
            currentselection.GetComponent<Interactable>().destroychild();
            currentselection.GetComponent<Interactable>().applychild(7);
        }
        // --------------------------
        // delete button with confirm
        if (button.buttonid == 6)
        {
            UIconfirm = true;
        }
        if (button.buttonid == 7 && UIconfirm == true)
        {
            currentselection.GetComponent<Interactable>().destroychild();
            UIconfirm = false;
        }
    }    


    private void Start()
    {
        Screen.SetResolution(1024, 768, false);
        Dictionary<string, int> buttontestpossible = new Dictionary<string, int>();
        buttontestpossible.Add("test text for button meow meow meow", 1);
        uic.Add(new UIclass(1, "button1", "Build Windmill", -100, buttontestpossible));
        uic.Add(new UIclass(2, "button2", "Build Farm", -100, buttontestpossible));
        uic.Add(new UIclass(3, "button3", "Build Factory", -100, buttontestpossible));
        uic.Add(new UIclass(4, "button4", "Build Castle", -100, buttontestpossible));
        uic.Add(new UIclass(5, "bottom button", "info about the objects here later, move with wasd, construction/destruction works now", -600, buttontestpossible));
        uic.Add(new UIclass(6, "destroy button", "Destroy", -100, buttontestpossible));
        uic.Add(new UIclass(7, "confirm button", "confirm", -100, buttontestpossible));
        foreach (var item in uic)
        {
            var tempitem = item;
            item.buttonobj.onClick.AddListener(() => buttonclicked(tempitem));
            Debug.Log("built ui item " + item.buttondescription.ToString());
            item.buttonobjtext.text = item.buttondescription;
        }
        UIbuilt = true;
    }

    void Update()
    {
        if (UIbuilt)
        {
            if (UIactive)
            {
                uiHide(1, false);
                uiHide(2, false);
                uiHide(3, false);
                uiHide(4, false);
                uiHide(5, false);
                uiHide(6, false);
            }
            else
            {
                uiHide(1, true);
                uiHide(2, true);
                uiHide(3, true);
                uiHide(4, true);
                uiHide(5, true);
                uiHide(6, true);
            }
            if (UIconfirm)
            {
                uiHide(7, false);
            }
            else
            {
                uiHide(7, true);
            }
        }

    }
}
