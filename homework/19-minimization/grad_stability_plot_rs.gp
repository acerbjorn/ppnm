set terminal svg background "white"
set out "grad_stability_rs.svg"
set title "Gradient stability for Rosenstock's function"
set xlabel "log(dx)"
set ylabel "Sum of squares error"
set logscale y
set format y "10^{%T}"

plot \
	'Out_hessian_stability.txt' index 5 using 1:2 with lines title "Forward difference",\
	'Out_hessian_stability.txt' index 5 using 1:3 with lines title "Central difference",\
	'Out_hessian_stability.txt' index 5 using 1:4 with lines title "Upper triangular central difference"
