output := Out-stdio.txt Out-args.txt Out-file.txt

.PHONY: default
default: $(output)

Out-stdio.txt : stdio-parse.exe In-file.txt
	mono $< < In-file.txt 2> $@ 

Out-file.txt : file-parse.exe In-file.txt
	mono $< -input:In-file.txt -output:$@
		
Out-args.txt : args-parse.exe Makefile
	mono $< -numbers:1,2,3,4,5,6 > $@

stdio-parse.exe: stdio-parse.cs
	mcs $<
	
args-parse.exe : args-parse.cs
	mcs $<

file-parse.exe : file-parse.cs
	mcs $<

.PHONY: clean
clean:
	rm -f $(output) *.exe
