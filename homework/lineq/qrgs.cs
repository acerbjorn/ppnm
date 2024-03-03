using System;
using static matrix;

public class QRGS{
    public matrix Q,R;
    public QRGS(matrix A, bool verbose=false){ /* the above "decomp" is the constructor here */
        int n = A.size2;
        Q=A.copy();
        R=new matrix(n,n);
        for (int i = 0; i< n; i++ ) {
            // Construction of R follows from our normalized orthogonalized requirement for Q
            R[i,i] = Q[i].norm();
            Q[i] /= R[i,i]; // Normalizing Q
            for (int j=i+1; j<n; j++) {
                R[i,j] = Q[i].dot(Q[j]);
                // GS Orthogonalizing subsequent columns in Q wrt. Q[i]
                Q[j] -= Q[i]*R[i,j];
            }
        }
    }
    public vector solve(vector b) {
        // Rewrite eq QRx=b to Rx=Q'b and solve by backsubstitution
        vector x = Q.T*b; // matrix*vec already allocates new vec
        // x.print(intro: "\tQ'b:");
        for (int i = x.size-1; i>=0; i-- ) {
            double sub = 0;
            for (int j = x.size-1; j>i; j--) sub += x[j]*R[i,j];
            x[i] -= sub;
            x[i] /= R[i,i];
        }
        return x;                
    }
    public double abs_det() {
        double ans = 1;
        for (int i=0; i<R.size1; i++) ans *= R[i,i];
        return ans;
    }
    public matrix inverse(){
        // as in solve but Q'e_i is the ith row of Q. 
        // if (this.Q.size1 != this.Q.size2) throw new RankException("Cannot invert non-square matrix");
        int n = Q.size1;
        int m = Q.size2;
        matrix B = Q.T;
        for (int ei=0; ei<n; ei++) {
            for (int i = m-1; i>=0; i-- ) {
                double sub = 0;
                for (int j = m-1; j>i; j--) sub += B[j,ei]*R[i,j];
                B[i,ei] -= sub;
                B[i,ei] /= R[i,i];
            }
        }
        return B;
    }
    public void print() {
        this.Q.print(intro: "Q:", row_sep: "\t");
        this.R.print(intro: "\n\nR:", row_sep: "\t");
    }
} 

