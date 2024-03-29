## Table of Contents

- [Installation](#installation)
- [HelloWorld](#hello-world)
- [Animation](#animation)

[Sample Project](https://github.com/qkrsogusl3/VFrameGettingStarted)

Installation
===

Dependencies
---

* [UniTask](https://github.com/Cysharp/UniTask)
* [VContainer](https://github.com/hadashiA/VContainer)
* [DoTween](https://openupm.com/packages/com.demigiant.dotween/)

Manifest
---

> manifest.json

```json
{
    "dependencies": {
        "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
        "jp.hadashikick.vcontainer": "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.9.0",
        "com.demigiant.dotween": "1.2.632"
    },
    "scopedRegistries": [
        {
            "name": "package.openupm.com",
            "url": "https://package.openupm.com",
            "scopes": [
                "com.openupm",
                "com.demigiant.dotween"
            ]
        }
    ]
}
```

Add Package from git url
---

```
https://github.com/qkrsogusl3/VFrame.git?path=Assets/VFrame
```

Hello World
===

- [Create RootScope](https://vcontainer.hadashikick.jp/scoping/project-root-lifetimescope)


- Create VFrameSettings

    `Assets -> Create -> VFrame -> VFrame Settings`

- Create RootCanvas

    - Create Canvas Prefab
    - Attach 'RootCanvas' Component
    - Set `VFrameSettings -> RootCanvas`

- Configure RootScope

    ```csharp
    public class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseUISystem();
        }
    }
    ```

- Configure SceneScope

    - [Create LifetimeScope](https://vcontainer.hadashikick.jp/getting-started/hello-world)

    - Change LifetimeScope to SceneScope

    ```csharp
    public class GameScope : SceneScope<GameScene>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
    }

    public class GameScene : SceneEntry
    {
        public GameScene(IObjectResolver resolver) : base(resolver)
        {
        }

        public override void Initialize()
        {
        }

        protected override void OnDisposed()
        {
        }
    }
    ```

- Create FirstView

    - `Assets -> Create -> VFrame -> UI -> ComponentView`

    ```csharp
    public class FirstView : ComponentView<FirstView>
    {
        public override void OnEnter()
        {
            Debug.Log("Hello World");
        }

        public override void OnExit()
        {
        }
    }
    ```

    - Create UGUI UI and Attach FirstView Component.

- Go to FirstView

    > GameScene.cs

    ```csharp
    public class GameScene : SceneEntry, ITickable
    {
        //...

        public override void Initialize()
        {
            UISystem.Entry<FirstView>();
        }

        //...
    }
    ```

- Go to SecondView

    - Create SecondView.cs

    > SecondView.cs
    ```csharp
    public override void OnEnter()
    {
        Debug.Log(nameof(SecondView));
    }
    ```

    > GameScene.cs

    ```csharp
    public class GameScene : SceneEntry, ITickable
    {

        //...

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UISystem.To<SecondView>();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UISystem.Back();
            }
        }

        //...

    }
    ```

Animation
---

- FadeAnimation

    > RootScope.cs

    ```csharp
    builder.RegisterViewDefaultAnimation<FadeAnimation<IView>>();
    ```
    
    > SecondView.cs

    ```csharp
    //...
    public override UniTask Ready()
    {
        PositionZero();
        return UniTask.CompletedTask;
    }
    //...
    ```
