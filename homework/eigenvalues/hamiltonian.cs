using static matrix;
using System;

public class hamiltonian {
    public static matrix construct_hamiltonian(vector rs, Func<double,double> potential) {
            int n_points = rs.size;
            matrix H = new matrix(n_points,n_points);
            double diff_scale = -2*Math.Pow(rs[1] - rs[0],2);

            for (int i = 0; i<n_points-1; i++) {
                H[i,i] = -2/diff_scale + potential(rs[i]);
                H[i,i+1] = 1/diff_scale;
                H[i+1,i] = 1/diff_scale;
            }
            H[n_points-1, n_points-1] = -2/diff_scale + potential(rs[n_points-1]);
            return H;
    }
}
