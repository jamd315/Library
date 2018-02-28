import os
from PIL import Image

filetype = ".bmp"


def get_extension(file):
	return "."+file.split(".")[-1]


def get_filename(file):
	return ".".join(file.split(".")[:-1])


imgs = [x for x in os.listdir() if get_extension(x) not in [".bmp", ".py"]]
for i in imgs:
	img = Image.open(i)
	img.save(get_filename(i)+filetype)
	os.remove(i)
