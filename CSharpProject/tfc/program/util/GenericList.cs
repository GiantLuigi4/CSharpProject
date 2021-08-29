using System;
using System.Collections.Generic;
using System.Text;

namespace tfc.program.util {
    class GenericList<T> : Iterable<T> {
        private T[] elements;
        private int size = 0;

        public unsafe GenericList() {
            elements = new T[0];
        }

        public GenericList(int initalCapacity) {
            elements = new T[initalCapacity];
        }

        public int getSize() {
            return size;
        }

        public void add(T obj) {
            if (elements.Length < (size + 1)) expand(size + 1);
            elements[size++] = obj;
        }

        public bool replace(T old, T val) {
            int indx = indexOf(old);
            if (indx == -1) return false;
            elements[indx] = val;
            return true;
        }

        public T remove(T obj) {
            if (elements.Length < (size + 1)) expand(size + 1);
            int indx = indexOf(obj);
            T old = elements[indx];
            indx += 1;
            Array.Copy(elements, indx, elements, indx - 1, size - indx);
            elements[size--] = default(T);
            return old;
        }

        public T remove(int indx) {
            T old = elements[indx];
            indx += 1;
            Array.Copy(elements, indx, elements, indx - 1, size - indx);
            elements[size--] = default(T);
            return old;
        }

        public int indexOf(T obj) {
            for (int i = 0; i < size + 1; i++) {
                if (elements[i].Equals(obj)) return i;
            }
            return -1;
        }

        public int lastIndexOf(T obj) {
            for (int i = size; i > 0; i--) {
                if (elements[i].Equals(obj)) return i;
            }
            return -1;
        }

        public T get(int indx) {
            return elements[indx];
        }

        protected void expand(int amt) {
            T[] newArray = new T[size + amt];
            Array.Copy(elements, newArray, size);
            elements = newArray;
        }

        public override Iterator<T> getIterator() {
            return new Itr<T>(this);
        }

        public T[] toArray() {
            return elements;
        }
    }

    class Itr<T> : Iterator<T> {
        T[] array;
        int indx = 0;
        int end;

        public Itr(GenericList<T> list) {
            array = (T[])list.toArray();
            end = list.getSize();
        }

        public override bool hasNext() {
            return indx < end;
        }

        public override T next() {
            return array[indx++];
        }
    }
}
