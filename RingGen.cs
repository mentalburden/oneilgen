using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.UI;

public class RingGen : MonoBehaviour
{
    public GameObject[] tiles;
    public int ringsides;
    public int ringradius;
    public int cylinderlength;
    public List<GameObject> ringholder;
    public int calculatedlength;
    public bool cameraRelease = false;
    UIcontroller uic;

    public static int getrand(int min, int max)
    {
        if (min >= max)
        {
            return min;
        }
        byte[] intBytes = new byte[4];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetNonZeroBytes(intBytes);
        }
        return min + Math.Abs(BitConverter.ToInt32(intBytes, 0)) % (max - min + 1);
    }

    void rungen(int sides, int raydist)
    {
        calculatedlength = Mathf.CeilToInt(Mathf.Sin(3.14f / sides) * (raydist * 2.07f));
        int thisangle = (360 / sides);
        int thisstarty = 0;
        for (int ring = 0; ring < cylinderlength; ring++)
        {
            transform.position = new Vector3(0,thisstarty,0);
            for (int side = 1; side < sides + 1; side++)
            {
                int myangle = thisangle * side;
                int rando = getrand(0, tiles.Length -1);
                transform.rotation = Quaternion.Euler(0, myangle, 0);
                Debug.Log(myangle);
                Quaternion rotato = Quaternion.Euler(0, myangle, 0);
                GameObject chonker = Instantiate(tiles[rando], fireray(raydist), rotato);
                uic.selectables.Add(chonker);
                ringholder.Add(chonker);
            }
            thisstarty += calculatedlength;
        }
        foreach (var item in ringholder)
        {
            item.transform.localScale = new Vector3(calculatedlength, calculatedlength, 1);            
        }
        ringholder.RemoveRange(0,ringholder.Count);
        cameraRelease = true;
    }

    public Vector3 fireray(int raydist)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 chonk = ray.GetPoint(raydist);
        Debug.Log(chonk.ToString());
        return chonk;
    }

    void Start()
    {
        uic = GameObject.Find("UIController").GetComponent<UIcontroller>();
        rungen(ringsides, ringradius);
    }
}
