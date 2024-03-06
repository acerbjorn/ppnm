using System;
using static matrix;
using static System.Math;

public class EVD{
    public matrix V;
    public vector w;

    public static void timesJ(matrix A, int p, int q, double theta) {
        double cos_theta = Cos(theta);
        double sin_theta = Sin(theta);
        for (int i = 0; i<A.size1; i++) {
            double Ap = A[i,p];
            double Aq = A[i,q];
            A[i,p] = cos_theta*Ap - sin_theta*Aq;
            A[i,q] = sin_theta*Ap + cos_theta*Aq;
        }
    }
    public static void Jtimes(matrix A, int p, int q, double theta) {
        double cos_theta = Cos(theta);
        double sin_theta = Sin(theta);
        for (int i = 0; i<A.size1; i++) {
            double Ap = A[p,i];
            double Aq = A[q,i];
            A[p,i] =   cos_theta*Ap + sin_theta*Aq;
            A[q,i] = - sin_theta*Ap + cos_theta*Aq;
        }
    }
    public EVD(matrix A_orig, double convergence_limit= 0) {
        if (!A_orig.approx(A_orig.T)) throw new System.RankException("Matrix must be symmetric");
        matrix A = A_orig.copy();
        int n = A.size1;
        V = matrix.id(n);

        bool changed;
        do {
            changed = false;
            for (int p = 0; p<n-1; p++) {
            for (int q = p+1; q<n; q++ ) {
                double Apq = A[p,q], App = A[p,p], Aqq = A[q,q];
                double theta = Atan2(2*Apq, Aqq-App)/2;
                if (diag_changes(Apq, App, Aqq, theta, convergence_limit)) {
                    changed = true;
                    timesJ(A,p,q,  theta);
                    Jtimes(A,p,q, -theta);
                    timesJ(V,p,q,  theta);
                }
            }}
        } while (changed);
        w = A.diag();
    }
    
    public void print() {
        this.V.print(intro: "V:", row_sep: "\t");
        this.w.print(intro: "\n\nw:");
    }
    public bool diag_changes(
        double apq, double app, double aqq, double theta, double convergence_limit
        ) {
        double c=Cos(theta),s=Sin(theta);
		double d_app=Abs(app - (c*c*app-2*s*c*apq+s*s*aqq));
        // System.Console.WriteLine($"d_app = {d_app}");
		if (d_app > convergence_limit) return true;
        double d_aqq=Abs(aqq - (s*s*app+2*s*c*apq+c*c*aqq));
        // System.Console.WriteLine($"d_aqq = {d_aqq}");
		if (d_aqq > convergence_limit) return true;
        return false;
    }
} 

