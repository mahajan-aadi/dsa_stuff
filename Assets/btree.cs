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
        this.min_keys = Mathf.FloorToInt(Mathf.Ceil(max_keys / 2));
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
        for (int p = 0, q = 0; p < max_keys + 2; q++, p++)
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
    public void delete(int data)
    {
        delete_element(data, false);
    }
    void delete_element(int data, bool remix)
    {
        List<btree_node> nodes = path(data);
        btree_node main = nodes[0];
        if (!main.keys.Contains(data)) { print("not present"); return; }
        else if ((main.isleaf && main.keys.Count > min_keys) || (main == root && main.keys.Count > 1) || (main == root && main.children.Length == 0))
        { main.keys.Remove(data); main.keys.Sort(); }
        else if ((main.isleaf && main != root) || remix )
        {
            bool temp=leaf_delete(nodes[1], main, root);
            if (!temp) { leaf_mix(data, nodes[1], main); }
            else { main.keys.Remove(data); main.keys.Sort(); }
        }

    }

    private void leaf_mix(int data, btree_node upper_node, btree_node main)
    {
        int index = Array.IndexOf(upper_node.children, main);
        List<int> new_key = new List<int>();
        if (index != 0) { foreach (int insert in upper_node.children[index - 1].keys) { new_key.Add(insert); } }
        else { foreach (int insert in upper_node.children[index + 1].keys) { new_key.Add(insert); } }
        foreach (int insert in main.keys) { new_key.Add(insert); }
        if (index != 0) { new_key.Add(upper_node.keys[index - 1]); upper_node.keys.RemoveAt(index - 1); }
        else { new_key.Add(upper_node.keys[index]); upper_node.keys.RemoveAt(index); }
        new_key.Remove(data);
        upper_node.keys.Sort();
        new_key.Sort();
        btree_node new_child = new btree_node(max_keys);
        new_child.keys = new_key;
        btree_node[] children = new btree_node[max_keys + 2];
        for (int i = 0, q = 0; q < max_keys + 2; i++, q++)
        {
            if ((index == 0 && i == 0) || (i == index - 1))
            {
                children[i] = new_child;
                q++;
            }
            else { children[i] = upper_node.children[q]; }
        }
        upper_node.children = children;
        if (upper_node.keys.Count < min_keys)
        {
            bool temp = leaf_delete(path(data)[2], upper_node, root);
            //if (!temp) { leaf_mix(data, upper_node, upper_node); }
        }
    }

    private bool leaf_delete(btree_node upper_node, btree_node main, btree_node temp)
    {
        int index= Array.IndexOf(upper_node.children, main);
        string type = "";
        if (upper_node.children[0] != main)
        {
            temp = upper_node.children[index - 1];
            if (temp.keys.Count > min_keys) { type = "max"; }
        }
        if (last_real(upper_node.children) != main && type == "")
        {
            temp = upper_node.children[index + 1];
            if (temp!=null && temp.keys.Count > min_keys) { type = "min"; }
        }
        if (type == "min" || type == "max")
        {
            List<btree_node> left_c = new List<btree_node>();
            List<btree_node> right_c = new List<btree_node>();
            if (type == "max")
            {
                foreach (btree_node child in main.children) { right_c.Add(child); }
                btree_node going_element = last_real(temp.children);
                right_c.Insert(0,going_element);
                int temp_index = Array.IndexOf(temp.children, going_element);
                for(int i = 0; i < temp_index; i++) { left_c.Add(temp.children[i]); }
                for(int i = temp_index; i < max_keys + 2; i++) { left_c.Add(null); }
            }
            else
            {
                foreach (btree_node child in main.children) { left_c.Add(child); }
                left_c.Add(temp.children[0]);
                for (int i = 1; i < max_keys + 2; i++) { right_c.Add(temp.children[i]); }
            }
            int removable_data = 0;
            if (type == "max") { removable_data = temp.keys[temp.keys.Count - 1]; }
            else { removable_data = temp.keys[0]; }
            temp.keys.Remove(removable_data);
            upper_node.keys.Add(removable_data);
            upper_node.keys.Sort();
            index = upper_node.keys.IndexOf(removable_data);
            if (type == "max") 
            {
                removable_data = upper_node.keys[++index];
                for (int i = 0; i < max_keys; i++) { main.children[i] = right_c[i]; }
                for (int i = 0; i < max_keys; i++) { temp.children[i] = left_c[i]; }
            }
            else 
            {
                removable_data = upper_node.keys[--index];
                for (int i = 0; i < max_keys; i++) { main.children[i] = left_c[i]; }
                for (int i = 0; i < max_keys; i++) { temp.children[i] = right_c[i]; }
            }
            upper_node.keys.Remove(removable_data);
            main.keys.Add(removable_data);
            main.keys.Sort();
            return true;
        }
        else { return false; }
    }

    btree_node last_real(btree_node[] children)
    {
        btree_node temp = new btree_node(max_keys);
        foreach(btree_node child in children)
        {
            if (child != null) { temp = child; }
        }
        return temp;
    }
    List<btree_node> path(int data)
    {
        btree_node current = root;
        List<btree_node> path_node = new List<btree_node>();
        while (current != null)
        {
            path_node.Add(current);
            for (int i = 0; i < current.keys.Count; i++)
            {
                if (data == current.keys[i])
                {
                    path_node.Reverse();
                    return path_node;
                }
                if (data < current.keys[i])
                {
                    current = current.children[i];
                    break;
                }
                if (i == current.keys.Count - 1) { current = current.children[i + 1]; break; }
            }
        }
        path_node.Reverse();
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
            //else { print("=="); }
            i++;
        }
    }
}
