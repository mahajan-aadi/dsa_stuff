using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binary_search_tree: binary_tree<int>
{
    public class binary_search_node : binary_node<int>
    {
        public binary_search_node(int data) : base(data) 
        {
        }
        public void set_right(int data) { return; }
        public void set_left(int data) { return; }
    }
    public binary_search_tree(int root_data)
    {
        root = new binary_search_node(root_data);
    }
    public binary_search_tree() { }
    public virtual void insert(int data)
    {
        binary_search_node temp = new binary_search_node(data);
        insert_data(root, temp,null);
    }
    protected void insert_data(binary_node<int> node, binary_search_node insert, binary_node<int> base_node)
    {
        if (node == null)
        {
            if (base_node.data > insert.data) { base_node.left= insert; return; }
            else { base_node.right = insert; return; }
        }
        else
        {
            if (node.data > insert.data) { insert_data(node.left, insert,node); }
            else if(node.data < insert.data) { insert_data(node.right, insert,node); }
        }
        return;
    }
    public virtual void remove(int data)
    {
        remove(root, data,root);
    }

    protected void remove(binary_node<int> node, int data,binary_node<int> base_node)
    {
        if (root.data == data && height(root) < 2)
        {
            if (root.left == null && root.right == null)
            {
                root = null;
                return;
            }
            else if (root.left == null)
            {
                root = root.right;
                return; 
            }
            else if (root.right == null) 
            {
                root = root.left;
                return;
            }
        }
        if (node == null) { return ; }
        if (node.data > data) { remove(node.left, data,node); }
        else if (node.data < data) { remove(node.right, data,node); }
        else
        {
            if(height(node)==0)
            {
                if (base_node.data > node.data) { base_node.left = null; }
                else { base_node.right = null; }
                return;
            }
            else
            {
                int temp_data = min_value(node);
                binary_node<int> temp_node = node.right;
                if (node.data == temp_data) { temp_data = max_value(node);temp_node = node.left; }
                node.data = temp_data;
                remove(temp_node, node.data,node);
            }
        }
    }
    protected int min_value(binary_node<int> node)
    {
        while (node.left != null) { node = node.left; }
        return node.data;
    }
    protected int max_value(binary_node<int> node)
    {
        while (node.right != null) { node = node.right; }
        return node.data;
    }
    public void post_order_to_tree(int[] arr)
    {
        Array.Reverse(arr);
        post_order_to_tree_convert(arr);
    }
    void post_order_to_tree_convert(int[] postorder)
    {
        root = new binary_tree<int>.binary_node<int>(postorder[0]);
        foreach (int i in postorder)
        {
            if (i == postorder[0]) { continue; }
            binary_search_node temp = new binary_search_node(i);
            insert_data(root, temp, null);
        }
    }
    public void pre_order_to_tree(int[] arr)
    {
        post_order_to_tree_convert(arr);
    }
    protected List<binary_node<int>> path(binary_node<int> node)
    {
        List<binary_node<int>> arr = new List<binary_node<int>>();
        binary_node<int> main_node = root;
        while (main_node != null)
        {
            if (node.data == main_node.data) { arr.Add(main_node); return arr; }
            if (node.data < main_node.data)
            {
                arr.Add(main_node);
                main_node = main_node.left;
            }
            else
            {
                arr.Add(main_node);
                main_node = main_node.right;
            }
        }
        return arr;
    }
    protected binary_node<int> left_left(binary_node<int> i)
    {
        binary_node<int> temp_parent = i.left;
        temp_parent.left = i.left.left;
        i.left = temp_parent.right;
        temp_parent.right = i;
        return temp_parent;
    }
    protected binary_node<int> right_right(binary_node<int> i)
    {
        binary_node<int> temp_parent = i.right;
        temp_parent.right = i.right.right;
        i.right = temp_parent.left;
        temp_parent.left = i;
        return temp_parent;
    }
    protected binary_node<int> right_left(binary_node<int> i)
    {
        binary_node<int> check = left_left(i.right);
        i.right = check;
        return right_right(i);

    }
    protected binary_node<int> left_right(binary_node<int> i)
    {
        binary_node<int> check = right_right(i.left);
        i.left = check;
        return left_left(i);
    }
}
