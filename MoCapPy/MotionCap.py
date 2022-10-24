import cv2
from cvzone.PoseModule import PoseDetector

cap = cv2.VideoCapture('Video.mp4')

detector = PoseDetector()

posList = []
while True:
    success, img = cap.read()
    img = detector.findPose(img) # Returns an image with drawings
    lmList, bboxInfo = detector.findPosition(img) # Returns LandmarkList and BoundingBoxInfo

    if bboxInfo:
        lmString = ""
        for lm in lmList:
            lmString += f'{lm[1]},{img.shape[0]-lm[2]},{lm[3]},'

        posList.append(lmString)

    cv2.imshow("Image", img)
    key = cv2.waitKey(1)

    if key == ord('s'):
        with open("AnimationFile.txt", "w") as file:
            file.writelines(["%s\n" %item for item in posList])