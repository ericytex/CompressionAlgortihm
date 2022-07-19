set Inputfile3=%1
set Outputfile3=%2
gswin64.exe -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/screen -dNOPAUSE -dQUIET -dBATCH -sOutputFile=%Outputfile3% %Inputfile3%
