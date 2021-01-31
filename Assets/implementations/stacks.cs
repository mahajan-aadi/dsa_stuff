using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stacks <T>: MonoBehaviour
{
    int len;
    private node<T> head;
    private node<T> temp;
    public class node<T>
    {
        public node<T> next;
        public T data;
        public node(T data) { this.data = data; }
        public node() { }
    }
    public stacks()
    {
        len = 0;
        head = null;
   }
    public int length() { return len; }
    public bool isEmpty() { return len == 0; }
    public void push(T data)
    {
        node<T> new_node = new node<T>(data);
        new_node.next = head.next;
        head.next = new_node;
        len++;
    }
    public T pop()
    {
        temp = new node<T>();
        if (len == 0) { return default(T); }
        else
        {
            temp = head.next;
            head = head.next.next;
            T data = temp.data;
            temp.data = default(T);
            temp.next = null;
            len--;
            return data;
        }

    }
    public void display()
    {
        temp = head.next;
        while (temp != null)
        {
            print(temp.data);
            temp = temp.next;
        }
    }
    public T peek() { return (head.next.data); }
}
