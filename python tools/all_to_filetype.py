import os
from PIL import Image

filetype = ".bmp"


def get_extension(file):
	return "."+file.split(".")[-1]


def get_filename(file):
	return ".".join(file.split(".")[:-1])


imgs = [x for x in os.listdir(os.getcwd()) if get_extension(x) not in [".bmp", ".py"]]
print(imgs)
for i in imgs:
	img = Image.open(i)
	img.save(get_filename(i)+filetype)
	os.remove(i)
