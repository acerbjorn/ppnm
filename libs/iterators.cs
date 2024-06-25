partial class matrix {
    public IEnumerable<int> idx_diag()
    int n = max(this.size1, this.size2);
    int i = 0;
    while (i<n) {
        yield return i++;
    }
    public IEnumerable<int> idx()
    int n = this.size1; int m = this.size2;
    int i = 0; int j = 0;
    while (i<n) {
        while (j<m) {
            yield return (i, j++);
        }
        j = 0;
        i++
    }
}
