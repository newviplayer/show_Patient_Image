25000개의 이미지 파일과 
환자 정보를 125000개 담고있는 data_entry.csv 파일과 
BBOX 정보 1000개가 담긴 bbox_list.csv 파일로 작업했음.
data_entry.csv, bbox_list.csv 파일에는 30000번대 까지의 환자 정보가 담겨있지만,
환자 정보 이미지는 용량의 이유로 6000번대까지의 환자밖에 사용하지 못했다.

따라서 csv파일의 125000개의 image_name과 이미지파일 25000개의 이름을 비교하여 이미지파일의 이름이 존재하는 csv목록만 따로 필터링 처리 해주었다.
( 필터링 해주지 않으면 csv파일을 datagridview에 출력하고 이미지파일에 존재하지 않는 image_name을 클릭했을때 해당하는 이미지가 나오지 않았기 때문, 오류를 출력하는것은 별로라고 생각되어 필터링처리 해주게 되었음 )


1. 가장 기본적으로 환자 이미지를 담고있는 picbox와 필터링된 data_list, bbox_list가 datagridview에 출력된 화면.
![image](https://github.com/newviplayer/show_Patient_Image/assets/123538301/a83a41e1-02d4-4aa3-a400-11619c9f87de)
