using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        btree tr = new btree(4);
        int[] a = new int[] { 4, 8, 26, 11, 2, 16, 17, 5, 1, 19, 23, 20, 3, 12, 14, 25, 13 };
        //int[] a = new int[] { 4, 8, 26, 11, 2, 16, 17, 5, 1, 19, 23, 20, 3 ,12};
        foreach (int i in a) { tr.insert(i); }
        tr.printing();

    }

}
