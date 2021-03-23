using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        red_black_tree tr = new red_black_tree(10);
        int[] a = new int[] { 18, 7, 15, 16, 30, 25, 40, 60, 2, 1, 70 };
        //int[] a = new int[] { 18, 7, 15, 16, 30, 25, 40 };
        foreach (int i in a) { tr.insert(i); }
        //int[] b = new int[] { 64, 23, 72, 65, 20, 70, 95, 77, 80, 100, 6, 27, 60, 16, 50, 81 };
       // foreach (int i in b) { tr.delete(i); }
        tr.preorder_traversal();

    }

}
