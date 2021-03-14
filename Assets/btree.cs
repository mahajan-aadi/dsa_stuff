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
        delete_element(data);
    }
    void delete_element(int data)
    {
        List<btree_node> nodes = path(data);
        btree_node main = nodes[0];
        if (!main.keys.Contains(data)) { print("not present"); return; }
        else if ((main.isleaf && main.keys.Count > min_keys) || (main == root && main.keys.Count > 1) || (main == root && main.children.Length == 0))
        { main.keys.Remove(data); main.keys.Sort(); }
        else if ((main.isleaf && main != root) )
        {
            bool temp=leaf_delete(nodes[1], main);
            if (!temp) { leaf_mix(data, nodes[1], main); }
            else { main.keys.Remove(data); main.keys.Sort(); }
        }
        else
        {
            int temp_index = Array.IndexOf(main.keys.ToArray(), data);
            int temp = 0;
            temp = max_value_of_left(main.children[temp_index]);
            delete_element(temp);
            List<btree_node> temp_nodes = path(data);
            temp_nodes[0].keys.Remove(data);
            temp_nodes[0].keys.Add(temp);
            temp_nodes[0].keys.Sort();          
        }

    }

    private void leaf_mix(int data, btree_node upper_node, btree_node main)
    {
        bool con = upper_node == root && upper_node.keys.Count == 1;
        btree_node upper_upper_node = null;
        if (upper_node.keys.Count == 1 && main != null) { upper_upper_node = path(data)[2]; }
        bool leaf_condition = true;
        int index = Array.IndexOf(upper_node.children, main);
        List<int> new_key = new List<int>();
        List<btree_node> my_children = new List<btree_node>();
        if (index != 0)
        {
            foreach (int insert in upper_node.children[index - 1].keys) { new_key.Add(insert); }
            if (real_children(upper_node.children[index - 1].children).Count > 0)
            {
                leaf_condition = false;
                foreach (btree_node child in real_children(upper_node.children[index - 1].children)) { my_children.Add(child); }
            }
        }
        foreach (int insert in main.keys) { new_key.Add(insert); }
        foreach (btree_node child in real_children(main.children)) { my_children.Add(child); leaf_condition = false; }
        if (index==0)
        {
            if (real_children(upper_node.children[index + 1].children).Count > 0)
            {
                leaf_condition = false;
                foreach (btree_node child in real_children(upper_node.children[index + 1].children)) { my_children.Add(child); }
            }
            foreach (int insert in upper_node.children[index + 1].keys) { new_key.Add(insert); }
        }
        if (index != 0) { new_key.Add(upper_node.keys[index - 1]); upper_node.keys.RemoveAt(index - 1); }
        else { new_key.Add(upper_node.keys[index]); upper_node.keys.RemoveAt(index); }
        new_key.Remove(data);
        upper_node.keys.Sort();
        new_key.Sort();
        btree_node new_child = new btree_node(max_keys);
        new_child.keys = new_key;
        if (!leaf_condition) { new_child.isleaf = false; }
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
        new_child.children = new btree_node[max_keys + 2];
        for (int q = 0; q < my_children.Count; q++) { new_child.children[q] = my_children[q]; }
        if (con ) { root = new_child; return; }
        if (upper_node.keys.Count < min_keys && upper_node !=root)
        {
            if (upper_upper_node != null)
            {
                bool temp = leaf_delete(upper_upper_node, upper_node);
                if (!temp)
                {
                    leaf_mix(data, upper_upper_node, upper_node);
                }
            }
            else
            {
                int temp_index = Array.IndexOf(path(data).ToArray(), upper_node);
                bool temp = leaf_delete(path(data)[temp_index + 1], upper_node);
                if (!temp)
                {
                    leaf_mix(data, path(data)[temp_index + 1], upper_node);
                }
            }
        }
    }

    private bool leaf_delete(btree_node upper_node, btree_node main)
    {
        btree_node temp = root;
        int index= Array.IndexOf(upper_node.children, main);
        string type = "";
        if (upper_node.children[0] != main)
        {
            temp = upper_node.children[index - 1];
            if (temp.keys.Count > min_keys) { type = "max"; }
        }
        if (type == "")
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
                List<btree_node> going_element = real_children(temp.children);
                going_element.Reverse();
                if (going_element.Count > 0) { right_c.Insert(0, going_element[0]); }
                int temp_index = Array.IndexOf(temp.children, going_element);
                for(int i = 0; i < temp_index; i++) { left_c.Add(temp.children[i]); }
                for(int i = temp_index; i < max_keys + 2; i++) { left_c.Add(null); }
            }
            else
            {
                foreach (btree_node child in main.children) { left_c.Add(child); }
                left_c.Insert(real_children(main.children).Count,temp.children[0]);
                for (int i = 1; i < max_keys + 2; i++) { right_c.Add(temp.children[i]); }
                right_c.Add(null);
            }
            int removable_data = 0;
            if (type == "max") { removable_data = temp.keys[temp.keys.Count - 1]; }
            else { removable_data = temp.keys[0]; }
            temp.keys.Remove(removable_data);
            upper_node.keys.Add(removable_data);
            upper_node.keys.Sort();
            index = upper_node.keys.IndexOf(removable_data);
            main.children =new btree_node [max_keys+2];
            temp.children =new btree_node [max_keys+2];
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
    int max_value_of_left(btree_node current)
    {
        int temp = 0;
        while (current != null)
        {
            temp = current.keys[current.keys.Count - 1];
            List<btree_node> current_list = real_children(current.children);
            if (current_list.Count < 1) { break; }
            current_list.Reverse();
            current = current_list[0];
        }
        return temp;
    }
    List<btree_node> real_children(btree_node[] children)
    {
        List<btree_node> temp = new List<btree_node>(max_keys);
        foreach(btree_node child in children)
        {
            if (child != null) { temp.Add(child); }
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
        }
    }
}
