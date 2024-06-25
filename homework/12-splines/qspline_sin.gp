set terminal svg background "white"
set out "qspline_sin.svg"

plot \
	"qspline_sin.txt" index 0 with points title "sin(x) at integer points",\
	"qspline_sin.txt" index 1 using 1:2 with lines title "Quadratic interpolation",\
	"qspline_sin.txt" index 1 using 1:3 with lines title "Definite integral of interpolation"
