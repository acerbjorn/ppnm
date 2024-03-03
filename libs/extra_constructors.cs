using System;

public partial class matrix {
    public static matrix rnd(int n, int m, double min = 0.0, double max = 1.0, Random rng= null) {
        if (rng == null) {
            rng = new System.Random(); 
        }
        matrix A = new matrix(n,m);
        for (int i = 0; i<n; i++) {
            for (int j = 0; j<m; j++) {
                A[i,j] = (max-min)*rng.NextDouble()+min;
            }
        }
        return A;
    }
}

public partial class vector {
    public static vector rnd(int n, double min = 0.0, double max = 1.0, Random rng= null) {
        if (rng == null) {
            rng = new System.Random(); 
        }
        vector x = new vector(n);
        for (int i = 0; i<n; i++) {
            x[i] = (max-min)*rng.NextDouble()+min;
        }
        return x;
    }
}
