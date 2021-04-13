# EBSLectureDownloader

CLI로 이루어진 강좌 다운로더입니다.

현재는 ebsi.co.kr 로만 테스트하였습니다.
## 사용 방법:

- ebsi.co.kr에서 사용하고자 하는 경우
`Computer\HKEY_CLASSES_ROOT\EBSDownloader2020\shell\open\command` 의 registery data를  `"바이너리 이름(실행 파일 이름)" "%1"` 으로 변경해주세요. [예시 동영상](https://youtu.be/JTaxT62v-04)

- 직접 사용하고자 하는 경우 아래와 같이 사용할 수 있습니다.
```
run> EBSLectureDownloader.exe -h
Usage: EBSLectureDownloader [options] <GroupId>

Arguments:
  GroupId       EBS에서 다운로드 요청시 필요한 그룹 ID

Options:
  -subtitle     자막 다운로드 여부
  -folder       폴더 위치 여부
                Default value is: E:\EBSi.
  -?|-h|--help  Show help information.
```

## 왜?
2021년 4월 13일 기준으로 공식 다운로더가 작동이 되지 않았습니다. 
추측하기로는 파일 크기가 0임으로 0 바이트 다운로드 후 무시하는것으로 보입니다.
이러한 문제로 시험공부를 하기 힘든 상황을 대비하여 이렇게 제작하였습니다.

## 경고
이 프로그램으로 얻는 개발자의 금전적인 이득은 전혀 없으며, Apache 2.0 라이선스에 따라 법적 책임과 품질 보증이 없습니다.