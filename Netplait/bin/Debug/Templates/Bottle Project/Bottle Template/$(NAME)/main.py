'''
	Project maintained by: $Author
	Project description: $Desc
'''
from bottle import run, route

@route("/")
def index():
	return "Hello, World"

run(host='localhost', port=8080, debug=True)