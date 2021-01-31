using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linked_list_no_pointer : MonoBehaviour
{
    public class node<T>
    {
        public node<T> next;
        public T data;
    }
    public class linked__list<T>
    {
        int len;
        private node<T> head;
        private node<T> current;

        public linked__list()
        {
            len = 0;
            head = new node<T>();
            current = head;
        }
        public int size() { return len; }
        public void add_last(T data)
        {
            node<T> new_one = new node<T>();
            new_one.data = data;
            current.next = new_one;
            current = new_one;
            len++;
        }
        public void change_node(T data,int number)
        {
            node<T> curr = head;
            for (int i = 1; i <= number; i++)
            {
                curr = curr.next;
            }
            curr.data = data;
        }
        public void add_first(T data)
        {
            node<T> new_node = new node<T>();
            new_node.data = data;
            new_node.next = head.next;
            head.next = new_node;
            len++;
        }
        public void add_node(T data,int number)
        {
            if (number > len + 1) { return; }
            if (len+1 == number) { add_last(data); }
            else
            {
                node<T> curr = head;
                for (int i=1; i<number; i++)
                {
                    curr = curr.next;
                }
                node<T> new_node = new node<T>();
                new_node.data = data;
                new_node.next = curr.next;
                curr.next = new_node;
                len++;
            }
        }
        public void print_list()
        {
            node<T> curr = head;
            while (curr.next != null )
            {
                curr = curr.next;
                print(curr.data);
            }
        }
        public void delete_first()
        {
            if (len>1)
            {
                node<T> temp = head.next;
                head.next = head.next.next;
                temp.next = null;
                len--;
            }
            else if (len==1)
            {
                head.next = null;
                current = head;
                len--;
            }
        }
        public void delete_last()
        {
            if (len>1)
            {
                node<T> curr = head;
                for(int i=1;i<len;i++)
                {
                    curr = curr.next;
                }
                curr.next = null;
                current = curr;
                len--;
            }
            else if (len == 1)
            {
                head.next = null;
                current = head;
                len--;
            }
        }
        public void delete_node(int number )
        {
            if (number > len) { return; };
            if (number == 1 || len == 1) { delete_first(); }
            else if (number == len ) { delete_last(); }
            else if (number > 1)
            {
                node<T> curr = head;
                for (int i = 1; i < number; i++)
                {
                    curr = curr.next;
                }
                node<T> temp = curr.next;
                curr.next = curr.next.next;
                temp.next = null;
                len--;
            }
        }
        public void reverse()
        {
            if (len == 1) { return; }
            current = head.next;
            node<T> curr = head.next;
            node<T> previous = head;
            node<T> temp1 = curr;
            node<T> temp2 = curr;
            for (int i = 1; i < len; i++)
            {
                temp1 = curr;
                temp2 = previous;
                previous = curr;
                curr = curr.next;
                temp1.next = temp2;
            }
            curr.next = previous;
            current.next = null;
            head.next = curr;
        }
    }
}
