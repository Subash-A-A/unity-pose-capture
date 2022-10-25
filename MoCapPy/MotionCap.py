import cv2
import socket
from cvzone.PoseModule import PoseDetector


cap = cv2.VideoCapture(0)
# cap = cv2.VideoCapture('Video.mp4')

detector = PoseDetector()
posList = []

# UDP Protocol
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverSocketAddress = ("127.0.0.1", 5052)

while True:
    success, img = cap.read()
    img = detector.findPose(img) # Returns an image with drawings
    lmList, bboxInfo = detector.findPosition(img) # Returns LandmarkList and BoundingBoxInfo

    if bboxInfo:
        lmString = ""
        for lm in lmList:
            lmString += f'{lm[1]},{img.shape[0]-lm[2]},{lm[3]},'

        posList.append(lmString)
        sock.sendto(str.encode(lmString), serverSocketAddress)

    cv2.imshow("Image", img)
    key = cv2.waitKey(1)

    if key == ord('s'):
        with open("AnimationFile.txt", "w") as file:
            file.writelines(["%s\n" %item for item in posList])