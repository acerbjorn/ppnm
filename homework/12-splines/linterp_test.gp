set terminal svg background "white"
set out "linterp_cos.svg"

plot \
	"cos_x_linear.txt" index 0 with points title "Cos(x) at integer points",\
	"cos_x_linear.txt" index 1 using 1:2 with lines title "Linear interpolation",\
	"cos_x_linear.txt" index 1 using 1:3 with lines title "Definite integral of interpolation"
