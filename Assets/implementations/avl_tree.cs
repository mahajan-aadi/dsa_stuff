using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avl_tree : binary_search_tree
{
    public class avl_node:binary_search_node
    {
        public avl_node(int data) : base(data) {}

    }
    public avl_tree(int data)
    { root = new avl_node(data); }
    public override void insert(int data)
    {
        avl_node temp = new avl_node(data);
        insert_data(root, temp, null);
        correct_bf(path(temp));

    }
    public override void remove(int data)
    {
        base.remove(data);
        extra_remove();
    }
    void extra_remove()
    {
        Queue<binary_node<int>> qu = new Queue<binary_node<int>>();
        qu.Enqueue(root);
        List<binary_node<int>> arr = new List<binary_node<int>>();
        while (qu.Count > 0)
        {
            binary_node<int> temp = qu.Dequeue();
            if (temp != null)
            {
                qu.Enqueue(temp.left);
                qu.Enqueue(temp.right);
                arr.Add(temp); 
            }
        }
        foreach (binary_node<int> temp in arr) { correct_bf(path(temp)); }

    }
    private int balance(avl_node node)
    {
        int left_height = height(node.left);
        int right_height = height(node.right);
        return (left_height - right_height);
    }
 
    void correct_bf(List<binary_node<int>> arr)
    {
        foreach(avl_node i in arr) { i.bf = balance(i); }
        arr.Reverse();
        int c = 0;
        binary_node<int> check =new avl_node(0);
        foreach (binary_node<int> i in arr) 
        {
            if (c == 1)
            {
                if (i.left == null || i.right == check.left || i.right == check.right) { i.right = check; }
                else if (i.right == null || i.left == check.left || i.left == check.right) { i.left = check; }
                arr.Reverse();
                correct_bf(arr);
                return;
            }
            if (-1 > i.bf || i.bf > 1) 
            {
                check= check_conditions(i);
                if (check == null) { continue; }
                else{ c = 1; }
            }
        }
        if (c == 1)
        {
            root = check;
        }
    }
    binary_node<int> check_conditions(binary_node<int> i)
    {
        if (i.bf >1)
        {
            if (i.left.bf == 1) { return left_left(i); }
            else { return left_right(i); }
        }
        if (i.bf < -1)
        {
            if (i.right.bf == 1) { return right_left(i); }
            else { return right_right(i); }
        }
        else return null;
    }

}
