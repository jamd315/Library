import requests
import pyodbc
import json
import base64
import io
from PIL import Image
import os

id = int(input("CoverID: "))
cn_str = "Driver={ODBC Driver 13 for SQL Server};Server=tcp:lizardswimmer-dbserver.database.windows.net,1433;Database=lizardswimmer-db;Uid=jamd315@lizardswimmer-dbserver;Pwd=ALongCreativePasswordForTheDatabase:);Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;"
conn = pyodbc.connect(cn_str)
while id != -1:
	cur = conn.cursor()
	result = cur.execute("SELECT Base64Encoded FROM tblCovers WHERE CoverID = ?", (id,)).fetchone()[0]
	raw = base64.b64decode(result)
	with open("tmp.bmp", "wb") as f:
		f.write(raw)
	img = Image.open("tmp.bmp")
	img.show()
	os.remove("tmp.bmp")
	id = int(input("CoverID: "))
	