her er min kode hvis det er: A = [[5, 3, -2]
     [0, -1, 6]
     [0, 0, -4]]
public static vector solve(matrix Q, matrix R, vector b){
        int m = R.size1;
        //bliver c rigtig??
        vector c = Q.T*b;
        c.print("vector c:");
        int cSize = c.size;
        Console.WriteLine($"Size of vector c: {cSize}");
        vector x = new vector(m);
        //back substitution
        x[m-1] = c[m-1]*(1/(R[m-1,m-1]));
        for (int i=m-2; i>=0; i--){
            double sub = 0;
            for (int k = i+1; k<=m-1; k++){
                sub += R[i,k]*x[k];}
            //Console.Write($"sub = {sub}\n");
            x[i] = (c[i]-sub) / R[i,i];
        }
        return x;
   } // solve closedc
	// Rewritten to fit into QRGS class
    public vector solve_sofie(vector b) {
            int m = R.size1;
            //bliver c rigtig??
            vector c = Q.T*b;
            int cSize = c.size;
            vector x = new vector(m);
            //back substitution
            x[m-1] = c[m-1]*(1/(R[m-1,m-1]));
            for (int i=m-2; i>=0; i--){
                double sub = 0;
                for (int k = i+1; k<=m-1; k++){
                    sub += R[i,k]*x[k];}
                //Console.Write($"sub = {sub}\n");
                x[i] = (c[i]-sub) / R[i,i];
            }
            return x;
    } // solve closedc
