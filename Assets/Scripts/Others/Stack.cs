using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack<T> where T : IStackItem<T>
{

        T[] items;
        int currentItemCount;

        public Stack(int maxHeapSize)
        {
            items = new T[maxHeapSize];

        }
        public void Add(T item)
        {
            item.StackIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }
        void SortUp(T item)
        {
            int parentIndex = (item.StackIndex - 1) / 2;

            while (true)
            {
                T parentItem = items[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                    break;
            }

        }
        void Swap(T a, T b)
        {
            items[a.StackIndex] = b;
            items[b.StackIndex] = a;
            int itemAndex = a.StackIndex;
            a.StackIndex = b.StackIndex;
            b.StackIndex = itemAndex;
        }
        public T RemoveFirst()
        {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].StackIndex = 0;
            SortDowm(items[0]);
            return firstItem;
        }

        private void SortDowm(T t)
        {
            while (true)
            {
                int chidLeft = t.StackIndex * 2 + 1;
                int chidRight = t.StackIndex * 2 + 2;

                int swapIndex = 0;
                if (chidLeft < currentItemCount)
                {
                    swapIndex = chidLeft;

                    if (chidRight < currentItemCount)
                    {
                        if (items[chidLeft].CompareTo(items[chidRight]) < 0)
                        {
                            swapIndex = chidRight;
                        }
                    }
                    if (t.CompareTo(items[swapIndex]) < 0)
                    {
                        Swap(t, items[swapIndex]);
                    }
                    else
                        return;
                }
                else
                    return;

            }
        }
        public void UpdateItem(T item)
        {
            SortUp(item);
        }
        public bool Contains(T item)
        {
            return Equals(items[item.StackIndex], item);
        }
        public int Count
        {
            get
            {
                return currentItemCount;
            }
        }
    }
    public interface IStackItem<T> : IComparable<T>
    {
        int StackIndex
        {
            get;
            set;
        }
    }
