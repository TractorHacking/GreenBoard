import sys
in_path = sys.argv[1]
out_path = in_path
out_path = out_path[0:-3] + "csv"
out = open(out_path,"w")
print ("outpath\n")
print(out_path )
print(in_path)
fp = open(in_path,"r") 
line = fp.readline()
while line:
	print(line)
	word = line.split()
	i =0
	for s in word:
		if(s == word[0]):
			s = s[0:-1]
		out.write(s)
		print(s)
		if(i==0):
			out.write(" , ")
		i = i + 1
	out.write("\n")	
	line = fp.readline()

