class main{
static void Main(){
	vector ve;
	ve=new vector(n:1); ve.print("ve=");
	ve=new vector(1,2,3); ve.print("ve=");
	ve=new vector(7,8,9,8,7); ve.print("ve=");
	var ma=new matrix("1 2 ; 5 6");
	ma.print();
	(ma+ma.T).print();
	(matrix.id(2)).print();
}
}
