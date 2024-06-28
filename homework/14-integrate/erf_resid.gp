# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set title "Residuals of the three different error functions compared to reference values from wikipedia"
set out output_file

plot \
	input_file using 1:2 with linespoints title "Integral error function, with domain specific functions",\
	input_file using 1:3 with linespoints dashtype 2 title "Integral error function, without domain specific functions",\
	input_file using 1:4 with linespoints dashtype 4 title "Polynomial error function, from sfuns.cs",\
	

