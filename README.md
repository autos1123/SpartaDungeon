# Unity3D 과제 프로젝트 - Sparta Dungeon

## 🎮 프로젝트 소개
Unity를 사용해 구현한 3D 게임 과제입니다. 기본적인 캐릭터 조작부터 아이템 시스템, UI, 벽타기, 점프대, 게임오버/클리어 등 다양한 기능을 포함한 실습 프로젝트입니다.

## 📌 주요 기능

### ✅ 필수 구현
- **캐릭터 이동 및 점프**
  - Input System을 활용한 WASD 및 점프 처리
  - Rigidbody의 ForceMode를 활용한 점프 및 점프대 구현

- **UI 구성**
  - 체력바, 스태미나바, 아이템 효과 UI
  - 게임 시작/종료/클리어 화면 UI

- **Raycast 조사**
  - 플레이어 전방을 다중 Ray로 스캔
  - 아이템/오브젝트 설명 출력

- **ScriptableObject 기반 아이템 시스템**
  - 아이템 종류, 효과, 지속 시간 등 SO로 관리
  - IInteractable 인터페이스 기반 E키 상호작용으로 획득

- **점프대 기능**
  - 충돌 시 캐릭터를 위로 튕겨 올리는 기능 구현

- **Coroutine 기반 아이템 효과**
  - 일정 시간 동안 속도 증가 등 효과 지속 후 자동 해제

- **체력 0일 시 게임 오버 처리**
  - GameManager에서 일괄 제어하며 UI 표시

### 🌟 도전 과제 (선택 구현)
- **벽 타기 및 매달리기**
  - 전방 Ray를 5방향으로 발사해 벽 감지 후 올라가기 가능

- **움직이는 플랫폼**
  - 양 방향 이동, 캐릭터 탑승 시 자연스러운 따라가기 구현

- **Input 방식 비교 실험**
  - `SendMessage()` vs `UnityEvent.Invoke()` 방식 비교 테스트용 스크립트 구현

- **예외 처리 학습 적용**
  - try-catch 구문을 Item, UI 처리 로직에 적용
  - 예외 발생 시 Debug.LogError로 오류 및 StackTrace 출력

## 🛠️ 개발 환경
- Unity 2021.3.x LTS
- C# (.NET 4.x Equivalent)
- 대상 플랫폼: Windows (1920x1080 기준)

## 🧪 실행 방법
1. Unity에서 프로젝트 열기
2. MainScene 실행
3. WASD로 캐릭터 이동, Space로 점프
4. E키로 아이템 상호작용
5. 체력이 0이 되거나 클리어존 도달 시 결과 화면 출력

## 📂 폴더 구조 (간략)

<pre lang="markdown"> ``` Assets/ 
  ├── Externals/ 
  ├── Fonts/ 
  ├── Prefabs/ 
  ├── Scenes/
  ├── ScriptableObjects/ 
  │ └── Items/ 
  ├── Scripts/ │ 
  ├── Environment/ │ 
  ├── Interactions/ │ 
  ├── Interface/ │ 
  ├── Items/ │
  ├── Managers/ │ 
  ├── Player/ 
  │ └── UI/ 
  ├── TextMesh Pro/ 
  ├── Texture/ 
  └── UI/ 
  Packages/ ``` </pre>


## ⚠️ 유의 사항
- `try-catch` 예외 처리를 통해 아이템 null 참조, UI 미연결 등의 오류에 대응
- 실전 구조에서는 InputSystem을 직접 코드로 구성하며 안정성 고려
- SendMessage는 테스트용으로만 사용하였으며, 실전 적용은 UnityEvent 기반 구조 사용

## 📸 스크린샷 (선택 사항)
>![image](https://github.com/user-attachments/assets/1bf8a6f5-7cab-4faf-af0f-e5ecaeb8d9f7)
>
메인화면

> ![image](https://github.com/user-attachments/assets/f846bdc5-f5b6-4911-9ddf-418d62db0f75)
> 
게임화면

> ![image](https://github.com/user-attachments/assets/5812ca06-dab5-489e-9188-da912f73972c)
> 
클리어화면

>![image](https://github.com/user-attachments/assets/adedbc00-07e5-4caa-b9c7-45499d80fd80)
>
게임오버화면


## 👤 개발자
- 과제 제출자: 공재원
- 제출일: 2025년 5월 23일
