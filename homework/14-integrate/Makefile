GPIO = "input_file = '$<'; output_file = '$@';"

.PHONEY: default
default: test_definite.txt test_erf.svg test_erf_resid.svg ClenCurt_test.txt
	
scenic: default
	imv *.svg

include ../preamble.mk

ClenCurt_test.txt: ClenCurt_test.exe
	mono $< > $@

ClenCurt_test.exe: ClenCurt_test.cs integrate.dll
	$(MKEXE)

test_definite.txt: test_definite.exe
	mono $< > $@

test_definite.exe: test_definite.cs integrate.dll
	$(MKEXE)

test_erf.svg: test_erf.txt erf.gp
	gnuplot -e $(GPIO) erf.gp

test_erf_resid.svg: test_erf_resid.txt erf_resid.gp
	gnuplot -e $(GPIO) erf_resid.gp

test_erf.txt: test_erf.exe
	mono $< | column -t > $@

test_erf_resid.txt: test_erf_resid.exe erf_table.txt
	cat erf_table.txt | mono $< | column -t > $@

test_erf.exe: test_erf.cs integrate.dll sfuns.dll matrix.dll
	$(MKEXE)
	
test_erf_resid.exe: test_erf_resid.cs integrate.dll sfuns.dll matrix.dll genlist.dll
	$(MKEXE)

integrate.dll: integrate.cs matrix.dll
	$(MKLIB)
		
sfuns.dll: ../../exercises/plot/sfuns.cs 
	$(MKLIB)
		
.PHONEY: clean
clean: 
	$(RM) matrix.dll sfuns.dll integrate.dll test_erf_resid.exe test_erf.exe test_erf_resid.txt test_erf.txt test_erf_resid.svg test_erf.svg test_definite.txt test_definite.exe ClenCurt_test.txt ClenCurt.exe
