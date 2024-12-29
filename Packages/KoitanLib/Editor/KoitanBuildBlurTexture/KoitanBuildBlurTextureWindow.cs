using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace KoitanLib
{
    public class KoitanBuildBlurTextureWindow : EditorWindow
    {
        private static SDFTextureGeneratorSettingData data;
        public static SDFTextureGeneratorSettingData Data
        {
            get
            {
                if (data == null) data = Load();
                return data;
            }
            private set
            {
                data = value;
            }
        }

        static string tmpPath;

        [MenuItem("KoitanLib/SDF Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<KoitanBuildBlurTextureWindow>();
        }

        void OnGUI()
        {
            //　ラベルを太字で追加
            //GUILayout.Label("ラベルです", EditorStyles.boldLabel);


            //Data.sourceTex = (Texture2D)EditorGUILayout.ObjectField(Data.sourceTex, typeof(Texture2D));

            //　0〜100までの値で変化するスライダー
            Data.sig = EditorGUILayout.FloatField("sig", Data.sig);

            //Data.filterMode = (FilterMode)EditorGUILayout.EnumPopup("FilterMode", Data.filterMode);
            //Data.format = (TextureFormat)EditorGUILayout.EnumPopup("TextureFormat", Data.format);

            //　ボタンを追加
            /*
            if (GUILayout.Button("生成"))
            {
                ShowSelectionObjects();
            }
            */
        }

        /*
        [MenuItem("KoitanLib/BuildBlurTexture")]
        static void ShowSelectionObjects()
        {
            Object[] selectedAsset = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
            foreach (var go in selectedAsset)
            {
                var tex = (Texture2D)go;
                CreateBlurTexture(tex);
            }
        }
        */

        static Texture2D CreateBlurTexture(Texture2D tex)
        {

            // 保存処理
            string assetPath = AssetDatabase.GetAssetPath(tex);
            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            // 書き込み不可なら一時的に許可する
            bool isReadable = importer.isReadable;
            if (isReadable == false)
            {
                importer.isReadable = true;
                importer.SaveAndReimport();
            }

            Texture2D newTex = KoitanBuildBlurTexture.BuildTexture(tex, Data.sig);

            string d = Path.GetDirectoryName(assetPath);
            string f = Path.GetFileNameWithoutExtension(assetPath);
            string e = Path.GetExtension(assetPath);
            string newPath = Path.Combine(d, $"{f}_blur{e}");
            tmpPath = newPath;
            if (!string.IsNullOrEmpty(newPath))
            {
                var png = newTex.EncodeToPNG();
                File.WriteAllBytes(newPath, png);

                AssetDatabase.ImportAsset(newPath);
                var newImporter = AssetImporter.GetAtPath(newPath) as TextureImporter;
                newImporter.spritePixelsPerUnit = importer.spritePixelsPerUnit;
                newImporter.filterMode = importer.filterMode;
                newImporter.textureCompression = importer.textureCompression;
                // フォーマットを変えるのがめんどい
                var defaultPlatform = newImporter.GetDefaultPlatformTextureSettings();
                defaultPlatform.format = TextureImporterFormat.Alpha8;
                newImporter.SetPlatformTextureSettings(defaultPlatform);
                newImporter.SaveAndReimport();
                //var asset = AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture2D));
            }

            // read/writeをもどす
            if (isReadable == false)
            {
                importer.isReadable = false;
                importer.SaveAndReimport();
            }

            return AssetDatabase.LoadAssetAtPath<Texture2D>(newPath);
        }

        [MenuItem("GameObject/Create Outline", false, 0)]
        public static void CreateOutlineOnHierarchy()
        {
            var go = Selection.activeGameObject;
            if (go == null) return;
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;
            Texture2D blurTexture = CreateBlurTexture(spriteRenderer.sprite.texture);
            Sprite blurSprite = AssetDatabase.LoadAssetAtPath<Sprite>(tmpPath);
            GameObject outlineObject = new GameObject("Outline");
            outlineObject.transform.SetParent(go.transform);
            outlineObject.transform.localPosition = Vector3.zero;
            outlineObject.transform.transform.localRotation = Quaternion.identity;
            outlineObject.transform.localScale = Vector3.one;
            SpriteRenderer outlineSpriteRenderer = outlineObject.AddComponent<SpriteRenderer>();
            outlineSpriteRenderer.sprite = blurSprite;
            outlineSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
            outlineSpriteRenderer.material = AssetDatabase.LoadAssetAtPath<Material>("Packages/com.koitan.koitanlib/KoitanLib/Shaders/KoitanOutlineDefaultMat.mat");
            EditorUtility.SetDirty(outlineObject);
            /*
            Debug.Log($"{AssetDatabase.GetAssetPath(outlineSpriteRenderer.sprite.texture)}");
            var png = outlineSpriteRenderer.sprite.texture.EncodeToPNG();
            File.WriteAllBytes(AssetDatabase.GetAssetPath(outlineSpriteRenderer.sprite.texture), png);
            */
        }

        static SDFTextureGeneratorSettingData Load()
        {
            string json = EditorPrefs.GetString("SDFTextureGeneratorSettingData");
            if (json == string.Empty)
            {
                return new SDFTextureGeneratorSettingData();
            }
            else
            {
                return JsonUtility.FromJson<SDFTextureGeneratorSettingData>(json);
            }
        }

        static void Save()
        {
            if (Data == null)
            {
                Data = new SDFTextureGeneratorSettingData();
            }
            string json = JsonUtility.ToJson(Data);
            EditorPrefs.SetString("SDFTextureGeneratorSettingData", json);
            Debug.Log("Save");
        }

        private void OnDestroy()
        {
            Save();
        }

        [System.Serializable]
        public class SDFTextureGeneratorSettingData
        {
            public float sig;
            //public TextureFormat format;
            //public FilterMode filterMode;
            public Texture2D sourceTex;
        }
    }
}
