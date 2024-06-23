set terminal svg background "white"
set out "lambda_fit.svg"
set key left
set ylabel "Activity"
set xlabel "Time (day)"
set title "ThX Activity"
set tics out 
set grid 

plot \
	`cat ThX_lambda.fit`,\
	"ThX_data.txt" with err title "ThX Activity"
	
	

