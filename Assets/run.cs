using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        avl_tree tr = new avl_tree(14);
        int[] arr= new int[] { 17,11,7,53,4,13,12,8,60,19,16,20 };
        foreach(int i in arr) { tr.insert(i); }
        tr.remove(8);
        tr.remove(7);
        tr.remove(11);
        tr.remove(14);
        tr.remove(17);
        tr.inorder_traversal(tr.root);*/
        //(correct post_order_to_tree function of binary_search_tree)
        splay_tree tr = new splay_tree(12);
        int[] arr = new int[] { 7,10,15,17,16,14,13 };
        tr.post_order_to_tree(arr);
        int[] arrr = new int[] { 16,12,7 };
        //int[] arrr = new int[] { 12 };
        foreach (int i in arrr) { tr.top_down_splaying(i); }
        tr.inorder_traversal();
        print("////");
        tr.preorder_traversal();

    }

}
