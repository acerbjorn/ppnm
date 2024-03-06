set terminal svg background "white"
set fit quiet
set macro
set out "time.svg"
set key left
set xlabel "Size n of n×n matrix"
set ylabel "Time (s)"
set yrange [0:1]
set title "Timing QRGS implementation"
set tics out 
set grid 
#set multiplot

O(x) = a*x**3
a = 1

print datafiles

# plot[0:400] sin(x) with line linecolor rgb"#ffffff" notitle

do for [filename in datafiles]  {
	fit O(x) filename using 1:2 via a
	plot[0:400] \
		filename using 1:2 with points pointtype 2,\
		O(x) notitle;\
}

