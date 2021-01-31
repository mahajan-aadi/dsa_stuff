using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_linked_list <T>: MonoBehaviour
{
    DNode<T> head;
    DNode<T> current;
    DNode<T> temp;
    int len;
    public class DNode<T>
    {
        public DNode<T> next=null;
        public DNode<T> previous=null;
        public T data;
        public DNode(T data) { this.data = data; }
        public DNode() { this.data = default(T); }

    }
    public double_linked_list()
    {
        temp = new DNode<T>();
        head = new DNode<T>();
        len = 0;
        current = head;
    }
    public int get_size() { return len; }
    public T get_data(int number)
    {
        DNode<T> curr = new DNode<T>();
        if (number < len / 2)
        {
            curr = head;
            for (int i = 1; i <= number; i++)
            {
                curr = curr.next;
                if (i == number) { return(curr.data); }
            }
        }
        else if(number<=len)
        {
            curr = current;
            for (int i = len; i >= number; i--)
            {
                if (i == number) { return(curr.data); }
                curr = curr.previous;
            }
        }
        return default(T);
        print("out of bounds");
    }
    public int index_of(T data)
    {
        int i = 1;
        for (DNode<T> curr = head.next; curr != null; curr = curr.next)
        {
            if (data.Equals(curr.data)) { return (i); }
            i++;
        }
        return -1;
    }
    public bool contain(T data)
    {
        int check = index_of(data);
        if (check==-1) { return false; }
        else { return true; }
    }
    public void clear()
    {
        DNode<T> curr = head.next;
        while (curr != null)
        {
            temp = curr.next;
            clear_data(curr);
            curr = temp;
        }
        len = 0;
        head.next = null;
        current = head;
    }
    public void add_last(T data)
    {
        DNode<T> new_node = new DNode<T>(data);
        if (len == 0) { current.next = new_node;current = new_node; }
        else
        {
            current.next = new_node;
            new_node.previous = current;
            current = new_node; 
        }
        len++;
    }
    public void add_first(T data)
    {
        DNode<T> new_node = new DNode<T>(data);
        if (len == 0) { current.next = new_node; current = new_node; }
        else
        {
            new_node.next = head.next;
            head.next.previous = new_node;
            head.next = new_node;
        }
        len++;
    }
    public void add_node(T data,int number)
    {
        DNode<T> new_node = new DNode<T>(data);
        if (number > len + 1) { return; }
        if (number < 2) { add_first(data); }
        else if (number == len + 1) { add_last(data); }
        else if(number <len/2)
        {
            DNode<T> curr = head;
            for (int i=1; i<=number; i++)
            {
                curr = curr.next;
                if (i == number) { add(curr,new_node); }
            }
            len++;
        }
        else
        {
            DNode<T> curr = current;
            for (int i = len; i >= number; i--)
            {
                if (i == number) { add(curr,new_node); }
                curr = curr.previous;
            }
            len++;
        }
    }
    void add(DNode<T> curr,DNode<T> new_node)
    {
        curr.previous.next = new_node;
        new_node.previous = curr.previous;
        curr.previous = new_node;
        new_node.next = curr;
    }
    public void delete_last()
    {
        if (len < 1) { return; }
        current.previous.next = null;
        temp = current;
        current = current.previous;
        len--;
    }
    public void delete_first()
    {
        temp = head.next;
        head.next = head.next.next;
        len--;
    }
    public void delete_node(int number)
    {
        if (number > len) { return; }
        if (number < 2) { delete_first(); }
        else if (number == len) { delete_last(); }
        else if (number < len / 2)
        {
            DNode<T> curr = head;
            for (int i = 1; i <= number; i++)
            {
                curr = curr.next;
                if (i == number) { remove(curr); }
            }
            len--;
        }
        else
        {
            DNode<T> curr = current;
            for (int i = len; i >= number; i--)
            {
                if (i == number) { remove(curr); }
                curr = curr.previous;
            }
            len--;

        }
    }
    public void delete_item(T data)
    {
        for(DNode<T> curr = head.next; curr != null; curr = curr.next)
        {
            if (data.Equals(curr.data))
            {
                if (curr == head.next) { delete_first(); }
                else if (curr == current) { delete_last(); }
                else { remove(curr); }
                return;
            }
        }
        print("item not found");
    }
    void remove(DNode<T> node)
    {
        node.previous.next=node.next;
        node.next.previous = node.previous;
        clear_data(node);
    }
    void clear_data(DNode<T> node)
    {
        node.data = default(T);
        node.previous = node.next = node = null;
    }
    public void reverse()
    {
        DNode<T> curr;
        for (curr=head.next.next; curr!=null; curr = curr.next)
        {
            temp = curr.previous.next;
            curr.previous.next = curr.previous.previous;
            curr.previous.previous = temp;
        }
        head.next.next = null;
        current.next = current.previous;
        temp = head.next;
        head.next = current;
        current = temp;
    }
    public void print_list()
    {
        DNode<T> curr = head;
        while (curr.next != null)
        {
            curr = curr.next;
            print(curr.data);
        }
        clear_data(curr);
    }
    public void print_list_opposite()
    {
        DNode<T> curr = current;
        while (curr != head.next)
        {
            print(curr.data);
            curr = curr.previous;
        }
        print(curr.data);
    }

}
