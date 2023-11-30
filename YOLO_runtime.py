
from ultralytics import YOLO
from PIL import Image
import os
import sys

script_name = sys.argv[0]
arguments = sys.argv[1:]

RESULT_PATH = r"C:\Users\asus\source\repos\YOLO_GUI"

IMAGE_LIST = arguments #[r'C:\Users\asus\source\repos\YOLO_GUI\bin\Debug\runs\detect\predict\20170314-SPI-AOI-1_jpeg.rf.4109b5b3b4bc4d0a4bf9a37c69262d78.jpg']
# 加载模型
model = YOLO(r'C:\Users\asus\source\repos\YOLO_GUI\best.pt')  # 預先訓練好的的 YOLOv8n 模型

# Batch inference 
results = model(IMAGE_LIST)  # 返回 Results 對象列表

# Show the results
for r in results:
    im_array = r.plot()  # plot a BGR numpy array of predictions
    im = Image.fromarray(im_array[..., ::-1])  # RGB PIL image
    #im.show()  # show image
    im.save(os.path.join(RESULT_PATH, 'results.jpg'))  # save image
    

