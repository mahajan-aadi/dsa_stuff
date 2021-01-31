using System;

public class DynamicArray <T>
{
    private int len;
    private int capacity;
    private T[] arr;
    public DynamicArray(int capacity)
    {
        if (capacity >= 0)
        {
            this.capacity = capacity;
            this.arr = new T[capacity];
        }
    }
    public DynamicArray()
    {
        this.capacity = 16;
        this.arr = new T[capacity];
    }
    private void _double_length()
    {
        if (capacity == 0) { capacity = 1; }
        else { capacity *= 2; }
        T[] new_array = new T[capacity];
        for (int i = 0; i < len; i++) { new_array[i] = arr[i]; }
        arr = new_array;
    }
    public int size() { return len; }
    public bool isempty() { return size() == 0; }
    
    public T get(int index) { return arr[index]; }
    public void set(int index,T element) { arr[index] = element; }
    public void clear()
    {
        for(int i = 0; i < capacity; i++) { arr[i] = default(T); }
        len = 0;
        capacity = 0;
        arr = new T[0];
    }
    public void add(T element)
    {
        if (len + 1 > capacity){ _double_length(); }
        arr[len++] = element;
    }
    public void remove(int index)
    {
        for (int i = index; i < len; i++)
        {
            arr[i] = arr[i + 1];
        }
        len--;
    }
    public void insert(int index,T element)
    {
        if (len + 1 > capacity) { _double_length(); }
        for (int i = len; i > index; i--)
        {
            arr[i+1] = arr[i];
        }
        arr[index] = element;
        len++;
    }
    public int indexof(T element)
    {
        for(int i = 0; i < len; i++) 
        {
            if (arr[i].Equals(element)) { return i; }
        }
        return -1;
    }
}
