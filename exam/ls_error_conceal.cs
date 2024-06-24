using System;
using System.Collections.Generic;
using static System.Math;

public class lsec {
    public matrix M;
    public matrix D;
    public vector z;
    public vector x;
    
    public lsec(vector y, Func<double,bool> error_condition) {
        List<(int,int)> error_loc = new List<(int,int)>();
        int errors = 0;
        int n = y.size;
        for (int i=0; i<n; i++) {
            if (error_condition(y[i])) {
                error_loc.Add((i,errors));
                errors++;
            }
        }
        D = second_order_difference(n);
        M = new matrix(n,errors);
        foreach (var error in error_loc) {
            var (i, j) = error; // Can C# not destruct tuples in foreach?
            M[i,j] = 1;
        }
        matrix DM = D*M; vector nDy = -D*y;
        QRGS QRDM = new QRGS(DM);
        z = QRDM.solve(nDy);
        x = y + M*z;
    }
    public static matrix second_order_difference(int n) {
        // Contrary to the eigenvalues second order difference, 
        // we don't want to assume zero at the ends
        matrix D = new matrix(n);
        D[0,0] = 1; D[0,1] = -2; D[0,2] = 1;
        for(int i = 1; i<n-1; i++) {
            D[i,i-1] = 1; D[i,i] = -2; D[i,i+1] = 1;
        }
        D[n-1,n-3] = 1; D[n-1,n-2] = -2; D[n-1,n-1] = 1;
        return D;
    }
}
