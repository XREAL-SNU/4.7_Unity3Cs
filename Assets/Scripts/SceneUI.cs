using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    enum Buttons {
        EnterEmotionPanel,
    }

    public void Start() {
        Init();
    }

    public override void Init() {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.EnterEmotionPanel).gameObject.BindEvent(OnClick_EnterEmotionPanel);

        
    }

    public void OnClick_EnterEmotionPanel(PointerEventData data) {
        UIManager.UI.ShowPopupUI<UIPopup>("EmotionsPanel");
    }
}
