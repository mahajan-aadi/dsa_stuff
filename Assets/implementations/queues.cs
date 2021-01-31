using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queues <T>: MonoBehaviour
{
    T[] arr;
    int front, size, rear, len;
    public queues(int size)
    {
        this.size = size;
        arr = new T[size];
        len = 0;
        front = 0;
        rear = 0;
    }
    public void enqueue(T element)
    {
        if (len == size) { print("full"); }
        else
        {
            rear = rear % size;
            arr[rear] = element;
            rear++;
            len++;
        }
    }
    public void dequeue()
    {
        if (len == 0) { print("empty"); }
        else
        {
            front = front % size;
            len--;
            front++;
        }
    }
    public void print_queue()
    {
        if (len == 0) { print("empty"); }
        else
        {
            for(int i = front,j=0; j < len;i++,j++ )
            {
                i = i % size;   
                print(arr[i]);
            }
        }
    }
}

