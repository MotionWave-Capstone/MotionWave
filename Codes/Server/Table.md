## 데이터베이스
### 테이블 구조

1. **carInfo 테이블**

    | 컬럼명 | column | 자료형 | Constraint |
    | --- | --- | --- | --- |
    | 번호 | inum | int | not null primary key auto_increment |
    | 차량명 | generation | varchar(50) | not null |
    | 제조사 | brand | varchar(50) | not null |
    | 제조년도 | prdate | varchar(50) | not null |

2. **carData 테이블**

    | 컬럼명 | column | 자료형 | Constraint |
    | --- | --- | --- | --- |
    | Id | dnum | int | not null primary key auto_increment |
    | 번호 | inum | int | foreign key |
    | 마력 | power | int | not null |
    | 마력 rpm | power_rpm | int | not null |
    | 토크 | torque | int | not null |
    | 토크 rpm | torque_rpm | int | not null | 
    | 차량 무게 | weight | int | not null |
    | 시내 연료 소비량 | urban | int | not null |
    | 시외 연료 소비량 | extra_urban | int | not null |
    | 고속도로 연료 소비량 | combined | int | not null |

### 데이터 삽입

1. **carInfo**

    ```sql
    insert into carInfo (generation, brand, prdate) 
        values ('Grandeur/Azera VII (GN7)','Hyundai','November, 2022 year');
    insert into carInfo (generation, brand, prdate) 
        values ('Santa Fe V (MX5)','Hyundai','August, 2023 year');
    insert into carInfo (generation, brand, prdate) 
        values ('Sorento IV (facelift 2024)','Kia','March, 2024 year');
    insert into carInfo (generation, brand, prdate) 
        values ('Seltos','Kia','July, 2019 year');
    ```

2. **carData**

    ```sql
    insert into carData (inum, power, power_rpm, torque, torque_rpm, weight, urban, extra_urban, combined) 
	    values (1, 300, 6400, 359, 5000, 1695, 11.5, 7.5, 9.6);
    insert into carData (inum, power, power_rpm, torque, torque_rpm, weight, urban, extra_urban, combined) 
        values (2, 281, 5800, 421, 1700, 1795, 10.4, 7.5, 9.1);
    insert into carData (inum, power, power_rpm, torque, torque_rpm, weight, urban, extra_urban, combined) 
        values (3, 281, 5800, 422, 1700, 1749, 10.7, 8.1, 9.4);
    insert into carData (inum, power, power_rpm, torque, torque_rpm, weight, urban, extra_urban, combined) 
        values (4, 177, 5500, 265, 1345, 1500, 8.7, 6.9, 7.9);
    ```