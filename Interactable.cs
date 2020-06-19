using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class Interactable : MonoBehaviour
{
    private Color startcolor;
    private Renderer rendy;
    UIcontroller uic;
    public bool selectable;
    public bool selected;
    public string mytype;
    public GameObject myobj;
    private GameObject thisobj;
    public Dictionary<int,GameObject> possibleobjs;
    public int possibleobjcount;

    // Model types
    /// <summary>
    /// 
    /// blank tile
    /// 
    /// strucural tile (cant be used)
    /// 
    /// food:
    ///     sheep tile
    ///     farm tile
    ///     
    /// Energy:
    ///     windmill tile
    ///     loop reactor (use placeholder)
    ///     
    /// atmosphere:
    ///     air scrubber (use placeholder)
    ///     tree farm tile
    ///     
    /// control:
    ///     castle tile
    ///     flight tower (use placeholder) 
    /// 
    /// 
    /// </summary>

    private void Start()
    {
        possibleobjs = new Dictionary<int, GameObject>();
        possibleobjs.Add(1,GameObject.Find("SHEEP"));
        possibleobjs.Add(2, GameObject.Find("FARM"));
        possibleobjs.Add(3, GameObject.Find("WINDMILL"));
        possibleobjs.Add(4, GameObject.Find("LOOPREACTOR"));
        possibleobjs.Add(5, GameObject.Find("SCRUBBER"));
        possibleobjs.Add(6, GameObject.Find("TREEFARM"));
        possibleobjs.Add(7, GameObject.Find("CASTLE"));
        possibleobjs.Add(8, GameObject.Find("TOWER"));
        possibleobjcount = possibleobjs.Count;
        rendy = GetComponent<Renderer>();
        uic = GameObject.Find("UIController").GetComponent<UIcontroller>();
        startcolor = rendy.material.color;
    }

    public void unselect()
    {
        selected = false;
        rendy.material.color = startcolor;
    }

    public GameObject Getobj(int objtype)
    {        
        foreach (var item in possibleobjs)
        {
            if (item.Key == objtype)
            {
                thisobj = item.Value;
            }
        }
        return thisobj;
    }

    public void applychild(int objtype)
    {   
        GameObject myobj = Instantiate(Getobj(objtype), this.gameObject.transform.position, Quaternion.identity);
        myobj.transform.parent = this.gameObject.transform; // set obj as child
        myobj.transform.localScale = new Vector3(0.7f, 10f, 0.7f);
        Quaternion rotato = Quaternion.Euler(270, 0, 0);
        myobj.transform.localRotation = rotato;
    }

    public void destroychild()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void OnMouseExit()
    {
        if (!selected)
        {
            rendy.material.color = startcolor;
        }        
    }

    private void OnMouseOver()
    {
        if (selectable)
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                foreach (var item in uic.selectables)
                {
                    item.GetComponent<Interactable>().unselect();
                }
                uic.currentselection = this.gameObject;
                uic.UIactive = true;
                rendy.material.color = Color.red;
                selected = true;
            }
        }
        if (!selected)
        {
            this.rendy.material.color = Color.yellow;
        }
    }

}
