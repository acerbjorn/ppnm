# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set yrange [-1.5:1.5]
set out output_file

plot \
	input_file using 1:2 with lines title "Integral error function, with domain specific functions",\
	input_file using 1:3 with lines dashtype 2 title "Integral error function, without domain specific functions",\
	input_file using 1:4 with lines dashtype 4 title "Polynomial error function, from sfuns.cs",\
	

