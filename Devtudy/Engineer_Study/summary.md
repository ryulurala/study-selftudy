---
title: "요약"
category: 정보처리기사
tags: [정보처리기사2021]
---

## 애자일(Agile)

- 애자일(Agile)

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

## 공통 모듈 설계

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

- HIPO(Hierarchy Input Process Output)

  > 시스템의 분석 및 설계, 문서화할 때 사용되며, **하향식 S/W 개발**을 위한 문서

  - 차트 종류
    - 가시적 도표(Visual Table of Content)
    - 총체적 도표(Overview Diagram)
    - 세부적 도표(Detail Diagram)

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
