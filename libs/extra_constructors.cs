using System;
using static System.Math;

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
    public vector diag() {
        int n = Min(this.size1, this.size2);
        vector diags = new vector(n);
        for (int i=0; i<n; i++) {
            diags[i] = this[i,i];
        }
        return diags;
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
    public static vector linspace(double start, double end, int entries=100, double spacing=0){
        if (spacing == 0) { // I don't like checking this way but the is null comparison doesn't seem to work in this version of c#
            spacing = (end-start)/entries;
        } else { // spacing parameter takes precedence
            entries = (int)((end-start)/spacing);
        }
        vector points = new vector(entries);
        for (int i = 0; i<entries; i++) {
            points[i] = start+(i)*spacing;
        }
        return points;
        
    }
}
