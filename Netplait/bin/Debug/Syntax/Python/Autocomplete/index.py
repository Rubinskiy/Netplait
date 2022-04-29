'''
	This file is part of the Netplait project.
	The free and open-source code editor for CS50
	and Python web-frameworks.

	File: index.py
	Desc: Main file for indexing the Flask module 
	for classes and functions, for autocompletion.

	Copyright (C) 2020 Robin H. Janssen
	Author: Robin H. Janssen <robinhannan@mail-me.com>
	URL: <robinhannan.com>
	Modified: 8/1/2020, 12:07 PM
'''

def IndexModules():
		try:
			#Get list of functions and order them
			import flask
			out = str(dir(flask))
			out = out.replace(",", "")
			out = out.replace("'", "")
			out = out.replace("[", "")
			out = out.replace("]", "")
			out = out.replace(" ", "\n")				

			#Write to .dat
			f = open("indexed.dat", "w")
			f.write(out)
			f.close()

			#Append the correct icon code to each line
			filepath = "indexed.dat"
			with open(filepath) as fp:
			    lines = fp.read().splitlines()
			with open(filepath, "w") as fp:
			    for line in lines:
			        print(line + "?2", file=fp)

			#Open and read the file after appending
			f = open("indexed.dat", "r")
			print(f.read())

		except Exception as e:
			print("Failed to index 'Flask' module. Details: " + str(e))
IndexModules()