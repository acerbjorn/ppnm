using static QRGS;
using static matrix;


public class lsfit {
    public vector betas;
    public matrix covars;
    // public Func<double, double> f;
    public lsfit(System.Func<double, double>[] funcs, vector x, vector y, vector dy) {
        int n = x.size;
        int m = funcs.Length;
        matrix A = new matrix(n,m);
        vector b = new vector(n);
        for (int i = 0; i<n; i++) {
            b[i] = y[i]/dy[i];
            for (int k = 0; k<m; k++) {
                A[i,k] = funcs[k](x[i])/dy[i];
            }
        }
        QRGS QR = new QRGS(A);
        betas = QR.solve(b);
        matrix Ainv = QR.inverse();
        covars = Ainv*Ainv.T;
    }
}

