

public class genlist<T> {
    public T[] data;
    public int size;
    public int capacity => data.Length;
    public genlist() { data = new T[1]; size = 0; }

    public genlist(T[] array) { data = array; size = array.Length; }

    // Technically this indexing operation needs to consider size...
    public T this[int i] => data[i];
    
    public void add(T new_element) {
        if (size >= capacity) {
            // alocate more space 
            T[] new_data = new T[this.capacity*2];
            System.Array.Copy(data, new_data, size);
            data = new_data;
        }
        size += 1;
        data[size-1] = new_element;
    }
    public void rem(int index) {
        for (int i=index; i<size-1; i++) {
            data[i] = data[i+1]; 
        }
        size -= 1;
    }
    public T pop() {
        // Remove last element;
        size -= 1;
        return data[size];
        
    }
}
