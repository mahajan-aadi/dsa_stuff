using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splay_tree : binary_search_tree
{
    public splay_tree(int root_data):base(root_data) { }
    public override void insert(int data)
    {
        binary_search_node temp = new binary_search_node(data);
        insert_data(root, temp, null);
        Splaying(temp);
    }

    void Splaying(binary_node<int> node)
    {
        if (node == root) { return; }
        List<binary_node<int>> path_node = path(node);
        binary_node<int> temp=new binary_node<int>(0);
        binary_node<int> temp2=new binary_node<int>(0);
        binary_node<int> gp = new binary_node<int>(0);
        while (root != node)
        {
            if (root.right == node) { root = right_right(root); }
            else if (root.left == node) { root = left_left(root); }
            else
            {
                if (path_node.Count < 4) { temp = root; gp = temp; }
                else { temp = path_node[path_node.Count - 4]; gp = path_node[path_node.Count - 3]; }
                if (gp.data>node.data)
                {
                    if (gp.left.left == node) 
                    {
                        temp2 = left_left(gp);
                        temp2 = left_left(temp2);
                    }
                    else if (gp.left.right == node) { temp2 = left_right(gp); }
                }
                else
                {
                    if (gp.right.right == node) 
                    {
                        temp2 = right_right(gp);
                        temp2 = right_right(temp2);
                    }
                    else if (gp.right.left == node) { temp2 = right_left(gp); }
                }
                if (temp == root&& path_node.Count<=3) { root = node; }
                else
                {
                    if (temp.data < node.data) { temp.right = temp2; }
                    else if (temp.data > node.data) { temp.left = temp2; }
                    path_node = path(node);
                }
            }
        }
    }

    public override void remove(int data){ bottom_up_splay(data); }
    public void bottom_up_splaying(int data){ bottom_up_splay(data); }
    void bottom_up_splay(int data)
    {
        binary_search_node temp = new binary_search_node(data);
        int temp_data = root.data;
        List<binary_node<int>> path_node = path(temp);
        base.remove(data);
        if (temp.data == temp_data) { return; }
        if (temp.data == path_node[path_node.Count - 1].data){ Splaying(path_node[path_node.Count - 2]); }
        else { Splaying(path_node[path_node.Count - 1]); }
    }
    public void top_down_splaying(int data)
    {
        top_down_splay(data);
    }

    void top_down_splay(int data)
    {
        binary_node<int> temp = new binary_search_node(data);
        List<binary_node<int>> path_node = path(temp);
        Splaying(path_node[path_node.Count - 1]);
        if (root.left == null)
        {
            root = root.right;
            return;
        }
        temp = root.right;
        root = root.left;
        Splaying(max_value(root));
        root.right = temp;
    }
    binary_node<int> max_value(binary_node<int> node)
    {
        while (node.right != null) { node = node.right; }
        return node;
    }
}
