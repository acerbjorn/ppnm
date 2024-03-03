public partial class matrix {
    public bool is_upper_triangular(double eps=0.0) {
        for (int i = 0; i<this.size1; i++) {
        for (int j = i+1; j<this.size2; j++) {
            if (System.Math.Abs(this[j,i]) > eps) return false;
        }    
        }
        return true;
    }
    public bool is_lower_triangular(double eps=0.0) {
        for (int i = 0; i<this.size2; i++) {
        for (int j = i+1; j<this.size1; j++) {
            if (System.Math.Abs(this[i,j]) > eps) return false;
        }    
        }
        return true;
    }
}
