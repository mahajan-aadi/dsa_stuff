using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binary_tree <T>: MonoBehaviour
{
    public binary_node<T> root;
    string data;
    public class binary_node<T>
    {
        public int bf = 0;
        public binary_node<T> left = null;
        public binary_node<T> right = null;
        public T data;
        public binary_node(T data) { this.data = data; }
        public void set_left(T data)
        {
            this.left = new binary_node<T>(data);
        }
        public void set_right(T data)
        {
            this.right = new binary_node<T>(data);
        }
        public T get_right() { return this.right.data; }
        public T get_left() { return this.left.data; }

    }
    public binary_tree(T root_data)
    {
        root = new binary_node<T>(root_data);
    }
    public binary_tree(){}
    public binary_tree(binary_node<T> root_node) { root = root_node; }


    public void inorder_traversal()
    {
        data = string.Empty;
        inorder_traversal(root);
        print(data);
    }

    private void inorder_traversal(binary_node<T> node)
    {
        if (node != null)
        {
            inorder_traversal(node.left);
            data += node.data;
            data += "  ";
            inorder_traversal(node.right);
        }
        else return;
    }

    public void preorder_traversal()
    {
        data = string.Empty;
        preorder_traversal(root);
        print(data);
    }

    private void preorder_traversal(binary_node<T> node)
    {
        if (node != null)
        {
            data += node.data;
            data += "  ";
            preorder_traversal(node.left);
            preorder_traversal(node.right);
        }
        else return;
    }

    public void postorder_traversal()
    {
        data=string.Empty;
        postorder_traversal(root);
        print(data);
    }

    private void postorder_traversal(binary_node<T> node)
    {
        if (node != null)
        {
            postorder_traversal(node.left);
            postorder_traversal(node.right);
            data += node.data;
            data += "  ";
        }
        else return;
    }

    public string[] binary_to_array()
    {
        Queue<binary_node<T>> qu = new Queue<binary_node<T>>();
        qu.Enqueue(root);
        int i = 0;
        List<string> arr = new List<string>();
        while (qu.Count > 0)
        {
            binary_node<T> temp = qu.Dequeue();
            if (temp != null)
            {
                qu.Enqueue(temp.left);
                qu.Enqueue(temp.right);
                arr.Add(temp.data.ToString());
            }
            else { arr.Add(null); }
            i++;
        }
        return arr.ToArray();
    }
    public int height(binary_node<T> node)
    {
        if (node == null) { return 0; }
        else return (Mathf.Max(height(node.left), height(node.right)) + 1);
    }
}
