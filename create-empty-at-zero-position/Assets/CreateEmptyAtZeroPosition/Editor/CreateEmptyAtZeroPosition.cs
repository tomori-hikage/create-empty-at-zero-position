using UnityEngine;
using UnityEditor;
using EditorExtensions;


namespace HC.Editor
{
    /// <summary>
    /// 空のゲームオブジェクトのローカル座標をVector3.zeroにするエディタ拡張
    /// </summary>
    [InitializeOnLoad]
    public class CreateEmptyAtZeroPosition
    {
        #region 定数

        private const string MenuPath = "Tools/Create Empty At Zero Position";

        #endregion


        #region フィールド / プロパティ

        private static bool _isOn;

        #endregion


        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static CreateEmptyAtZeroPosition()
        {
            EditorApplication.delayCall += Initialize;
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        private static void Initialize()
        {
            OnOff();

            // ReSharper disable once DelegateSubtraction
            EditorApplication.delayCall -= Initialize;
        }

        /// <summary>
        /// ON / OFFの切り替え
        /// </summary>
        [MenuItem(MenuPath)]
        private static bool OnOff()
        {
            _isOn = !_isOn;
            Menu.SetChecked(MenuPath, _isOn);

            // 機能のON/OFFを切り替える
            if (Menu.GetChecked(MenuPath))
            {
                On();
            }
            else
            {
                Off();
            }

            return true;
        }

        /// <summary>
        /// 機能のON
        /// </summary>
        private static void On()
        {
            ObjectPostprocessor.gameObjectAdded += GameObjectAdded;
            ObjectPostprocessor.gameObjectDuplicated += GameObjectDuplicated;
        }

        /// <summary>
        /// 機能のOFF
        /// </summary>
        private static void Off()
        {
            ObjectPostprocessor.gameObjectAdded -= GameObjectAdded;
            ObjectPostprocessor.gameObjectDuplicated -= GameObjectDuplicated;
        }

        private static void GameObjectAdded(GameObject gameObject)
        {
            SetLocalPositionToZero(gameObject);
        }

        private static void GameObjectDuplicated(GameObject gameObjectFrom, GameObject gameObjectTo)
        {
            SetLocalPositionToZero(gameObjectTo);
        }

        /// <summary>
        /// ローカル座標をVector3.zeroにする
        /// </summary>
        /// <param name="gameObject">gameObject.</param>
        private static void SetLocalPositionToZero(GameObject gameObject)
        {
            if (gameObject.name.StartsWith("GameObject"))
            {
                gameObject.transform.localPosition = Vector3.zero;
            }
        }

        #endregion
    }
}