---
title: "요약"
category: 정보처리기사
tags: [정보처리기사2021]
---

## 애자일(Agile)

- 애자일(Agile)

  > 절차보다는 사람이 중심이 돼 변화에 유연하고 신속하게 적응하면서 효율적으로 개발하는 신속 적응적 경량 개발방법론  
  > 개발과 함께 즉시 피드백을 받아 유동적으로 개발하는 소프트웨어 개발 방법론

  - 특징
    - 프로젝트의 요구사항은 모듈 중심보다 **기능 중심**으로 정의
    - 절차와 도구보다 **개인과 소통**을 중시
    - 짧은 작업 계획 -> 요구 변화에 유연하고 신속하게 대응
    - 소프트웨어가 잘 실행되는 데 가치를 둠
    - 고객과의 피드백을 중시
  - 선언문
    1. 공정과 도구보다 개인과 상호작용
    2. 계획을 따르기보다 변화에 대응하기
    3. 포괄적인 문서보다 동작하는 소프트웨어
    4. 계약 협상보다 고객과의 협력
  - 유형

    - XP(eXtreme Programming)

      > 의사소통 개선과 즉각적 피드백으로 소프트웨어 품질을 높이기 위한 방법론

      - `용기`, `단순성`, `의사소통`, `피드백`, `존중`
      - `Pair Programming`, `Collective Ownership`, `CI(Continuous Integration)`, `Planning Process`, `Small Release`, `Mehaphor`, `Simple Design`, `TDD(Test Driven Develop)`, `Refactoring`, `40-Hour Work`, `On Site Custormer`, `Coding Standard`

    - Lean

      > 낭비 요소를 제거해 품질을 향상시킨 방법론

      - `낭비 제거`, `품질 내재화`, `지식 창출`, `늦은 확정`, `빠른 인도`, `사람 존중`, `전체 최적화`

    - SCRUM
      > 매일 정해진 시간, 장소에서 짧은 시간의 개발을 하는 팀을 위한 프로젝트 관리 중심 방법론

---

### 모듈

- 모듈

  > 독립된 하나의 S/W or H/W 단위

  - 단독 컴파일 가능, 재사용 가능
  - **결합도는 낮게**, **응집도는 높게**, **크기는 작게** 만들어야 Best!!
  - **복잡도는 낮게**, **중복성을 줄이고**, **일관성 유지**
  - 모듈의 기능은 예측 가능해야 하며, 지나치게 제한적이면 안 된다.

  - 원칙 5가지

    - 정확성(Correctness)
    - 명확성(Clarity)
    - 완전성(Completeness)
    - 일관성(Consistency)
    - 추적성(Traceability)

  - 팬인(Fan-In), 팬아웃(Fan-Out)
    - 모듈 자신을 기준으로 들어오면 팬인(in) - 깊이가 높아지면 들어옴
    - 모듈 자신을 기준으로 나가면 팬아웃(out) - 깊이가 낮아지면 나감

### S/W 설계 유형

- 상위 설계

  1. 자료 구조 설계(Data Structure Design)
     > 요구 분석 단계의 정보를 바탕으로 필요한 자료구조로 변환
  2. 아키텍처 설계(Architecture Design)
     > S/W 시스템의 전체 구조 기술, 컴포넌트 간의 관계 정의
  3. 인터페이스 설계(Interface Design)
     > 컴퓨터 시스템, 사용자 등이 어떻게 통신하는지를 기술
  4. 프로시저 설계(Procedure Design)
     > 프로그램 아키텍처 컴포넌트 -> S/W 컴포넌트의 프로시저 기술 변환

- 하위 설계

  1. 협약에 의한 설계(Design by Contract)
     > S/W 컴포넌트에 대한 인터페이스, 클래스 명세를 위한 선행 조건, 결과조건, 불변조건을 나타내는 설계 방법

- 코드 설계

  - 기능

    - `표준화`, `분류`, `식별`, `배열`, `간소화`, `연상`, `암호화`, `오류 검출`

  - 설계 종류
    - 연상 코드(Mnemonic Code)
      > 코드만 보고 대상을 연상  
      > ex) 한국: KS, 미국: US, ...
    - 블록 코드(Block Code)
      > 공통성 있는 것끼리 그룹화, `구분 코드`  
      > ex) 지역번호 - 국번 - ...
    - 순차 코드(Sequence Code)
      > 일정한 기준에 따라 순서대로 일련번호 부여한 코드  
      > ex) 중고등 학생들의 반 번호(가나다순으로 1번, 2번, ...)
    - 표의 숫자 코드(Significant Digit Code)
      > 대상 자료의 물리적인 수치인 길이, 넓이, 용량 등을 표시한 코드  
      > ex) 20-100-300(길이 - 넓이 - 용량)
    - 십진 코드
      > 10진수 형태로 표현한 코드  
      > ex) 상품 바코드(880)
    - 그룹 분류식 코드
      > 대상을 기준에 따라 대분류, 중분류, 소분류로 구분해 번호 부여한 코드  
      > ex) 학번(입학 연도 - 일련번호 조합)

### HIPO

- HIPO(Hierarchy Input Process Output)

  > 시스템의 분석 및 설계, 문서화할 때 사용되며, **하향식 S/W 개발**을 위한 문서

  - 차트 종류
    - 가시적 도표(Visual Table of Content)
    - 총체적 도표(Overview Diagram)
    - 세부적 도표(Detail Diagram)

### S/W 아키텍처

- S/W Architecture

  > 여러 S/W 구성요소와 그 구성요소가 가진 특성 중에서 외부에 드러나는 특성, 구성요소 간의 관계를 표현

  - 아키텍처는 시스템의 비기능적인 요소에 집중, 기능적인 요소도 고려.

  - S/W 아키텍처 프레임워크 구성요소

    - 아키텍처 명세서(Architectural Description)
      > 아키텍처를 기록하기 위한 산출물들
    - 이해관계자(Stakeholder)
      > 시스템 개발에 관련된 모든 사람과 조직
    - 관심사(Concerns)
      > 시스템에 대해 이해관계자들의 서로 다른 의견과 목표
    - 관점(Viewpoint)
      > 개별 뷰를 개발할 때 토대가 되는 패턴이나 양식
    - 뷰(View)
      > 서로 관련 있는 관심사들의 집합이라는 관점에서 전체 시스템을 표현
    - 근거(Rationale)
      > 아키텍처 결정 근거
    - 목표(Mission)
      > 시스템의 목적, 사용, 운영방법
    - 환경(Environment)
      > 시스템에 영향을 주는 요인
    - 시스템(System)
      > 각 애플리케이션, 서브 시스템, 시스템의 집합, 제품군 등의 구현체

  - S/W Architecture 4+1 뷰

    > 고객의 요구사항을 정리해 놓은 시나리오를 4개의 관점에서 바라보는 S/W 적인 접근 방법

    - 1
      - 유스케이스 뷰(Use-case View)
        > 다른 뷰를 검증하는데 사용되는 뷰
    - 4
      - 논리 뷰(Logical View)
        > 시스템의 기능적인 요구사항이 어떻게 제공되는지 설명해주는 뷰
      - 프로세스 뷰(Process View)
        > 시스템의 비기능적인 속성으로서 자원의 효율적인 사용, 병행 실행, 비동기, 이벤트 처리 등을 표현한 뷰
      - 구현 뷰(Implementation View)
        > 정적인 S/W 모듈의 구성을 보여주는 뷰
      - 배포 뷰(Deployment View)
        > 물리적인 아키텍처에 어떻게 배치되는가를 매핑해서 보여주는 뷰

  - 비용 평가 모델 종류

    - SAAM(Software Architecture Analysis Method)
      > 변경 용이성과 기능성에 집중.
    - ATAM(Architecture Trade-off Analysis Method)
      > **아키텍처 품질 속성**을 만족시키는지 판단 및 품질 속성들의 이해 상충 관계까지 평가.
    - CBAM(Cost Benefit Analysis Method)
      > ATAM 바탕의 분석 중심으로 **경제적 의사결정에 대한 요구를 충족**하는 비용 평가 모델
    - ADR(Active Design Review)
      > 소프트웨어 아키텍처 구성요소 간 **응집도**를 평가
    - ARID(Active Reviews for Intermediate Designs)
      > 전체 [X], **특정 부분에 대한 품질요소**에 집중하는 비용 평가

  - S/W 아키텍처 패턴

    > 개발 시간을 단축, 높은 품질의 S/W 생산  
    > 개발의 안정적 수행, 시스템 특성 예측 가능

    - 계층화 패턴(Layered Pattern)
      > 시스템을 계층(Layer)으로 구분해 구성하는 패턴
    - 클라이언트-서버 패턴(Client-Server Pattern)
      > 하나의 서버와 다수의 클라이언트로 구성된 패턴
    - 파이프-필터 패턴(Pipe-Filter Pattern)
      > 데이터 스트림을 생성하고 처리하는 시스템에서 사용 가능한 패턴
    - 브로커 패턴(Broker Pattern)
      > 분리된 컴포넌트들로 이루어진 분산 시스템에서 사용되고, 원격 서비스 실행을 통해 상호작용이 가능한 패턴
    - 모델-뷰-컨트롤러 패턴(MVC, Model-View-Controller Pattern)
      > 대화형 애플리케이션을 모델, 뷰, 컨트롤 뷰 3개의 서브 시스템으로 구조화하는 패턴

---

### 객체지향

- 객체지향

  > 실세계의 개체를 객체(속성+메서드)로 표현하는 기법

  - 구성요소

    - 클래스(Class)
      > 데이터를 추상화하는 단위
    - 객체(Object)
      > 구현할 대상으로 각각의 상태와 식별성을 가짐.
    - 메서드(Method)
      > 객체가 메시지를 받아 실행해야 할 객체의 구체적인 연산
    - 메시지(Message)
      > 객체 간의 상호작용 수단
    - 인스턴스(Instance)
      > 실제로 메모리상에 할당된 각각의 객체
    - 속성(Property)
      > 객체들이 가지고 있는 데이터 값들을 단위별로 정의

  - 기법

    - 캡슐화(Encapsulation)
      > 관련성 높은 데이터와 함수들을 한 묶음으로 처리
    - 상속성(Inheritance)
      > 상위 클래스의 속성과 메소드를 하위 클래스에서 재정의 없이 물려받아 사용
    - 다형성(Polymorphism)
      > 하나의 메시지에 대해 각 객체가 고유한 방법으로 응답.  
      > ex) 오버로딩(Overloading), 오버라이딩(Overriding)
    - 추상화(Abstraction)
      > 공통 성질을 추상 클래스로 설정
    - 정보은닉(Information Hiding)
      > 코드 내부 데이터와 메서드를 숨기고 공개 인터페이스로만 접근 가능하도록 하는 코드 보안 기술  
      > Side-Effect들을 최소화
    - 관계성(Relationship)
      > 두 개 이상의 엔터티 형에서 데이터를 참조하는 관계를 나타내는 기법.

  - 설계 원칙(`S` `O` `L` `I` `D`)

    - 단일 책임의 원칙(`S`ingle Responsibility Principle)
      > 하나의 클래스는 하나의 목적을 위해 생성.
    - 개방 폐쇄 원칙(`O`pen Close Principle)
      > S/W 구성요소는 확장에는 열려있고, 변경에는 닫혀있어야 함.
    - 리스코프 치환의 원칙(`L`iskov Substitution)
      > 서브 타입(하위 클래스)은 자신의 기반 타입(상위 클래스)로 교체 가능해야 함.
    - 인터페이스 분리의 원칙(`I`nterface Segregation)
      > 클래스 내에 사용하는 않는 인터페이스는 구현하지 말아야 함.
    - 의존성 역전의 원칙(`D`ependency Inversion Principle)
      > 실제 사용 관계는 바뀌지 않으며, 추상을 매개로 메시지를 주고받음으로써 관계를 최대한 느슨하게 만듦.

  - 객체지향 방법론 종류
    - OOSE(Object Oriented Software Engineering)
      > 유스케이스를 모든 모델의 근간으로 활용
    - OMT(Object Modeling Technology)
      > 4단계: 객체지향 분석, 시스템 설계, 오브젝트 설계, 구현  
      > 럼바우의 객체지향 분석 절차: 객체 모델링 -> 동적 모델링 -> 기능 모델링
    - OOD(Object Oriented Design)
      > 설계 부분만 존재.  
      > 미시적(Micro) 개발 프로세스 + 거시적(Macro) 개발 프로세스 사용

### 디자인 패턴

- 디자인 패턴

  > S/W 설계에서 공통으로 발생하는 문제에 대해 자주 쓰이는 설계 방법

  - 개발 효율성, 유지보수성, 상호운용성, 프로그램 최적화

  - 구성요소

    - 패턴 이름
    - 문제 및 배경
    - 솔루션
    - 사례
    - 결과
    - 샘플 코드

  - 패턴 유형

    - 목적
      - 생성, 구조, 행위
    - 범위
      - 클래스(컴파일 타임), 객체(런타임)

  - 디자인 패턴 종류

    - 생성 패턴
      - Builder
        > 단계별로 캡슐화해 구축 공정을 동일하게 이용하는 패턴
      - Prototype
        > 복사해 새 개체를 생성하는 패턴
      - Factory Method: `if-else` or `switch` 사용
        > 인스턴스 생성 처리를 서브 클래스가 결정하도록 하는 패턴
      - Abstract Factory: `interface` 사용
        > 생성군들을 하나에 모아놓고 팩토리 중에서 선택하게 하는 패턴
      - Singleton
        > 유일한 하나의 인스턴스를 보장하도록 하는 패턴
    - 구조 패턴
      - Bridge
        > 추상과 구현을 분리해 결합도를 낮춘 패턴
      - Decorator
        > 소스를 변경하지 않고 기능을 확장하도록 하는 패턴
      - Facade
        > 하나의 인터페이스로 느슨한 결합을 제공하는 패턴
      - Flyweight
        > 대량의 작은 객체들을 공유하는 패턴
      - Proxy
        > 대리인이 대신 그 일을 처리하는 패턴
      - Composite
        > 개별 객체와 복합 객체를 클라이언트에서 동일하게 사용하도록 하는 패턴, 일괄적인 관리
      - Adapter
        > 인터페이스로 인해 함께 사용하지 못하는 클래스를 함께 사용하도록 하는 패턴
    - 행위 패턴

      - Interpreter
        > 언어 규칙 클래스를 이용하는 패턴
      - Template Method
        > 알고리즘 골격의 구조를 정의한 패턴
      - Chain of Responsibility
        > 객체들끼리 연결 고리를 만들어 내부적으로 전달하는 패턴
      - Command
        > 요청 자체를 캡슐화해 파라미터로 넘기는 패턴  
        > ex) Job Queue, Packet Queue
      - Iterator
        > 내부 표현은 보여주지 않고 순회하는 패턴
      - Mediator
        > 객체 간 상호작용을 캡슐화한 패턴
      - Memento
        > 상태 값을 미리 저장해 두었다가 복구하는 패턴
      - Observer
        > 상태가 변할 때 의존자들에게 알리고, 자동 업데이트하는 패턴
      - State
        > 객체 내부 상태에 따라서 행위를 변경하는 패턴
      - Strategy
        > 다양한 알고리즘을 캡슐화해 알고리즘 대체가 가능하도록 한 패턴
      - Visitor
        > 오퍼레이션을 별도의 클래스에 새롭게 정의한 패턴

    - 장점

      > 재사용, 소스 코드 변경 최소화, 개발자 간의 원활한 의사소통 가능

    - 단점
      > 초기 투자 비용의 부담

---

### 자료 구조

- 자료 구조(Data Structure)

  > 자료를 효율적으로 저장하기 위해 만들어진 논리적인 구조.

  - 선형 구조
    > 데이터를 연속적으로 연결한 자료 구조
    - List
      - `Linear List`
        > 배열(Array)과 같이 연속되는 기억 장소에 저장되는 리스트  
        > **접근 빠름**, **삽입 및 삭제 느림**
      - `Linked List`
        > 노드의 포인터로 서로 연결시킨 리스트  
        > **접근 느림**, **삽입 및 삭제 빠름**
    - `Stack`
      > 한 방향으로만 자료를 넣고 꺼내는 구조  
      > LIFO(Last-In First_Out) 구조
    - `Queue`
      > 한쪽은 삽입만, 한쪽은 삭제만 작업하는 자료 구조  
      > FIFO(First-In First-Out) 구조
    - `Deque`(Double Ended Queue)
      > 양쪽 모두 삽입, 삭제 가능한 자료 구조
  - 비선형 구조
    > 데이터를 비연속적으로 연결한 자료 구조
    - `Tree`
      > 데이터들을 계층화시킨 자료 구조  
      > Node + Link 로 구성됨.
    - `Graph`
      > Node(노드) + Edge(간선)을 하나로 모아 놓은 자료 구조

### 논리 데이터 저장소

- 논리 데이터 저장소

  > 업무를 모델링 표기법으로 형상화한 데이터 저장소

  - 개체(Entity): 직사각형
    > 관리할 대상이 되는 실체
  - 속성(Attribute): 원
    > 관리할 정보의 구체적 항목
  - 관계(Relationship): 마름모
    > 개체 간의 대응 관계

---

### 알고리즘

- 알고리즘

  > 어떠한 문제를 해결하기 위한 정해진 일련의 절차나 방법을 공식화

  - 기법

    - 분할과 정복(Divide and Conquer)
      > 문제를 나눌 수 없을 때까지 나누고, 각각을 풀면서 다시 병합해 문제의 답을 얻는 알고리즘
    - 동적계획법(Dynamic Programming)
      > 더 작은 문제의 연장선으로 생각해 과거에 구한 해를 활용하는 방식의 알고리즘
    - 탐욕법(Greedy)
      > 결정을 할 때마다 그 순간에 가장 좋다고 생각되는 해답을 선택하면서 최종적으로 해답에 도달하는 알고리즘
    - 백트래킹(Backtracking)
      > 어떤 노드의 유망성 점검 후, 유망하지 않으면 그 노드의 부모 노드로 되돌아간 후 다른 자식 노드를 검색하는 알고리즘

  - 시간 복잡도에 따른 알고리즘
    - `O(1)`
      - 해시 함수(Hash Function)
        > 임의의 길이 데이터를 고정된 길이 데이터로 매핑
    - `O(logN)`
      - 이진 탐색(Binary Search)
        > 정렬된 상태에서 임의의 값을 찾을 때 1/2로 나누면서 찾음
    - `O(N)`
      - 순차 탐색(Sequential Search)
        > 입력 자료를 차례로 하나씩 탐색
    - `O(NlogN)`
      - 퀵 정렬(Quick Sort)
        > 피벗을 정해 큰 원소와 작은 원소들로 분할하면서 정렬.
      - 병합 정렬(Merge Sort)
        > 전체 원소를 하나의 단위의 원소까지 분할 후 다시 병합해 정렬.
      - 힙 정렬(Heap Sort)
        > Heap 구조로 구성해 정렬
    - `O(N^2)`
      - 거품 정렬(Bubble Sort)
        > 두 인접한 원소를 검사해 정렬
      - 선택 정렬(Selection Sort)
        > 가장 큰 or 작은 원소를 선택해 정렬되지 않은 앞 부분의 데이터와 교환해나가며 정렬
      - 삽입 정렬(Insertion Sort)
        > 앞에서부터 차례대로 정렬된 배열과 비교하며 적합한 위치에 삽입하며 정렬

### 코드 최적화

- 코드 최적화

  > 읽기 쉽고 변경 및 추가가 쉬운 **클린 코드(Clean Code)**를 작성하는 것

  - 클린 코드(Clean Code) 작성 원칙
    - 가독성
      > 이해하기 쉬운 용어, 들여쓰기 작성
    - 단순성
      > 단순, 명료한 코드, 한 번에 한 가지 처리만 수행
    - 의존성 최소
      > 코드의 변경이 다른 부분에 영향이 없도록 작성
    - 중복성 제거
      > 중복된 코드 제거, 공통된 코드 사용
    - 추상화
      > 추상화 구현, 상세 내용은 하위 클래스에서 구현

---

### 절차형 SQL

- 트리거(Trigger)

  > DB System에서 삽입, 갱신, 삭제 등의 Event가 발생할 때마다 관련 작업이 자동적으로 수행되는 절차형 SQL  
  > 데이터 무결성 유지 및 LOG 메시지 출력 등의 별도 처리를 위해 사용되기도 함.

  ```sql
  CREATE TRIGGER T_STUDENT
  BEFORE INSERT ON STUDENT
  BEGIN
    IF(AGE < 19) THEN
      RAISE_APPLICATION_ERROR(-20502, '미성년자 삽입 불가');
    END IF;
  END;
  ```

- 이벤트(Event)

  > 특정 시간에 특정한 Query, Procedure, Function 등을 실행시키는 기능

  ```sql
  CREATE EVENT T_TEST
    ON SCHEDULE
      EVERY 50 SERCOND
    DO
      INSERT INTO TEST
        SET REGDATE=NOW();
  ```

- 사용자 정의 함수(User-Defined Function)

  > 일련의 연산 처리 결과를 단일 값으로 반환  
  > 프로시저와 다른 점은 **종료 시 단일 값으로 반환**한다는 점.

  ```sql
  CREATE FUNCTION GET_AGE(V_BIRTH_YEAR IN CHAR(4))
  IS
    V_THIS_YEAR CHAR(4);
  BEGIN
    SELECT TO_CHAR(SYSDATE, 'YYYY')
        INTO V_THIS_YEAR
        FROM DUAL;

      RETURN V_THIS_YEAR - V_BIRTH_YEAR+1;
  END;
  ```

### SQL

- SQL(Structured Query Language)

  > DB를 접근하고 조작하는데 필요한 표준 언어

  - 종류: 사용 용도에 따라

    - DDL(Data Definition Language)
      > 데이터를 정의하는 언어
      - `CREATE`, `ALTER`, `DROP`, `TRUNCATE`
    - DML(Data Manipulation Language)
      > DB에 저장된 자료들을 입력, 수정, 삭제, 조회하는 언어
      - `SELECT`, `INSERT`, `UPDATE`, `DELETE`
    - DCL(Data Control Language)
      > DBA가 데이터 보안, 무결성 유지, 병행 제어, 회복을 위해 사용하는 언어
      - GRANT, REVOKE

  - WHERE 조건

    - 비교

      > `=`, `<>`(다름), `<`, `<=`, `>`, `>=`

      ```sql
      -- PRICE가 50000이 아닐 경우
      WHERE PRICE <> 50000
      ```

    - 범위

      > `BETWEEN 숫자 AND 숫자`

      ```sql
      -- PRICE가 50000 이상 80000 이하일 경우
      -- PRICE >= 50000 AND PRICE <= 80000
      WHERE PRICE BETWEEN 50000 AND 80000
      ```

    - 집합

      > `IN`, `NOT IN`

      ```sql
      -- PRICE가 40000 or 50000 or 60000 일 경우
      WHERE PRICE IN (40000, 50000, 60000)
      ```

    - 패턴

      > `LIKE`

      ```sql
      -- NAME이 김으로 시작하는 사람
      WHERE NAME LIKE '김%'

      -- NAME이 최_률 인사람
      WHERE NAME LIKE '최_률'
      ```

    - NULL

      > `IS NULL`, `IS NOT NULL`

      ```sql
      -- PRICE가 NULL이 아닐 경우
      WHERE PRICE IS NULL
      ```

    - 복합 조건

      > `AND`, `OR`, `NOT`

      ```sql
      -- PRICE가 50000 초과이고 NAME이 김으로 시작하는 사람
      WHERE (PRICE < 50000) AND (NAME LIKE '김%')
      ```

### DML(Data Manipulation Language)

- 데이터 조작어

  > DB에 저장된 자료들을 입력, 수정, 삭제, 조회하는 언어

  - `SELECT`

    > 데이터 조회

    ```sql
    SELECT [DISTINCT] 속성명
      FROM 테이블명
    [WHERE 조건]
    [GROUP BY 속석명]
    [HAVING 그룹 조건]
    [ORDER BY 속성];
    ```

  - `INSERT`

    > 데이터 생성

    ```sql
    INSERT INTO 테이블명(속성명, ...)
    VALUES(데이터, ...);
    ```

  - `UPDATE`

    > 데이터 변경

    ```sql
    UPDATE 테이블명
      SET 속성명 = 데이터, ...
    WHERE 조건;
    ```

  - `DELETE`

    > 데이터 삭제

    ```sql
    DELETE FROM 테이블명
    WHERE 조건;
    ```

### DCL(Data Control Language)

- 데이터 제어어

  > DB 관리자가 데이터 보안, 무결성 유지, 병행 수행 제어, 회복을 위해 사용하는 언어

  - `GRANT`

    > 사용 권한 부여

    ```sql
    -- 시스템 권한 부여
    GRANT 권한 TO 사용자;

    -- 객체 권한 부여
    -- [WITH 권한 옵션]: 옵션을 나눠 가지기 가능
    GRANT 권한 ON 테이블 TO 사용자 [WITH 권한 옵션];
    ```

  - `REVOKE`

    > 사용 권한 취소

    ```sql
    -- 시스템 권한 회수
    REVOKE 권한 FROM 사용자;

    -- 객체 권한 회수
    -- [CASCADE]: 연쇄적인 권한을 해제할 때
    REVOKE 권한 ON 테이블 FROM 사용자 [CASCADE];
    ```

  - `COMMIT`: TCL(Transaction Control Language)

    > 트랜잭션 확정

    ```sql
    -- 시스템 권한 회수
    COMMIT;
    ```

  - `ROLLBACK`: TCL

    > 트랜잭션 취소

    ```sql
    -- 권한 회수
    ROLLBACK 이름;
    ```

  - `SAVEPOINT`(= `CHECKPOINT`): TCL

    > 저장 시기 설정

    ```sql
    -- 포인트 지정
    SAVEPOINT 이름;

    -- 롤백
    ROLLBACK TO SAVEPOINT 이름;
    ```

### DDL(Data Definition Language)

- 데이터 정의어

  > 테이블(데이터 저장공간)과 같은 데이터 구조를 정의하는 데 사용되는 명령어

  - CREATE

    > DB 오브젝트 생성

    ```sql
    CREATE TABLE 테이블명{
      속성명 데이터타입 [NOT NULL], ...
      PRIMARY KEY(속성명)
      FOREIGN KEY(속성명) REFERENCES 참조테이블(속성명)
      CONSTRAINT 제약조건명 CHECK(조건식)
    };
    ```

  - ALTER

    > DB 오브젝트 변경

    ```sql
    ALTER TABLE 테이블명 ADD 컬럼명 데이터타입;
    ALTER TABLE 테이블명 MODIFY 컬럼명 데이터타입 [DEFAULT 값] [NOT NULL];
    ALTER TABLE 테이블명 DROP 컬럼명;
    ALTER TABLE 테이블명 RENAME COLUMN 변경 전 컬럼명 TO 변경 후 컬럼명;
    ```

  - DROP

    > DB 오브젝트 삭제

    ```sql
    -- CASCADE: 참조하는 테이블까지 연쇄적 제거
    -- RESTRICT: 참조 중이면 제거 [X]
    DROP TABLE 테이블명 [CASCADE | RESTRICT];
    ```

  - TRUNCATE

    > DB 오브젝트 내용 삭제

    ```sql
    TRUNCATE TABLE 테이블명;
    ```

### Window Function

- 윈도우 함수

  > 행과 행 간의 관계를 쉽게 정의하기 위해 만든 함수  
  > `OLAP`(On-Line Analytical Processing)

  - 집계 함수
    - `COUNT`
      > 복수 행의 줄 수를 구하는 함수
    - `SUM`
      > 복수 행의 해당 컬럼에 대한 합계를 구하는 함수
    - `AVG`
      > 복수 행의 해당 컬럼 간의 평균을 구하는 함수
    - `MAX`
      > 복수 행의 해당 컬럼 중 최댓값을 구하는 함수
    - `MIN`
      > 복수 행의 해당 컬럼 중 최솟값을 구하는 함수
    - `STDDEV`
      > 복수 행의 해당 컬럼에 대한 표준편차를 구하는 함수
    - `VARIAN`
      > 복수 행의 해당 컬럼에 대한 분산을 구하는 함수
  - 순위 함수
    - `RANK`
      > 특정 컬럼에 대한 순위를 구하는 함수  
      > ex) 2위, 2위, 2위, 5위, ...
    - `DENSE_RANK`
      > 동일 순위가 존재해도 후순위로 넘어가지 않음  
      > ex) 2위, 2위, 2위, 3위, ...
    - `ROW_NUMBER`
      > 동일 순위의 값이 존재해도 무관하게 연속적으로 순위 부여  
      > ex) 2위, 3위, 4위, 5위, ...
  - 행 순서 함수
    - `FIRST_VALUE`
      > 가장 먼저 나오는 값을 반환하는 함수  
      > 집계 함수의 `MIN`과 동일한 결과
    - `LAST_VALUE`
      > 가장 늦게 나오는 값을 반환하는 함수  
      > 집계 함수의 `MAX`와 동일한 결과
    - `LAG`
      > 이전 행의 값을 반환하는 함수
    - `LEAD`
      > 이후 행의 값을 반환하는 함수
  - 그룹 내 비율 함수
    - `RATIO_TO_REPORT`
      > 그룹의 합을 기준으로 각 행의 상대적 비율을 반환하는 함수(**0 ~ 1**)
    - `PERCENT_RANK`
      > 그룹에서 제일 먼저 나온 것은 0, 제일 나중에 나온 것은 1로 하여 순서별 백분율을 구하는 함수(**0 ~ 1**)

### 관계 데이터 모델

- 관계 데이터 모델(Relation Data Model)

  > 실세계 데이터를 행과 열로 구성된 테이블 형태로 구성된 데이터 모델.

  - 구성 요소
    - 릴레이션(Relation)
      > 행(Row)와 열(Column)로 구성된 테이블
    - 튜플(Tuple)
      > 릴레이션의 행(Row)에 해당
    - 속성(Attribute)
      > 릴레이션의 열(Column)에 해당
    - 카디널리티(Cardinality)
      > 튜플(Row)의 수
    - 차수(Degree)
      > 속성(Column)의 수
    - 스키마(Schema): 변수
      > DB의 구조, 제약조건 등의 정보를 담고 있는 기본구조
    - 인스턴스(Instance): 변수 값
      > 테이블에 실제 저장된 데이터의 집합

### 관계 대수 vs 관계 해석

#### 관계 대수

> 관계형 DB에서 원하는 정보와 그 정보를 어떻게 유도하는가를 기술  
> 관계로 표현하고 데이터를 취급하는 대수적인 연산 체계  
> 절차적 정형 언어

- 일반 집합 연산자

  - 합집합(Union): `R ∩ S`
    > R과 S의 합집합
  - 교집합(Intersection): `R ∪ S`
    > R과 S의 교집합
  - 차집합(Difference): `R - S`
    > R에 존재하고 S에 존재하지 않는 튜플로 결과 릴레이션 구성
  - 카티션 프로덕트(CARTESIAN Product): `R X S`
    > R과 S에 속한 모든 튜플을 연결해 만들어진 새로운 튜플로 릴레이션 구성  
    > 카디널리티는 곱하기, 차수는 더하기

- 순수 관계 연산자

  - Select: `σ조건(R)`, R은 Relation

    > 릴레이션 R에서 조건을 만족하는 튜플 반환

    ```sql
    SELECT *
    FROM R
    WHERE 조건
    ```

  - Project: `π속성들(R)`

    > 릴레이션 R에서 주어진 속성들의 값으로만 구성된 튜플 반환

    ```sql
    SELECT 속성들
    FROM R
    ```

  - Join: `R ⋈ S`
    > 공통 속성 R과 S의 튜플들을 연결해 만들어진 튜플 반환
  - Division: `R ÷ S`
    > 릴레이션 S의 모든 튜플과 관련 있는 R의 튜플 반환

#### 관계 해석

> 튜플 관계 해석, 도메인 관계 해석을 하는 비절차적 언어  
> 프레디킷 해석(Predicate Calculus)에 기반한 언어

- 연산자

  - OR 연산: `∨`
    > 원자식 간 "또는" 이라는 관계로 연결
  - AND 연산: `∧`
    > 원자식 간 "그리고" 라는 관계로 연결
  - NOT 연산: `¬`
    > 원자식에 대한 부정

- 정량자
  - 전칭 정량자(Universal Quantifier): `∀`
    > "for all"
  - 존재 정량자(Existential Quantifier): `∃`
    > "there exists"

### 데이터베이스 정규화

- DB 정규화(DB Normalization)

  > 데이터의 중복성을 제거해 이상 현상을 방지 .  
  > 데이터의 일관성과 정확성을 유지하기 위해 무손실 분해 과정.

  - 이상현상(Anomaly)

    - 삽입 이상(Insertion Anomaly)
      > 의도와는 상관없이 원하지 않은 값들도 함께 삽입
    - 삭제 이상(Deletion Anomaly)
      > 의도와는 상관없는 값들도 함께 삭제되는 연쇄가 일어나는 현상
    - 갱신 이상(Update Anomaly)
      > 일부 튜플의 정보만 갱신돼 정보의 모순이 생기는 현상

  - 정규화 단계

    - 1정규형(1NF)

      > 원자 값으로 구성

      - 정규화 전

        | 고객 ID |                 이메일                 |
        | :-----: | :------------------------------------: |
        |    1    | customer@naver.com, customer@gmail.com |

      - 정규화 후

        | 고객 ID |       이메일       |
        | :-----: | :----------------: |
        |    1    | customer@naver.com |
        |    1    | customer@gmail.com |

    - 2정규형(2NF)

      > 부분 함수 종속 제거(완전 함수적 종속 관계)

      - 정규화 전

        | _고객명_ | _서비스 이름_ | 서비스 가격 | 서비스 이용 기간 |
        | :------: | :-----------: | :---------: | :--------------: |
        |  홍길동  |     헬스      |    70000    |       1달        |
        |  홍길동  |     수영      |   100000    |       2달        |
        |  장길산  |     수영      |   100000    |       2달        |

      - 정규화 후

        | _고객명_ | _서비스 이름_ | 서비스 이용 기간 |
        | :------: | :-----------: | :--------------: |
        |  홍길동  |     헬스      |       1달        |
        |  홍길동  |     수영      |       2달        |
        |  장길산  |     수영      |       2달        |

        | 서비스 이름 | 서비스 가격 |
        | :---------: | :---------: |
        |    헬스     |    70000    |
        |    수영     |   100000    |

    - 3정규형(3NF)

      > 이행 함수 종속 제거(A->B, B->C 일 때 A->C)

      - 정규화 전

        | _책번호_ | 도서 이름 | 도시 가격 | 출판사 | 홈페이지  |
        | :------: | :-------: | :-------: | :----: | :-------: |
        |    1     |  C 언어   |   30000   |  A사   | www.a.com |
        |    2     | C++ 언어  |   25000   |  B사   | www.b.com |
        |    3     | JAVA 언어 |   40000   |  B사   | www.b.com |

      - 정규화 후

        | _책번호_ | 도서 이름 | 도서 가격 | 출판사 |
        | :------: | :-------: | :-------: | :----: |
        |    1     |  C 언어   |   30000   |  A사   |
        |    2     | C++ 언어  |   25000   |  B사   |
        |    3     | JAVA 언어 |   40000   |  B사   |

        | _출판사_ | 홈페이지  |
        | :------: | :-------: |
        |   A사    | www.a.com |
        |   B사    | www.b.com |

    - 보이스-코드 정규형(BCNF)

      > 결정자가 후보 키가 아닌 함수 종속 제거

      - 정규화 전

        | _학번_ |   _과목명_   | 교수명 |
        | :----: | :----------: | :----: |
        | 202001 |    C 언어    | 장길산 |
        | 202001 | 데이터베이스 | 홍길동 |
        | 202002 | 데이터베이스 | 홍길동 |

      - 정규화 후

        | _학번_ | _교수명_ |
        | :----: | :------: |
        | 202001 |  장길산  |
        | 202001 |  홍길동  |
        | 202002 |  홍길동  |

        | _교수명_ |    과목명    |
        | :------: | :----------: |
        |  장길산  |    C 언어    |
        |  홍길동  | 데이터베이스 |

    - 4정규형(4NF)
      > 다중 값 종속성 제거
    - 5정규형(5NF)
      > 조인 종속성 제거

---

### 재사용

- 재사용(Reuse)

  > 개발 시간 및 비용 절감을 위해 검증된 기능을 파악하고 재구성해 시스템에 응용하기 위한 최적화 작업

  - 함수와 객체 재사용
    > 클래스(Class)나 함수(Function) 단위로 구현한 소스 코드를 재사용
  - 컴포넌트 재사용
    > 컴포넌트(Component) 단위로 재사용
  - 애플리케이션 재사용

    > 공통 기능을 제공하는 애플리케이션과 기능을 공유해 재사용

  - 사례

    - 라이브러리(Library)

      > S/W 개발 시 공통으로 사용될 특정한 기능을 모듈화한 기법

      - 표준 라이브러리
        > 특정 언어의 개발환경에 기본적으로 포함된 라이브러리
      - 런타임 라이브러리
        > 프로그램이 실제 환경에서 실행되기 위해 필요한 모듈

    - 프레임워크(Framework)
      > 어떠한 목적을 달성하기 위해 복잡하게 얽혀있는 문제를 해결하기 위한 툴  
      > `Class` + `Library`
    - 소프트웨어 아키텍처(Software Architecture)
      > 시스템을 구성하는 요소와 관계를 간략하고 명확하게 나타냄.

### 모둘화

- 모듈화

  > 프로그램을 개발 시 생산성과 최적화, 관리에 용이하게 기능 단위로 분할하는 기법

  - 모듈화의 적정성을 측정하는 지표는 **결합도**와 **응집도**다.

  - 결합도

    > 모듈 간의 관련성을 측정하는 척도

    - 결합도 순서: `내공 외제 스자`
      > **내용 > 공통 > 외부 > 제어 > 스탬프 > 자료**
      - 내용 결합도(Content Coupling): `결합도↑`
        > 하나의 모듈이 직접적으로 다른 모듈의 내용을 참조할 경우
      - 공통 결합도(Common Coupling)
        > 모듈 밖에 선언돼 있는 전역 변수를 참조하고 갱신하는 식의 경우
      - 외부 결합도(External Coupling)
        > 모듈이 다수의 관련 기능을 가질 때 모듈 안의 구성요소들이 그 기능을 순차적으로 수행할 경우
      - 제어 결합도(Control Coupling)
        > 어떤 모듈이 다른 모듈의 내부 논리 조직을 제어하기 위한 목적으로 제어 신호를 이용해 통신하는 경우
      - 스탬프 결합도(Stamp Coupling)
        > 모듈 간의 인터페이스로 배열, 객체, 구조 등이 전달되는 경우
      - 자료 결합도(Data Coupling): `결합도↓`
        > 모듈 간의 인터페이스로 전달되는 파라미터를 통해서만 모듈 간의 상호 작용이 일어나는 경우

  - 응집도

    > 모듈의 독립성을 나타내는 개념

    - 응집도 순서: `우논시절 통순기`
      > **우연적 < 논리적 < 시간적 < 절차적 < 통신적 < 순차적 < 기능적**
      - 우연적 응집도(Coincidental Cohesion): `응집도↓`
        > 서로 다른 상위 모듈에 의해 호출돼 처리상의 연관성이 없는 서로 다른 기능을 수행할 경우
      - 논리적 응집도(Logical Cohesion)
        > 유사한 성격을 갖는 요소들이 한 모듈에서 처리되는 경우
      - 시간적 응집도(Temporal Cohesion)
        > 특정 시간에 처리돼야 하는 활동들을 한 모듈에서 처리할 경우
      - 절차적 응집도(Procedural Cohesion)
        > 모듈이 다수의 관련 기능을 가질 때 모듈 안의 구성요소들이 그 기능을 순차적으로 수행할 경우
      - 통신적 응집도(Communication Cohesion)
        > 동일한 입력과 출력을 사용해 다른 기능을 수행하는 활동들이 모여 있을 경우
      - 순차적 응집도(Sequential Cohesion)
        > 모듈 내에서 한 활동으로부터 나온 출력값을 다른 활동이 사용할 경우
      - 기능적 응집도(Functional Cohesion): `응집도↑`
        > 모듈 내부의 기능이 단일한 목적을 위해 수행되는 경우

---

### 운영체제

- 운영체제(OS)

  > 컴퓨터의 H/W를 보다 쉽게 사용할 수 있도록 인터페이스를 제공해 주는 S/W

  - 특징

    - 사용자 편리성 제공
    - 인터페이스 기능 담당
    - 스케줄링 담당
    - 자원 관리
    - 제어 기능

  - 쉘(Shell)
    > 사용자 명령에 대한 처리하는 역할
  - 커널(Kernel)

    > H/W와 관련된 내부적인 역할

    - 프로세스 관리
    - 기억장치 관리
    - 주변장치 관리
    - 파일 관리

### 메모리 관리

- 메모리 관리

  > 중앙 처리 장치, 메모리, 스토리지, 주변 기기 등을 적절히 관리

  - 기본 사항

    - 가상 메모리(Virtual Memory)
      > 각 프로그램에 실제 메모리 주소가 아닌 가상의 메모리 주소를 부여
    - 메모리 관리 장치(MMU)
      > 가상 메모리 주소 -> 실제 메모리 주소 변환
    - 메모리 관리자
      > 프로세스에게 필요할 때마다 기억장치를 할당 후 회수 작업

  - 메모리 관리 기법

    - 반입 기법: `When`
      - 요구 반입
        > 다음에 실행될 프로세스가 참조 요구가 있을 경우 적재
      - 호출 반입
        > 시스템의 요구를 예측해 미리 메모리에 적재
    - 배치 기법: `Where`
      - First Fit
        > 가용 공간 중 첫 번째 분할에 할당
      - Best Fit
        > 가용 공간 중 가장 크기가 비슷한 공간에 할당
      - Worst Fit
        > 가용 공간 중 가장 큰 공간에 할당
    - 할당 기법: `How`
      - 연속 할당
        > 주기억장치에 연속적으로 할당
        - 단일 분할 할당
        - 다중 분할 할당
      - 분산 할당
        > 주기억장치 공간 내 분산해 할당
        - 페이징(Paging): 페이지 프레임(실제 공간)
          > 가상기억장치 내의 프로세스를 일정하게 분할해 주기억장치의 분산된 공간에 적재시킨 후 프로세스 수행
          - 내부 단편화 현상 발생: -> Slab Allocator
            > 적재된 후 남은 공간
          - 스레싱(Thrashing): -> Working Set, PFF(Page-Fault Frequency)
            > 페이지 폴트가 계속 증가해 기억장치 접근 시간이 증가
        - 세그먼테이션(Segmentation)
          > 가상기억장치 내의 프로세스를 서로 크기가 다른 논리적 단위인 Segment로 분할하고 메모리 할당
          - 외부 단편화 현상 발생: -> 버디 메모리 할당
            > 빈 공간이나 적재가 불가능
        - 페이징(Paging) + 세그먼테이션(Segmentation)
    - 교체 기법: `Who`
      - FIFO
        > 가장 먼저 들어와 가장 오래 있던 페이지를 교체
      - LRU
        > 가장 오랫동안 사용되지 않은 페이지를 교체
      - LFU
        > 사용된 횟수가 가장 적은 페이지를 교체

### 프로세스 vs 스레드

- 프로세스(Process)

  > 실행 중인 프로그램

  - 상태
    - Create
      > 사용자에 의해 프로세스가 생성된 상태
    - Ready
      > CPU를 할당받을 수 있는 상태
    - Running
      > CPU를 할당받아 동작 중인 상태
    - Waiting
      > 실행 중 CPU를 양도하고 대기 리스트에서 기다리는 상태
    - Complete
      > 프로세스가 완전히 수행을 종료한 상태

- 스레드(Thread)

  > Process의 실행 단위

- |           프로세스(Process)           |          스레드(Thread)          |
  | :-----------------------------------: | :------------------------------: |
  |     PCB, Code, Data, Heap, Stack      |   Thread ID, Registers, Stack    |
  | IPC, Pipe, Message, 공유메모리로 통신 |            전역 변수             |
  |       Context Switching 비용 큼       |   Context Switching 비용 적음    |
  |                fork()                 | pthread_create(), pthread_join() |

### 프로세스 스케줄링

- 프로세스 스케줄링

  > CPU를 사용할 프로세스들 사이의 우선순위를 관리하는 작업

  - 용어

    - 대기 시간
      > 서비스 시작 시간 - 도착 시간  
      > 응답 시간 - 서비스 시간
    - 응답 시간
      > 대기 시간 + 수행 시간  
      > 종료 시간 - 도착 시간
    - 응답률
      > (대기시간 + 서비스 시간) / (서비스 시간)

  - 선점형 스케줄링(Preemptive Scheduling)
    > 우선순위가 높은 다른 프로세스가 현재 프로세스를 중단시키고 CPU 점유
    - 라운드 로빈(Round Robin)
      > 일정 주기로 균등한 CPU 점유 시간을 할당 후 실행
    - SRT(Shortest Remaining Time First)
      > 남은 처리 시간이 더 짧다고 판단되는 프로세스 우선 수행
    - 다단계 큐(Multi-Level Queue)
      > 독립된 스케중링 큐 이용, 상위 작업 큐가 선접
    - 다단계 피드백 큐(Multi-Level Feedback Queue)
      > 독립된 큐마다 다른 시간 할당
  - 비선점형 스케줄링(Non-Preemptive Scheduling)
    > 한 프로세스가 CPU를 할당받으면 작업 종료까지 다른 프로세스는 CPU 점유 불가
    - 우선순위(Priority)
      > 우선순위를 주고 우선순위에 따라 CPU를 할당
    - 기한부(Deadline)
      > 작업들이 명시된 시간이나 기한 내에 완료되도록 계획
    - FCFS(First Come First Service)
      > 대기 큐에 도착한 순서대로 CPU 할당
    - SJF(Shortest Job First)
      > 대기 큐에 도착하는 시점에 따라 가장 작은 서비스 시간을 갖는 프로세스에게 CPU 할당
      - Starvation 현상: -> Aging 기법
    - HRN(Highest Response Ratio Next)
      > (대기 시간 + 서비스 시간)/(서비스 시간) 으로 우선순위 계산해 CPU 할당
      - Starvation 현상 최소화

### 교착상태

- 교착상태(Deadlock)

  > 두 개 이상의 프로세스가 특정 자원할당을 무한정 대기하는 상태

  - 발생 조건

    - 상호배제(Mutual Exclusive)
      > 한 프로세스가 자원을 점유해서 다른 프로세스가 자원 사용 불가
    - 점유와 대기(Block & Wait)
      > 한 프로세스가 자원을 점유하고 있지만 다른 자원을 요청해 대기함.
    - 비선점(Non-Preemption)
      > 한 프로세스가 점유한 자원에 대해 선점 불가(Only 그 프로세스만 해제 가능)
    - 환형 대기(Circular Wait)
      > 두 개 이상의 프로세스 간 자원의 점유와 대기가 원형을 구성한 상태

  - 해결 방법
    - 예방(Prevention)
      > 점유 자원 해제 후 새 자원 요청
    - 회피(Avoidance)
      > 안전한 상태를 유지할 수 있는 요구만 수락, Banker's Algorithm
    - 발견(Detection) & 복구(Recovery)
      > 교착 상태를 발견하고 Deadlock을 없어지도록 함.

---

### 네트워크

- OSI(Open System Interconnection) 7 계층

  - | 계층 |     구분     |                 설명                  |
    | :--: | :----------: | :-----------------------------------: |
    |  7   | Application  |  HTTP, FTP, SMTP, POP3, IMAP, Telnet  |
    |  6   | Presentation |        메시지 단위, JPEG, MPEG        |
    |  5   |   Session    |          RPC, 복호화, 부호화          |
    |  4   |  Transport   |   세그먼트(Segment) 단위, TCP, UDP    |
    |  3   |   Network    |     패킷(Packet) 단위, IP, Router     |
    |  2   |  Data Link   |   프레임(Frame) 단위 Switch, Bridge   |
    |  1   |   Physical   | 전기적 신호(Bits) 단위, Hub, Repeater |

- IP

  > 인터넷 프로토콜(Internet Protocol)

  - |             IPv4              |              IPv6              |
    | :---------------------------: | :----------------------------: |
    |            32 bit             |            128 bit             |
    | 8 bit씩 4부분으로 나뉜 10진수 | 16 bit씩 8부분으로 나뉜 16진수 |
    |        (192.168.10.1)         |      (2001:9e76:...:e11c)      |
    |           약 43억개           |         4.3 x 10^38개          |
    |        헤더 크기 가변         |         헤더 크기 고정         |
    |    Plug & Player 지원 [X]     |     Plug & Player 지원 [O]     |
    |   멀티, 유니, 브로드 캐스트   |    멀티, 유니, 애니 캐스트     |

  - IPv4 -> IPv6
    - 듀얼 스택(Dual Stack)
      > IPv4, IPv6 중 IP 스택을 선택
    - 터널링(Tunneling)
      > IPv4 망에 터널을 만들고 IPv4에서 사용하는 Protocol로 캡슐화해 전송
    - 주소 변환(Address Translation)
      > 주소 변환기(IPv4 - IPv6 Gateway)를 사용해 패킷 변환

- 라우팅 프로토콜

  > 라우터 간의 상호 통신규약

  - 내부(IGP)
    - RIP
      > Distance - Vector 방식, 벨만-포드 알고리즘 사용
    - OSPF
      > Linked - State 방식, 다익스트라 알고리즘 사용
  - 외부(EGP)
    - BGP
      > Path-Vector 방식

- TCP, UDP
  - |                       TCP                        |             UDP              |
    | :----------------------------------------------: | :--------------------------: |
    |                      신뢰성                      |           비신뢰성           |
    |                   연결 지향적                    |           비연결성           |
    |                순서화된 세그먼트                 |  순서화되지 않은 데이터그램  |
    |               흐름 제어, 혼잡 제어               | 제어가 없어 전송 속도가 빠름 |
    | 3-way handshaking(연결), 4-way handshaking(종료) |                              |

---
