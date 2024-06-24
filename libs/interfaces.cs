using System;
using System.Collections;

partial class vector: IEnumerable {
    public IEnumerator GetEnumerator() {
        return data.GetEnumerator();
    }
}
