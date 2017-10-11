SRC	:= $(shell egrep -l '^[^%]*\\begin\{document\}' *.tex)

all: pdf

pdf: $(SRC)
	pdflatex $<

clean:
	rm -f *.pdf *.ps *.dvi *.out *.log *.aux *.bbl *.blg *.pyg
	
.PHONY: all show clean ps pdf showps
