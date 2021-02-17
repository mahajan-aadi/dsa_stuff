using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btree : MonoBehaviour
{
    public class btree_node
    {
        public bool isleaf;
        public List<int> keys;
        public btree_node[] children;
        public btree_node(int max) 
        {
            isleaf = true;
            keys = new List<int>();
            children = new btree_node[max + 2];
        }
    }
    int max_keys;
    int min_keys;
    public btree_node root;
    public btree(int max_keys)
    {
        this.max_keys = max_keys;
        root = new btree_node(max_keys);
    }
    int median_index()
    {
        float temp = Mathf.Floor((max_keys + 1) / 2);
        return Mathf.FloorToInt(temp);
    }
    public void insert(int data)
    {
        insert(data,root);
    }

    private void insert(int data, btree_node current)
    {

        if (current.isleaf && current.keys.Count < max_keys)
        {
            current.keys.Add(data);
            current.keys.Sort();
            return;
        }
        else if (!current.isleaf)
        {
            for (int i = 0; i < current.keys.Count; i++)
            {
                if (data < current.keys[i])
                {
                    current = current.children[i];
                    break;
                }
                if (i == current.keys.Count - 1) { current = current.children[i + 1]; break; }
            }
            insert(data, current);
        }
        else
        {
            current.isleaf = false;
            //print("qw");
            full_node_insertion(data, current);
        }
    }

    void full_node_insertion(int data, btree_node btree_node)
    {
        List<btree_node> nodes = path(data);
        btree_node.keys.Add(data);
        btree_node.keys.Sort();
        for(int i=0;i<nodes.Count - 1;i++)
        {
            correct_insertion(nodes[i],nodes[i+1]);
        }
        if (nodes[nodes.Count - 1].keys.Count == max_keys+1)
        {
            root = new btree_node(max_keys);
            root.isleaf = false;
            correct_insertion(nodes[nodes.Count - 1], root);
        }
    }

    void correct_insertion(btree_node node,btree_node parent)
    {
        if (node.keys.Count <= max_keys) { return; }
        /*print(node.keys.Count);
        print(parent.keys.Count);*/
        int temp = node.keys[median_index()];
        parent.keys.Add(temp);
        parent.keys.Sort();
        int temp_index = parent.keys.IndexOf(temp);
        List<btree_node> left_c = new List<btree_node>(max_keys);
        List<btree_node> right_c = new List<btree_node>(max_keys);
        btree_node left = new btree_node(max_keys);
        btree_node right = new btree_node(max_keys);
        int i = -1;
        foreach (btree_node trial in node.children)
        {
            i++;
            //if (i == median_index()) { continue; }
            if (i< node.children.Length/2) 
            {
                left_c.Add(trial);
                if (trial != null) { left.isleaf = false; }
            }
            else 
            {
                right_c.Add(trial);
                if (trial != null) { right.isleaf = false; }
            }
        }
        i = -1;
        foreach (int trial in node.keys)
        {
            i++;
            if (i == median_index()) { continue; }
            if (i < node.keys.Count / 2) { left.keys.Add(trial); }
            else { right.keys.Add(trial); }
        }
        i = 0;
        foreach(btree_node child in left_c) { left.children[i] = child;i++; }
        i = 0;
        foreach(btree_node child in right_c) { right.children[i] = child;i++; }
        node.keys.Remove(temp);
        btree_node[] children_arr = new btree_node[max_keys + 2];
        for (int p = 0,q=0; p < max_keys + 2;q++,p++)
        {
            if (p == temp_index)
            {
                children_arr[temp_index] = left;
                children_arr[temp_index + 1] = right;
                p++;
            }
            else
            {
                children_arr[p] = parent.children[q];
            }
        }
        parent.children = children_arr;
    }

    List<btree_node> path(int data)
    {
        btree_node current = root;
        List<btree_node> path_node = new List<btree_node>();
        while (current != null)
        {
            path_node.Add(current);
            //print("qwe");
            for (int i = 0; i < current.keys.Count; i++)
            {
                /*print(i);
                print(current.keys.Count);
                print(current.keys[i]);*/
                if (data < current.keys[i])
                {
                    //print("ss");
                    current = current.children[i];
                    break;
                }
                if (i == current.keys.Count - 1) { /*print(current.children.Length);*/ current = current.children[i + 1]; break; }
                //print("YU");
            }
        }
        path_node.Reverse();
        //print(path_node.Count);
        return path_node;
    }
    public void printing()
    {
        Queue<btree_node> qu = new Queue<btree_node>();
        qu.Enqueue(root);
        int i = 0;
        while (qu.Count > 0)
        {
            btree_node temp = qu.Dequeue();
            if (temp != null)
            {
                foreach(btree_node q in temp.children) { qu.Enqueue(q); }
                string r = string.Empty;
                foreach(int q in temp.keys)
                {
                    r += q.ToString();
                    r += "  ";
                }
                print(r);
            }
            //else { print("null"); }
            i++;
        }
    }
}
