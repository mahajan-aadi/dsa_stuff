using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_node <T>: MonoBehaviour
{
    public T data;
    public tree_node<T>[] children;
    public tree_node(T data)
    {
        this.data = data;
    }
    public void set_children(tree_node<T>[] nodes)
    {
        this.children = nodes;
    }
}
