import requests
import time

while True:
	print(requests.get("https://flbamobileapp.azurewebsites.net/simple/books"))
	time.sleep(120)