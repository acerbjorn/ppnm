TIMES = $(filter time.%.txt, $^)
GPVARS = "datafiles = '$(TIMES)'; n_files = $(words $(TIMES))"

.PHONEY: default
default: Out.txt time.svg

include ../preamble.mk

scenic: default
	imv *.svg

time.svg: time.gp time.decomp.txt # time.abs_det.txt time.solve.txt time.inverse.txt 

	gnuplot -e $(GPVARS) $<
	

Out.txt: test.exe
	mono test.exe -tests:1000 -max_dim:100 > $@

time.%.txt: time.exe
	for timed_var in $$(seq 5 5 400); do\
		$(TIME) mono time.exe $* $$timed_var;\
	done

test.exe: test.cs matrix.dll qrgs.dll
	$(MKEXE)
	
time.exe: time.cs matrix.dll qrgs.dll
	$(MKEXE)

qrgs.dll: qrgs.cs matrix.dll
	$(MKLIB)

.PHONEY: clean
clean: 
	rm matrix.dll Out.txt time.*.txt log.gpi time.svg
