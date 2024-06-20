.PHONEY: default
default: Out.txt higgs_fit.txt higgs_fit.svg Out_simplex.txt hessian_stability_para.svg hessian_stability_hb.svg hessian_stability_rs.svg grad_stability_para.svg grad_stability_hb.svg grad_stability_rs.svg

scenic: higgs_fit.svg hessian_stability_para.svg hessian_stability_hb.svg hessian_stability_rs.svg grad_stability_para.svg grad_stability_hb.svg grad_stability_rs.svg	
	imv $^

include ../preamble.mk

higgs_fit.svg: higgs_fit_plot.gp higgs_fit.txt
	gnuplot -e "$(shell cat higgs_fit.txt)" higgs_fit_plot.gp

hessian_stability_para.svg: hessian_stability_plot_para.gp Out_hessian_stability.txt
	gnuplot $<
hessian_stability_hb.svg: hessian_stability_plot_hb.gp Out_hessian_stability.txt
	gnuplot $<
hessian_stability_rs.svg: hessian_stability_plot_rs.gp Out_hessian_stability.txt
	gnuplot $<

grad_stability_para.svg: grad_stability_plot_para.gp Out_hessian_stability.txt
	gnuplot $<
grad_stability_hb.svg: grad_stability_plot_hb.gp Out_hessian_stability.txt
	gnuplot $<
grad_stability_rs.svg: grad_stability_plot_rs.gp Out_hessian_stability.txt
	gnuplot $<

Out.txt: test_function_minimization.exe
	mono $< > $@

Out_simplex.txt: test_simplex.exe
	mono $< > $@

Out_hessian_test.txt: hessian_test.exe
	mono $< > $@

Out_hessian_stability.txt: hessian_stability.exe
	mono $< > $@

higgs_fit.txt: breit_wigner_fit.exe higgs_data.txt
	mono $< < higgs_data.txt > $@

breit_wigner_fit.exe: breit_wigner_fit.cs minimize.dll matrix.dll genlist.dll 
	$(MKEXE)

test_function_minimization.exe: test_function_minimization.cs minimize.dll matrix.dll
	$(MKEXE)

test_simplex.exe: test_simplex.cs minimize.dll matrix.dll
	$(MKEXE)

hessian_test.exe: hessian_test.cs matrix.dll hessian.dll;
	$(MKEXE)
	
hessian_stability.exe: hessian_stability.cs matrix.dll hessian.dll;
	$(MKEXE)

# Many of the dll recipes are kept in ../preamble.mk to enable easy sharing between weeks.
	
.PHONEY: clean
clean: 
	rm matrix.dll out.%.txt
