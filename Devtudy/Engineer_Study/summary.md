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

- S/W 설계 유형

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
       > S/W 컴포넌트에 대한 인터페이스 명세를 위한 선행 조건, 결과조건, 불변조건을 나타내는 설계 방법
