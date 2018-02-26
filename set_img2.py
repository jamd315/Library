import sys
import base64

import pyodbc
from PIL import Image

cn_str = "Driver={ODBC Driver 13 for SQL Server};Server=tcp:lizardswimmer-dbserver.database.windows.net,1433;Database=lizardswimmer-db;Uid=jamd315@lizardswimmer-dbserver;Pwd=ALongCreativePasswordForTheDatabase:);Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;"
conn = pyodbc.connect(cn_str)

img = Image.open(sys.argv[1])
img.save(img.filename+".bmp")
with open(img.filename+".bmp", "rb") as f:
	b = f.read()
encoded = base64.b64encode(b).decode()
book_id = int(input("Book ID: "))

cursor = conn.cursor()
cursor.execute(r"INSERT INTO tblCovers(Base64Encoded, BookID) VALUES(?, ?);", (encoded, book_id))
cursor.commit()
conn.close()