using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        red_black_tree tr = new red_black_tree();
        int[] a = new int[] { 15, 35, 30, 55, 68, 90, 80, 70, 65, 50 };
        tr.post_order_to_tree(a);
        tr.root.right.right.Colors = binary_tree<int>.binary_node<int>.Colour.red;
        tr.root.right.right.right.right.Colors = binary_tree<int>.binary_node<int>.Colour.red;
        int[] b = new int[] { 55, 30, 90, 80, 50, 35, 15, 65, 68 };
        foreach (int i in b) { tr.remove(i); }
        tr.postorder_traversal();

    }

}
