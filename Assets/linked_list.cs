using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public unsafe class linked_list:MonoBehaviour
{
    private int len;
    node* head,tail,temp;
    public unsafe struct node
    {
        public int data;
        public node* next;
    }
    public linked_list()
    {
        this.head = null;
        this.tail = null;
    }
    public void add_element(int element)
    {
        node new_node = new node();
        new_node.data = element;
        new_node.next=null;
        if (head == null){ head = &new_node; tail = &new_node; }
        else
        {
            print(head->data);
            (*tail).next = &new_node;//or tail->next = &new_node;
            tail = &new_node;
        }
        len++;
    }
    public void print_list()
    {
        temp = head;
        int i = 0;
        while (i!=len)
        {
            print(head->data);
            temp = temp->next;
            i++;
        }

    }
}
