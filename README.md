25000개의 이미지 파일과 
환자 정보를 125000개 담고있는 data_entry.csv 파일과 
BBOX 정보 1000개가 담긴 bbox_list.csv 파일로 작업했음.
data_entry.csv, bbox_list.csv 파일에는 30000번대 까지의 환자 정보가 담겨있지만,
환자 정보 이미지는 용량의 이유로 6000번대까지의 환자밖에 사용하지 못했다.

따라서 csv파일의 125000개의 image_name과 이미지파일 25000개의 이름을 비교하여 이미지파일의 이름이 존재하는 csv목록만 따로 필터링 처리 해주었다.

( 필터링 해주지 않으면 csv파일을 datagridview에 출력하고 이미지파일에 존재하지 않는 image_name을 클릭했을때 해당하는 이미지가 나오지 않았기 때문, 오류를 출력하는것은 별로라고 생각되어 필터링처리 해주게 되었음 )
( bbox_list.csv에 있는 이미지이름은 data_entry.csv에도 존재함. )


1. 가장 기본적으로 환자 이미지를 담고있는 picbox와 필터링된 data_list, bbox_list가 datagridview에 출력된 화면.
2. 또한, 이미지를 한장씩, 10장씩 앞으로 뒤로 넘겨볼수있는 버튼과 맨처음, 맨마지막 이미지를 볼 수 있는 버튼이 있고, 우측 하단에 출력된 이미지에 해당하는 환자 정보를 보여준다.
![image](https://github.com/newviplayer/show_Patient_Image/assets/123538301/a83a41e1-02d4-4aa3-a400-11619c9f87de)

3. data_entry가 담긴 datagridview에서 특정행을 더블클릭하면 해당하는 이미지를 picbox에 출력해준다.
![image](https://github.com/newviplayer/show_Patient_Image/assets/123538301/246305ba-b68a-458a-ae1d-07f6de2d41ca)

4. bbox_list가 담긴 datagridview에서 특정행을 더블클릭하면 해당하는 이미지와 해당 좌표에맞는 bbox를 picbox에 출력해준다.
![image](https://github.com/newviplayer/show_Patient_Image/assets/123538301/18d85baf-373f-4f7a-8cfe-dc0280537d41)

5. 이전, 다음버튼을 누르다가 bbox_list에 존재하는 이미지가 출력되면 해당하는 좌표값으로  box(rectangle)를 쳐주는 모습.
![Form12024-03-1115-53-05-ezgif com-video-to-gif-converter](https://github.com/newviplayer/show_Patient_Image/assets/123538301/edc262f9-5782-460d-b01f-71355b2b4639)

6. textbox에 단어를 입력하면 해당단어를 가진 환자들의 정보들만 필터링 처리 해준다.
![image](https://github.com/newviplayer/show_Patient_Image/assets/123538301/7eb578d7-cc3c-4e15-a422-ad98933b5bb5)

7. 필터링 후 다음, 이전버튼을 누르면 필터링된 환자정보와 이미지만 출력해주는 모습.
![Form12024-03-1115-44-07-ezgif com-video-to-gif-converter](https://github.com/newviplayer/show_Patient_Image/assets/123538301/09532907-3db7-493a-940d-a5b4ccd92fa6)
