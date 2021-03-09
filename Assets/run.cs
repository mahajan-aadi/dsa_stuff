using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        btree tr = new btree(4);
        int[] a = new int[] { 4, 5, 10, 14, 15, 20, 23, 27, 50, 51, 52, 60, 64, 65, 70, 72, 73, 80, 81,
                                82, 90, 92, 93, 95, 100, 110, 6, 16, 68, 75, 77, 78, 79, 89, 111 };
        foreach (int i in a) { tr.insert(i); }
        //int[] b = new int[] { 64, 23, 72, 65, 20};
        int[] b = new int[] { 82, 81};
        foreach (int i in b) { tr.delete(i); }
        tr.printing();

    }

}
