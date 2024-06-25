# Expects an input_file and output_file to be set in environment when called.
set terminal svg background white
set out output_file

E(x,n) = 1/(2*n^2)
farve = ["red", "green", "blue"]
plot ,\
	for [col=2:4] input_file using 1:col with points pointcolor rgb farve[cols] title gprintf("wavenumber n=%g", col )
	for [n=2:4] E(x,n) with linecolor rgb farve[n] notitle

