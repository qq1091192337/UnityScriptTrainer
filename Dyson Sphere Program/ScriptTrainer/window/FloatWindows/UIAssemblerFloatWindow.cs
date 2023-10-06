using UnityEngine;
using UnityGameUI;

namespace ScriptTrainer
{
    public class UIAssemblerFloatWindow:ManualBehaviour
    {
        // 声明变量
        public static GameObject obj = null;
        public static UIAssemblerWindow instance;
        public static bool initialized = false;
        public static bool _optionToggle = false;
        // UI
        public static AssetBundle testAssetBundle = null;
        public static GameObject canvas = null;
        public static readonly int width = Mathf.Min(Screen.width, 590);
        public static readonly int height = (Screen.height < 400) ? Screen.height : (430);
        // 窗口开关
        public static bool optionToggle
        {
            get => _optionToggle;
            set
            {
                _optionToggle = value;
                if (initialized) canvas.SetActive(optionToggle);
                else Init();
            }
        }
        public static AssemblerComponent assemblerInstance
        {
            get => instance.factorySystem.assemblerPool [instance.assemblerId];
        }
        public static Proto ItemProto
        {
            get => LDB.items.Select(instance.factory.entityPool[assemblerInstance.entityId].protoId);
        }
        public UIAssemblerFloatWindow()
        {
            Init();
        }
        private static void Init()
        {
            if (initialized)
            {
                return;
            }
            CreateUI();
            canvas.SetActive(optionToggle);
            initialized = true;
        }

        private static void CreateUI()
        {
            canvas = UIControls.createUICanvas();
            DontDestroyOnLoad(canvas);
            canvas.name = "UIFloatWindow_Assembler";
            // 创建背景              
            UIInventoryWindow background = Components.createUIPanel(canvas,ItemProto.name.Translate(), width, height);
            background.gameObject.name = "background";

            // 关闭按钮 panel-bg/btn-box/close-btn
            Transform close_btn = background.transform.Find("panel-bg/btn-box/close-btn");

            close_btn.GetComponent<UIButton>().onClick += (int i) =>
            {
                optionToggle = false;
                canvas.SetActive(optionToggle);
            };
            
        }
    }
}