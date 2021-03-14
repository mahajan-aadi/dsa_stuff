using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int order = 3;
        btree tr = new btree(order - 1);
        //int[] a = new int[] { 4, 5, 10, 14, 15, 20, 23, 27, 50, 51, 52, 60, 64, 65, 70, 72, 73, 80, 81,
        //                      82, 90, 92, 93, 95, 100, 110, 6, 16, 68, 75, 77, 78, 79, 89, 111 };
        int[] a = new int[] { 3, 10, 17, 30, 38, 2, 7, 32, 41, 48, 16, 20, 14, 24 };
        foreach (int i in a) { tr.insert(i); }
        //int[] b = new int[] { 64, 23, 72, 65, 20, 70, 95, 77, 80, 100, 6, 27, 60, 16, 50, 81 };
        //int[] b = new int[] { 38, 41, 20, 24, 3 };
        int[] b = new int[] { 38, 41, 20, 24, 3 };
        foreach (int i in b) { tr.delete(i); }
        tr.printing();

    }

}
