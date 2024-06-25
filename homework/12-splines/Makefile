.PHONEY: default
default: linterp_cos.svg 

include ../preamble.mk

scenic: *.svg
	imv *.svg

linterp_cos.svg: linterp_test.gp cos_x_linear.txt 
	gnuplot $< 

cos_x_linear.txt: linterp_test.exe
	mono $< >$@

linterp_test.exe: linterp_test.cs splines.dll matrix.dll
	$(MKEXE)

splines.dll: splines.cs matrix.dll
	$(MKLIB)


.PHONEY: clean
clean: 
