TIMES = $(filter time.%.txt, $^)
GPVARS = "datafiles = '$(TIMES)'; n_files = $(words $(TIMES))"

.PHONEY: default
default: lambda_fit.svg Out.txt 

include ../preamble.mk

scenic: default
	imv *.svg

lambda_fit.svg: lambda_fit.gp ThX_lambda.fit ThX_data.txt 
	gnuplot $<
	

ThX_lambda.fit: halflife.exe
	mono $< fit > $@

ThX_data.txt: halflife.exe
	mono $< data > $@ 

Out.txt: halflife.exe
	mono $< > $@

time.%.txt: time.exe
	for timed_var in $$(seq 5 5 400); do\
		$(TIME) mono time.exe $* $$timed_var;\
	done

halflife.exe: halflife.cs lsfit.dll matrix.dll
	$(MKEXE)

lsfit.dll: qrgs.dll matrix.dll lsfit.cs
		$(MKLIB)

qrgs.dll: ../lineq/qrgs.cs matrix.dll
	$(MKLIB)

.PHONEY: clean
clean: 
	rm matrix.dll Out.txt time.*.txt log.gpi time.svg
