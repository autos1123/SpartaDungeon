# Unity3D 과제 프로젝트

## 🎮 프로젝트 소개
Unity를 사용해 구현한 3D 게임 과제입니다. 기본적인 캐릭터 조작부터 상호작용 가능한 NPC, 아이템 효과, UI 시스템까지 포함되어 있습니다.

## 📌 주요 기능

### ✅ 필수 구현
- **캐릭터 이동 및 점프**  
  - 방향키 또는 WASD로 이동  
  - Space 키로 점프 가능  

- **UI 구성**  
  - 아이템 획득 및 효과 시간 표시  
  - NPC 상호작용 시 텍스트 패널 출력  

- **Raycast 조사**  
  - 플레이어 앞의 오브젝트 조사 기능  
  - F 키로 NPC와 상호작용 가능  

- **ScriptableObject 기반 아이템 시스템**  
  - 아이템 효과, 이름, 지속 시간 등을 ScriptableObject로 관리  

- **점프대 기능**  
  - 캐릭터가 닿으면 위로 튕겨 오르는 점프대 구현  

- **Coroutine 기반 아이템 효과**  
  - 일정 시간 동안 효과 지속 후 자동 해제 (예: 속도 증가)

### 🌟 도전 과제 (선택 구현)
- **NPC와 미니게임 또는 퀴즈 상호작용**  
  - 퀴즈를 내거나 미니게임을 시작하는 NPC 추가  
  - 추후 다양한 NPC 기능 확장을 고려함

## 🛠️ 개발 환경
- Unity 2021.3.x LTS
- C# (.NET 4.x Equivalent)
- 대상 플랫폼: Windows (1920x1080 해상도 기준 UI 구성)

## 🧪 실행 방법
1. Unity로 프로젝트 열기
2. `MainScene` 실행
3. 방향키 또는 WASD로 캐릭터 조작
4. F 키로 NPC와 상호작용
5. 아이템을 획득하면 효과가 UI에 표시됨

## 📂 폴더 구조 (간략)

<pre lang="markdown"> ``` Assets/ ├── Externals/ ├── Fonts/ ├── Prefabs/ ├── Scenes/ ├── ScriptableObjects/ │ └── Items/ ├── Scripts/ │ ├── Environment/ │ ├── Interactions/ │ ├── Interface/ │ ├── Items/ │ ├── Managers/ │ ├── Player/ │ └── UI/ ├── TextMesh Pro/ ├── Texture/ └── UI/ Packages/ ``` </pre>


## ⚠️ 유의 사항
- 예외 처리에는 `try-catch`와 Unity API를 함께 활용하여 오류 발생 시 로그 출력 및 안전한 흐름 유지가 가능하도록 했습니다.
- 모든 기능은 Unity Editor 내에서 테스트 완료되었습니다.

## 📸 스크린샷 (선택 사항)
>![image](https://github.com/user-attachments/assets/1bf8a6f5-7cab-4faf-af0f-e5ecaeb8d9f7)
메인화면
> ![image](https://github.com/user-attachments/assets/f846bdc5-f5b6-4911-9ddf-418d62db0f75)
게임화면
> ![image](https://github.com/user-attachments/assets/5812ca06-dab5-489e-9188-da912f73972c)
클리어화면
>![image](https://github.com/user-attachments/assets/adedbc00-07e5-4caa-b9c7-45499d80fd80)
게임오버화면

## 👤 개발자
- 과제 제출자: 공재원
- 제출일: 2025년 5월 23일
